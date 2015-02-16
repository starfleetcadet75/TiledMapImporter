
namespace TiledMapImporter
{
    /// <summary>
    /// UNSURE
    /// </summary>
    public class Terrain
    {
        /// <summary>
        /// The name of the terrain type
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// The local tile-id of the tile that represents the terrain visually.
        /// </summary>
        public int Tile { set; get; }
        /// <summary>
        /// Custom properties for the terrain object
        /// </summary>
        public Properties Props { set; get; }
    }
}
