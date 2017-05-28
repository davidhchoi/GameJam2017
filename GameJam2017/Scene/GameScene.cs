using GameJam2017.Unit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameJam2017.Glamour;
using GameJam2017.Component;
using static GameJam2017.Core;

namespace GameJam2017.Scene {
    public class GameScene : Scene {
        SpriteBatch spriteBatch;
        Field field;
        private Deck d;

        Button holdBtn;
        bool holdBtnPressed;
        Button stopBtn;
        bool stopBtnPressed;
        Button amoveBtn; 
        bool amoveBtnPressed;
        Texture2D icontray;

        private TimeSpan lastSpellTime;
        
        public GameScene() {
        }
        public override void Initialize() {
            base.Initialize();

            // Initialize the buttons
            amoveBtn = new Button(delegate {
                amoveBtnPressed = true;
            }, new Vector2(20, Core.ScreenHeight- 85), new Vector2(75, 75) ,"Icons\\attackmove");
            amoveBtn.Initialize();
            entities.Add(amoveBtn);

            holdBtn = new Button(delegate {
                holdBtnPressed = true;
            }, new Vector2(110, Core.ScreenHeight- 85), new Vector2(75, 75) , "Icons\\hold" );
            holdBtn.Initialize();
            entities.Add(holdBtn);

            stopBtn = new Button(delegate {
                stopBtnPressed = true;
            }, new Vector2(200, Core.ScreenHeight- 85), new Vector2(75, 75) , "Icons\\stop");
            stopBtn.Initialize();
            entities.Add(stopBtn);

            icontray = Core.Game.Content.Load<Texture2D>("Icons\\icontray");

        }

        public override void LoadContent() {
            base.LoadContent();
        }

        public override void UnloadContent() {
            base.UnloadContent();
        }

        public override void MakeActive(GameTime gameTime) {
            entities.RemoveWhere(x => !(x is Deck || x is Button));
            field = new Field(this);
            field.Initialize();
            entities.Add(field);
            lastSpellTime = gameTime.TotalGameTime.Duration();
            base.MakeActive(gameTime);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            var mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            if (Core.Game.MouseRightBecame(ButtonState.Pressed)) {
                field.RightClick(mousePosition);
            }
            if (Core.Game.MouseLeftBecame(ButtonState.Pressed)) {
                field.BeginSelection(mousePosition);
            }
            if (Core.Game.MouseLeftBecame(ButtonState.Released)) {
                field.EndSelection(mousePosition);
            }

            if (Core.Game.KeyboardBecame(Keys.S, KeyState.Down)
                || stopBtnPressed) {
                field.StopSelected();
                stopBtnPressed = false;
            }

            if (Core.Game.KeyboardBecame(Keys.H, KeyState.Down)
                || holdBtnPressed) {
                field.HoldSelected();
                holdBtnPressed = false;
            }

            if (Core.Game.KeyboardBecame(Keys.A, KeyState.Down)
                || amoveBtnPressed) {
                field.AMoveSelected(mousePosition);
                amoveBtnPressed = false;
            }

            if (Core.Game.KeyboardBecame(Keys.Space, KeyState.Down)) {
                field.CentreCamera();
            }

            if (Core.Game.KeyboardBecame(Keys.Up, KeyState.Down)
                || Mouse.GetState().Position.Y < 50) {
                field.MoveCamera(Field.Direction.UP);
            }
            if (Core.Game.KeyboardBecame(Keys.Down, KeyState.Down)
                || Mouse.GetState().Position.Y > 1030) {
                field.MoveCamera(Field.Direction.DOWN);
            }
            if (Core.Game.KeyboardBecame(Keys.Right, KeyState.Down)
                || Mouse.GetState().Position.X > 1870) {
                field.MoveCamera(Field.Direction.RIGHT);
            }
            if (Core.Game.KeyboardBecame(Keys.Left, KeyState.Down)
                || Mouse.GetState().Position.X < 50) {
                field.MoveCamera(Field.Direction.LEFT);
            }

            var diff = Mouse.GetState().Position + Core.Game.camera.GetTopLeft().ToPoint() - field.player.GetPos.ToPoint();
            field.player.Angle = (float)(Math.Atan2(diff.Y, diff.X) + Math.PI);

            if ((gameTime.TotalGameTime.Duration() - lastSpellTime).Seconds > 1) {
                d.CastNext(field.player.GetPos, (float)(Math.Atan2(diff.X, diff.Y)), field);
                lastSpellTime = gameTime.TotalGameTime.Duration();

                // When we run out of spells, the level is finished
                if (d.Count == 0) {
                    Core.currentLevel++;
                    Core.Game.ChangeScene(SceneTypes.Deck);
                }
            }
            field.AddNewUnits();
            field.RemoveKilledUnits();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            field.Draw(gameTime, spriteBatch);
            base.Draw(gameTime, spriteBatch);
        }
        public override void DrawUI(GameTime gameTime, SpriteBatch spriteBatch) {
            // Draw the deck
            d.Draw(gameTime, spriteBatch);

            // Draw a background for the buttons
            spriteBatch.Draw(icontray, new Vector2(0, Core.ScreenHeight - 100));

            holdBtn.Draw(gameTime, spriteBatch);
            stopBtn.Draw(gameTime, spriteBatch);
            amoveBtn.Draw(gameTime, spriteBatch);

            // Draw the game icons
        }

        public void SetDeck(Deck d) {
            entities.RemoveWhere(x => (x is Deck));
            this.d = d;
            entities.Add(d);
        }

    }
}
