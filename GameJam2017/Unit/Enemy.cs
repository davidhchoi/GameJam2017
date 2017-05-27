using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace GameJam2017.Unit {
    class Enemy : Unit {
        Vector2 Target;
        public Enemy(Vector2 position, Field f) : base(Core.Game.Content.Load<Texture2D>("Units\\enemy"), position, 2.0f, f) { }

        public override void Update(GameTime time) {
            // Move towards the nearest unit
            Unit moveTo = f.ClosestUnit(getPos());
            Target = moveTo.getPos();

            var diff = Target - getPos();
            if (diff.Length() > 1) {
                diff.Normalize();
                Pos = Pos + diff * MoveSpeed;
            }
        }
    }
}
