using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameJam2017.Glamour.Bullets;

namespace GameJam2017.Unit {
    public class Minion : Controllable {
        public Minion(string texture, Vector2 position, Factions faction, Core.Colours c, Field f) : base(texture, position, 5.0f, faction, c, f) {
            range = 500;
        }
        int timeSinceLastShot = 0;
        
        protected override void Shoot(Unit u) {
            var diff = u.GetPos - GetPos;
            var dir = (float)(Math.Atan2(diff.X, diff.Y));

            if (timeSinceLastShot <= 0) {
                Bullet b = new Bullet(80, Colour, 8f, dir, GetPos, new Vector2(20, 20), Faction, f);
                timeSinceLastShot = 120;
                f.AddUnit(b);
            }
        }

        public override void Update(GameTime g) {
            base.Update(g);
            timeSinceLastShot -= 1;
        }
    }
}
