using Microsoft.Xna.Framework;
using System;

namespace MapImporter
{
    /// <summary>
    /// An individual Tile object that contains the
    /// location of a tile inside a Tileset object.
    /// <see cref="MapImporter.Tileset"/>
    /// </summary>
    public class Tile
    {
        /// <summary>
        /// The local tile id that identifys this Tile inside of its own tileset.
        /// <remarks>While this property is accessible, it would be inefficient to reference
        /// an individual Tile by its local id.</remarks>
        /// </summary>
        public int Id { set; get; }
        /// <summary>
        /// The global tile id of this Tile object. Every Tile has a unique id property
        /// to identify it across all tilesets.
        /// </summary>
        public int Gid { set; get; }
        /// <summary>
        /// Defines the terrain type of each corner of the tile, given as
        /// comma-separated indexes in the terrain types array in the order top-left,
        /// top-right, bottom-left, bottom-right. Leaving out a value means that corner has no terrain.
        /// (optional) (since 0.9.0)
        /// </summary>
        public Terrain Terrain { set; get; }
        /// <summary>
        /// A percentage indicating the probability that this tile is chosen when it competes with others
        /// while editing with the terrain tool. (optional) (since 0.9.0)
        /// </summary>
        public int Probability { set; get; }
        /// <summary>
        /// Custom properties for the tile object.
        /// <see cref="MapImporter.Properties"/>
        /// </summary>
        public Properties Props { set; get; }
        /// <summary>
        /// The location of this individual tile in the tileset.
        /// </summary>
        public Rectangle Location { set; get; }
        /// <summary>
        /// Object group this tile is part of (optional).
        /// <see cref="MapImporter.ObjectGroup"/>
        /// </summary>
        public ObjectGroup ObjectGroup { set; get; }

        /// <summary>
        /// Constructor for the Tile class.
        /// </summary>
        public Tile()
        {
        }

        /// <summary>
        /// Constructor for the Tile class.
        /// </summary>
        public Tile(int id, int gid, Rectangle location)
        {
            Id = id;
            Gid = gid;
            Location = location;
        }

        /// <summary>
        /// The global id property for Tiles is used to compare
        /// Tiles for greater than and less than operations.
        /// </summary>
        /// <param name="var1">The first Tile object to compare.</param>
        /// <param name="var2">The second Tile object to compare.</param>
        /// <returns>Whether the first Tile has a lower global id than the second Tile.</returns>
        public static bool operator <(Tile var1, Tile var2)
        {
            if (var1.Gid < var2.Gid)
                return true;
            else
                return false;
        }

        /// <summary>
        /// The global id property for Tiles is used to compare
        /// Tiles for greater than and less than operations.
        /// </summary>
        /// <param name="var1">The first Tile object to compare.</param>
        /// <param name="var2">The second Tile object to compare.</param>
        /// <returns>Whether the first Tile has a lower global id than the second Tile.</returns>
        public static bool operator >(Tile var1, Tile var2)
        {
            if (var1.Gid > var2.Gid)
                return true;
            else
                return false;
        }

        /// <summary>
        /// The global id property for Tiles is used to compare
        /// Tiles for greater than and less than operations.
        /// </summary>
        /// <param name="var1">The first Tile object to compare.</param>
        /// <param name="var2">The second Tile object to compare.</param>
        /// <returns>Whether the first Tile has a higher global id
        /// than the second Tile or whether they are equal.</returns>
        public static bool operator <=(Tile var1, Tile var2)
        {
            if ((var1.Gid < var2.Gid) || (var1.Gid == var2.Gid))
                return true;
            else
                return false;
        }

        /// <summary>
        /// The global id property for Tiles is used to compare
        /// Tiles for greater than and less than operations.
        /// </summary>
        /// <param name="var1">The first Tile object to compare.</param>
        /// <param name="var2">The second Tile object to compare.</param>
        /// <returns>Whether the first Tile has a lower global id
        /// than the second Tile or whether they are equal.</returns>
        public static bool operator >=(Tile var1, Tile var2)
        {
            if ((var1.Gid > var2.Gid) || (var1.Gid == var2.Gid))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Overrided Equals method for Tile objects.
        /// </summary>
        /// <param name="var">The Tile object to compare.</param>
        /// <returns>Whether the Tiles are equal.</returns>
        public bool Equals(Tile var)
        {
            if (var.Gid == Gid)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Overrided ToString method for Tile objects.
        /// </summary>
        /// <returns>The string representation of Tileset objects.</returns>
        public override string ToString()
        {
            return String.Format("(Global id: {0})", Gid);
        }
    }
}
