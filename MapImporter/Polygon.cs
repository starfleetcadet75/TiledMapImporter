using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MapImporter
{
    /// <summary>
    /// Each polygon object is made up of a space-delimited list of x,y coordinates.
    /// The origin for these coordinates is the location of the parent object. By default,
    /// the first point is created as 0,0 denoting that the point will originate exactly
    /// where the object is placed.
    /// </summary>
    public class Polygon
    {
        /// <summary>
        /// List of points as Vector2 objects that make up the polygon.
        /// </summary>
        public List<Vector2> Points { set; get; }

        /// <summary>
        /// Constructor for a Polygon object.
        /// </summary>
        public Polygon()
        {
        }

        /// <summary>
        /// Constructor for a Polygon object.
        /// </summary>
        /// <param name="points"></param>
        public Polygon(List<Vector2> points)
        {
            Points = points;
        }
    }
}
