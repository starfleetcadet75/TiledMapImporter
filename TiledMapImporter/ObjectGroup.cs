
namespace TiledMapImporter
{
    /// <summary>
    /// The object group is in fact a map layer, and is hence called
    /// "object layer" in Tiled Qt.
    /// </summary>
    public class ObjectGroup
    {
        /// <summary>
        /// The name of the object group
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// The color used to display the objects in this group
        /// </summary>
        public string Color { set; get; }
        /// <summary>
        /// The x coordinate of the object group in tiles.
        /// Defaults to 0 and can no longer be changed in Tiled Qt.
        /// </summary>
        public int X { set; get; }
        /// <summary>
        /// The y coordinate of the object group in tiles.
        /// Defaults to 0 and can no longer be changed in Tiled Qt.
        /// </summary>
        public int Y { set; get; }
        /// <summary>
        /// The width of the object group in tiles. Meaningless.
        /// </summary>
        public int Width { set; get; }
        /// <summary>
        /// The height of the object group in tiles. Meaningless.
        /// </summary>
        public int Height { set; get; }
        /// <summary>
        /// The opacity of the layer as a value from 0 to 1.
        /// Defaults to 1.
        /// </summary>
        public int Opacity { set; get; }
        /// <summary>
        /// Whether the layer is shown (1) or hidden (0). Defaults to 1.
        /// </summary>
        public bool Visible { set; get; }
        /// <summary>
        /// Objects contained in this object group
        /// </summary>
        public Object[] Objects { set; get; }
        /// <summary>
        /// Custom properties for the object group
        /// </summary>
        public Properties Props { set; get; }
    }
}
