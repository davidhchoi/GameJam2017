namespace GameJam2017.Glamour {
    public class Effect {
        public static Effect [] effects = new Effect[1];

        public enum Type {
            Damage
        };

        public Type T { get; }

        private Effect(Type t) {
            this.T = t;
        }

        public static void Initialize() {
            for (int i = 0; i < effects.Length; i++) {
                effects[i] = new Effect((Type)i);
            }
        }
    }
}