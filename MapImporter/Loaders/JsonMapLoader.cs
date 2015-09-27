using MapImporter.Enums;
using MapImporter.Objects;
using MapImporter.Utils;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace MapImporter.Loaders
{
    /// <summary>
    /// Responsible for loading Tiled maps saved in Json format.
    /// </summary>
    public class JsonMapLoader : MapLoader
    {
        private int gid = 1;  // Keeps track of the global ids for all tiles

        public JsonMapLoader(string filename)
            : base(filename)
        {
        }

        public override Map Load()
        {
            string fileText = File.ReadAllText(@filename);
            JObject mapJson = JObject.Parse(fileText);

            if (mapJson != null)
            {
                ParseMapValues(mapJson);

                JArray tilesetJsonArray = (JArray)mapJson["tilesets"];
                if (tilesetJsonArray != null)
                {
                    ParseTilesets(tilesetJsonArray);
                }

                foreach (JObject layerJson in mapJson["layers"])
                {
                    map.Layers.AddLayer(ParseLayer(layerJson));
                }
            }

            return map;
        }

        private void ParseMapValues(JObject mapJson)
        {
            map.Version = mapJson["version"].ToString();

            if (mapJson["orientation"].ToString() == "orthogonal")
                map.Orientation = Orientation.Orthogonal;
            else throw new UnsupportedOrientationException();

            map.Width = (int)mapJson["width"];
            map.Height = (int)mapJson["height"];
            map.TileWidth = (int)mapJson["tilewidth"];
            map.TileHeight = (int)mapJson["tileheight"];

            if (mapJson["backgroundcolor"] != null)
                map.BackgroundColor = ValueConverter.ConvertToColor(mapJson["backgroundcolor"].ToString());

            if (mapJson["renderorder"].ToString() == "right-down")
                map.RenderOrder = RenderOrder.RightDown;
            else if (mapJson["renderorder"].ToString() == "right-up")
                map.RenderOrder = RenderOrder.RightUp;
            else if (mapJson["renderorder"].ToString() == "left-down")
                map.RenderOrder = RenderOrder.LeftDown;
            else if (mapJson["renderorder"].ToString() == "left-up")
                map.RenderOrder = RenderOrder.LeftUp;

            if (mapJson["properties"] != null)
            {
                map.Properties = new MapProperties(JsonConvert.DeserializeObject<Dictionary<string, object>>(mapJson["properties"].ToString()));
            }

            if (mapJson["nextobjectid"] != null)
            {
                int num;
                int.TryParse(mapJson["nextobjectid"].ToString(), out num);
                map.NextObjectId = num;
            }
        }

        private void ParseTilesets(JArray tilesetJsonArray)
        {
            foreach (JObject tilesetJson in tilesetJsonArray)
            {
                map.Tilesets.AddTileset(ParseTileset(tilesetJson));
            }
        }

        private Tileset ParseTileset(JObject tilesetJson)
        {
            Tileset tileset = new Tileset();
            tileset.FirstGid = (int)tilesetJson["firstgid"];
            tileset.Name = tilesetJson["name"].ToString();
            tileset.TileWidth = (int)tilesetJson["tilewidth"];
            tileset.TileHeight = (int)tilesetJson["tileheight"];
            tileset.Spacing = (int)tilesetJson["spacing"];
            tileset.Margin = (int)tilesetJson["margin"];

            ParseTileOffset(tilesetJson, tileset);
            ParseTilesetImage(tilesetJson, tileset);

            if (tilesetJson["properties"] != null)
            {
                tileset.Properties = new MapProperties(JsonConvert.DeserializeObject<Dictionary<string, object>>(tilesetJson["properties"].ToString()));
            }

            ParseTilesetTerrain(tilesetJson, tileset);
            ParseTilesetTiles(tileset);
            ParseTilesetTileProperties(tilesetJson, tileset);

            return tileset;
        }

        private void ParseTileOffset(JObject tilesetJson, Tileset tileset)
        {
            if (tilesetJson["tileoffset"] != null)
            {
                JObject offset = (JObject)tilesetJson["tileoffset"];
                tileset.TileOffset = new TileOffset();

                if (offset["x"] != null)
                    tileset.TileOffset.X = (int)offset["x"];

                if (offset["y"] != null)
                    tileset.TileOffset.Y = (int)offset["x"];
            }
        }

        private void ParseTilesetImage(JObject tilesetJson, Tileset tileset)
        {
            if (tilesetJson["image"] != null)
            {
                tileset.Image.Source = tilesetJson["image"].ToString();
                tileset.Image.Format = Path.GetExtension(tileset.Image.Source);

                if (tilesetJson["trans"] != null)
                    tileset.Image.Trans = ValueConverter.ConvertToColor(tilesetJson["trans"].ToString());

                if (tilesetJson["imagewidth"] != null)
                    tileset.Image.Width = (int)tilesetJson["imagewidth"];

                if (tilesetJson["imageheight"] != null)
                    tileset.Image.Height = (int)tilesetJson["imageheight"];

                tileset.Source = tileset.Image.Source;
            }
        }

        private void ParseTilesetTerrain(JObject tilesetJson, Tileset tileset)
        {
            if (tilesetJson["terraintypes"] != null)
            {
                // TODO: add terraintypes
            }
        }

        private void ParseTilesetTiles(Tileset tileset)
        {
            int id = 0;  // Keeps track of the local id for the tiles

            for (int y = (tileset.Margin / tileset.TileHeight); y < ((tileset.Image.Height - tileset.Margin) / tileset.TileHeight); y++)
            {
                for (int x = (tileset.Margin / tileset.TileWidth); x < ((tileset.Image.Width - tileset.Margin) / tileset.TileWidth); x++)
                {
                    Tile tile = new Tile();
                    tile.Id = id;
                    tile.Gid = gid;
                    // TODO: tile.Probability
                    tile.Location = new Rectangle((x * tileset.TileWidth) + tileset.Spacing, (y * tileset.TileHeight) + tileset.Spacing, tileset.TileWidth, tileset.TileHeight);
                    tileset.Tiles.Add(tile);
                    id++;
                    gid++;
                }
            }
        }

        private void ParseTilesetTileProperties(JObject tilesetJson, Tileset tileset)
        {
            if (tilesetJson["tileproperties"] != null)
            {
                int index = 0;
                string str = tilesetJson["tileproperties"].ToString();

                if (str.Contains(","))
                {
                    string[] elements = str.Split(',');
                    foreach (string s in elements)
                    {
                        string[] i = s.Split('\"');

                        //Property for a tile that does not currently have any
                        if (i.Length > 5)
                        {
                            try
                            {
                                int.TryParse(i[1], out index);
                            }
                            catch (OverflowException ex)
                            {
                                Console.Write(ex.Message);
                            }

                            tileset.Tiles[index].Properties = new MapProperties();
                            tileset.Tiles[index].Properties.AddProperty(i[3].ToString(), i[5].ToString());
                        }
                        else
                        {
                            tileset.Tiles[index].Properties.AddProperty(i[1].ToString(), i[3].ToString());
                        }
                    }
                }
            }
        }

        private Layer ParseLayer(JObject layerJson)
        {
            if (layerJson["type"].ToString() == "tilelayer")
            {
                return ParseTileLayer(layerJson);
            }
            else if (layerJson["type"].ToString() == "objectgroup")
            {
                return ParseObjectGroup(layerJson);
            }
            else if (layerJson["type"].ToString() == "imagelayer")
            {
                return ParseImageLayer(layerJson);
            }
            else
            {
                return null;
            }
        }

        private Layer ParseTileLayer(JObject layerJson)
        {
            string name = layerJson["name"].ToString();
            float opacity = (float)layerJson["opacity"];

            bool isVisible = true;
            if (layerJson["visible"].ToString() == "false")
                isVisible = false;

            int x = (int)layerJson["x"];
            int y = (int)layerJson["y"];
            int width = (int)layerJson["width"];
            int height = (int)layerJson["height"];

            MapProperties properties = null;
            if (layerJson["properties"] != null)
            {
                properties = new MapProperties(JsonConvert.DeserializeObject<Dictionary<string, object>>(layerJson["properties"].ToString()));
            }

            TileLayer tileLayer = new TileLayer(name, opacity, isVisible, properties, x, y, width, height);
            ParseTileLayerCells(tileLayer, (JArray)layerJson["data"]);
            
            return tileLayer;
        }

        private void ParseTileLayerCells(TileLayer tileLayer, JArray cellJson)
        {
            List<int> values = new List<int>();
            foreach (int v in cellJson)
            {
                values.Add(v);
            }

            int k = 0;
            for (int j = 0; j < tileLayer.Height; j++)
            {
                for (int i = 0; i < tileLayer.Width; i++)
                {
                    int gid = values[k];
                    Cell cell = new Cell(map.Tilesets.GetTile(gid));
                    tileLayer.SetCell(cell, i, j);
                    k++;
                }
            }
        }

        private Layer ParseObjectGroup(JObject layerJson)
        {
            string name = layerJson["name"].ToString();
            float opacity = (float)layerJson["opacity"];

            bool isVisible = true;
            if (layerJson["visible"].ToString() == "false")
                isVisible = false;

            int x = (int)layerJson["x"];
            int y = (int)layerJson["y"];
            int width = (int)layerJson["width"];
            int height = (int)layerJson["height"];

            // TODO: Do ObjectGroups have properties as a whole?
            ObjectGroup objGroup = new ObjectGroup(name, opacity, isVisible, null, x, y, width, height);

            if (layerJson["color"] != null)
            {
                objGroup.Color = ValueConverter.ConvertToColor(layerJson["color"].ToString());
            }

            string strDrawOrder = layerJson["draworder"].ToString();
            if (strDrawOrder.Equals("topdown"))
            {
                objGroup.DrawOrder = DrawOrder.TopDown;
            }
            else
            {
                objGroup.DrawOrder = DrawOrder.TopDown;
            }

            ParseObjects(objGroup, layerJson);
            return objGroup;
        }

        private void ParseObjects(ObjectGroup objGroup, JObject layerJson)
        {
            JArray objectJson = (JArray)layerJson["objects"];
            foreach (JObject objJson in objectJson)
            {
                MapObject obj = new MapObject();


            }
        }

        private Layer ParseImageLayer(JObject layerJson)
        {
            throw new NotImplementedException();
        }
    }
}

