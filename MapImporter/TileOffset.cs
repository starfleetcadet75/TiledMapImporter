
namespace MapImporter
{
    /// <summary>
    /// This element is used to specify an offset in pixels, to be applied
    /// when drawing a tile from the related tileset. When not present,
    /// no offset is applied.
    /// </summary>
    public class TileOffset
    {
        /// <summary>
        /// Horizontal offset in pixels.
        /// </summary>
        public int X { set; get; }
        /// <summary>
        /// Vertical offset in pixels (positive is down).
        /// </summary>
        public int Y { set; get; }

        /// <summary>
        /// Constructor for the Tile class.
        /// </summary>
        public TileOffset()
        {
        }

        /// <summary>
        /// Constructor for the Tile class.
        /// </summary>
        public TileOffset(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
