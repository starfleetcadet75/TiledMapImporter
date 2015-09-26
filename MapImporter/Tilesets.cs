using System;
using System.Collections;
using System.Collections.Generic;

namespace MapImporter
{
    public class Tilesets : IEnumerable<Tileset>
    {
        private List<Tileset> tilesets;

        public Tilesets()
        {
            tilesets = new List<Tileset>();
        }

        public void AddTileset(Tileset tileset)
        {
            tilesets.Add(tileset);
        }

        public Tileset GetTileset(int index)
        {
            return tilesets[index];
        }

        public Tileset GetTileset(string name)
        {
            foreach (Tileset tileset in tilesets)
            {
                if (tileset.Name.Equals(name))
                {
                    return tileset;
                }
            }
            return null;
        }

        public int GetIndex(Tileset tileset)
        {
            return tilesets.IndexOf(tileset);
        }

        public int GetIndex(string name)
        {
            return GetIndex(GetTileset(name));
        }

        public Tile GetTile(int gid)
        {
            for (int i = tilesets.Count - 1; 0 <= i; i--)
            {
                Tileset tileset = tilesets[i];
                Tile tile = tileset.GetTile(gid);

                if (tile != null)
                {
                    return tile;
                }
            }
            return null;
        }

        public void Remove(Tileset tileset)
        {
           tilesets.Remove(tileset);
        }

        public void Remove(string name)
        {
            tilesets.Remove(GetTileset(name));
        }

        public void Remove(int index)
        {
            tilesets.RemoveAt(index);
        }

        public int Count()
        {
            return tilesets.Count;
        }

        public IEnumerator<Tileset> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
