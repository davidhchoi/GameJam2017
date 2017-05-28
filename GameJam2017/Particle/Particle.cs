using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameJam2017.Content;
using GameJam2017.Glamour;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2017.Particle {
    public class Particle : Entity {
        private Texture2D texture;
        private Scene.Scene s;
        public int Lifetime { get; private set; }

        public Particle(Vector2 pos, int strength, Scene.Scene s) : base(pos, new Vector2(10, 10), 
            new Vector2(Core.rnd.Next(strength * 2) - strength, Core.rnd.Next(strength * 2) - strength)) {
            Core.Colours c = (Core.Colours)Core.rnd.Next(Enum.GetValues(typeof(Core.Colours)).Length);
            texture = Core.GetRecoloredCache("particle", c);

            Lifetime = Core.rnd.Next(Core.FPS * 3) + Core.FPS;
            this.s = s;
            s.toAdd.Add(this);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            Pos += Vel;
            Vel /= 1.5f;
            Lifetime--;
            if (Lifetime <= 0) {
                s.toRemove.Add(this);
            }
        }

        public static void CreateParticles(Vector2 pos, Scene.Scene s, int num, int strength) {
            for (int i = 0; i < num; i++) {
                new Particle(pos, strength, s);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            base.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(texture, Pos, Color.White);
        }
    }
}
