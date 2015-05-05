
namespace MapImporter
{
    /// <summary>
    /// UNSURE
    /// </summary>
    /// <see cref="MapImporter.TerrainTypes"/>
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
        /// <see cref="MapImporter.Properties"/>
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
        /// <param name="name">The name of the terrain type.</param>
        /// <param name="tile">The local tile-id of the tile that represents the terrain visually.</param>
        public Terrain(string name, int tile)
        {
            Name = name;
            Tile = tile;
            Props = new Properties();
        }

        /// <summary>
        /// Constructor for the Terrain object.
        /// </summary>
        /// <param name="name">The name of the terrain type.</param>
        /// <param name="tile">The local tile-id of the tile that represents the terrain visually</param>
        /// <param name="props">Custom properties for the Terrain object.</param>
        public Terrain(string name, int tile, Properties props)
        {
            Name = name;
            Tile = tile;
            Props = props;
        }
    }
}
