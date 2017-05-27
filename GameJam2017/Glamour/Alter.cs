namespace GameJam2017.Glamour {
    public class Alter {
        public static Alter [] alters = new Alter[0];
        enum Type {
        };

        private Type t;
        private Alter(Type t) {
            this.t = t;
        }

        public static void Initialize() {
            for (int i = 0; i < alters.Length; i++) {
                alters[i] = new Alter((Type)i);
            }
        }
    }
}