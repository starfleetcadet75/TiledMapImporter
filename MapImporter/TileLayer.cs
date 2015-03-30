using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapImporter
{
    /// <summary>
    /// A single layer of tiles in the map.
    /// </summary>
    public class TileLayer
    {
        /// <summary>
        /// The name of the layer.
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// The x coordinate of the layer in tiles.
        /// Defaults to 0 and can no longer be changed in Tiled Qt.
        /// </summary>
        public int X { set; get; }
        /// <summary>
        /// The y coordinate of the layer in tiles.
        /// Defaults to 0 and can no longer be changed in Tiled Qt.
        /// </summary>
        public int Y { set; get; }
        /// <summary>
        /// The width of the layer in tiles. Traditionally required,
        /// but as of Tiled Qt always the same as the map width.
        /// </summary>
        public int Width { set; get; }
        /// <summary>
        /// The height of the layer in tiles. Traditionally required,
        /// but as of Tiled Qt always the same as the map width.
        /// </summary>
        public int Height { set; get; }
        /// <summary>
        /// The opacity of the layer as a value from 0 to 1. Defaults to 1.
        /// </summary>
        public float Opacity { set; get; }
        /// <summary>
        /// Whether the layer is shown (1) or hidden (0). Defaults to 1.
        /// </summary>
        public bool Visible { set; get; }
        /// <summary>
        /// Contains the global ids for the layer.
        /// </summary>
        public Data Data { set; get; }
        /// <summary>
        /// Custom properties for the layer object.
        /// </summary>
        public Properties Props { set; get; }

        /// <summary>
        /// Constructor for TileLayer class.
        /// </summary>
        public TileLayer()
        {
        }

        /// <summary>
        /// Constructor for TileLayer class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="opacity"></param>
        public TileLayer(string name, int x, int y, int width, int height, float opacity)
        {
            Name = name;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Opacity = opacity;
        }
    }
}
