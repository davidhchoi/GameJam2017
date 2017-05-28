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
        int remainingTime;
        bool timingOut;

        private SpellIndicator spellIndicator;

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

            spellIndicator = new SpellIndicator(new Vector2(0, 0), new Vector2(50, 50));
            entities.Add(spellIndicator);
            timingOut = false;
        }

        public override void LoadContent() {
            base.LoadContent();
        }

        public override void UnloadContent() {
            base.UnloadContent();
        }

        public override void MakeActive(GameTime gameTime) {
            entities.RemoveWhere(x => (x is Unit.Unit));
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

            var spellProgress = (gameTime.TotalGameTime.Duration() - lastSpellTime).TotalMilliseconds / Core.TimePerSpell / 1000.0;
            if (spellProgress > 1) {
                d.CastNext((float)(Math.Atan2(diff.X, diff.Y)), field.player, field);
                lastSpellTime = gameTime.TotalGameTime.Duration();
                
                if (d.Count == 0 && timingOut == false) {
                    timingOut = true;
                    remainingTime = 5;
                } else {
                    // Spawn some more enemies to fight
                }
                if (remainingTime == 0 && timingOut == true) {
//                    Core.currentLevel++;
//                    timingOut = false;
//                    Core.Game.ChangeScene(SceneTypes.Deck);
                } else {
                    remainingTime -= 1;
                }
                field.SpawnEnemies(5);
            }
            spellIndicator.UpdateProgress(spellProgress);
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

            spellIndicator.Pos = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);
            spellIndicator.Draw(gameTime, spriteBatch);

            if (timingOut) {
                spriteBatch.DrawString(Core.freestyle70,
                    remainingTime.ToString(),
                    new Vector2(Core.ScreenWidth / 2, Core.ScreenHeight / 2),
                    Color.Black, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
            }
        }

        public void SetDeck(Deck d) {
            entities.RemoveWhere(x => (x is Deck));
            this.d = d;
            entities.Add(d);
        }

    }
}
