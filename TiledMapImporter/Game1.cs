using MapImporter;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DemoGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // The Tiled map object
        Map map;

        /// <summary>
        /// The Game1 class constructor
        /// </summary>
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 576;
            graphics.PreferredBackBufferHeight = 576;
            this.Window.AllowUserResizing = false;
            this.Window.Title = "Tiled Map Importer Demo";
            this.IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // This line creates a new Map object from the given file.
            // The file can be in any of the supported formats and can be located anywhere provided
            // you put the right file path. Unfortunately, your images for the tilesets
            // must be in XNB format and located in your Content folder. The ContentManager
            // is passed here because it is required to load the tileset textures.
            map = MapFactory.Load(@"Content/NewBarkTown.json");
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // To move the map around, change the startIndex vector with input.
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                startIndex.X += 1;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                startIndex.X -= 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                startIndex.Y -= 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                startIndex.Y += 1;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                map.LayerToTop(0);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                map.LayerToBottom(1);
            }

            //Prints the gid of the tile the player is currently on if it contains a trigger tile
            //int i = map.GetTile(playerTilePosition, startIndex, map.GetTileLayer("TriggerLayer"));
            //Console.Write("The gid is: " + i + "\n");
            //Console.Write("The player is at tile: " + playerTilePosition.ToString() + "\n");

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            // This line calls the Draw function of our Map object.
            // It will draw all tile layers that are marked visible to the screen.
            map.Draw(spriteBatch, graphics.GraphicsDevice.Viewport.Bounds, startIndex);

            // If you wish to draw tile layers manually, use any of these lines:
            // 
            // This one draws the 1st tile layer in the list of tile layers
            // map.DrawLayer(spriteBatch, 0, graphics.GraphicsDevice.Viewport.Bounds);
            // 
            // This one draws the tile layer which has the name "GroundLayer"
            // map.DrawLayer(spriteBatch, "GroundLayer", graphics.GraphicsDevice.Viewport.Bounds, startIndex);
            //
            // I recommend the previous two but there is also this one which draws the given Layer object
            // map.DrawLayer(spriteBatch, new Layer(), graphics.GraphicsDevice.Viewport.Bounds);

            spriteBatch.Draw(map.Tilesets[1].Image.Texture, playerPosition, new Rectangle(0, 0, 64, 64), Color.CadetBlue);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }

#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
                game.Run();
        }
    }
#endif
}
