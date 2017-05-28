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

namespace GameJam2017.Scene {
    public class GameScene : Scene {
        SpriteBatch spriteBatch;
        Field field;
        private Deck d;

        private Double lastSpellTime;
        
        public GameScene() {
        }
        public override void Initialize() {
            base.Initialize();
        }

        public override void LoadContent() {
            base.LoadContent();
        }

        public override void UnloadContent() {
            base.UnloadContent();
        }

        public override void MakeActive(GameTime gameTime) {
            entities = entities.Where(x => (x is Deck)).ToList();
            field = new Field(this);
            field.Initialize();
            entities.Add(field);
            lastSpellTime = gameTime.TotalGameTime.Seconds;
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

            if (Core.Game.KeyboardBecame(Keys.S, KeyState.Down)) {
                field.StopSelected();
            }

            if (Core.Game.KeyboardBecame(Keys.H, KeyState.Down)) {
                field.HoldSelected();
            }

            if (Core.Game.KeyboardBecame(Keys.A, KeyState.Down)) {
                field.AMoveSelected(mousePosition);
            }

            if (Core.Game.KeyboardBecame(Keys.Space, KeyState.Down)) {
                field.CentreCamera();
            }

            if (Core.Game.KeyboardBecame(Keys.Up, KeyState.Down)) {
                field.MoveCamera(Field.Direction.UP);
            }
            if (Core.Game.KeyboardBecame(Keys.Down, KeyState.Down)) {
                field.MoveCamera(Field.Direction.DOWN);
            }
            if (Core.Game.KeyboardBecame(Keys.Right, KeyState.Down)) {
                field.MoveCamera(Field.Direction.RIGHT);
            }
            if (Core.Game.KeyboardBecame(Keys.Left, KeyState.Down)) {
                field.MoveCamera(Field.Direction.LEFT);
            }

            var diff = Mouse.GetState().Position + Core.Game.camera.GetTopLeft().ToPoint() - field.player.GetPos.ToPoint();
            field.player.Angle = (float)(Math.Atan2(diff.Y, diff.X) + Math.PI);

            if (gameTime.TotalGameTime.Seconds - lastSpellTime > 1) {
                d.CastNext(field.player.GetPos, (float)(Math.Atan2(diff.X, diff.Y)), field);
                lastSpellTime = gameTime.TotalGameTime.Seconds;
            }
            field.AddNewUnits();
            field.RemoveKilledUnits();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            field.Draw(gameTime, spriteBatch);
            d.Draw(gameTime, spriteBatch);
            base.Draw(gameTime, spriteBatch);
        }
        public override void DrawUI(GameTime gameTime, SpriteBatch spriteBatch) {
            d.Draw(gameTime, spriteBatch);
        }

        public void SetDeck(Deck d) {
            entities = entities.Where(x => !(x is Deck)).ToList();
            this.d = d;
            entities.Add(d);
        }

    }
}
