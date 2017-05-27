using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameJam2017.Unit {
    class Enemy : Unit {
        Unit Target;
        int range = 50;
        public Enemy(Vector2 position, Field f) : base(Core.Game.Content.Load<Texture2D>("Units\\enemy"), position, 2.0f, f) { }
        
        public void Shoot(Unit u) {
            return;
        }

        public override void Update(GameTime time) {
            Target = f.ClosestUnit(GetPos);
            if (f.IsEnemyInRange(GetPos, Target, range)) {
                Shoot(Target);
            } else {
                var diff = Target.GetPos - Pos;
                if (diff.Length() > 5) {
                    diff.Normalize();
                    Pos = Pos + diff * MoveSpeed;
                    if (diff.X == 0) {
                        Angle = 0;
                    }
                    Angle = (float)(Math.Atan2(diff.Y, diff.X) + Math.PI);
                }
            }
        }
    }
}
