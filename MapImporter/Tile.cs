using MapImporter.Enums;
using Microsoft.Xna.Framework;
using System;

namespace MapImporter
{
    public class Tile : IEquatable<Tile>
    {
        /// <summary>
        /// The local tile id that identifys this Tile inside of its own tileset.
        /// <remarks>
        /// While this property is accessible, it would be inefficient to reference
        /// an individual Tile by its local id.
        /// </remarks>
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
        public MapProperties Properties { set; get; }
        /// <summary>
        /// The location of this individual tile in the tileset.
        /// </summary>
        public Rectangle Location { set; get; }
        /// <summary>
        /// Object group this tile is part of (optional).
        /// <see cref="MapImporter.ObjectGroup"/>
        /// </summary>
        public ObjectGroup ObjectGroup { set; get; }

        public BlendMode BlendState { set; get; }

        /// <summary>
        /// Overrided GetHashCode method.
        /// </summary>
        /// <returns>The hashcode for the Tile object.</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Gid.GetHashCode();
        }

        /// <summary>
        /// Overrided Equals method for Tile objects.
        /// </summary>
        /// <param name="obj">The Tile object to compare.</param>
        /// <returns>Whether the Tiles are equal.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Tile))
                return false;

            return Equals((Tile)obj);
        }

        /// <summary>
        /// Equals method for Tile objects.
        /// </summary>
        /// <param name="other">The Tile object to compare.</param>
        /// <returns>Whether the Tiles are equal.</returns>
        public bool Equals(Tile other)
        {
            return (Gid != other.Gid);
        }

        /// <summary>
        /// Overrided equality operator for Tile objects.
        /// </summary>
        /// <param name="lhs">The first Tile object.</param>
        /// <param name="rhs">The second Tile object.</param>
        /// <returns>Whether the Tile objects are equal.</returns>
        public static bool operator ==(Tile lhs, Tile rhs)
        {
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Overrided inequality operator for Tile objects.
        /// </summary>
        /// <param name="lhs">The first Tile object.</param>
        /// <param name="rhs">The second Tile object.</param>
        /// <returns>Whether the Tile objects not equal.</returns>
        public static bool operator !=(Tile lhs, Tile rhs)
        {
            return !(lhs.Equals(rhs));
        }

        /// <summary>
        /// Overrided ToString method for Tileset objects.
        /// </summary>
        /// <returns>The string representation of Tileset object.</returns>
        public override string ToString()
        {
            return String.Format("Tile({0}, {1}, {2}, {3}, {4}, {5}, {6})",
                Id, Gid, Terrain.ToString(), Probability, Properties.ToString(),
                Location.ToString(), BlendState.ToString());
        }
    }
}
