using System.Collections.Generic;

namespace MapImporter
{
    /// <summary>
    /// Tileset object that contains the tiles used in a map
    /// </summary>
    public class Tileset
    {
        /// <summary>
        /// The first global tile id of this tileset
        /// . This global id maps to the first tile in this tileset.
        /// </summary>
        public int Firstgid { set; get; }
        /// <summary>
        /// The file path that specifies where the tileset image is located.
        ///  Be sure to manually check inside the Tiled map and verify the path!
        /// </summary>
        public string Source { set; get; }
        /// <summary>
        /// The name of this tileset.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The (maximum) width of the tiles in this tileset.
        /// </summary>
        public int TileWidth { get; set; }
        /// <summary>
        /// The (maximum) height of the tiles in this tileset.
        /// </summary>
        public int TileHeight { get; set; }
        /// <summary>
        /// The spacing in pixels between the tiles in this tileset.
        /// </summary>
        public int Spacing { get; set; }
        /// <summary>
        /// The margin around the tiles in this tileset.
        /// </summary>
        public int Margin { get; set; }
        /// <summary>
        /// Offset in pixels for this tileset.
        /// </summary>
        public TileOffset TileOffset { set; get; }
        /// <summary>
        /// An image used as a tileset. Not the same as the image at Source.
        /// </summary>
        public Image Image { set; get; }
        /// <summary>
        /// Custom properties for the tileset object.
        /// </summary>
        public Properties Props { set; get; }
        /// <summary>
        /// Terrain types in this tileset.
        /// </summary>
        public TerrainTypes TerrainTypes { set; get; }
        /// <summary>
        /// List of all tiles in this tileset.
        /// </summary>
        public List<Tile> Tiles { set; get; }

        /// <summary>
        /// Constructor for the Tileset class.
        /// </summary>
        public Tileset()
        {
            Image = new Image();
            Props = new Properties();
            Tiles = new List<Tile>();
        }

        /// <summary>
        /// Constructor for the Tileset class.
        /// </summary>
        /// <param name="firstGid"></param>
        /// <param name="name"></param>
        /// <param name="tileWidth"></param>
        /// <param name="tileHeight"></param>
        /// <param name="spacing"></param>
        /// <param name="margin"></param>
        public Tileset(int firstGid, string name, int tileWidth, int tileHeight, int spacing, int margin)
        {
            Firstgid = firstGid;
            Name = name;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            Spacing = spacing;
            Margin = margin;
            Image = new Image();
            Props = new Properties();
            Tiles = new List<Tile>();
        }

        /// <summary>
        /// Finds and returns the Tile with the given local id
        /// (its Id within its Tileset; chances are you will never need this).
        /// </summary>
        /// <param name="id">The local id of the tile to search for</param>
        /// <returns>The tile with the given local id</returns>
        public Tile GetTileByLocalId(int id)
        {
            foreach (Tile t in Tiles)
            {
                if (t.Id == id)
                {
                    return t;
                }
            }
            return null;
        }

        /// <summary>
        /// Finds and returns the Tile with the given global id.
        /// </summary>
        /// <param name="gid">The global id of the tile to search for</param>
        /// <returns>The tile with the given global id</returns>
        public Tile GetTile(int gid)
        {
            foreach (Tile t in Tiles)
            {
                if (t.Gid == gid)
                {
                    return t;
                }
            }
            return null;
        }

        /// <summary>
        /// Finds and returns all Tiles with the given property name.
        /// </summary>
        /// <param name="propertyName">The name of the property to search for</param>
        /// <returns>The tiles with the given property</returns>
        public List<Tile> GetTilesWithProperty(string propertyName)
        {
            List<Tile> tilesWithProperty = new List<Tile>();

            foreach (Tile t in Tiles)
            {
                if (t.Props.GetValue(propertyName) != null)
                {
                    tilesWithProperty.Add(t);
                }
            }

            if (tilesWithProperty.Count > 0)
            {
                return tilesWithProperty;
            }
            else
            {
                return null;
            }
        }
    }
}
