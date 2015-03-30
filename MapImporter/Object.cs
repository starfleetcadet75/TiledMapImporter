
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace MapImporter
{
    /// <summary>
    /// Defines the possible types that an Object can be.
    /// Objects have the same properties and ObjectType
    /// allows the program determine how to treat them.
    /// </summary>
    public enum ObjectType
    {
        /// <summary>
        /// The Object is a Rectangle.
        /// </summary>
        Rectangle,
        /// <summary>
        /// The Object is a Polygon.
        /// </summary>
        Polygon,
        /// <summary>
        /// The Object is an Ellipse.
        /// </summary>
        Ellipse,
        /// <summary>
        /// The Object is a Polyline.
        /// </summary>
        Polyline,
        /// <summary>
        /// The Object is an Image.
        /// </summary>
        Image
    }

    /// <summary>
    /// While tile layers are very suitable for anything repetitive aligned to the tile grid,
    /// sometimes you want to annotate your map with other information, not necessarily aligned to the grid.
    /// Hence the objects have their coordinates and size in pixels, but you can still easily align that to the grid when you want to.
    /// You generally use objects to add custom information to your tile map, such as spawn points, warps, exits, etc.
    /// When the object has a gid set, then it is represented by the image of the tile with that global ID.
    /// Currently that means width and height are ignored for such objects. The image alignment currently depends on the map orientation.
    /// In orthogonal orientation it's aligned to the bottom-left while in isometric it's aligned to the bottom-center.
    /// If IsEllipse is true, then the object is an Ellipse.
    /// If Polygon is not null, the object is a Polygon.
    /// If Polyline is not null, then the object is a Polyline.
    /// If Image is not null, the object is an Image.
    /// If all of these are null, then the object is a Rectangle.
    /// </summary>
    public class Object
    {
        /// <summary>
        /// Identifies the type of the object.
        /// </summary>
        public ObjectType ObjType { set; get; }
        /// <summary>
        /// The name of the object. An arbitrary string.
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// The type of the object. An arbitrary string.
        /// </summary>
        public string Type { set; get; }
        /// <summary>
        /// The Id of the object.
        /// </summary>
        public int Id { set; get; }
        /// <summary>
        /// The x coordinate of the object in pixels.
        /// </summary>
        public double X { set; get; }
        /// <summary>
        /// The y coordinate of the object in pixels.
        /// </summary>
        public double Y { set; get; }
        /// <summary>
        /// The width of the object in pixels (defaults to 0).
        /// </summary>
        public double Width { set; get; }
        /// <summary>
        /// The height of the object in pixels (defaults to 0).
        /// </summary>
        public double Height { set; get; }
        /// <summary>
        /// The rotation of the object in degrees clockwise (defaults to 0).
        /// </summary>
        public double Rotation { set; get; }
        /// <summary>
        /// A reference to a tile (optional).
        /// </summary>
        public int Gid { set; get; }
        /// <summary>
        /// Whether the object is shown (1) or hidden (0). Defaults to 1. (since 0.9.0)
        /// </summary>
        public bool Visible { set; get; }
        /// <summary>
        /// Custom properties for this object.
        /// </summary>
        public Properties Props { set; get; }
        /// <summary>
        /// A Polygon object.
        /// </summary>
        public Polygon Polygon { set; get; }
        /// <summary>
        /// A Polyline object.
        /// </summary>
        public Polyline Polyline { set; get; }
        /// <summary>
        /// An image object.
        /// </summary>
        public Image Image { set; get; }

        /// <summary>
        /// Constructor for the Object class.
        /// </summary>
        public Object()
        {
            Props = new Properties();
        }

        /// <summary>
        /// Constructor for the Object class.
        /// </summary>
        public Object(string name, int id, double width, double height, double x, double y,  string type, double rotation)
        {
            Name = name;
            Type = type;
            Width = width;
            Height = height;
            Id = id;
            Rotation = rotation;
            X = x;
            Y = y;
            Props = new Properties();
        }

        /// <summary>
        /// Constructor for the Object class.
        /// </summary>
        public Object(string name, int id, double width, double height, double x, double y,  string type, double rotation, Properties props)
        {
            Name = name;
            Type = type;
            Width = width;
            Height = height;
            Id = id;
            Rotation = rotation;
            X = x;
            Y = y;
            Props = props;
        }

        /// <summary>
        /// Draws the Object to the screen.
        /// </summary>
        /// <param name="spriteBatch">A spritebatch object for drawing</param>
        /// <param name="location">The location to draw the layers</param>
        /// <param name="startIndex">The i and j indices of the first tile to draw</param>
        /// <param name="color">The color to be used when drawing the Object.</param>
        /// <param name="opacity">The opacity or transparency of the Object. Determined by its ObjectGroup.</param>
        public void Draw(SpriteBatch spriteBatch, Rectangle location, Vector2 startIndex, Color color, float opacity)
        {
            Texture2D pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { color });

            switch (ObjType)
            {
                case ObjectType.Rectangle:
                    DrawRectangle(spriteBatch, location, startIndex, pixel, color, opacity);
                    break;
                case ObjectType.Polygon:
                    break;
                case ObjectType.Polyline:
                    break;
                case ObjectType.Ellipse:
                    break;
                case ObjectType.Image:
                    break;
            }
        }

        private void DrawRectangle(SpriteBatch spriteBatch, Rectangle location, Vector2 startIndex, Texture2D pixel, Color color, float opacity)
        {
            spriteBatch.Draw(pixel, new Rectangle((int)X, (int)Y, (int)Width, (int)Height), color * opacity);
        }

        private void DrawPolygon(SpriteBatch spriteBatch, Rectangle location, Vector2 startIndex, Color color, float opacity)
        {
        }

        private void DrawPolyline(SpriteBatch spriteBatch, Rectangle location, Vector2 startIndex, Color color, float opacity)
        {
        }

        private void DrawEllipse(SpriteBatch spriteBatch, Rectangle location, Vector2 startIndex, Color color, float opacity)
        {
        }

        private void DrawImage(SpriteBatch spriteBatch, Rectangle location, Vector2 startIndex, Color color, float opacity)
        {
        }
    }
}
