using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameJam2017.Glamour.Bullets;

namespace GameJam2017.Unit {
    class Minion : Controllable {
        public Minion(Vector2 position, Field f) : base(Core.Game.Content.Load<Texture2D>("Units\\minion"), position, 5.0f, f) {
            range = 500;
        }
        int timeSinceLastShot = 0;
        
        protected override void Shoot(Unit u) {
            var diff = u.GetPos - Pos;
            var dir = (float)(Math.Atan2(diff.Y, diff.X) + Math.PI);

            if (timeSinceLastShot <= 0) {
                Bullet b = new Bullet(80, Core.Colours.Blue, 8f, dir, GetPos, new Vector2(20, 20), f);
                timeSinceLastShot = 120;
                f.AddUnit(b);
            }
        }

        public override void Update(GameTime g) {
            base.Update(g);
            timeSinceLastShot -= 1;
        }

        public override void Draw(SpriteBatch sb) {
            sb.Draw(Texture, destinationRectangle: new Rectangle(Pos.ToPoint(), Size.ToPoint()), origin: new Vector2(Width / 2, Height / 2), rotation: Angle);
        }
    }
}
