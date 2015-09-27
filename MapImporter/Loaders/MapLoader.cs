namespace MapImporter
{
    /// <summary>
    /// The base class for each MapLoader object.
    /// </summary>
    public abstract class MapLoader
    {
        protected string filename;
        protected Map map;

        /// <summary>
        /// Constructor for the MapLoader.
        /// </summary>
        /// <param name="filename">The name of the map file to load.</param>
        public MapLoader(string filename)
        {
            this.filename = filename;
            this.map = new Map();
        }

        /// <summary>
        /// Abstract Load method that each MapLoader overrides.
        /// </summary>
        /// <returns>The new Map object.</returns>
        public abstract Map Load();
    }
}

