using System.Collections.Generic;

namespace MapImporter
{
    public class MapProperties
    {
        private Dictionary<string, object> properties;

        public MapProperties()
        {
            properties = new Dictionary<string, object>();
        }

        public MapProperties(Dictionary<string, object> properties)
        {
            this.properties = properties;
        }

        public bool ContainsKey(string key)
        {
            return properties.ContainsKey(key);
        }

        public object GetValue(string key)
        {
            return properties[key];
        }

        public object GetValue(string key, object defaultValue)
        {
            object value = properties[key];
            if (value != null)
            {
                return value;
            }
            else
            {
                return defaultValue;
            }
        }

        public void AddProperty(string key, object value)
        {
            properties.Add(key, value);
        }

        public void Remove(string key)
        {
            properties.Remove(key);
        }

        public void Clear()
        {
            properties.Clear();
        }

        public int Count()
        {
            return properties.Count;
        }
    }
}
