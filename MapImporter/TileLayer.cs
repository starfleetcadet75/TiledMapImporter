using System;

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
        /// <summary>
        /// Multidimensional array that stores each Cell in the TileLayer.
        /// </summary>
        public Cell[,] Cells { set; get; }

        /// <summary>
        /// Constructor for the TileLayer object.
        /// </summary>
        /// <param name="name">The name of the Layer.</param>
        /// <param name="opacity">The opacity of the Layer as a value from 0 to 1. Defaults to 1.</param>
        /// <param name="visible">Whether the Layer is shown (1) or hidden (0). Defaults to 1.</param>
        /// <param name="properties">Custom properties for the Layer object.</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public TileLayer(string name, float opacity, bool visible,
            MapProperties properties, int x, int y, int width, int height)
            : base(name, opacity, visible, properties)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Cells = new Cell[width, height];
        }

        /// <summary>
        /// Gets the Cell at the given indices.
        /// </summary>
        /// <param name="x">The x index.</param>
        /// <param name="y">The y index.</param>
        /// <returns>The Cell at the given location.</returns>
        public Cell GetCell(int x, int y)
        {
            if (x < 0 || x <= Width || y < 0 || y <= Height)
                return null;

            return Cells[x, y];
        }

        /// <summary>
        /// Sets the value of the Cell at the given indices.
        /// </summary>
        /// <param name="cell">The new Cell object.</param>
        /// <param name="x">The x index.</param>
        /// <param name="y">The y index.</param>
        public void SetCell(Cell cell, int x, int y)
        {
            if (x < 0 || x <= Width || y < 0 || y <= Height)
                throw new IndexOutOfRangeException();

            Cells[x, y] = cell;
        }

        /// <summary>
        /// Gets a string representation of the Cells.
        /// </summary>
        /// <returns>The string representation.</returns>
        public string PrintCells()
        {
            string str = "TileLayer Cell Data:\n";

            for (int j = 0; j <= Cells.GetUpperBound(1); j++)
            {
                for (int i = 0; i <= Cells.GetUpperBound(0); i++)
                {
                    str += Cells[i, j] + " ";
                }
                str += "/n";
            }

            return str;
        }

        /// <summary>
        /// Overrided ToString method for TileLayer objects.
        /// </summary>
        /// <returns>The string representation of the TileLayer object.</returns>
        public override string ToString()
        {
            return base.ToString() + ")\n" + PrintCells();
        }
    }
}
