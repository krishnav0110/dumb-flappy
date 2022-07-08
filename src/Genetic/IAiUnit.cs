using NeuralNetwork;

namespace Genetic {
    public interface IAiUnit {
        public float[] Inputs { set; }
        public NeuralNet Brain { get; set; }
        public int Score { get; }
        public float Fitness { get; set; }

        public bool Alive{ get; }
    }
}