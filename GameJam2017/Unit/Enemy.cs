using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace GameJam2017.Unit {
    class Enemy : Unit {
        Vector2 Target;
        public void Initialize(Vector2 position) {
            base.Initialize(Core.Game.Content.Load<Texture2D>("Units\\enemy"), position, 2.0f);
        }

        public override void Update(GameTime time, List<Unit> other) {
            // Move towards the nearest unit
            Unit moveTo = other.Where(u => !(u is Enemy))
                .OrderBy(u => (u.getPos() - getPos()).Length())
                .First();
            Target = moveTo.getPos();

            var diff = Target - getPos();
            if (diff.Length() > 1) {
                diff.Normalize();
                Position = Position + diff * MoveSpeed;
            }
        }
    }
}
