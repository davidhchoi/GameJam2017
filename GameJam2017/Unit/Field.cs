using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameJam2017.Unit {
    class Field {
        int width = 1920;
        int height = 1080;
        Vector2 pos = new Vector2(0, 0);
        Player player;
        Cursor cursor;
        List<Controllable> selected;
        List<Unit> units;
        Vector2 selectBegin;
        bool selecting = false;
        public Texture2D FieldTexture;

        public void Initialize( Vector2 playerPos ) {
            FieldTexture = Core.Game.Content.Load<Texture2D>("Units\\stage");
            player = new Player();
            cursor = new Cursor();

            player.Initialize(playerPos);
            cursor.Initialize(playerPos);
            selected = new List<Controllable>();
            units = new List<Unit>();
            units.Add(player);
            for (int i = 0; i < 5; i ++) {
                var minion = new Minion();
                var x = Core.rnd.Next(100, width -100);
                var y = Core.rnd.Next(100, height - 100);
                minion.Initialize(new Vector2(x, y));
                units.Add(minion);
            }

            for (int i = 0; i < 5; i ++) {
                var enemy = new Enemy();
                var x = Core.rnd.Next(100, width -100);
                var y = Core.rnd.Next(100, height - 100);
                enemy.Initialize(new Vector2(x, y));
                units.Add(enemy);
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
            selected.Clear();
            selectBegin = click;
        }

        public void EndSelection(Vector2 click) {
            foreach (Unit unit in units) {
                if (unit is Controllable && IsInBox(selectBegin, click, unit.getPos())) {
                    selected.Add((Controllable) unit);
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

        public void Update(GameTime time, Vector2 mousePos) {
            foreach (var unit in units) {
                unit.Update(time, units);
            }
            cursor.Update(time, mousePos);
        }

        public void Draw(SpriteBatch sb) {
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
