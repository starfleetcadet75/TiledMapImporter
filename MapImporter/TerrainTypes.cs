
namespace MapImporter
{
    /// <summary>
    /// This element defines an array of terrain types, which can be referenced
    /// from the terrain attribute of the tile element.
    /// </summary>
    public class TerrainTypes
    {
        /// <summary>
        /// Array of Terrains to define the types
        /// </summary>
        public Terrain[] Terrains { set; get; }
    }
}
