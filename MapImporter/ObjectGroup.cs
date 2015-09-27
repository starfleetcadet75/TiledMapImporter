using MapImporter.Enums;
using Microsoft.Xna.Framework;
using System;

namespace MapImporter
{
    /// <summary>
    /// The object group is in fact a map layer, and is hence called
    /// "object layer" in Tiled Qt.
    /// </summary>
    /// <see cref="MapImporter.TileLayer"/>
    /// <see cref="MapImporter.ImageLayer"/>
    public class ObjectGroup : Layer
    {
        /// <summary>
        /// The draw order for the ObjectGroup.
        /// </summary>
        public DrawOrder DrawOrder { set; get; }
        /// <summary>
        /// The color used to display the objects in this group.
        /// </summary>
        public Color Color { set; get; }
        /// <summary>
        /// The x coordinate of the ObjectGroup in tiles.
        /// Defaults to 0 and can no longer be changed in Tiled Qt.
        /// </summary>
        public int X { set; get; }
        /// <summary>
        /// The y coordinate of the ObjectGroup in tiles.
        /// Defaults to 0 and can no longer be changed in Tiled Qt.
        /// </summary>
        public int Y { set; get; }
        /// <summary>
        /// The width of the ObjectGroup in tiles. Meaningless.
        /// </summary>
        public int Width { set; get; }
        /// <summary>
        /// The height of the ObjectGroup in tiles. Meaningless.
        /// </summary>
        public int Height { set; get; }
        /// <summary>
        /// Objects contained in this ObjectGroup.
        /// </summary>
        //public List<Object> Objects { set; get; }

        /// <summary>
        /// Constructor for the ObjectGroup object.
        /// </summary>
        /// <param name="name">The name of the Layer.</param>
        /// <param name="opacity">The opacity of the Layer as a value from 0 to 1. Defaults to 1.</param>
        /// <param name="visible">Whether the Layer is shown (1) or hidden (0). Defaults to 1.</param>
        /// <param name="properties">Custom properties for the Layer object.</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public ObjectGroup(string name, float opacity, bool visible, MapProperties properties, int x, int y, int width, int height)
            : base(name, opacity, visible, properties)
        {

        }


        /// <summary>
        /// Overrided ToString method for ObjectGroup objects.
        /// </summary>
        /// <returns>The string representation of the ObjectGroup object.</returns>
        public override string ToString()
        {
            return base.ToString() + String.Format(", {0}, {1}, {2})", Name, Opacity.ToString(), Visible);
        }
    }
}
