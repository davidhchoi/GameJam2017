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
        public List<Controllable> Controllables { get; set; }
        List<Unit> units;
        Vector2 selectBegin;
        List<Unit> toAddUnits;
        List<Unit> toRemoveUnits;
        bool selecting = false;
        public Texture2D FieldTexture;

        private Scene.Scene scene;

        public Field(Scene.Scene scene) : base(Vector2.Zero, new Vector2(Core.ScreenWidth, Core.ScreenHeight),
            Vector2.Zero) {
            this.scene = scene;
            toAddUnits = new List<Unit>();
            toRemoveUnits = new List<Unit>();
        }

        public void AddUnit(Unit u) {
            toAddUnits.Add(u);
        }

        public void RemoveUnit(Unit u) {
            toRemoveUnits.Add(u);
        }
        
        public Unit ClosestAlly(Unit u1) {
            return Controllables.Where(u => u.Faction == u1.Faction)
                .OrderBy(u => (u.GetPos - u1.GetPos).Length())
                .FirstOrDefault();
        }

        public Unit ClosestEnemy(Unit u1) {
            return Controllables.Where(u => u.Faction != u1.Faction)
                .OrderBy(u => (u.GetPos - u1.GetPos).Length())
                .FirstOrDefault();
        }

        public override void Initialize() {
            Vector2 playerPos = new Vector2(Width / 2, Height / 2);
            FieldTexture = Core.Game.Content.Load<Texture2D>("Units\\stage");
            player = new Player(playerPos, this);
            cursor = new Cursor();
            
            cursor.Initialize(playerPos);
            selected = new List<Controllable>();
            units = new List<Unit>();
            Controllables = new List<Controllable>();
            AddUnit(player);
            for (int i = 0; i < 5; i ++) {
                var x = Core.rnd.Next(100, Width - 100);
                var y = Core.rnd.Next(100, Height - 100);
                var minion = new Minion("Units\\minion", new Vector2(x, y), Unit.Factions.P1, Core.Colours.White, this);
                AddUnit(minion);
            }

            for (int i = 0; i < 5; i ++) {
                var x = Core.rnd.Next(100, Width - 100);
                var y = Core.rnd.Next(100, Height - 100);
                var enemy = new Minion("Units\\enemy", new Vector2(x, y), Unit.Factions.Enemy, Core.Colours.Red, this);
                AddUnit(enemy);
            }


            selected.Add(player);
        }
        public void RightClick(Vector2 click) {
            foreach (var unit in units) {
                if (unit.Intersects(click.ToPoint())) {
                    foreach (Controllable u in selected) {
                        u.Attack(unit);
                    }
                    return;
                }
            }
            foreach (Controllable unit in selected) {
                unit.Move(click);
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
            Rectangle r = new Rectangle(selectBegin.ToPoint(), (click - selectBegin).ToPoint());
            foreach (Controllable unit in Controllables) {
                if (unit.Faction == Unit.Factions.P1 && unit.Intersects(r)) {
                    (unit).Select();
                    selected.Add(unit);
                }
            }
            selecting = false;
        }
        
        public void HoldSelected() {
            foreach (Controllable unit in selected) {
                unit.HoldPos();
            }
        }

        public void StopSelected() {
            foreach (Controllable unit in selected) {
                unit.Stop();
            }
        }

        public void AMoveSelected(Vector2 target) {
            foreach (Controllable unit in selected) {
                unit.AttackMove(target);
            }
        }
        
        public override void Update(GameTime time) {
            cursor.Update(time, new Vector2(Mouse.GetState().X, Mouse.GetState().Y));

            for (int i = 0; i < Controllables.Count; i++) {
                Controllables[i].Vel = Vector2.Zero;
                for (int j = 0; j < Controllables.Count; j++) {
                    if (i == j) continue;
                    Vector2 diff = Controllables[i].GetPos - Controllables[j].GetPos;
                    var dist = diff.Length() - Controllables[i].Height / 4 - Controllables[i].Width / 4 -
                               Controllables[j].Height / 4
                               - Controllables[j].Width / 4;
                    if (dist > 20) continue;
                    diff.Normalize();
                    dist = (float)(1.0 / Math.Pow(Math.Max(dist, -9.5f) + 10, 1));
                    
                    Controllables[i].Vel += diff * dist;
                }
            }

            // Every 5 seconds, spawn between 1-5 enemies

        }

        public void AddNewUnits() {
            foreach (var unit in toAddUnits) {
                units.Add(unit);
                scene.entities.Add(unit);
                if (unit is Controllable)
                    Controllables.Add((Controllable)unit);
            }
            toAddUnits.Clear();
        }

        public void RemoveKilledUnits() {
            foreach (var unit in toRemoveUnits) {
                units.Remove(unit);
                scene.entities.Remove(unit);
                if (unit is Controllable)
                    Controllables.Remove((Controllable)unit);
            }
            toRemoveUnits.Clear();
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
