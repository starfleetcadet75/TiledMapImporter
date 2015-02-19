using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MapImporter
{
    /// <summary>
    /// The different kinds of orientation types that Tiled supports
    /// </summary>
    public enum Orientation
    {
        /// <summary>
        /// The Tiled map has an orthogonal orientation
        /// </summary>
        Orthogonal,
        /// <summary>
        /// The Tiled map has an isometric orientation
        /// </summary>
        Isometric,
        /// <summary>
        /// The Tiled map has a staggered orientation
        /// </summary>
        Staggered
    }

    /// <summary>
    /// The different render orders possible
    /// </summary>
    public enum RenderOrder
    {
        /// <summary>
        /// The Tiled map should be rendered right and down
        /// </summary>
        RightDown,
        /// <summary>
        /// The Tiled map should be rendered right and up
        /// </summary>
        RightUp,
        /// <summary>
        /// The Tiled map should be rendered left and down
        /// </summary>
        LeftDown,
        /// <summary>
        /// The Tiled map should be rendered left and up
        /// </summary>
        LeftUp
    }

    /// <summary>
    /// The different types of Layers possible in a Tiled map
    /// </summary>
    public enum LayerType
    {
        /// <summary>
        /// A layer of tiles from a tileset
        /// </summary>
        TileLayer,
        /// <summary>
        /// A layer of objects
        /// </summary>
        ObjectGroup,
        /// <summary>
        /// A layer of images
        /// </summary>
        ImageLayer
    }

    /// <summary>
    /// Holds layer specific information for each layer
    /// </summary>
    public struct LayerData
    {
        /// <summary>
        /// The Layer's index in the overall layer list
        /// </summary>
        public int Index;
        /// <summary>
        /// The type of this specific layer
        /// </summary>
        public LayerType LayerType;
    }

    /// <summary>
    /// A map object created using the Tiled Map Editor
    /// http://www.mapeditor.org/
    /// </summary>
    public class Map
    {
        /// <summary>
        /// The TMX format version, generally 1.0
        /// </summary>
        public string Version { set; get; }
        /// <summary>
        /// Map orientation. This library only supports Orthogonal.
        /// </summary>
        public Orientation Orientation { set; get; }
        /// <summary>
        /// The map width in tiles
        /// </summary>
        public int Width { set; get; }
        /// <summary>
        /// The map height in tiles
        /// </summary>
        public int Height { set; get; }
        /// <summary>
        /// The width of a tile
        /// </summary>
        public int TileWidth { set; get; }
        /// <summary>
        /// The height of a tile
        /// </summary>
        public int TileHeight { set; get; }
        /// <summary>
        /// The background color of the map (since 0.9, optional)
        /// </summary>
        public Color BackgroundColor { set; get; }
        /// <summary>
        /// The order in which tiles on tile layers are rendered
        /// </summary>
        public RenderOrder RenderOrder { set; get; }
        /// <summary>
        /// Custom properties for the map object
        /// </summary>
        public Properties Props { set; get; }
        /// <summary>
        /// List of all tilesets used in this map
        /// </summary>
        public List<Tileset> Tilesets { set; get; }
        /// <summary>
        /// List of all layers(regular tile layers) in this map
        /// </summary>
        public List<TileLayer> TileLayers { set; get; }
        /// <summary>
        /// List of all object groups in this map
        /// </summary>
        public List<ObjectGroup> ObjectGroups { set; get; }
        /// <summary>
        /// List of all image layers in this map
        /// </summary>
        public List<ImageLayer> ImageLayers { set; get; }
        /// <summary>
        /// The list of every layer regardless of layer type
        /// </summary>
        public List<LayerData> LayerDataList { set; get; }

        /// <summary>
        /// Returns the tileset with the given name
        /// </summary>
        /// <param name="name">The name of the tileset to search for</param>
        /// <returns>The tileset with the given name</returns>
        public Tileset GetTilesetByName(string name)
        {
            foreach (Tileset t in Tilesets)
            {
                if (t.Name == name)
                {
                    return t;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the tileset that contains the given global id
        /// </summary>
        /// <param name="gid">The global id to search for</param>
        /// <returns>The tileset that contains the tile with the given gid</returns>
        public Tileset GetTilesetWithGid(int gid)
        {
            foreach (Tileset t in Tilesets)
            {
                if (t.GetTileByGid(gid) != null)
                {
                    return t;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the tile layer with the given name
        /// </summary>
        /// <param name="name">The name of the tile layer to search for</param>
        /// <returns>The tile layer with the given name</returns>
        public TileLayer GetTileLayerByName(string name)
        {
            foreach (TileLayer l in TileLayers)
            {
                if (l.Name == name)
                {
                    return l;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the object group with the given name
        /// </summary>
        /// <param name="name">The name of the object group to search for</param>
        /// <returns>The object group with the given name</returns>
        public ObjectGroup GetObjectGroupByName(string name)
        {
            foreach (ObjectGroup obj in ObjectGroups)
            {
                if (obj.Name == name)
                {
                    return obj;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the image layer with the given name
        /// </summary>
        /// <param name="name">The name of the image layer to search for</param>
        /// <returns>The image layer with the given name</returns>
        public ImageLayer GetImageLayerByName(string name)
        {
            foreach (ImageLayer l in ImageLayers)
            {
                if (l.Name == name)
                {
                    return l;
                }
            }
            return null;
        }

        /// <summary>
        /// Moves the given layer in the layer list from its current index to the top.
        /// </summary>
        /// <param name="layerIndex">The index of layerdata in the layer list</param>
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
        /// <param name="layerData">The layerData object to be moved</param>
        public void LayerToTop(LayerData layerData)
        {
            LayerData temp = layerData;
            LayerDataList.Remove(layerData);
            LayerDataList.Add(temp);
        }

        /// <summary>
        /// Moves the given layer in the layer list from its current index to the bottom.
        /// </summary>
        /// <param name="layerIndex">The index of the layer object's current position in the layer list</param>
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
        /// <param name="layerData">The layerData object to be moved</param>
        public void LayerToBottom(LayerData layerData)
        {
            LayerData temp = layerData;
            LayerDataList.Remove(layerData);
            LayerDataList.Insert(0, temp);
        }

        /// <summary>
        /// Returns the gid of the tile at the specified postion on the screen. Use this method
        /// in combination with your game logic to determine collisions, doors, etc.
        /// </summary>
        /// <param name="tileOnScreen">The i and j indices of the tile on the screen</param>
        /// <param name="startIndex">The i and j indices of the upper left most tile in the actual layer data</param>
        /// <param name="tileLayer">The specific tile layer whose data we want to look at</param>
        /// <returns></returns>
        public int GetTileGidAt(Vector2 tileOnScreen, Vector2 startIndex, TileLayer tileLayer)
        {
            Vector2 temp = TranslateScreenToMap(tileOnScreen, startIndex);
            return tileLayer.Data.GetTileDataAt((int)temp.X, (int)temp.Y);
        }

        /// <summary>
        /// Translates the tile at the screen loaction to a tile coordinate on the overall map.
        /// </summary>
        /// <param name="tileOnScreen">The i and j indices of the tile on the screen</param>
        /// <param name="startIndex">The i and j indices of the upper left most tile in the actual layer data</param>
        /// <returns>The i and j indices of the tile in the map's layer data</returns>
        public Vector2 TranslateScreenToMap(Vector2 tileOnScreen, Vector2 startIndex)
        {
            return new Vector2(startIndex.X + tileOnScreen.X, startIndex.Y + tileOnScreen.Y);
        }

        /*   Untested  */
        /// <summary>
        /// Translates the tile at the given loaction on the overall map to its tile position on the screen.
        /// </summary>
        /// <param name="tileOnScreen">The i and j indices of the tile on the screen</param>
        /// <param name="startIndex">The i and j indices of the upper left most tile in the actual layer data</param>
        /// <returns>The i and j indices of the tile in the map's layer data</returns>
        public Vector2 TranslateMapToScreen(Vector2 tileOnScreen, Vector2 startIndex)
        {
            return new Vector2(startIndex.X - tileOnScreen.X, startIndex.Y - tileOnScreen.Y);
        }

        /// <summary>
        /// The textures need to be loaded for each tileset.
        /// THIS MUST BE CALLED BEFORE DRAWING ANYTHING OR YOU WILL GET AN EXCEPTION!
        /// </summary>
        public void LoadTilesetTextures(ContentManager content)
        {
            foreach (Tileset t in Tilesets)
            {
                t.Image.Texture = content.Load<Texture2D>(t.Image.Source);
            }
        }

        /// <summary>
        /// Draws all visible layers of the Map to the screen
        /// </summary>
        /// <param name="spriteBatch">A spritebatch object for drawing</param>
        /// <param name="location">The location to draw the layers</param>
        /// <param name="startIndex">The i and j indices of the first tile to draw</param>
        public void Draw(SpriteBatch spriteBatch, Rectangle location, Vector2 startIndex)
        {
            foreach (LayerData data in LayerDataList)
            {
                if (data.LayerType == LayerType.TileLayer)
                {
                    if (TileLayers[data.Index].Visible)
                    {
                        DrawLayer(spriteBatch, TileLayers[data.Index], location, startIndex);
                    }
                }
                else if (data.LayerType == LayerType.ObjectGroup)
                {
                    if (ObjectGroups[data.Index].Visible)
                    {
                        DrawLayer(spriteBatch, ObjectGroups[data.Index], location, startIndex);
                    }
                }
                else if (data.LayerType == LayerType.ImageLayer)
                {
                    if (ObjectGroups[data.Index].Visible)
                    {
                        DrawLayer(spriteBatch, ImageLayers[data.Index], location, startIndex);
                    }
                }
            }
        }

        /// <summary>
        /// Draws the specified layer from the overall layer list to the screen
        /// </summary>
        /// <param name="spriteBatch">A spritebatch object for drawing</param>
        /// <param name="layerId">The id of the layer to be drawn in the LayerDataList list</param>
        /// <param name="location">The location to draw the layer</param>
        /// <param name="startIndex">The i and j indices of the first tile to draw</param>
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
        /// Draws the specified layer to the screen
        /// </summary>
        /// <param name="spriteBatch">A spritebatch object for drawing</param>
        /// <param name="layerName">The name of the layer to be drawn</param>
        /// <param name="location">The location to draw the layer</param>
        /// <param name="startIndex">The i and j indices of the first tile to draw</param>
        public void DrawLayer(SpriteBatch spriteBatch, string layerName, Rectangle location, Vector2 startIndex)
        {
            if (GetTileLayerByName(layerName) != null)
                DrawLayer(spriteBatch, GetTileLayerByName(layerName), location, startIndex);
            else if (GetObjectGroupByName(layerName) != null)
                DrawLayer(spriteBatch, GetObjectGroupByName(layerName), location, startIndex);
            else if (GetImageLayerByName(layerName) != null)
                DrawLayer(spriteBatch, GetImageLayerByName(layerName), location, startIndex);
        }

        /// <summary>
        /// Draws the specified tile layer to the screen
        /// </summary>
        /// <param name="spriteBatch">A spritebatch object for drawing</param>
        /// <param name="layer">The Layer object to be drawn</param>
        /// <param name="location">The location to draw the layer</param>
        /// <param name="startIndex">The i and j indices of the first tile to draw</param>
        public void DrawLayer(SpriteBatch spriteBatch, TileLayer layer, Rectangle location, Vector2 startIndex)
        {
            Draw(spriteBatch, layer, location, startIndex);
        }

        /// <summary>
        /// Draws the specified object group to the screen
        /// </summary>
        /// <param name="spriteBatch">A spritebatch object for drawing</param>
        /// <param name="objectGroup">The object group object to be drawn</param>
        /// <param name="location">The location to draw the layer</param>
        /// <param name="startIndex">The i and j indices of the first tile to draw</param>
        public void DrawLayer(SpriteBatch spriteBatch, ObjectGroup objectGroup, Rectangle location, Vector2 startIndex)
        {
            Draw(spriteBatch, objectGroup, location, startIndex);
        }

        /// <summary>
        /// Draws the specified image layer to the screen
        /// </summary>
        /// <param name="spriteBatch">A spritebatch object for drawing</param>
        /// <param name="imageLayer">The Image Layer object to be drawn</param>
        /// <param name="location">The location to draw the layer</param>
        /// <param name="startIndex">The i and j indices of the first tile to draw</param>
        public void DrawLayer(SpriteBatch spriteBatch, ImageLayer imageLayer, Rectangle location, Vector2 startIndex)
        {
            Draw(spriteBatch, imageLayer, location, startIndex);
        }

        /// <summary>
        /// Performs all the math for the tile layer and then makes the call to render it
        /// </summary>
        /// <param name="spriteBatch">A spritebatch object for drawing</param>
        /// <param name="tileLayer">The tile layer to be drawn</param>
        /// <param name="location">The location to draw the layers</param>
        /// <param name="startIndex">The i and j indices of the first tile to draw</param>
        private void Draw(SpriteBatch spriteBatch, TileLayer tileLayer, Rectangle location, Vector2 startIndex)
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
        /// Performs the actual rendering for the object group
        /// </summary>
        /// <param name="spriteBatch">A spritebatch object for drawing</param>
        /// <param name="objectGroup">The object group to be drawn</param>
        /// <param name="location">The location to draw the layers</param>
        /// <param name="startIndex">The i and j indices of the first tile to draw</param>
        private void Draw(SpriteBatch spriteBatch, ObjectGroup objectGroup, Rectangle location, Vector2 startIndex)
        {

        }

        /// <summary>
        /// Performs the actual rendering for the image layer
        /// </summary>
        /// <param name="spriteBatch">A spritebatch object for drawing</param>
        /// <param name="imageLayer">The image layer to be drawn</param>
        /// <param name="location">The location to draw the layers</param>
        /// <param name="startIndex">The i and j indices of the first tile to draw</param>
        private void Draw(SpriteBatch spriteBatch, ImageLayer imageLayer, Rectangle location, Vector2 startIndex)
        {

        }

        /// <summary>
        /// Performs the actual drawing. Above Draw method tells it what render order to use.
        /// </summary>
        /// <param name="spriteBatch">A spritebatch object for drawing</param>
        /// <param name="i">The i index for drawwing in the i direction</param>
        /// <param name="j">The j index for drawwing in the i direction</param>
        /// <param name="iStartIndex">The i index of the first tile to draw</param>
        /// <param name="jStartIndex">The j index of the first tile to draw</param>
        /// <param name="layer">The layer to be drawn</param>
        private void Draw(SpriteBatch spriteBatch, int i, int j, int iStartIndex, int jStartIndex, TileLayer layer)
        {
            int tileOffsetX = 0;
            int tileOffsetY = 0;

            //This if statement keeps the code from throwing an out of range exception depending on where the start indices are.
            //There is an explanation along with comments on how to prevent this from happening in the documentation.
            if (iStartIndex + i < layer.Data.TileData.GetUpperBound(0) && jStartIndex + j < layer.Data.TileData.GetUpperBound(1)
                && iStartIndex > 0 && jStartIndex > 0)
            {
                int gid = layer.Data.GetTileDataAt(iStartIndex + i, jStartIndex + j); //The global id of the tile in the layer at this point

                //If a gid of 0 occurs, it means that for this layer there is no tile placed in that location
                if (gid != 0)
                {
                    Tileset tileset = GetTilesetWithGid(gid); //Find the tileset with the tile that we want to draw

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

                    Tile tile = tileset.GetTileByGid(gid); //Find the tile in that tilset
                    spriteBatch.Draw(tileset.Image.Texture, new Rectangle((i * TileWidth) + tileOffsetX, (j * TileHeight) + tileOffsetY, TileWidth, TileHeight),
                        tile.Location, Color.White);
                }
            }
        }
    }
}
