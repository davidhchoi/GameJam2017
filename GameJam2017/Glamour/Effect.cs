using System;

namespace GameJam2017.Glamour {
    public class Effect {
        public static Effect [] effects = new Effect[3];

        public enum Type {
            Damage,
            MindControl,
            Spawn
        }

        public Type T { get; }

        private Effect(Type t) {
            this.T = t;
        }

        public int Cost() {
            switch (T) {
                case Type.Damage: return 1; 
                case Type.MindControl: return 0;
                case Type.Spawn: return 2;
            }
            throw new Exception("Something went wrong");
        }

        public static void Initialize() {
            for (int i = 0; i < effects.Length; i++) {
                effects[i] = new Effect((Type)i);
            }
        }
    }
}