using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using GameJam2017.Content;
using Microsoft.Xna.Framework.Input;

namespace GameJam2017.Unit {
    public class Field : Entity {
        Vector2 pos = new Vector2(0, 0);
        public Player player;
        Cursor cursor;
        List<Controllable> selected;
        List<Unit> units;
        Vector2 selectBegin;
        bool selecting = false;
        public Texture2D FieldTexture;

        private Scene.Scene scene;

        public Field(Scene.Scene scene) : base(Vector2.Zero, new Vector2(Core.ScreenWidth, Core.ScreenHeight),
            Vector2.Zero) {
            this.scene = scene;
        }

        public void AddUnit(Unit u) {
            units.Add(u);
            scene.entities.Add(u);
        }

        public Unit ClosestUnit(Vector2 pos) {
            return units.Where(u => !(u is Enemy))
                .OrderBy(u => (u.getPos() - pos).Length())
                .First();
        }

        public override void Initialize() {
            Vector2 playerPos = new Vector2(Width / 2, Height / 2);
            FieldTexture = Core.Game.Content.Load<Texture2D>("Units\\stage");
            player = new Player(playerPos, this);
            cursor = new Cursor();
            
            cursor.Initialize(playerPos);
            selected = new List<Controllable>();
            units = new List<Unit>();
            AddUnit(player);
            for (int i = 0; i < 5; i ++) {
                var x = Core.rnd.Next(100, Width - 100);
                var y = Core.rnd.Next(100, Height - 100);
                var minion = new Minion(new Vector2(x, y), this);
                AddUnit(minion);
            }

            for (int i = 0; i < 5; i ++) {
                var x = Core.rnd.Next(100, Width - 100);
                var y = Core.rnd.Next(100, Height - 100);
                var enemy = new Enemy(new Vector2(x, y), this);
                AddUnit(enemy);
            }


            selected.Add(player);
        }
        public void RightClick(Vector2 click) {
            foreach (Controllable unit in selected) {
                unit.ChangeTarget(click);
            }
        }

        public void BeginSelection(Vector2 click) {
            selecting = true;
            foreach (Controllable unit in selected) {
                unit.Unselect();
            }
            selected.Clear();
            selectBegin = click;
        }

        public void EndSelection(Vector2 click) {
            // If the box is small, treat it as a click and select whatever is under the cursor
            if ((selectBegin - click).Length() < 5) {
                foreach (Unit unit in units) {
                    if ((unit.getPos() - click).Length() < 100) {
                        ((Controllable)unit).Select();
                        selected.Add((Controllable)unit);
                        break;
                    }
                }
            } else { // Otherwise, select everthing in the box
                foreach (Unit unit in units) {
                    if (unit is Controllable && IsInBox(selectBegin, click, unit.getPos())) {
                        ((Controllable)unit).Select();
                        selected.Add((Controllable)unit);
                    }
                }
            }
            selecting = false;
        }

        /**
         * Return true if pos is in the box formed by corner1 and corner2
         */
        public bool IsInBox(Vector2 corner1, Vector2 corner2, Vector2 pos) {
            return (pos.X > corner1.X && pos.X > corner1.Y && pos.X < corner2.X && pos.Y < corner2.Y)
                ||(pos.X < corner1.X && pos.X < corner1.Y && pos.X > corner2.X && pos.Y > corner2.Y);
        }

        public override void Update(GameTime time) {
            foreach (var unit in units) {
                unit.Update(time);
            }
            cursor.Update(time, new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
        }

        public override void Draw(GameTime g, SpriteBatch sb) {
            sb.Draw(FieldTexture, pos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            Vector2 size = cursor.getPos() - selectBegin;
            int boxWidth = (int)Math.Abs(size.X);
            int boxHeight = (int)Math.Abs(size.Y);
            if (selecting && boxWidth > 0 && boxHeight > 0) {
                Texture2D rect = new Texture2D(Core.Game.GraphicsDevice, boxWidth, boxHeight);
                Color[] data = new Color[boxWidth * boxHeight];
                for (int i = 0; i < data.Length; ++i) data[i] = Color.Aqua;
                rect.SetData(data);

                Vector2 topLeft = new Vector2(Math.Min(selectBegin.X, cursor.getPos().X), Math.Min(selectBegin.Y, cursor.getPos().Y));
                sb.Draw(rect, topLeft, Color.White);
            }

            foreach (var unit in units) {
                unit.Draw(sb);
            }
            cursor.Draw(sb);
        }
    }
}
