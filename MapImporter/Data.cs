using System;

namespace MapImporter
{
    /// <summary>
    /// Stores the data for each layer. Each layer contains a list
    /// of gids and we store those as a matrix.
    /// </summary>
    public class Data
    {
        /// <summary>
        /// Contains the gid at the given indices for a layer.
        /// </summary>
        public int[,] TileData { set; get; }

        /// <summary>
        /// Instantiates the new 2d array.
        /// </summary>
        /// <param name="i">The number of tiles in the i direction</param>
        /// <param name="j">The number of tiles in the j direction</param>
        public Data(int i, int j)
        {
            TileData = new int[i, j];
        }

        /// <summary>
        /// Returns the gid of the tile at the specified indices.
        /// </summary>
        /// <param name="i">The index in the i direction</param>
        /// <param name="j">The index in the j direction</param>
        /// <returns>The gid at the given indices</returns>
        public int GetTileData(int i, int j)
        {
            try
            {
                return TileData[i, j];
            }
            catch (IndexOutOfRangeException)
            {
                Console.Write("ERROR: Class=Data --- Tried to access a value in Data index that does not exist" + "\n");
                return 0;
            }
        }

        /// <summary>
        /// Changes the gid of the given indices to the new value.
        /// Here if you need it, but don't try rewriting your whole map using it.
        /// </summary>
        /// <param name="newVal">The new gid to be stored at this position</param>
        /// <param name="i">The index in the i direction</param>
        /// <param name="j">The index in the j direction</param>
        public void ChangeTileDataAt(int newVal, int i, int j)
        {
            try
            {
                TileData[i, j] = newVal;
            }
            catch (IndexOutOfRangeException)
            {
                Console.Write("ERROR: Class=Data --- Tried to assign a value to an index that does not exist" + "\n");
            }
        }

        /// <summary>
        /// Prints the entire matrix to the console.
        /// </summary>
        public void PrintData()
        {
            for (int j = 0; j <= TileData.GetUpperBound(1); j++)
            {
                for (int i = 0; i <= TileData.GetUpperBound(0); i++)
                {
                    Console.Write(TileData[i, j] + " ");
                }
                Console.Write("\n");
            }
        }
    }
}
