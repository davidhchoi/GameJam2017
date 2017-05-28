using System;

namespace GameJam2017.Glamour {
    public class Alter {
        public static Alter [] alters = new Alter[Enum.GetValues(typeof(Type)).Length];

        public enum Type {
            Split,
            Explode,
            Homing,
        };

        public Type T { get; }

        private Alter(Type t) {
            this.T = t;
        }

        public int Cost() {
            if (T == Type.Split) return 2;
            if (T == Type.Explode) return 1;
            if (T == Type.Homing) return 1;
            return 0;
        }

        public static void Initialize() {
            for (int i = 0; i < alters.Length; i++) {
                alters[i] = new Alter((Type)i);
            }
        }
    }
}