using MapImporter.Enums;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace MapImporter.Loaders
{
    public class JsonMapLoader
    {
        private string filename;
        private Map map;
        private int gid = 1;  // Keeps track of the global ids for all tiles

        public JsonMapLoader(string filename)
        {
            this.filename = filename;
            this.map = new Map();
        }

        public Map Load()
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


            }

            return map;
        }

        private void ParseMapValues(JObject mapJSON)
        {
            map.Version = mapJSON["version"].ToString();

            if (mapJSON["orientation"].ToString() == "orthogonal")
                map.Orientation = Orientation.Orthogonal;
            else throw new UnsupportedOrientationException();

            map.Width = (int)mapJSON["width"];
            map.Height = (int)mapJSON["height"];
            map.TileWidth = (int)mapJSON["tilewidth"];
            map.TileHeight = (int)mapJSON["tileheight"];

            if (mapJSON["backgroundcolor"] != null)
                map.BackgroundColor = ConvertToColor(mapJSON["backgroundcolor"].ToString());

            if (mapJSON["renderorder"].ToString() == "right-down")
                map.RenderOrder = RenderOrder.RightDown;
            else if (mapJSON["renderorder"].ToString() == "right-up")
                map.RenderOrder = RenderOrder.RightUp;
            else if (mapJSON["renderorder"].ToString() == "left-down")
                map.RenderOrder = RenderOrder.LeftDown;
            else if (mapJSON["renderorder"].ToString() == "left-up")
                map.RenderOrder = RenderOrder.LeftUp;

            if (mapJSON["properties"] != null)
            {
                Dictionary<string, object> properties = JsonConvert.DeserializeObject<Dictionary<string, object>>(mapJSON["properties"].ToString());
                map.Properties = new MapProperties(properties);
            }

            if (mapJSON["nextobjectid"] != null)
            {
                int num;
                int.TryParse(mapJSON["nextobjectid"].ToString(), out num);
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
                Dictionary<string, object> properties = JsonConvert.DeserializeObject<Dictionary<string, object>>(tilesets["properties"].ToString());
                tileset.Properties = new MapProperties(properties);
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
                    tileset.Image.Trans = ConvertToColor(tilesetJson["trans"].ToString());

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
    }
}

