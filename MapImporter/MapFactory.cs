using System;
using MapImporter.Exceptions;
using System.IO;
using MapImporter.Loaders;

namespace MapImporter
{
    public class MapFactory
    {
        /// <summary>
        /// Creates the Tiled map from the given file.
        /// File must include its extension for the loader
        /// to use the correct parsing tool.
        /// </summary>
        /// <param name="filename">The file for the Tiled map to be loaded.</param>
        /// <returns></returns>
        public static Map Create(string filename)
        {
            try
            {
                switch (Path.GetExtension(filename))
                {
                    case ".tmx":
                        TmxMapLoader tmxMapLoader = new TmxMapLoader(filename);
                        return tmxMapLoader.Load();
                    case ".json":
                        JsonMapLoader jsonMapLoader = new JsonMapLoader(filename);
                        return jsonMapLoader.Load();
                    default:
                        throw new UnsupportedMapTypeException();
                }
            }
            catch (UnsupportedMapTypeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (UnsupportedOrientationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null; ;
        }

        /// <summary>
        /// Tiled maps have images that they use for tilesets. The path
        /// to these images is automatically set by Tiled. Calling this
        /// method will override the path used to locate the images.
        /// </summary>
        /// <param name="filepath"></param>
        public static void SetImagePath(string filepath)
        {
            throw new NotImplementedException();
        }
    }
}
