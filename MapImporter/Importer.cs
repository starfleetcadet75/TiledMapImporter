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
    /// The Importer class contains the methods needed to import
    /// the Tiled maps and turn them into Map objects.
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
                    Console.Write("ERROR: Class=Importer --- Map file is not of type JSON or TMX" + "\n");
                    return null;
                }
            }
            catch (FileNotFoundException)
            {
                Console.Write("ERROR: Class=Importer --- Map file was not found --- Value=" + filepath + "\n");
                return null;
            }
            catch (NullReferenceException)
            {
                Console.Write("ERROR: Class=Importer --- Null reference found ---" + "\n");
                return null;
            }
            /*catch
            {
                throw new Exception("ERROR: Class=Importer --- General exception found during Importing process ---" + "\n");
            }*/
        }

        /// <summary>
        /// Parses the given text from a JSON file and turns it into a Map object.
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
                    m.Props.PropertiesList = JsonConvert.DeserializeObject<Dictionary<string, string>>(mapJSON["properties"].ToString());
                }

                if (mapJSON["nextobjectid"] != null)
                {
                    int num;
                    int.TryParse(mapJSON["nextobjectid"].ToString(), out num);
                    m.NextObjectId = num;
                }

                // Create and add the tilesets
                int gid = 1; //Keeps track of the global ids for all tiles
                JArray tilesetJson = (JArray)mapJSON["tilesets"];
                if (tilesetJson != null)
                {
                    foreach (JObject tilesets in tilesetJson)
                    {
                        Tileset tileset = new Tileset((int)tilesets["firstgid"], tilesets["name"].ToString(), (int)tilesets["tilewidth"], (int)tilesets["tileheight"], 
                            (int)tilesets["spacing"], (int)tilesets["margin"]);

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
                            tileset.Props.PropertiesList = JsonConvert.DeserializeObject<Dictionary<string, string>>(tilesets["properties"].ToString());
                        }

                        if (tilesets["terraintypes"] != null)
                        {
                            // add terraintypes
                        }

                        int id = 0; //Keeps track of the local id for the tiles
                        for (int y = (tileset.Margin / tileset.TileHeight); y < ((tileset.Image.Height - tileset.Margin) / tileset.TileHeight); y++)
                        {
                            for (int x = (tileset.Margin / tileset.TileWidth); x < ((tileset.Image.Width - tileset.Margin) / tileset.TileWidth); x++)
                            {
                                Tile tile = new Tile(id, gid, new Rectangle((x * tileset.TileWidth) + tileset.Spacing, (y * tileset.TileHeight) + tileset.Spacing,
                                    tileset.TileWidth, tileset.TileHeight));
                                tileset.Tiles.Add(tile);
                                id++;
                                gid++;
                            }
                        }

                        if (tilesets["tileproperties"] != null)
                        {
                            int index = 0;
                            string str = tilesets["tileproperties"].ToString();
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
                                        catch (OverflowException)
                                        {
                                            Console.Write("ERROR: Class=Importer --- Int32 conversion failed --- Value=" + i[1] + "\n");
                                        }
                                       
                                        tileset.Tiles[index].Props = new Properties();
                                        tileset.Tiles[index].Props.PropertiesList.Add(i[3].ToString(), i[5].ToString());
                                    }
                                    else
                                    {
                                        tileset.Tiles[index].Props.PropertiesList.Add(i[1].ToString(), i[3].ToString());
                                    }
                                }
                            }
                        }
                        m.Tilesets.Add(tileset);
                    }
                }

                // Add the layers
                int indexInLayerList = 0;
                foreach (JObject layerJson in mapJSON["layers"])
                {
                    if (layerJson["type"].ToString() == "tilelayer")
                    {
                        LayerData ld = new LayerData(indexInLayerList, LayerType.TileLayer);
                        m.LayerDataList.Add(ld);

                        TileLayer l = new TileLayer(layerJson["name"].ToString(), (int)layerJson["x"], (int)layerJson["y"], (int)layerJson["width"],
                            (int)layerJson["height"], (int)layerJson["opacity"]);

                        //Put the data from the layer into the Data object
                        JArray dataJson = (JArray)layerJson["data"];
                        l.Data = new Data(l.Width, l.Height);
                        List<int> nums = new List<int>();

                        try
                        {
                            foreach (int num in dataJson)
                            {
                                nums.Add(num);
                            }
                        }
                        catch
                        {
                            throw new OverflowException("ERROR: Class=Importer --- Int32 conversion failed ---" + "\n");
                        }

                        int k = 0;
                        for (int j = 0; j < l.Height; j++)
                        {
                            for (int i = 0; i < l.Width; i++)
                            {
                                try
                                {
                                    l.Data.TileData[i, j] = nums[k];
                                    k++;
                                }
                                catch
                                {
                                    throw new IndexOutOfRangeException("ERROR: Class=Importer --- Problem with indices in TileData --- Value="
                                        + l.Data.TileData[i, j] + "\n");
                                }
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

                        m.TileLayers.Add(l); // Add the new layer to the list of Layers
                    }
                    else if (layerJson["type"].ToString() == "objectgroup")
                    {
                        LayerData ld = new LayerData(indexInLayerList, LayerType.ObjectGroup);
                        m.LayerDataList.Add(ld);

                        ObjectGroup objGroup = new ObjectGroup(layerJson["name"].ToString(), (int)layerJson["x"], (int)layerJson["y"],
                            (int)layerJson["width"], (int)layerJson["height"], (int)layerJson["opacity"], ToColor(layerJson["color"].ToString()));

                        string str = layerJson["draworder"].ToString();
                        if (str.Equals("topdown"))
                        {
                            objGroup.DrawOrder = DrawOrder.TopDown;
                        }
                        else
                        {
                            objGroup.DrawOrder = DrawOrder.TopDown;
                        }

                        string s = layerJson["visible"].ToString();
                        if (s.Equals("true") || s.Equals("True"))
                        {
                            objGroup.Visible = true;
                        }
                        else
                        {
                            objGroup.Visible = false;
                        }

                        JArray objectJson = (JArray)layerJson["objects"];
                        foreach (JObject o in objectJson)
                        {
                            Object obj = new Object(objectJson["name"].ToString(), (int)objectJson["id"], (double)objectJson["width"], (double)objectJson["height"], 
                                (double)objectJson["x"], (double)objectJson["y"], objectJson["type"].ToString(), (double)objectJson["rotation"]);

                            string st = objectJson["visible"].ToString();
                            if (st.Equals("true") || s.Equals("True"))
                            {
                                obj.Visible = true;
                            }
                            else
                            {
                                obj.Visible = false;
                            }

                            if (objectJson["properties"] != null)
                            {
                                obj.Props.PropertiesList = JsonConvert.DeserializeObject<Dictionary<string, string>>(objectJson["properties"].ToString());
                            }

                            if (objectJson["gid"] != null)
                            {
                                obj.Gid = (int)objectJson["gid"];
                            }

                            // If the property Ellipse exists, then the object is an Ellipse
                            if (objectJson["ellipse"] != null)
                            {
                                obj.ObjType = ObjectType.Ellipse;
                            }
                            else if (objectJson["polygon"] != null)
                            {
                                obj.ObjType = ObjectType.Polygon;
                                JArray polygonJson = (JArray)objectJson["polygon"];
                                List<Vector2> points = new List<Vector2>();
                                foreach (JObject point in polygonJson)
                                {
                                    points.Add(new Vector2((int)polygonJson["x"], (int)polygonJson["y"]));
                                }
                                obj.Polygon = new Polygon(points);
                            }
                            else if (objectJson["polyline"] != null)
                            {
                                obj.ObjType = ObjectType.Polyline;
                                JArray polylineJson = (JArray)objectJson["polyline"];
                                foreach (JObject polyline in polylineJson)
                                {

                                }
                            }
                            else
                            {
                                obj.ObjType = ObjectType.Rectangle;
                            }

                            objGroup.Objects.Add(obj);
                        }

                        m.ObjectGroups.Add(objGroup);
                    }
                    else if (layerJson["type"].ToString() == "imagelayer")
                    {
                        LayerData ld = new LayerData(indexInLayerList, LayerType.ImageLayer);
                        m.LayerDataList.Add(ld);
                    }
                    indexInLayerList++;
                }
            }
            return m;
        }

        /// <summary>
        /// Parses the given text from a Tmx file and turns it into a Map object.
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
