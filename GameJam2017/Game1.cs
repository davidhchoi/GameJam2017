﻿using System.Diagnostics.Eventing.Reader;
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
        private Texture2D cursor;

        private MainMenuScene mainScene;
        private DraftScene draftScene;
        private GameScene gameScene;

        MouseState currentMouseState;
        MouseState prevMouseState;

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        private Scene.Scene[] scenes;
        private Scene.Scene activeScene;

        public int Width = 1920;
        public int Height = 1080;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = Width;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = Height;   // set this value to the desired height of your window
            graphics.ApplyChanges();
            Window.IsBorderless = true;

            mainScene = new MainMenuScene();
            draftScene = new DraftScene();
            gameScene = new GameScene();
            scenes = new Scene.Scene[]{mainScene, draftScene, gameScene};

            activeScene = gameScene;
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

        public bool MouseLeftBecame(ButtonState state) {
            return currentMouseState.LeftButton == state && prevMouseState.LeftButton != state;
        }

        public bool MouseRightBecame(ButtonState state) {
            return currentMouseState.RightButton == state && prevMouseState.RightButton != state;
        }

        public bool KeyboardBecame(Keys key, KeyState state) {
            if (state == KeyState.Down) {
                return currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key);
            } else {
                return currentKeyboardState.IsKeyUp(key) && previousKeyboardState.IsKeyDown(key);
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
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            var mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);
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

            spriteBatch.Draw(cursor, new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
