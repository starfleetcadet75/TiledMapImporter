namespace MapImporter.Loaders
{
    /// <summary>
    /// Responsible for loading Tiled maps saved in TMX format.
    /// </summary>
    public class TmxMapLoader : MapLoader
    {
        public TmxMapLoader(string filename)
            : base(filename)
        {
        }

        public override Map Load()
        {
            return map;
        }
    }
}
