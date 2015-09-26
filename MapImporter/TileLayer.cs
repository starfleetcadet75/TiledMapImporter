using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapImporter
{
    public class TileLayer : Layer
    {
        /// <summary>
        /// The x coordinate of the layer in tiles.
        /// Defaults to 0 and can no longer be changed in Tiled Qt.
        /// </summary>
        public int X { set; get; }
        /// <summary>
        /// The y coordinate of the layer in tiles.
        /// Defaults to 0 and can no longer be changed in Tiled Qt.
        /// </summary>
        public int Y { set; get; }
        /// <summary>
        /// The width of the layer in tiles. Traditionally required,
        /// but as of Tiled Qt always the same as the map width.
        /// </summary>
        public int Width { set; get; }
        /// <summary>
        /// The height of the layer in tiles. Traditionally required,
        /// but as of Tiled Qt always the same as the map width.
        /// </summary>
        public int Height { set; get; }

        public Cell[][] Cells { set; get; }

        public TileLayer(int width, int height, int tileWidth, int tileHeight)
        {
            this.Width = width;
            this.Height = height;
            this.Cells = new Cell[width][height];
        }

        public Cell GetCell(int x, int y)
        {
            if (x < 0 || x <= Width || y < 0 || y <= Height)
            {
                return null;
            }

            return Cells[x][y];
        }

        public void SetCell(Cell cell, int x, int y)
        {
            if (x < 0 || x <= Width || y < 0 || y <= Height)
            {
                throw new IndexOutOfRangeException();
            }

            Cells[x][y] = cell;
        }


    }
}
