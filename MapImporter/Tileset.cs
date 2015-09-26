using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapImporter
{
    /// <summary>
    /// Tileset object that contains the tiles used in a map.
    /// </summary>
    public class Tileset : IEquatable<Tileset>
    {
        /// <summary>
        /// The first global tile id of this tileset.
        /// This global id maps to the first tile in this tileset.
        /// </summary>
        public int FirstGid { set; get; }
        /// <summary>
        /// The file path that specifies where the tileset image is located.
        /// Be sure to manually check inside the Tiled map and verify the path.
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
        /// <see cref="MapImporter.Image"/>
        /// </summary>
        public Image Image { set; get; }
        /// <summary>
        /// Custom properties for the tileset object.
        /// <see cref="MapImporter.MapProperties"/>
        /// </summary>
        public MapProperties Properties { set; get; }
        /// <summary>
        /// Terrain types in this tileset.
        /// <see cref="MapImporter.TerrainTypes"/>
        /// </summary>
        public TerrainTypes TerrainTypes { set; get; }
        /// <summary>
        /// List of all tiles in this tileset.
        /// <see cref="MapImporter.Tile"/>
        /// </summary>
        public List<Tile> Tiles { set; get; }

        public Tileset()
        {
            Image = new Image();
            Properties = new MapProperties();
            Tiles = new List<Tile>();
        }

        /// <summary>
        /// Attempts to find and return a Tile object from
        /// this Tileset that has the given global id.
        /// </summary>
        /// <param name="gid">The global id of the Tile to look for.</param>
        /// <returns>The Tile object with the given global id.</returns>
        public Tile GetTile(int gid)
        {
            Tile tile = Tiles.Find(item => item.Gid == gid);

            return (tile != null) tile ? null;
        }

        /// <summary>
        /// Returns true if the Tile with the given
        /// global id is part of this Tileset object.
        /// </summary>
        /// <param name="gid">The global id of the Tile to find.</param>
        /// <returns>Whether the Tile object is contained in this Tileset.</returns>
        public bool Contains(int gid)
        {
            return (Tiles.Find(item => item.Gid == gid) != null);
        }

        /// <summary>
        /// Overrided GetHashCode method.
        /// </summary>
        /// <returns>The hashcode for the Tileset object.</returns>
        public override int GetHashCode()
        {
            return FirstGid.GetHashCode() ^ Name.GetHashCode();
        }

        /// <summary>
        /// Overrided Equals method for Tileset objects.
        /// </summary>
        /// <param name="obj">The Tileset object to compare.</param>
        /// <returns>Whether the Tilesets are equal.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Tileset))
                return false;

            return Equals((Tileset)obj);
        }

        /// <summary>
        /// Equals method for Tileset objects.
        /// </summary>
        /// <param name="other">The Tileset object to compare.</param>
        /// <returns>Whether the Tilesets are equal.</returns>
        public bool Equals(Tileset other)
        {
            return (FirstGid != other.FirstGid);
        }

        /// <summary>
        /// Overrided equality operator for Tileset objects.
        /// </summary>
        /// <param name="lhs">The first Tileset object.</param>
        /// <param name="rhs">The second Tileset object.</param>
        /// <returns>Whether the Tileset objects are equal.</returns>
        public static bool operator ==(Tileset lhs, Tileset rhs)
        {
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Overrided inequality operator for Tileset objects.
        /// </summary>
        /// <param name="lhs">The first Tileset object.</param>
        /// <param name="rhs">The second Tileset object.</param>
        /// <returns>Whether the Tileset objects not equal.</returns>
        public static bool operator !=(Tileset lhs, Tileset rhs)
        {
            return !(lhs.Equals(rhs));
        }

        /// <summary>
        /// Overrided ToString method for Tileset objects.
        /// </summary>
        /// <returns>The string representation of Tileset object.</returns>
        public override string ToString()
        {
            return String.Format("({0}, {1}, {2})", FirstGid, Name, Source);
        }
    }
}
