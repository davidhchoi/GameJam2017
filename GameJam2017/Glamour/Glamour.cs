﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameJam2017.Content;
using GameJam2017.Glamour.Bullets;
using GameJam2017.Scene;
using GameJam2017.Unit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2017.Glamour {
    class Glamour : Entity {
        private GlamourColour c;
        private Shape s;
        private Effect e;
        private Alter [] a;

        public Glamour(GlamourColour c, Shape s, Effect e, Alter[] a, Vector2 pos, Vector2 size) : base(pos, size, Vector2.Zero) {
            this.c = c;
            this.s = s;
            this.e = e;
            this.a = new Alter[a.Length];
            Array.Copy(this.a, a, a.Length);
        }

        public override String ToString() {
            String st = "";
            for (int i = 0; i < a.Length; i++) {
                st += a.ToString() + " ";
            }
            st += c.C.ToString() + " " + s.T.ToString() + " " + e.T.ToString();
            return st;
        }

        public override void Draw(GameTime g, SpriteBatch spriteBatch) {
            Rectangle destination = new Rectangle(Pos.ToPoint(), Size.ToPoint());
            c.Draw(spriteBatch, destination);
            spriteBatch.Draw(Core.Rectangles[(int)Core.Colours.White], new Rectangle(
                destination.Left + destination.Width / 10, destination.Top + destination.Height * 6 / 10,
                destination.Width * 8 / 10, destination.Height * 3 / 10), Color.White * .4f);
            spriteBatch.DrawString(Core.freestyle12, ToString(),
                new Vector2(destination.Left, destination.Top), Color.Black);
        }

        public List<Bullet> Cast(Vector2 pos, float angle, Field f) {
            List<Bullet> bullets = new List<Bullet>();
            float startAngle = 0, endAngle = 0, increment = 0;
            switch (s.T) {
                case Shape.Type.Bullet:
                    startAngle = angle;
                    endAngle = angle + 0.5f;
                    increment = 1;
                    break;
                case Shape.Type.Circle:
                    startAngle = angle;
                    endAngle = angle + (float)(2 * Math.PI);
                    increment = (float)(Math.PI / 10);
                    break;
                case Shape.Type.Cone:
                    startAngle = angle - (float)(Math.PI / 2);
                    endAngle = angle + (float)(Math.PI / 2);
                    increment = (float)(Math.PI / 20);
                    break;
            }
            for (; startAngle < endAngle; startAngle += increment) {
                Bullet b = new Bullet(10, c.C, 5, startAngle, pos, new Vector2(5, 5), f);
                bullets.Add(b);
            }
            return bullets;
        }

        public static Glamour RandomGlamour(Vector2 pos, Vector2 size) {
            int i = Core.rnd.Next(GlamourColour.GlamourColours.Length);
            GlamourColour c = GlamourColour.GlamourColours[i];
            i = Core.rnd.Next(Shape.shapes.Length);
            Shape s = Shape.shapes[i];
            i = Core.rnd.Next(Effect.effects.Length);
            Effect e = Effect.effects[i];

            
            return new Glamour(c, s, e, new Alter[0], pos, size);
        }

        public static void Initialize() {
            GlamourColour.Initialize();
            Shape.Initialize();
            Effect.Initialize();
            Alter.Initialize();
        }
    }
}
