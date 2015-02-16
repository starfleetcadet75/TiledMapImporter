using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace MapImporter
{
    /// <summary>
    /// Importer class contains the methods needed to import the Tiled maps.
    /// </summary>
    public class Importer
    {
        /// <summary>
        /// Creates a Map object from the given file. File can be either in JSON or TMX format.
        /// </summary>
        /// <param name="filepath">The location of the Tiled map to be read</param>
        /// <returns>The new Map object that can be used in game</returns>
        public static Map ImportMap(string filepath)
        {
            try
            {
                if (filepath.Contains(".json"))
                {
                    return ReadMapAsJson(File.ReadAllText(@filepath));
                }
                else if (filepath.Contains(".tmx"))
                {
                    return ReadMapAsTmx(File.ReadAllText(@filepath));
                }
                else
                {
                    return null;
                }
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }

        /// <summary>
        /// Parses the given text from a JSON file and turns it into a Map object
        /// </summary>
        /// <param name="fileText">The text of the JSON file</param>
        /// <returns>A new Map object</returns>
        private static Map ReadMapAsJson(string fileText)
        {
            JObject mapJSON = JObject.Parse(fileText);
            Map m = new Map();

            if (mapJSON != null)
            {
                m.Version = mapJSON["version"].ToString();

                if (mapJSON["orientation"].ToString() == "orthogonal")
                {
                    m.Orientation = Orientation.Orthogonal;
                }
                else if (mapJSON["orientation"].ToString() == "isometric")
                {
                    m.Orientation = Orientation.Isometric;
                }
                else if (mapJSON["orientation"].ToString() == "staggered")
                {
                    m.Orientation = Orientation.Staggered;
                }
                
                m.Width = (int)mapJSON["width"];
                m.Height = (int)mapJSON["height"];
                m.TileWidth = (int)mapJSON["tilewidth"];
                m.TileHeight = (int)mapJSON["tileheight"];

                if (mapJSON["backgroundcolor"] != null)
                {
                    m.BackgroundColor = ToColor(mapJSON["backgroundcolor"].ToString());
                }

                if (mapJSON["renderorder"].ToString() == "right-down")
                {
                    m.RenderOrder = RenderOrder.RightDown;
                }
                else if (mapJSON["renderorder"].ToString() == "right-up")
                {
                    m.RenderOrder = RenderOrder.RightUp;
                }
                else if (mapJSON["renderorder"].ToString() == "left-down")
                {
                    m.RenderOrder = RenderOrder.LeftDown;
                }
                else if (mapJSON["renderorder"].ToString() == "left-up")
                {
                    m.RenderOrder = RenderOrder.LeftUp;
                }

                if (mapJSON["properties"] != null)
                {
                    m.Props = new Properties();
                    m.Props.PropertiesList = JsonConvert.DeserializeObject<Dictionary<string, string>>(mapJSON["properties"].ToString());
                }

                // Create and add the tilesets
                int gid = 1; //Keeps track of the global ids for all tiles
                JArray tilesetJson = (JArray)mapJSON["tilesets"];
                if (tilesetJson != null)
                {
                    m.Tilesets = new List<Tileset>();

                    foreach (JObject tilesets in tilesetJson)
                    {
                        Tileset tileset = new Tileset();
                        tileset.Firstgid = (int)tilesets["firstgid"];
                        tileset.Name = tilesets["name"].ToString();
                        tileset.TileWidth = (int)tilesets["tilewidth"];
                        tileset.TileHeight = (int)tilesets["tileheight"];
                        tileset.Spacing = (int)tilesets["spacing"];
                        tileset.Margin = (int)tilesets["margin"];

                        if (tilesets["tileoffset"] != null)
                        {
                            JObject offset = (JObject)tilesets["tileoffset"];
                            tileset.TileOffset = new TileOffset();
                            if (offset["x"] != null)
                            {
                                tileset.TileOffset.X = (int)offset["x"];
                            }
                            else
                            {
                                tileset.TileOffset.X = 0;
                            }

                            if (offset["y"] != null)
                            {
                                tileset.TileOffset.Y = (int)offset["x"];
                            }
                            else
                            {
                                tileset.TileOffset.Y = 0;
                            }
                        }

                        if (tilesets["image"] != null)
                        {
                            tileset.Image = new Image();
                            tileset.Image.Source = tilesets["image"].ToString();
                            tileset.Image.Format = Path.GetExtension(tileset.Image.Source);
                            if (tilesets["trans"] != null)
                            {
                                tileset.Image.Trans = ToColor(tilesets["trans"].ToString());
                            }
                            if (tilesets["imagewidth"] != null)
                            {
                                tileset.Image.Width = (int)tilesets["imagewidth"];
                            }
                            if (tilesets["imageheight"] != null)
                            {
                                tileset.Image.Height = (int)tilesets["imageheight"];
                            }
                            tileset.Source = tileset.Image.Source;
                        }

                        if (tilesets["properties"] != null)
                        {
                            tileset.Props = new Properties();
                            tileset.Props.PropertiesList = JsonConvert.DeserializeObject<Dictionary<string, string>>(tilesets["properties"].ToString());
                        }

                        if (tilesets["terraintypes"] != null)
                        {
                            // add terraintypes
                        }

                        tileset.Tiles = new List<Tile>(); //List of all tiles in this Tileset
                        int id = 0; //Keeps track of the local id for the tiles
                        for (int y = (tileset.Margin / tileset.TileHeight); y < ((tileset.Image.Height - tileset.Margin) / tileset.TileHeight); y++)
                        {
                            for (int x = (tileset.Margin / tileset.TileWidth); x < ((tileset.Image.Width - tileset.Margin) / tileset.TileWidth); x++)
                            {
                                Tile tile = new Tile();
                                tile.Id = id;
                                tile.Gid = gid;
                                tile.Location = new Rectangle((x * tileset.TileWidth) + tileset.Spacing, (y * tileset.TileHeight) + tileset.Spacing,
                                    tileset.TileWidth, tileset.TileHeight);
                                tileset.Tiles.Add(tile);
                                id++;
                                gid++;
                            }
                        }

                        if (tilesets["tileproperties"] != null)
                        {
                            string str = tilesets["tileproperties"].ToString();
                            if (str.Contains(","))
                            {
                                string[] elements = str.Split(',');
                                foreach (string s in elements)
                                {
                                    string[] i = s.Split('\"');
                                    int index = Convert.ToInt32(i[1]);
                                    tileset.Tiles[index].Props = new Properties();
                                    tileset.Tiles[index].Props.PropertiesList.Add(i[3].ToString(), i[5].ToString());
                                }
                            }
                        }
                        m.Tilesets.Add(tileset);
                    }
                }

                // Create and add the layers
                m.Layers = new List<Layer>();
                foreach (JObject layerJson in mapJSON["layers"])
                {
                    Layer l = new Layer();
                    l.Name = layerJson["name"].ToString();
                    l.X = (int)layerJson["x"];
                    l.Y = (int)layerJson["y"];
                    l.Width = (int)layerJson["width"];
                    l.Height = (int)layerJson["height"];
                    l.Opacity = (int)layerJson["opacity"];

                    //Put the data from the layer into the Data object
                    JArray dataJson = (JArray)layerJson["data"];
                    l.Data = new Data(l.Width, l.Height);
                    List<int> nums = new List<int>();

                    foreach (int num in dataJson)
                    {
                        nums.Add(num);
                    }

                    int k = 0;
                    for (int j = 0; j < l.Height; j++)
                    {
                        for (int i = 0; i < l.Width; i++)
                        {
                            l.Data.TileData[i, j] = nums[k];
                            k++;
                        }
                    }

                    string s = layerJson["visible"].ToString();
                    if (s.Equals("true") || s.Equals("True"))
                    {
                        l.Visible = true;
                    }
                    else
                    {
                        l.Visible = false;
                    }

                    if (layerJson["properties"] != null)
                    {
                        l.Props = new Properties();
                        l.Props.PropertiesList = JsonConvert.DeserializeObject<Dictionary<string, string>>(layerJson["properties"].ToString());
                    }

                    m.Layers.Add(l); // Add the new layer to the list of Layers
                }

                if (mapJSON["objectgroups"] != null)
                {
                    //Add object groups
                }

                if (mapJSON["imagelayers"] != null)
                {
                    //Add image layers
                }
            }
            return m;
        }

        /// <summary>
        /// Parses the given text from a Tmx file and turns it into a Map object
        /// </summary>
        /// <param name="fileText">The text of the Tmx file</param>
        /// <returns>A new Map object</returns>
        private static Map ReadMapAsTmx(string fileText)
        {
            return null;
        }

        /// <summary>
        /// Creates a color value from an RGB hex string.
        /// </summary>
        /// <param name="hexString">The RGB string to parse.</param>
        /// <returns>The color created from the hex string.</returns>
        private static Color ToColor(string hexString)
        {
            if (hexString.StartsWith("#"))
                hexString = hexString.Substring(1);
            uint hex = uint.Parse(hexString, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture);
            Color color = Color.White;
            if (hexString.Length == 8)
            {
                color.A = (byte)(hex >> 24);
                color.R = (byte)(hex >> 16);
                color.G = (byte)(hex >> 8);
                color.B = (byte)(hex);
            }
            else if (hexString.Length == 6)
            {
                color.R = (byte)(hex >> 16);
                color.G = (byte)(hex >> 8);
                color.B = (byte)(hex);
            }
            else
            {
                throw new InvalidOperationException("Invald hex representation of an RGB color value.");
            }
            return color;
        }
    }
}
