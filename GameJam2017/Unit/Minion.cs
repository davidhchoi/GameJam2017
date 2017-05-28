using System;
using GameJam2017.Glamour;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameJam2017.Glamour.Bullets;

namespace GameJam2017.Unit {
    public class Minion : Controllable {
        private SpellGlamour spell;
        private int reload;

        public Minion(UnitData data,
            Vector2 position, Factions faction, Core.Colours c, Field f) : base(data.Texture, position, 5.0f, faction, c, f) {
            range = data.Range;
            MaxHealth = data.Maxhealth;
            Health = data.Health;
            spell = data.Spell;
            reload = data.Reload;
        }
        int timeSinceLastShot = 0;

        protected override void Shoot(Unit u) {
            var diff = u.GetPos - GetPos;
            var dir = (float)(Math.Atan2(diff.X, diff.Y));
            Angle = (float)(-dir + Math.PI);

            if (timeSinceLastShot <= 0) {
                spell.Cast(dir, this, f);
                timeSinceLastShot = reload;
            }
        }

        public override void Update(GameTime g) {
            base.Update(g);
            timeSinceLastShot -= 1;
        }

        public override void Kill() {
            f.RemoveUnit(this);
            Particle.Particle.CreateParticles(GetPos, f.scene, 20, 30);
        }
    }
}