using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapImporter
{
    public class Layer
    {
        /// <summary>
        /// The name of the layer.
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// The opacity of the layer as a value from 0 to 1. Defaults to 1.
        /// </summary>
        public float Opacity { set; get; }
        /// <summary>
        /// Whether the layer is shown (1) or hidden (0). Defaults to 1.
        /// </summary>
        public bool IsVisible { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public Objects objects { set; get; }
        /// <summary>
        /// Custom properties for the layer object.
        /// </summary>
        /// <see cref="MapImporter.Properties"/>
        public MapProperties properties { set; get; }

        // TODO: Constructors
    }
}
