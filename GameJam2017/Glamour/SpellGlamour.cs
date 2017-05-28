using System;
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
    public class SpellGlamour : Glamour {
        public Shape S { get; }
        public Effect E { get; }
        public Alter[] A { get; }

        public int Cost { get; }
        public int Damage { get; }

        public SpellGlamour(GlamourColour c, Shape s, Effect e, Alter[] a, Vector2 pos, Vector2 size) : base(pos, size) {
            this.C = c;
            this.S = s;
            this.E = e;
            this.A = new Alter[a.Length];
            Array.Copy(this.A, a, a.Length);

            Cost = c.Cost() + s.Cost() + e.Cost();
            foreach (var alter in a) {
                Cost += alter.Cost();
            }
        }

        public override String ToString() {
            String st = "";
            for (int i = 0; i < A.Length; i++) {
                st += A.ToString() + " ";
            }
            st += C.C.ToString() + " " + S.T.ToString() + " " + E.T.ToString();
            return st;
        }

        public override string Description() {
            string s = "";
            if (S.T == Shape.Type.Bullet) {
                if (E.T == Effect.Type.Damage) {
                    s += "Fire one bullet in front of you. ";
                } else if (E.T == Effect.Type.Spawn) {
                    s += "Spawn one ally in front of you. ";
                } else {
                    throw new Exception("Error");
                }
            } else {
                if (E.T == Effect.Type.Damage) {
                    s += "Fire bullets ";
                } else if (E.T == Effect.Type.Spawn) {
                    s += "Spawn allies ";
                } else {
                    throw new Exception("Error");
                }
                s += "in a " + S.T + "in front of you. ";
            }
            if (E.T == Effect.Type.Damage) {
                if (C.C == Core.Colours.Red) {
                    s += "Bullets do bonus damage over time.";
                } else if (C.C == Core.Colours.Blue) {
                    s += "Bullets slow enemies.";
                } else if (C.C == Core.Colours.Yellow) {
                    s += "Bullets do bonus damage.";
                }
            }
            return s;
        }

        public override void Cast(Vector2 pos, float angle, Field f) {
            List<Unit.Unit> newEntities = new List<Unit.Unit>();
            float startAngle = 0, endAngle = 0, increment = 0;
            switch (S.T) {
                case Shape.Type.Bullet:
                    startAngle = angle;
                    endAngle = angle + 0.5f;
                    increment = 1;
                    break;
                case Shape.Type.Circle:
                    startAngle = angle;
                    endAngle = angle + (float)(2 * Math.PI);
                    switch (E.T) {
                        case Effect.Type.Spawn:
                            increment = (float)(Math.PI / 4);
                            break;
                        case Effect.Type.Damage:
                            increment = (float)(Math.PI / 10);
                            break;
                        case Effect.Type.MindControl:
                            increment = (float)(Math.PI / 3);
                            break;
                    }
                    break;
                case Shape.Type.Cone:
                    startAngle = angle - (float)(Math.PI / 4);
                    endAngle = angle + (float)(Math.PI / 4);
                    switch (E.T) {
                        case Effect.Type.Spawn:
                            increment = (float)(Math.PI / 12);
                            break;
                        case Effect.Type.Damage:
                            increment = (float)(Math.PI / 30);
                            break;
                        case Effect.Type.MindControl:
                            increment = (float)(Math.PI / 9);
                            break;
                    }
                    break;
            }
            for (; startAngle < endAngle; startAngle += increment) {
                Unit.Unit ent;
                if (E.T != Effect.Type.Spawn) {
                    ent = new Bullet(10, C.C, 5, startAngle, pos, new Vector2(20, 20), Unit.Unit.Factions.P1, f);

                    if (C.C == Core.Colours.Red) {
                        (ent as Bullet).StatusEffects.Add(new StatusEffect(1, 120, Core.Colours.Red));
                    } else if (C.C == Core.Colours.Blue) {
                        (ent as Bullet).StatusEffects.Add(new StatusEffect(4, 120, Core.Colours.Blue));
                    }
                } else {
                    ent = new Minion("Units\\minion",
                        pos + new Vector2((float)Math.Sin(startAngle) * 10, (float)Math.Cos(startAngle) * 10),
                        Unit.Unit.Factions.P1, C.C, f);
                }
                newEntities.Add(ent);
                f.AddUnit(ent);
            }
//            return newEntities;
        }
    }
}
