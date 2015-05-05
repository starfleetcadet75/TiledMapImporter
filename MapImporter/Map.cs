using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MapImporter
{
    /// <summary>
    /// The different kinds of orientation types that Tiled supports.
    /// </summary>
    public enum Orientation
    {
        /// <summary>
        /// The Tiled map has an orthogonal orientation.
        /// </summary>
        Orthogonal,
        /// <summary>
        /// The Tiled map has an isometric orientation.
        /// </summary>
        Isometric,
        /// <summary>
        /// The Tiled map has a staggered orientation.
        /// </summary>
        Staggered
    }

    /// <summary>
    /// The different render orders possible for drawing the maps.
    /// </summary>
    public enum RenderOrder
    {
        /// <summary>
        /// The Tiled map should be rendered right and down.
        /// </summary>
        RightDown,
        /// <summary>
        /// The Tiled map should be rendered right and up.
        /// </summary>
        RightUp,
        /// <summary>
        /// The Tiled map should be rendered left and down.
        /// </summary>
        LeftDown,
        /// <summary>
        /// The Tiled map should be rendered left and up.
        /// </summary>
        LeftUp
    }

    /// <summary>
    /// The different types of Layers possible in a Tiled map.
    /// </summary>
    public enum LayerType
    {
        /// <summary>
        /// A layer of tiles from a tileset.
        /// </summary>
        TileLayer,
        /// <summary>
        /// A layer of objects.
        /// </summary>
        ObjectGroup,
        /// <summary>
        /// A layer of images.
        /// </summary>
        ImageLayer
    }

    /// <summary>
    /// Holds layer specific information for each layer.
    /// </summary>
    public struct LayerData
    {
        /// <summary>
        /// The Layer's index in the overall layer list.
        /// </summary>
        public int Index;
        /// <summary>
        /// The Layer's index in it's specific layer list.
        /// </summary>
        public int LocalIndex;
        /// <summary>
        /// The type of this specific layer.
        /// </summary>
        public LayerType LayerType;

        /// <summary>
        /// Constructor for LayerData structure.
        /// </summary>
        /// <param name="index">The Layer's index in the overall layer list.</param>
        /// <param name="localIndex">The Layer's index in it's specific layer list.</param>
        /// <param name="layerType">The type of this specific layer.</param>
        public LayerData(int index, int localIndex, LayerType layerType)
        {
            Index = index;
            LocalIndex = localIndex;
            LayerType = layerType;
        }
    }

    /// <summary>
    /// A map object created using the Tiled Map Editor
    /// (http://www.mapeditor.org/).
    /// </summary>
    public class Map
    {
        /// <summary>
        /// The TMX format version, generally 1.0.
        /// </summary>
        public string Version { set; get; }
        /// <summary>
        /// Map orientation. This library only supports Orthogonal.
        /// <see cref="MapImporter.Orientation"/>
        /// </summary>
        public Orientation Orientation { set; get; }
        /// <summary>
        /// The map width in tiles.
        /// </summary>
        public int Width { set; get; }
        /// <summary>
        /// The map height in tiles.
        /// </summary>
        public int Height { set; get; }
        /// <summary>
        /// The width of a tile.
        /// </summary>
        public int TileWidth { set; get; }
        /// <summary>
        /// The height of a tile.
        /// </summary>
        public int TileHeight { set; get; }
        /// <summary>
        /// The background color of the map (since 0.9, optional).
        /// </summary>
        public Color BackgroundColor { set; get; }
        /// <summary>
        /// The order in which tiles on tile layers are rendered.
        /// <see cref="MapImporter.RenderOrder"/>
        /// </summary>
        public RenderOrder RenderOrder { set; get; }
        /// <summary>
        /// Custom properties for the map object.
        /// <see cref="MapImporter.Properties"/>
        /// </summary>
        public Properties Props { set; get; }
        /// <summary>
        /// List of all tilesets used in this map.
        /// <see cref="MapImporter.Tileset"/>
        /// </summary>
        public List<Tileset> Tilesets { set; get; }
        /// <summary>
        /// List of all layers (regular tile layers) in this map.
        /// <see cref="MapImporter.TileLayer"/>
        /// </summary>
        public List<TileLayer> TileLayers { set; get; }
        /// <summary>
        /// List of all object groups in this map.
        /// <see cref="MapImporter.ObjectGroup"/>
        /// </summary>
        public List<ObjectGroup> ObjectGroups { set; get; }
        /// <summary>
        /// List of all image layers in this map.
        /// <see cref="MapImporter.ImageLayer"/>
        /// </summary>
        public List<ImageLayer> ImageLayers { set; get; }
        /// <summary>
        /// The list of every layer regardless of layer type.
        /// <see cref="MapImporter.LayerData"/>
        /// </summary>
        public List<LayerData> LayerDataList { set; get; }
        /// <summary>
        /// The id of the next object in the map.
        /// </summary>
        public int NextObjectId { set; get; }

        /// <summary>
        /// Constructor for the Map class.
        /// </summary>
        public Map()
        {
            Props = new Properties();
            Tilesets = new List<Tileset>();
            TileLayers = new List<TileLayer>();
            ObjectGroups = new List<ObjectGroup>();
            ImageLayers = new List<ImageLayer>();
            LayerDataList = new List<LayerData>();
        }

        /// <summary>
        /// Translates the tile at the screen location to a tile coordinate on the overall map.
        /// </summary>
        /// <param name="tileOnScreen">The i and j indices of the tile on the screen.</param>
        /// <param name="startIndex">The i and j indices of the upper left most tile in the actual layer data.</param>
        /// <returns>The i and j indices of the tile in the map's layer data.</returns>
        public Vector2 TranslateScreenToMap(Vector2 tileOnScreen, Vector2 startIndex)
        {
            return new Vector2(startIndex.X + tileOnScreen.X - 1, startIndex.Y + tileOnScreen.Y - 1);
        }

        /// <summary>
        /// Finds and returns the Tile with the given global id.
        /// </summary>
        /// <param name="gid">The global id of the tile to find</param>
        /// <returns>The Tile in the Map with the given global id</returns>
        /// <see cref="MapImporter.Tile"/>
        public Tile GetTile(int gid)
        {
            Tileset t = GetTileset(gid);
            return t.GetTile(gid);
        }

        /// <summary>
        /// Returns the global id of the tile at the specified postion on the screen.
        /// </summary>
        /// <remarks>
        /// This is a powerful method that can be used in combination with your game logic to determine collisions, doors, etc.
        /// </remarks>
        /// <param name="tileOnScreen">The i and j indices of the tile on the screen.</param>
        /// <param name="startIndex">The i and j indices of the upper left most tile in the actual layer data.</param>
        /// <param name="tileLayer">The specific TileLayer to search.</param>
        /// <returns>The global id of the tile at the given location.</returns>
        /// <see cref="MapImporter.Tile"/>
        public int GetTile(Vector2 tileOnScreen, Vector2 startIndex, TileLayer tileLayer)
        {
            Vector2 temp = TranslateScreenToMap(tileOnScreen, startIndex);
            return tileLayer.Data.GetTileData((int)temp.X, (int)temp.Y);
        }

        /// <summary>
        /// Returns the tileset with the given name.
        /// </summary>
        /// <param name="name">The name of the tileset to search for.</param>
        /// <returns>The tileset with the given name.</returns>
        /// <see cref="MapImporter.Tileset"/>
        public Tileset GetTileset(string name)
        {
            try
            {
                return Tilesets.Find(item => item.Name == name);
            }
            catch
            {
                throw new Exception("\nCould not find Tileset.");
            }
        }

        /// <summary>
        /// Returns the tileset that contains the given global id.
        /// </summary>
        /// <remarks>
        /// This method can be used when given a global id in order to
        /// the find the Tileset that contains the Tile.
        /// </remarks>
        /// <param name="gid">The global id of the Tile to find.</param>
        /// <returns>The tileset that contains the tile with the given gid</returns>
        /// <see cref="MapImporter.Tileset"/>
        public Tileset GetTileset(int gid)
        {
            foreach (Tileset t in Tilesets)
                if (t.Contains(gid))
                    return t;

            throw new Exception("\nCould not find Tileset.");
        }

        /// <summary>
        /// Returns a Dictionary object of all properties for the Tile with the given global id.
        /// </summary>
        /// <param name="gid">The gid of the tile</param>
        /// <returns>The properties of the given tile</returns>
        /// <see cref="MapImporter.Properties"/>
        public Dictionary<string, string> GetTileProps(int gid)
        {
            Tile tile = GetTile(gid);
            return tile.Props.PropertiesList;
        }

        /// <summary>
        /// Returns the tile layer with the given name.
        /// </summary>
        /// <param name="name">The name of the tile layer to find.</param>
        /// <returns>The tile layer with the given name.</returns>
        public TileLayer GetTileLayer(string name)
        {
            try
            {
                return TileLayers.Find(item => item.Name == name);
            }
            catch
            {
                throw new Exception("\nCould not find the given TileLayer.");
            }
        }

        /// <summary>
        /// Returns the TileLayer object at the given index in the Map object's list of TileLayers.
        /// </summary>
        /// <param name="val">The index of the tilelayer to search for</param>
        /// <returns>The tilelayer at the given index</returns>
        public TileLayer GetTileLayer(int val)
        {
            if (TileLayers[val] == null)
                throw new IndexOutOfRangeException();
            return TileLayers[val];
        }

        /// <summary>
        /// Returns the object group with the given name.
        /// </summary>
        /// <param name="name">The name of the object group to find.</param>
        /// <returns>The object group with the given name.</returns>
        public ObjectGroup GetObjectGroup(string name)
        {
            try
            {
                return ObjectGroups.Find(item => item.Name == name);
            }
            catch
            {
                throw new Exception("\nCould not find the given ObjectGroup.");
            }
        }

        /// <summary>
        /// Returns the object group at the given index in the object group list.
        /// </summary>
        /// <param name="val">The index of the object group to find.</param>
        /// <returns>The object group at the given index.</returns>
        public ObjectGroup GetObjectGroup(int val)
        {
            if (ObjectGroups[val] == null)
                throw new IndexOutOfRangeException();
            return ObjectGroups[val];
        }

        /// <summary>
        /// Returns the image layer with the given name.
        /// </summary>
        /// <param name="name">The name of the image layer to find.</param>
        /// <returns>The image layer with the given name.</returns>
        public ImageLayer GetImageLayer(string name)
        {
            try
            {
                return ImageLayers.Find(item => item.Name == name);
            }
            catch
            {
                throw new Exception("\nCould not find the given ImageLayer.");
            }
        }

        /// <summary>
        /// Returns the image layer at the given index in the image layer list.
        /// </summary>
        /// <param name="val">The index of the image layer to find.</param>
        /// <returns>The image layer at the given index.</returns>
        public ImageLayer GetImageLayer(int val)
        {
            if (ImageLayers[val] == null)
                throw new IndexOutOfRangeException();
            return ImageLayers[val];
        }

        /// <summary>
        /// Moves the given layer in the layer list from its current index to the top.
        /// </summary>
        /// <param name="layerIndex">The index of LayerData in the layer list.</param>
        /// <see cref="MapImporter.LayerData"/>
        public void LayerToTop(int layerIndex)
        {
            foreach (LayerData data in LayerDataList)
            {
                if (data.Index == layerIndex)
                {
                    LayerToTop(data);
                    break;
                }
            }
        }

        /// <summary>
        /// Moves the given layer in the layer list from its current index to the top.
        /// </summary>
        /// <param name="layerData">The layerData object to be moved.</param>
        /// <see cref="MapImporter.LayerData"/>
        public void LayerToTop(LayerData layerData)
        {
            LayerData temp = layerData;
            LayerDataList.Remove(layerData);
            LayerDataList.Add(temp);
        }

        /// <summary>
        /// Moves the given layer in the layer list from its current index to the bottom.
        /// </summary>
        /// <param name="layerIndex">The index of the layer object's current position in the layer list.</param>
        /// <see cref="MapImporter.LayerData"/>
        public void LayerToBottom(int layerIndex)
        {
            foreach (LayerData data in LayerDataList)
            {
                if (data.Index == layerIndex)
                {
                    LayerToBottom(data);
                    break;
                }
            }
        }

        /// <summary>
        /// Moves the given layer in the layer list from its current index to the bottom.
        /// </summary>
        /// <param name="layerData">The layerData object to be moved.</param>
        /// <see cref="MapImporter.LayerData"/>
        public void LayerToBottom(LayerData layerData)
        {
            LayerData temp = layerData;
            LayerDataList.Remove(layerData);
            LayerDataList.Insert(0, temp);
        }

        /// <summary>
        /// Loads the Texture objects required for each Tileset.
        /// </summary>
        /// <remarks>
        /// This method can be a common cause of NullReferenceExceptions. If the Tileset
        /// textures are not properly loaded, the Map object cannot be drawn. If the files
        /// cannot be located in the project, there will be a Null Reference.
        /// </remarks>
        /// <example>
        /// If your project stores all Tileset textures inside a "tilesets" folder in your Content directory,
        /// you could alter the filepath to automatically look in that folder.
        /// </example>
        /// <code>
        /// public void LoadTilesetTextures(ContentManager content);
        /// </code>
        /// <param name="content">A ContentManager object is needed in order to load the textures.</param>
        public void LoadTilesetTextures(ContentManager content)
        {
            foreach (Tileset t in Tilesets)
            {
                // NOTE: This line loads in the image used for your Tileset
                // using the image source property, which was read from the
                // JSON file. If you get an exception here, chances are your
                // file path inside the JSON file is not correct. It should
                // be different from the one that Tiled automatically puts in
                // there. See the Documentation for a further description.
                t.Image.Texture = content.Load<Texture2D>(t.Image.Source);
            }
        }

        /// <summary>
        /// Draws all visible layers of the Map to the screen.
        /// </summary>
        /// <param name="spriteBatch">A spritebatch object for drawing.</param>
        /// <param name="location">The location to draw the layers.</param>
        /// <param name="startIndex">The i and j indices of the first tile to draw.</param>
        public void Draw(SpriteBatch spriteBatch, Rectangle location, Vector2 startIndex)
        {
            foreach (LayerData data in LayerDataList)
            {
                if (data.LayerType == LayerType.TileLayer)
                {
                    if (TileLayers[data.LocalIndex].Visible)
                    {
                        DrawLayer(spriteBatch, TileLayers[data.LocalIndex], location, startIndex);
                    }
                }
                else if (data.LayerType == LayerType.ObjectGroup)
                {
                    if (ObjectGroups[data.LocalIndex].Visible)
                    {
                        DrawLayer(spriteBatch, ObjectGroups[data.LocalIndex], location, startIndex);
                    }
                }
                else if (data.LayerType == LayerType.ImageLayer)
                {
                    if (ImageLayers[data.LocalIndex].Visible)
                    {
                        DrawLayer(spriteBatch, ImageLayers[data.LocalIndex], location, startIndex);
                    }
                }
            }
        }

        /// <summary>
        /// Draws the specified layer from the overall layer list to the screen.
        /// </summary>
        /// <param name="spriteBatch">A spritebatch object for drawing.</param>
        /// <param name="layerId">The id of the layer to be drawn in the LayerDataList list.</param>
        /// <param name="location">The location to draw the layer.</param>
        /// <param name="startIndex">The i and j indices of the first tile to draw.</param>
        public void DrawLayer(SpriteBatch spriteBatch, int layerId, Rectangle location, Vector2 startIndex)
        {
            if (LayerDataList[layerId].LayerType == LayerType.TileLayer)
            {
                if (TileLayers[LayerDataList[layerId].Index].Visible)
                {
                    DrawLayer(spriteBatch, TileLayers[LayerDataList[layerId].Index], location, startIndex);
                }
            }
            else if (LayerDataList[layerId].LayerType == LayerType.ObjectGroup)
            {
                if (ObjectGroups[LayerDataList[layerId].Index].Visible)
                {
                    DrawLayer(spriteBatch, ObjectGroups[LayerDataList[layerId].Index], location, startIndex);
                }
            }
            else if (LayerDataList[layerId].LayerType == LayerType.ImageLayer)
            {
                if (ObjectGroups[LayerDataList[layerId].Index].Visible)
                {
                    DrawLayer(spriteBatch, ImageLayers[LayerDataList[layerId].Index], location, startIndex);
                }
            }
        }

        /// <summary>
        /// Draws the specified layer to the screen.
        /// </summary>
        /// <param name="spriteBatch">A spritebatch object for drawing.</param>
        /// <param name="layerName">The name of the layer to be drawn.</param>
        /// <param name="location">The location to draw the layer.</param>
        /// <param name="startIndex">The i and j indices of the first tile to draw.</param>
        public void DrawLayer(SpriteBatch spriteBatch, string layerName, Rectangle location, Vector2 startIndex)
        {
            if (GetTileLayer(layerName) != null)
                DrawLayer(spriteBatch, GetTileLayer(layerName), location, startIndex);
            else if (GetObjectGroup(layerName) != null)
                DrawLayer(spriteBatch, GetObjectGroup(layerName), location, startIndex);
            else if (GetImageLayer(layerName) != null)
                DrawLayer(spriteBatch, GetImageLayer(layerName), location, startIndex);
        }

        /// <summary>
        /// Draws the specified object group to the screen.
        /// </summary>
        /// <param name="spriteBatch">A spritebatch object for drawing.</param>
        /// <param name="objectGroup">The object group object to be drawn.</param>
        /// <param name="location">The location to draw the layer.</param>
        /// <param name="startIndex">The i and j indices of the first tile to draw.</param>
        public void DrawLayer(SpriteBatch spriteBatch, ObjectGroup objectGroup, Rectangle location, Vector2 startIndex)
        {
            objectGroup.Draw(spriteBatch, location, startIndex);
        }

        /// <summary>
        /// Draws the specified image layer to the screen.
        /// </summary>
        /// <param name="spriteBatch">A spritebatch object for drawing.</param>
        /// <param name="imageLayer">The ImageLayer object to be drawn.</param>
        /// <param name="location">The location to draw the layer.</param>
        /// <param name="startIndex">The i and j indices of the first tile to draw.</param>
        public void DrawLayer(SpriteBatch spriteBatch, ImageLayer imageLayer, Rectangle location, Vector2 startIndex)
        {
            imageLayer.Draw(spriteBatch, location, startIndex);
        }

        /// <summary>
        /// Draws the specified tile layer to the screen.
        /// </summary>
        /// <param name="spriteBatch">A spritebatch object for drawing.</param>
        /// <param name="tileLayer">The TileLayer object to be drawn.</param>
        /// <param name="location">The location to draw the layer.</param>
        /// <param name="startIndex">The i and j indices of the first tile to draw.</param>
        public void DrawLayer(SpriteBatch spriteBatch, TileLayer tileLayer, Rectangle location, Vector2 startIndex)
        {
            int width = location.Width / TileWidth; //The number of tiles in the i direction that fit on the screen
            int height = location.Height / TileHeight; //The number of tiles in the j direction that fit on the screen
            int iStartIndex = (int)startIndex.X; //The i index of the first tile to draw
            int jStartIndex = (int)startIndex.Y; //The j index of the first tile to draw

            if (RenderOrder == RenderOrder.RightDown)
            {
                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        Draw(spriteBatch, i, j, iStartIndex, jStartIndex, tileLayer);
                    }
                }
            }
            else if (RenderOrder == RenderOrder.RightUp)
            {
                for (int j = height; 0 <= j; j--)
                {
                    for (int i = 0; i < width; i++)
                    {
                        Draw(spriteBatch, i, j, iStartIndex, jStartIndex, tileLayer);
                    }
                }
            }
            else if (RenderOrder == RenderOrder.LeftDown)
            {
                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        Draw(spriteBatch, i, j, iStartIndex, jStartIndex, tileLayer);
                    }
                }
            }
            else if (RenderOrder == RenderOrder.LeftUp)
            {
                for (int j = height; 0 <= j; j--)
                {
                    for (int i = width; 0 <= i; i--)
                    {
                        Draw(spriteBatch, i, j, iStartIndex, jStartIndex, tileLayer);
                    }
                }
            }
        }

        /// <summary>
        /// Performs the actual drawing. Above Draw method tells it what render order to use.
        /// </summary>
        /// <param name="spriteBatch">A spritebatch object for drawing.</param>
        /// <param name="i">The i index for drawwing in the i direction.</param>
        /// <param name="j">The j index for drawwing in the i direction.</param>
        /// <param name="iStartIndex">The i index of the first tile to draw.</param>
        /// <param name="jStartIndex">The j index of the first tile to draw.</param>
        /// <param name="layer">The layer to be drawn.</param>
        private void Draw(SpriteBatch spriteBatch, int i, int j, int iStartIndex, int jStartIndex, TileLayer layer)
        {
            int tileOffsetX = 0;
            int tileOffsetY = 0;
            float opacity = 1.0f;

            if (layer.Opacity != 1.0f)
            {
                opacity = layer.Opacity;
            }

            //This if statement keeps the code from throwing an out of range exception depending on where the start indices are.
            //There is an explanation along with comments on how to prevent this from happening in the documentation.
            if (iStartIndex + i < layer.Data.TileData.GetUpperBound(0) && jStartIndex + j < layer.Data.TileData.GetUpperBound(1)
                && iStartIndex >= 0 && jStartIndex >= 0)
            {
                int gid = layer.Data.GetTileData(iStartIndex + i, jStartIndex + j); //The global id of the tile in the layer at this point

                //If a gid of 0 occurs, it means that for this layer there is no tile placed in that location
                if (gid != 0)
                {
                    Tileset tileset = GetTileset(gid); //Find the tileset with the tile that we want to draw

                    //If the tileset has drawing offsets applied
                    if (tileset.TileOffset != null)
                    {
                        if (tileset.TileOffset.X != 0)
                        {
                            tileOffsetX = tileset.TileOffset.X;
                        }
                        else
                        {
                            tileOffsetX = 0;
                        }

                        if (tileset.TileOffset.Y != 0)
                        {
                            tileOffsetY = tileset.TileOffset.Y;
                        }
                        else
                        {
                            tileOffsetY = 0;
                        }
                    }

                    Tile tile = tileset.GetTile(gid); //Find the tile in that tileset
                    
                    spriteBatch.Draw(tileset.Image.Texture, new Rectangle((i * TileWidth) + tileOffsetX, (j * TileHeight) + tileOffsetY, TileWidth, TileHeight),
                        tile.Location, Color.White * opacity);
                }
            }
        }
    }
}
