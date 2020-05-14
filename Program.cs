using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvolutionalNeuralNetwork
{
    class Program
    {
        static void Main(string[] args)
        {

            NeuralNetwork neural = new NeuralNetwork();
            neural.Train();
            //neural.Filter(1,3,3);
            //
        }
    }
}
