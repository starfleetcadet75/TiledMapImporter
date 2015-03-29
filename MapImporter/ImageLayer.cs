
namespace MapImporter
{
    /// <summary>
    /// A layer consisting of a single image.
    /// </summary>
    public class ImageLayer
    {
        /// <summary>
        /// The name of the image layer.
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// The x position of the image layer in pixels.
        /// </summary>
        public int X { set; get; }
        /// <summary>
        /// The y position of the image layer in pixels.
        /// </summary>
        public int Y { set; get; }
        /// <summary>
        /// The width of the image layer in tiles. Meaningless.
        /// </summary>
        public int Width { set; get; }
        /// <summary>
        /// The height of the image layer in tiles. Meaningless.
        /// </summary>
        public int Height { set; get; }
        /// <summary>
        /// The opacity of the layer as a value from 0 to 1. Defaults to 1.
        /// </summary>
        public int Opacity { set; get; }
        /// <summary>
        /// Whether the layer is shown (1) or hidden (0). Defaults to 1.
        /// </summary>
        public bool Visible { set; get; }
        /// <summary>
        /// The image used in the layer.
        /// </summary>
        public Image Image { set; get; }
        /// <summary>
        /// Custom properties for the image layer.
        /// </summary>
        public Properties Props { set; get; }

        /// <summary>
        /// Constructor for an ImageLayer object.
        /// </summary>
        public ImageLayer()
        {
            Props = new Properties();
        }
    }
}
