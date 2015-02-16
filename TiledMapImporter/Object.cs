
namespace TiledMapImporter
{
    /// <summary>
    /// While tile layers are very suitable for anything repetitive aligned to the tile grid,
    /// sometimes you want to annotate your map with other information, not necessarily aligned to the grid.
    /// Hence the objects have their coordinates and size in pixels, but you can still easily align that to the grid when you want to.
    /// You generally use objects to add custom information to your tile map, such as spawn points, warps, exits, etc.
    /// When the object has a gid set, then it is represented by the image of the tile with that global ID.
    /// Currently that means width and height are ignored for such objects. The image alignment currently depends on the map orientation.
    /// In orthogonal orientation it's aligned to the bottom-left while in isometric it's aligned to the bottom-center.
    /// </summary>
    public class Object
    {
        /// <summary>
        /// The name of the object. An arbitrary string.
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// The type of the object. An arbitrary string.
        /// </summary>
        public string Type { set; get; }
        /// <summary>
        /// The x coordinate of the object in pixels.
        /// </summary>
        public int X { set; get; }
        /// <summary>
        /// The y coordinate of the object in pixels.
        /// </summary>
        public int Y { set; get; }
        /// <summary>
        /// The width of the object in pixels (defaults to 0).
        /// </summary>
        public int Width { set; get; }
        /// <summary>
        /// The height of the object in pixels (defaults to 0).
        /// </summary>
        public int Height { set; get; }
        /// <summary>
        /// The rotation of the object in degrees clockwise (defaults to 0).
        /// </summary>
        public int Rotation { set; get; }
        /// <summary>
        /// An reference to a tile (optional).
        /// </summary>
        public int Gid { set; get; }
        /// <summary>
        /// Whether the object is shown (1) or hidden (0). Defaults to 1. (since 0.9.0)
        /// </summary>
        public bool Visible { set; get; }

        //Can contain properties
        //ellipse
        //polygon
        //polyline
        //image
    }
}
