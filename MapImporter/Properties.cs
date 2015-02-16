using System.Collections.Generic;

namespace MapImporter
{
    /// <summary>
    /// Wraps any number of custom properties. Can be used as a child of the map,
    /// tile (when part of a tileset), layer, objectgroup and object elements.
    /// </summary>
    public class Properties
    {
        /// <summary>
        /// Dictionary of individual properties inside the properties class
        /// </summary>
        public Dictionary<string, string> PropertiesList;

        /// <summary>
        /// Constructor for Properties class
        /// </summary>
        public Properties()
        {
            PropertiesList = new Dictionary<string, string>();
        }

        /// <summary>
        /// Finds the value with the specified name and returns it 
        /// </summary>
        /// <param name="name">The name of the property</param>
        public string GetValue(string name)
        {
            if (PropertiesList.ContainsKey(name))
            {
                return PropertiesList[name];
            }
            return null;
        }
    }
}
