using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameJam2017.Content;
using GameJam2017.Unit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameJam2017.Particle;

namespace GameJam2017.Glamour.Bullets {
    public class Bullet : Unit.Unit {
        private int damage;
        public List<StatusEffect> StatusEffects { get; } = new List<StatusEffect>();
        private Unit.Unit isImmune = null;

        public static Texture2D[] textures = new Texture2D[Enum.GetValues(typeof(Core.Colours)).Length];

        public Bullet(int damage, Core.Colours colour, float moveSpeed, float angle, Vector2 pos, Vector2 size, Factions factions, Field f) : 
            base("bullet", pos, size, moveSpeed, factions, colour, f) {
            this.damage = damage;
            if (damage > 1000) {
                throw new Exception("awheh");
            }
            while (angle < 0) angle += (float) (Math.PI * 2);
            while (angle > (float) (Math.PI * 2)) angle -= (float) (Math.PI * 2);
            this.Angle = angle;
            MaxHealth = 0;
        }

        private bool split = false;
        private bool explode = false;
        private bool homing = false;

        public Bullet SetAlters(List<Alter> alters) {
            foreach (var alter in alters) {
                switch (alter.T) {
                    case Alter.Type.Explode: explode = true;
                        break;
                    case Alter.Type.Homing: homing = true;
                        break;
                    case Alter.Type.Split: split = true;
                        break;
                }
            }
            return this;
        }
        
        public void Apply(Unit.Unit u) {
            if (explode) {
                List<Controllable> allHit = f.AllEnemyWithin(this, 200);
                foreach (var u1 in allHit) {
                    u1.Health -= damage;
                }
            } else {
                u.Health -= damage;
            }
            Kill();
            foreach (var statusEffect in StatusEffects) {
                u.AddStatus(statusEffect);
            }
            Particle.Particle.CreateParticles(GetPos, f.scene, 5, 20);

            if (split) {
                for (int i = 0; i < 5; i++) {
                    Bullet b = new Bullet(damage, Colour, MoveSpeed, (float)(Core.rnd.NextDouble() * Math.PI * 2), GetPos, Size, Faction, f);
                    b.isImmune = u;
                    f.AddUnit(b);
                }
            }

            // Knockback
            u.Vel += new Vector2((float)Math.Sin(Angle) * MoveSpeed * 5, (float)Math.Cos(Angle) * MoveSpeed * 5);

        }

        public static void Initialize() {
            foreach (Core.Colours colour in Enum.GetValues(typeof(Core.Colours))) {
                textures[(int) colour] = Core.GetRecoloredCache("bullet", colour);
            }
        }

        public override void Update(GameTime gameTime) {
            Unit.Unit u = f.ClosestEnemy(this);
            if (u == isImmune) u = null;
            if (u != null) {
                if (u.Intersects(GetPos.ToPoint())) {
                    Apply(u);
                }
            }
            var move = new Vector2((float)Math.Sin(Angle) * MoveSpeed, (float)Math.Cos(Angle) * MoveSpeed);
            if (homing && u != null) move = move * .8f + (u.GetPos - GetPos) * .2f;
            Pos += move;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Draw(textures[(int)Colour], Pos, Color.White);
            base.Draw(gameTime, spriteBatch);
        }

        public override void Kill() {
            f.RemoveUnit(this);
        }
    }
}
