
namespace MapImporter
{
    /// <summary>
    /// This element defines an array of terrain types, which can be referenced
    /// from the terrain attribute of the tile element.
    /// </summary>
    /// <see cref="MapImporter.Terrain"/>
    public class TerrainTypes
    {
        /// <summary>
        /// Array of Terrain objects to define the types.
        /// </summary>
        public Terrain[] Terrains { set; get; }

        /// <summary>
        /// Constructor for the TerrainTypes object.
        /// </summary>
        public TerrainTypes()
        {
        }

        /// <summary>
        /// Constructor for the TerrainTypes object.
        /// </summary>
        /// <param name="terrains">Array of Terrain objects to define the types.</param>
        public TerrainTypes(Terrain[] terrains)
        {
            Terrains = terrains;
        }
    }
}
