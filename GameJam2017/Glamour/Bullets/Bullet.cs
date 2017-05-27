using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameJam2017.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2017.Glamour.Bullets {
    class Bullet : Entity {
        private int damage;
        private Core.Colours colour;
        List<StatusEffect> statusEffects = new List<StatusEffect>();

        public static Texture2D[] textures = new Texture2D[Enum.GetValues(typeof(Core.Colours)).Length];

        public Bullet(int damage, Core.Colours colour, Vector2 pos, Vector2 size, Vector2 vel) : base(pos, size, vel) {
            this.damage = damage;
            this.colour = colour;
        }

        public static void Initialize() {
            foreach (Core.Colours colour in Enum.GetValues(typeof(Core.Colours))) {
                textures[(int) colour] = Core.ReColor(Core.Game.Content.Load<Texture2D>("bullet"), colour);
            }
        }

        public override void Update(GameTime gameTime) {
            Pos += Vel;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Draw(textures[(int)colour], Pos, Color.White);
            base.Draw(gameTime, spriteBatch);
        }
    }
}
