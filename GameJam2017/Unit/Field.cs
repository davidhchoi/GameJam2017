using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using GameJam2017.Content;
using Microsoft.Xna.Framework.Input;

namespace GameJam2017.Unit {
    public class Field : Entity {
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
        public Texture2D ClickTexture;
        public int STAGE_HEIGHT = 1080 * 2;
        public int STAGE_WIDTH = 1920 * 2;

        int DisplayClick = 0;

        public enum Direction {
            UP,
            DOWN,
            LEFT,
            RIGHT
        };

        private Scene.Scene scene;

        public Field(Scene.Scene scene) : base(Vector2.Zero, new Vector2(Core.ScreenWidth*2, Core.ScreenHeight)*2,
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
            return Controllables.Where(u => u.Colour == u1.Colour)
                .OrderBy(u => (u.GetPos - u1.GetPos).Length())
                .FirstOrDefault();
        }

        public Unit ClosestEnemy(Unit u1) {
            return Controllables.Where(u => u.Colour != u1.Colour)
                .OrderBy(u => (u.GetPos - u1.GetPos).Length())
                .FirstOrDefault();
        }

        public void CentreCamera() {
            Core.Game.camera.CenterOn(player.GetPos);

            Vector2 topleft = Core.Game.camera.GetTopLeft();

            Vector2 newState = topleft + Mouse.GetState().Position.ToVector2();
            cursor.Update(newState);
        }

        public void MoveCamera(Direction d) {
            switch (d) {
                case Direction.UP:
                    Core.Game.camera.MoveCamera(new Vector2(0, -50));
                    break;
                case Direction.DOWN:
                    Core.Game.camera.MoveCamera(new Vector2(0, 50));
                    break;
                case Direction.LEFT:
                    Core.Game.camera.MoveCamera(new Vector2(-50, 0));
                    break;
                case Direction.RIGHT:
                    Core.Game.camera.MoveCamera(new Vector2(50, 0));
                    break;
            }
        }

        public override void Initialize() {

            Vector2 playerPos = new Vector2(STAGE_WIDTH / 2, STAGE_HEIGHT / 2);
            FieldTexture = Core.Game.Content.Load<Texture2D>("Units\\stage");
            ClickTexture = Core.Game.Content.Load<Texture2D>("select");
            player = new Player(playerPos, this);
            cursor = new Cursor();
            
            cursor.Initialize(playerPos);
            selected = new List<Controllable>();
            units = new List<Unit>();
            Controllables = new List<Controllable>();
            AddUnit(player);
            for (int i = 0; i < 5; i ++) {
                var x = Core.rnd.Next(100, STAGE_WIDTH - 100);
                var y = Core.rnd.Next(100, STAGE_HEIGHT - 100);
                var minion = new Minion("Units\\minion", new Vector2(x, y), Unit.Factions.P1, Core.Colours.White, this);
                AddUnit(minion);
            }

            for (int i = 0; i < 5; i ++) {
                var x = Core.rnd.Next(100, Width - 100);
                var y = Core.rnd.Next(100, Height - 100);
                var enemy = new Minion("Units\\enemy", new Vector2(x, y), Unit.Factions.Enemy, Core.Colours.Yellow, this);
                AddUnit(enemy);
            }


            CentreCamera();
            selected.Add(player);
        }
        public void RightClick(Vector2 _click) {
            var click = _click + Core.Game.camera.GetTopLeft();
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
            DisplayClick = 5;
        }

        public void BeginSelection(Vector2 _click) {
            var click = _click + Core.Game.camera.GetTopLeft();
            selectBegin = click;
            selecting = true;
            foreach (Controllable unit in selected) {
                unit.Unselect();
            }
            selected.Clear();
        }

        public void EndSelection(Vector2 _click) {
            var click = _click + Core.Game.camera.GetTopLeft();
            var endPos = click;
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
            cursor.Update(new Vector2(Mouse.GetState().X, Mouse.GetState().Y) + Core.Game.camera.GetTopLeft());
            
            // Stop units from overlapping
            for (int i = 0; i < Controllables.Count; i++) {
                Controllables[i].Vel /= 2;
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
            // Draw the field
            sb.Draw(FieldTexture, Pos);

            // Draw the selection box if it exists
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


            // Display the click cursor if needed
            if (DisplayClick > 0) {
                sb.Draw(ClickTexture, cursor.getPos() - new Vector2(ClickTexture.Width / 2, ClickTexture.Height / 2));
                DisplayClick -= 1;
            }
            // Draw each unit
            foreach (var unit in units) {
                unit.Draw(sb);
            }
        }
    }
}
