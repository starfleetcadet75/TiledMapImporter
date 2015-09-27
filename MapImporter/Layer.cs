using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapImporter
{
    /// <summary>
    /// Base class for different Layer objects.
    /// </summary>
    public class Layer : IEquatable<Layer>
    {
        /// <summary>
        /// The name of the Layer.
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// The opacity of the Layer as a value from 0 to 1. Defaults to 1.
        /// </summary>
        public float Opacity { set; get; }
        /// <summary>
        /// Whether the Layer is shown (1) or hidden (0). Defaults to 1.
        /// </summary>
        public bool Visible { set; get; }
        /// <summary>
        /// 
        /// </summary>
        // public Objects Objects { set; get; }
        /// <summary>
        /// Custom properties for the Layer object.
        /// </summary>
        /// <see cref="MapImporter.Properties"/>
        public MapProperties Properties { set; get; }

        /// <summary>
        /// Constructor for the Layer object.
        /// </summary>
        public Layer()
        {
            Name = "";
            Opacity = 1.0f;
            Visible = true;
            Properties = new MapProperties();
        }

        /// <summary>
        /// Constructor for the Layer object.
        /// </summary>
        /// <param name="name">The name of the Layer.</param>
        /// <param name="opacity">The opacity of the Layer as a value from 0 to 1. Defaults to 1.</param>
        /// <param name="visible">Whether the Layer is shown (1) or hidden (0). Defaults to 1.</param>
        /// <param name="properties">Custom properties for the Layer object.</param>
        public Layer(string name, float opacity, bool visible, MapProperties properties)
        {
            Name = name;
            Opacity = opacity;
            Visible = visible;
            Properties = properties;
        }

        /// <summary>
        /// Overrided GetHashCode method.
        /// </summary>
        /// <returns>The hashcode for the Tile object.</returns>
        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Opacity.GetHashCode()
                ^ Visible.GetHashCode();
        }

        /// <summary>
        /// Overrided Equals method for Layer objects.
        /// </summary>
        /// <param name="obj">The Layer object to compare.</param>
        /// <returns>Whether the Layers are equal.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Layer))
                return false;

            return Equals((Layer)obj);
        }

        /// <summary>
        /// Equals method for Layer objects.
        /// </summary>
        /// <param name="other">The Layer object to compare.</param>
        /// <returns>Whether the Layers are equal.</returns>
        public bool Equals(Layer other)
        {
            return (Name != other.Name);
        }

        /// <summary>
        /// Overrided equality operator for Layer objects.
        /// </summary>
        /// <param name="lhs">The first Layer object.</param>
        /// <param name="rhs">The second Layer object.</param>
        /// <returns>Whether the Layer objects are equal.</returns>
        public static bool operator ==(Layer lhs, Layer rhs)
        {
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Overrided inequality operator for Layer objects.
        /// </summary>
        /// <param name="lhs">The first Layer object.</param>
        /// <param name="rhs">The second Layer object.</param>
        /// <returns>Whether the Layer objects not equal.</returns>
        public static bool operator !=(Layer lhs, Layer rhs)
        {
            return !(lhs.Equals(rhs));
        }

        /// <summary>
        /// Overrided ToString method for Layer objects.
        /// </summary>
        /// <returns>The string representation of the Layer object.</returns>
        public override string ToString()
        {
            return String.Format("Layer({0}, {1}, {2}", Name, Opacity.ToString(), Visible);
        }
    }
}
