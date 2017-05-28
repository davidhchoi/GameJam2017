using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameJam2017.Glamour;
using Microsoft.Xna.Framework;

namespace GameJam2017.Unit {
    public class UnitData {
        public const int SpellDamage = 100;

        public int Range { get; }
        public int Maxhealth { get; }
        public int Health { get; }
        public string Texture { get; }
        public SpellGlamour Spell { get; }
        public int Reload { get; }

        public UnitData(int range, int maxhp, int hp, string texture, SpellGlamour spell, int reload) {
            Range = range;
            Maxhealth = maxhp;
            Health = hp;
            Texture = texture;
            Spell = spell;
            Reload = reload;
        }

        public static UnitData PlayerUnitData = new UnitData(300, 500, 500, "Units\\player", 
            new SpellGlamour(20, Shape.shapes[(int)Shape.Type.Bullet], Effect.effects[(int)Effect.Type.Damage], new Alter[0]), 120);

        public static UnitData EnemyMinion = new UnitData(300, 100, 100, "Units\\enemy",
            new SpellGlamour(20, Shape.shapes[(int)Shape.Type.Bullet], Effect.effects[(int)Effect.Type.Damage], new Alter[0]), 120);

        public static UnitData AllyMinion = new UnitData(300, 100, 100, "Units\\minion",
            new SpellGlamour(20, Shape.shapes[(int)Shape.Type.Bullet], Effect.effects[(int)Effect.Type.Damage], new Alter[0]), 120);
    }
}
