using System;
using System.Collections;
using System.Collections.Generic;

namespace MapImporter
{
    public class Layers : IEnumerable<Layer>
    {
        private List<Layer> layers;

        public Layers()
        {
            layers = new List<Layer>();
        }

        public void AddLayer(Layer layer)
        {
            layers.Add(layer);
        }

        public Layer GetLayer(int index)
        {
            return layers[index];
        }

        public Layer GetLayer(string name)
        {
            foreach (Layer layer in layers)
            {
                if (layer.Name.Equals(name))
                {
                    return layer;
                }
            }
            return null;
        }

        public int GetIndex(Layer layer)
        {
            return layers.IndexOf(layer);
        }

        public int GetIndex(string name)
        {
            return GetIndex(GetLayer(name));
        }

        public void Remove(Layer layer)
        {
            layers.Remove(layer);
        }

        public void Remove(string name)
        {
            layers.Remove(GetLayer(name));
        }

        public void Remove(int index)
        {
            layers.RemoveAt(index);
        }

        public int Count()
        {
            return layers.Count;
        }

        public IEnumerator<Layer> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
