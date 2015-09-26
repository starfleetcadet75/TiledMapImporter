namespace MapImporter
{
    /// <summary>
    /// This element is used to specify an offset in pixels, to be applied
    /// when drawing a tile from the related tileset. When not present,
    /// no offset is applied.
    /// </summary>
    /// <see cref="MapImporter.Tileset"/>
    public class TileOffset
    {
        /// <summary>
        /// The horizontal offset in pixels.
        /// </summary>
        public int X { set; get; }
        /// <summary>
        /// The vertical offset in pixels (positive is down).
        /// </summary>
        public int Y { set; get; }

        /// <summary>
        /// Constructor for the TileOffset class.
        /// </summary>
        public TileOffset()
        {
            X = 0;
            Y = 0;
        }

        /// <summary>
        /// Constructor for the TileOffset class.
        /// </summary>
        /// <param name="x">The horizontal offset in pixels.</param>
        /// <param name="y">The vertical offset in pixels.</param>
        public TileOffset(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}