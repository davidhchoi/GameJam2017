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

        public static Texture2D[] textures = new Texture2D[Enum.GetValues(typeof(Core.Colours)).Length];

        public Bullet(int damage, Core.Colours colour, float moveSpeed, float angle, Vector2 pos, Vector2 size, Factions factions, Field f) : 
            base("bullet", pos, size, moveSpeed, factions, colour, f) {
            this.damage = damage;
            while (angle < 0) angle += (float) (Math.PI * 2);
            while (angle > (float) (Math.PI * 2)) angle -= (float) (Math.PI * 2);
            this.Angle = angle;
        }

        public void Apply(Unit.Unit u) {
            u.Health -= damage;
            Kill();
            foreach (var statusEffect in StatusEffects) {
                u.AddStatus(statusEffect);
            }
            Particle.Particle.CreateParticles(GetPos, f.scene, 5);

            // Knockback
            u.Vel += new Vector2((float)Math.Sin(Angle) * MoveSpeed * 5, (float)Math.Cos(Angle) * MoveSpeed * 5);

        }

        public static void Initialize() {
            foreach (Core.Colours colour in Enum.GetValues(typeof(Core.Colours))) {
                textures[(int) colour] = Core.GetRecoloredCache("bullet", colour);
            }
        }

        public override void Update(GameTime gameTime) {
            Pos += new Vector2((float)Math.Sin(Angle) * MoveSpeed, (float)Math.Cos(Angle) * MoveSpeed);
            base.Update(gameTime);
            Unit.Unit u = f.ClosestEnemy(this);
            if (u != null) {
                if (u.Intersects(GetPos.ToPoint())) {
                    Apply(u);
                }
            }
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
