
namespace MapImporter
{
    /// <summary>
    /// UNSURE
    /// </summary>
    public class Terrain
    {
        /// <summary>
        /// The name of the terrain type.
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// The local tile-id of the tile that represents the terrain visually.
        /// </summary>
        public int Tile { set; get; }
        /// <summary>
        /// Custom properties for the terrain object.
        /// </summary>
        public Properties Props { set; get; }

        /// <summary>
        /// Constructor for the Terrain object.
        /// </summary>
        public Terrain()
        {
            Props = new Properties();
        }

        /// <summary>
        /// Constructor for the Terrain object.
        /// </summary>
        /// <param name="terrains"></param>
        public Terrain(string name, int tile)
        {
            Name = name;
            Tile = tile;
            Props = new Properties();
        }

        /// <summary>
        /// Constructor for the Terrain object.
        /// </summary>
        /// <param name="terrains"></param>
        public Terrain(string name, int tile, Properties props)
        {
            Name = name;
            Tile = tile;
            Props = props;
        }
    }
}
