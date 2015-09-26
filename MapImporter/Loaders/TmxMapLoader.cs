namespace MapImporter.Loaders
{
    public class TmxMapLoader : IMapLoader
    {
        private string filename;
        private Map map;

        public TmxMapLoader(string filename)
        {
            this.filename = filename;
            this.map = new Map();
        }

        public Map Load()
        {
            return map;
        }
   }
}
