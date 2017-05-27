using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameJam2017.Glamour.Bullets;

namespace GameJam2017.Unit {
    public class Player : Controllable {
        public Player(Vector2 position, Field f) : base(Core.Game.Content.Load<Texture2D>("Units\\player"), position, 8.0f, f) { }
        int timeSinceLastShot = 0;

        public virtual Vector2 getPos() { return new Vector2(Pos.X - 100 , Pos.Y -100); }

        public override void Update(GameTime time) {
            base.Update(time);

            var diff = Mouse.GetState().Position - GetPos.ToPoint();
            Angle = (float)(Math.Atan2(diff.Y, diff.X) + Math.PI);
            timeSinceLastShot -= 1;
        }

        protected override void Shoot(Unit u) {
            var diff = u.GetPos - Pos;
            var dir = (float)(Math.Atan2(diff.Y, diff.X) + Math.PI);

            if (timeSinceLastShot <= 0) {
                Bullet b = new Bullet(80, Core.Colours.Blue, 8f, dir, GetPos, new Vector2(20, 20), f);
                timeSinceLastShot = 60;
                f.AddUnit(b);
            }
        }

        public override void Draw(SpriteBatch sb) {
            sb.Draw(Texture, new Vector2(Texture.Width / 3 + X, Texture.Height / 3 - 20 + Y), null, Color.White, Angle, 
                new Vector2(Texture.Width / 3, Texture.Height * 2 / 3), 1f, SpriteEffects.None, 0f);
            sb.Draw(Core.Rectangles[2], new Rectangle((Pos + Size / 2 - new Vector2(5, 5)).ToPoint(), new Point(10, 10)), Color.White);
        }

    }
}
