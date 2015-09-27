using MapImporter.Enums;
using Microsoft.Xna.Framework;

namespace MapImporter
{
    /// <summary>
    /// A map object created using the Tiled Map Editor.
    /// <see cref="http://www.mapeditor.org/"/>
    /// </summary>
    public class Map
    {
        /// <summary>
        /// The TMX format version, generally 1.0.
        /// </summary>
        public string Version { set; get; }
        /// <summary>
        /// Map orientation. This library only supports Orthogonal.
        /// <see cref="Enums.Orientation"/>
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
        /// <see cref="Enums.RenderOrder"/>
        /// </summary>
        public RenderOrder RenderOrder { set; get; }
        /// <summary>
        /// Custom properties for the map object.
        /// <see cref="MapImporter.Properties"/>
        /// </summary>
        public MapProperties Properties { set; get; }
        /// <summary>
        /// Container object to store all of the map's Tilesets.
        /// </summary>
        public Tilesets Tilesets { set; get; }
        /// <summary>
        /// Container object to store all of the map's Layers.
        /// </summary>
        public Layers Layers { set; get; }
        /// <summary>
        /// The id of the next object in the map.
        /// </summary>
        public int NextObjectId { set; get; }

        /// <summary>
        /// Constructor for the Map class.
        /// </summary>
        public Map()
        {
            Properties = new MapProperties();
            Layers = new Layers();
            Tilesets = new Tilesets();
        }
    }
}

