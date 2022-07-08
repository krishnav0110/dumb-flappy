using System.Windows.Forms;
using System.Drawing;
using MainEngine;

namespace ML {
    public class Program {
        
        [STAThread]
        static void Main() {
            Engine engine = new Engine();
            engine.loop.start();

            Application.EnableVisualStyles();
            Application.Run(engine.renderer);
        }
    }
}