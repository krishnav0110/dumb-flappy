using Maths;
using Genetic;

namespace Objects {
    public interface IEntity {
        public Vec2 Position { get; }

        public void update(float dt);
        public void render(Graphics g);
    }
}