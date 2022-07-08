using System.Drawing;

namespace MainEngine {
    public class Renderer : Form {

        private static int width = 800;
        private static int height = 600;

        private static object locker = new object();
        private Bitmap? buffer;

        public Engine? engine;



        public static int WIDTH { get{ return width; }}
        public static int HEIGHT { get{ return height; }}

        



        public Renderer() {
            InitializeComponent();
        }

        private void InitializeComponent() {
            this.Text = "ML viewer";
            this.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.DoubleBuffer, true
            );
            this.ClientSize = new Size(width, height);

            this.Resize += new EventHandler(createBuffer);
            this.Load += new EventHandler(createBuffer);
            this.Paint += new PaintEventHandler(renderToScreen);
            this.KeyDown += new KeyEventHandler(handleKeyDown);
            this.MouseDown += new MouseEventHandler(handleMouseDown);
            this.FormClosing += new FormClosingEventHandler(handleExit);
        }





        private void createBuffer(object? sender, EventArgs e) {
            if(buffer != null)
                buffer.Dispose();

            buffer = new Bitmap(ClientSize.Width, ClientSize.Height);
            width = ClientSize.Width;
            height = ClientSize.Height;
        }





        public void render() {
            if(buffer == null || engine == null)
                return;
            
            Graphics g;
            lock(locker) {
                g = Graphics.FromImage(buffer);
            }
            
            g.Clear(Color.Black);
            engine.render(g);

            this.Invalidate();
        }

        private void renderToScreen(object? sender, PaintEventArgs e) {
            if(buffer == null)
                return;

            lock(locker) {
                e.Graphics.DrawImageUnscaled(buffer, Point.Empty);
            }
        }





        private void handleKeyDown(object? sender, KeyEventArgs e) {
            if(engine == null)
                return;
            
            engine.handleKeyDown(e.KeyCode);
            e.Handled = true;
        }

        private void handleMouseDown(object? sender, MouseEventArgs e) {
            
        }





        private void handleExit(object? sender, EventArgs e) {
            if(engine == null)
                return;

            engine.loop.stop();
        }
    }
}