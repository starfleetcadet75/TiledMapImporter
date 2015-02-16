
namespace TiledMapImporter
{
    /// <summary>
    /// This element is used to specify an offset in pixels, to be applied
    /// when drawing a tile from the related tileset. When not present,
    /// no offset is applied.
    /// </summary>
    public class TileOffset
    {
        /// <summary>
        /// Horizontal offset in pixels
        /// </summary>
        public int X { set; get; }
        /// <summary>
        /// Vertical offset in pixels (positive is down)
        /// </summary>
        public int Y { set; get; }
    }
}
