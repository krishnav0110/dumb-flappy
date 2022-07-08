using System.Windows.Forms;

namespace InputsHandler {
    public static class KeyboardHandler {
        private static Keys keyPressed;

        public static Keys KeyPressed {
            get {
                Keys key = keyPressed;
                keyPressed = default;
                return key;
            }
            set { keyPressed = value; }
        }
    }
}