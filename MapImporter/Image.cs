using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapImporter
{
    /// <summary>
    /// An image object that can be used like a tileset
    /// </summary>
    public class Image
    {
        /// <summary>
        /// Used for embedded images, in combination with a data child element.
        /// Valid values are file extensions like png, gif, jpg, bmp, etc. (since 0.9.0)
        /// </summary>
        public string Format { set; get; }
        /// <summary>
        /// The reference to the tileset image file (Tiled supports most common image formats).
        /// </summary>
        public string Source { set; get; }
        /// <summary>
        /// Defines a specific color that is treated as transparent (example value: "#FF00FF" for magenta).
        /// Up until Tiled 0.10, this value is written out without a # but this is planned to change.
        /// </summary>
        public Color Trans { set; get; }
        /// <summary>
        /// The image width in pixels (optional, used for tile index
        /// correction when the image changes)
        /// </summary>
        public int Width { set; get; }
        /// <summary>
        /// The image height in pixels (optional)
        /// </summary>
        public int Height { set; get; }
        /// <summary>
        /// Specifically for use by the Xna Framework. The image must be loaded
        /// from the Source path using the content pipeline.
        /// </summary>
        public Texture2D Texture { set; get; }
    }
}
