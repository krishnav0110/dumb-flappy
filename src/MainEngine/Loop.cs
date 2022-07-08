namespace MainEngine {
    public class Loop {
        public Engine? engine;

        private ThreadStart threadDelegate;
        private Thread thread;
        private bool running;

        public Loop() {
            running = false;
            threadDelegate = new ThreadStart(run);
            thread = new Thread(threadDelegate);
        }

        public void start() {
            if(this.engine == null)
                throw new ArgumentException("engine not loaded");

            running = true;
            thread.Start();
        }

        private void run() {
            if(engine == null)
                return;

            double FPS_CAP        = 1f / 60;
            DateTime previousTime = DateTime.Now;
            DateTime currentTime  = DateTime.Now;
            DateTime timer        = DateTime.Now;
            double delta = 0;
            int frames = 0;

            while(running) {
                currentTime = DateTime.Now;
                delta += (currentTime - previousTime).TotalSeconds;
                previousTime = currentTime;

                if(delta > FPS_CAP) {
                    engine.update((float) delta);
                    engine.renderer.render();
                    ++frames;
                    delta -= FPS_CAP;
                }

                if((DateTime.Now - timer).TotalSeconds >= 1) {
                    frames = 0;
                    timer = DateTime.Now;
                }
            }
        }

        public void stop() {
            running = false;
        }
    }
}