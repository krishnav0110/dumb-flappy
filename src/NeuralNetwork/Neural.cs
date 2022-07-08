using Maths;

namespace NeuralNetwork {
    public class NeuralNet {
        private float learningRate;
        private int inputNodes, hiddenNodes, outputNodes;
        private Matrix hiddenWeights, outputWeights;
        private Matrix hiddenBias, outputBias;





        public float LearningRate {get { return learningRate; } set { learningRate = value; }}
        public int InputNodes {get { return inputNodes; }}
        public int HiddenNodes {get { return hiddenNodes; }}
        public int OutputNodes {get { return outputNodes; }}





        public NeuralNet(int inputNodes, int hiddenNodes, int outputNodes, float learningRate = 0.01f) {
            this.learningRate = learningRate;
            this.inputNodes  = inputNodes;
            this.hiddenNodes = hiddenNodes;
            this.outputNodes = outputNodes;

            this.hiddenWeights  = new Matrix(hiddenNodes, inputNodes);
            this.outputWeights = new Matrix(outputNodes, hiddenNodes);
            this.hiddenBias = new Matrix(hiddenNodes, 1);
            this.outputBias = new Matrix(outputNodes, 1);

            this.hiddenWeights.randomize(4);
            this.outputWeights.randomize(4);
            this.hiddenBias.randomize(4);
            this.outputBias.randomize(4);
        }

        public Matrix think(float[] inputArray) {
            Matrix inputs = new Matrix(inputArray);
            Matrix hidden = this.hiddenWeights * inputs;
            hidden = hidden + this.hiddenBias;
            hidden.map(Matrix.sigmoid);

            Matrix outputs = this.outputWeights * hidden;
            outputs = outputs + this.outputBias;
            outputs.map(Matrix.sigmoid);

            return outputs;
        }

        public void train(float[] inputArray, float[] targetArray) {
            Matrix inputs = new Matrix(inputArray);
            Matrix targets = new Matrix(targetArray);

            Matrix hidden = this.hiddenWeights * inputs;
            hidden = hidden + this.hiddenBias;
            hidden.map(Matrix.sigmoid);

            Matrix outputs = this.outputWeights * hidden;
            outputs = outputs + this.outputBias;
            outputs.map(Matrix.sigmoid);





            //errors = targets - outputs
            Matrix outputErrors = targets - outputs;

            //gradient = outputs * (1 - outputs)
            Matrix outputGradient = outputs.copy();
            outputGradient.map(Matrix.dsigmoid);
            outputGradient.hadamardProduct(outputErrors);
            outputGradient = outputGradient * this.learningRate;

            //deltas = gradients * hidden_T
            Matrix outputDeltas = outputGradient * hidden.transpose();

            //adjusting output weights
            this.outputWeights = this.outputWeights + outputDeltas;
            this.outputBias = this.outputBias + outputGradient;





            //errors = weights_T * outputErrors;
            Matrix hiddenErrors = this.outputWeights.transpose() * outputErrors;

            //gradient = outputs * (1 - outputs)
            Matrix hiddenGradient = hidden.copy();
            hiddenGradient.map(Matrix.dsigmoid);
            hiddenGradient.hadamardProduct(hiddenErrors);
            hiddenGradient = hiddenGradient * this.learningRate;

            //deltas = gradients * inputs_T
            Matrix hiddenDeltas = hiddenGradient * inputs.transpose();

            //adjusting hidden weights
            this.hiddenWeights = this.hiddenWeights + hiddenDeltas;
            this.hiddenBias = this.hiddenBias + hiddenGradient;
        }

        public void mutate(float mutationChance = 0.01f) {
            hiddenWeights.mutate(mutationChance);
            outputWeights.mutate(mutationChance);
            hiddenBias.mutate(mutationChance);
            hiddenBias.mutate(mutationChance);
        }

        public NeuralNet copy() {
            NeuralNet nnCopy = new NeuralNet(inputNodes, hiddenNodes, outputNodes);

            nnCopy.hiddenWeights = hiddenWeights.copy();
            nnCopy.outputWeights = outputWeights.copy();
            nnCopy.hiddenBias = hiddenBias.copy();
            nnCopy.outputBias = outputBias.copy();

            return nnCopy;
        }
    }
}