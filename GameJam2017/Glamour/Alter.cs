namespace GameJam2017.Glamour {
    public class Alter {
        public static Alter [] alters = new Alter[0];

        public enum Type {
        };

        public Type T { get; }

        private Alter(Type t) {
            this.T = t;
        }

        public int Cost() {
            return 0;
        }

        public static void Initialize() {
            for (int i = 0; i < alters.Length; i++) {
                alters[i] = new Alter((Type)i);
            }
        }
    }
}