using Microsoft.Xna.Framework;

namespace MapImporter
{

    /// <summary>
    /// An individual tile object
    /// </summary>
    public class Tile
    {
        /// <summary>
        /// The local tile Id within its tileset
        /// </summary>
        public int Id { set; get; }
        /// <summary>
        /// The global tile Id
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
        /// Custom properties for the tile object
        /// </summary>
        public Properties Props { set; get; }
        /// <summary>
        /// The location of this individual tile in the tileset
        /// </summary>
        public Rectangle Location { set; get; }
        /// <summary>
        /// Object group this tile is part of
        /// </summary>
        public ObjectGroup ObjectGroup { set; get; }
    }
}
