using GameJam2017.Glamour;
using GameJam2017.Scene;
using GameJam2017.Unit;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameJam2017 {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Field field;
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        MouseState currentMouseState;
        MouseState prevMouseState;
        private Texture2D cursor;

        private MainMenuScene mainScene;
        private DraftScene draftScene;
        private GameScene gameScene;

        private Scene.Scene[] scenes;
        private Scene.Scene activeScene;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1920;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 1080;   // set this value to the desired height of your window
            graphics.ApplyChanges();
            Window.IsBorderless = true;

            mainScene = new MainMenuScene();
            draftScene = new DraftScene();
            gameScene = new GameScene();
            scenes = new Scene.Scene[]{mainScene, draftScene, gameScene};

            activeScene = draftScene;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here

            foreach (var scene in scenes) {
                scene.Initialize();
            }
            Core.Initialize();

            base.Initialize();
            graphics.ApplyChanges();
            field = new Field();
            Vector2 playerPos = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            field.Initialize(playerPos);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            foreach (var scene in scenes) {
                scene.LoadContent();
            }
            Glamour.Glamour.Initialize();
            cursor = Content.Load<Texture2D>("menucursor");
            draftScene.Reset();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here

            foreach (var scene in scenes) {
                scene.UnloadContent();
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            activeScene.Update(gameTime);

            prevMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            var mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);
            if (currentMouseState.RightButton == ButtonState.Pressed) {
                field.RightClick(mousePosition);
            }
            if (currentMouseState.LeftButton == ButtonState.Pressed &&
                prevMouseState.LeftButton != ButtonState.Pressed
                ) {
                field.BeginSelection(mousePosition);
            }
            if (currentMouseState.LeftButton == ButtonState.Released && 
                prevMouseState.LeftButton != ButtonState.Released
                ) {
                field.EndSelection(mousePosition);
            }
            field.Update(gameTime, mousePosition);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            activeScene.Draw(gameTime, spriteBatch);

            //field.Draw(spriteBatch);
            spriteBatch.Draw(cursor, new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
