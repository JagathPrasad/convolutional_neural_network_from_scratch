using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvolutionalNeuralNetwork
{
    public class NeuralNetwork
    {

        //Convolution Layer
        //Activation Layer
        //Max Pooling Layer
        //Flattern Layer
        //Fully Connected Layer



        public NeuralNetwork()
        {

        }


        public void InitiateProcess()
        {
            //(double[][][] input, double[][][][] filter, double[] bias, int stride,double[][][] output, double[][][] dropOutMask, boolean isTest)
        }

        public void Train()
        {
            //Convert to Pixel Array
            Bitmap img = new Bitmap(@"C:\Users\Preethi\Desktop\medium\resoureces\sites.jpeg");
            double[,,] pixelvalues = new double[3, img.Width, img.Height];
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {

                    Color pixel = img.GetPixel(i, j);
                    pixelvalues[0, i, j] = pixel.R;
                    pixelvalues[1, i, j] = pixel.G;
                    pixelvalues[2, i, j] = pixel.B;
                    //if (pixel == *somecondition *)
                    //{
                    //    **Store pixel here in a array or list or whatever**
                    //}
                }
            }


            #region Resize Bitmap
            //Replace Logic
            //Bitmap result = new Bitmap(100, 100);
            //using (Graphics g = Graphics.FromImage(result))
            //{
            //    g.DrawImage(img, 0, 0, 100, 100);
            //}
            ////var imgResize = result;
            //result.Save(@"C:\Users\Preethi\Desktop\medium\resoureces\test.png", System.Drawing.Imaging.ImageFormat.Png);
            #endregion


            //Add Filter 
            dynamic output = this.Filter(1, 3, 3);
            //Convolution Layer
            output = this.ConvolutionLayer(pixelvalues, output);
            //Activation Layer using ReLU
            output = this.ActivationLayer(output);
            //Max Pooling Layer with 2x2 filter and strides 2
            output = this.MaxPoolingLayer(output, 2);
            //Flattern Layer
            output = this.FlatternLayer(output);
            double[] weights = this.RandomWeights(output.Length);
            //Fully Connected Layer
            output = this.FullyConnectedLayer(output, weights);
        }


        public double[,,] ConvolutionLayer(double[,,] input, double[,,,] filter)
        {
            double[,,] output = new double[input.GetLength(0), input.GetLength(1), input.GetLength(2)];
            try
            {
                //formula for output is Output = Input - Filter + 1  eg: Output = 255 - 5 + 1
                for (int i = 0; i < filter.GetLength(0); i++)
                {
                    for (int j = 0; j < input.GetLength(0); j++)
                    {
                        for (int k = 0; k < input.GetLength(1); k++)
                        {
                            for (int l = 0; l < input.GetLength(2); l++)
                            {
                                output[j, k, l] = input[j, k, l] * filter[i, j, k, l];
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {


            }
            return output;
        }


        public double[,,] ActivationLayer(double[,,] input)
        {
            double[,,] output = new double[input.GetLength(0), input.GetLength(1), input.GetLength(2)];
            try
            {
                for (int j = 0; j < input.GetLength(0); j++)
                {
                    for (int k = 0; k < input.GetLength(1); k++)
                    {
                        for (int l = 0; l < input.GetLength(2); l++)
                        {
                            output[j, k, l] = input[j, k, l] < 0 ? 0 : input[j, k, l];
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return output;
        }

        public double[,,] MaxPoolingLayer(double[,,] input, int filtersize)
        {
            double[,,] output = null;
            try
            {
                //Formula for MaxPooling
                // newwidth = Width/filtersize and newheight = Height/filtersize
                // width+newwidth + width and height+newheight +heigth

                var newHeight = ((input.GetLength(1) - filtersize) / 2) + 1;
                var newWidth = ((input.GetLength(2) - filtersize) / 2) + 1;
                //output= new double[input.GetLength(0), input.GetLength(1), input.GetLength(2)]; for (int j = 0; j < input.GetLength(0); j++)
                output = new double[input.GetLength(0), newHeight, newWidth];
                for (int j = 0; j < input.GetLength(0); j++)
                {
                    var cuurent_y = 0; var out_y = 0;
                    for (int k = cuurent_y + filtersize; k < input.GetLength(1); k++)
                    {
                        var cuurent_x = 0; var out_x = 0;
                        var rowValue = input[j, k, 0] * newWidth + input[j, k, 0];
                        for (int l = cuurent_x + filtersize; l < input.GetLength(2); l++)
                        {
                            var columnValue = input[j, k, l] * newHeight + input[j, k, l];
                            double maxValue = MaxValue(input, j, k, l, filtersize);
                            output[j, out_y, out_x] = input[j, k, l] > maxValue ? input[j, k, l] : maxValue; // using which is maximum value
                            cuurent_x = cuurent_x + 2;
                            out_x = out_x + 1;
                        }
                        cuurent_y = cuurent_y + 2;
                        out_y = out_y + 1;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return output;
        }



        public double MaxValue(double[,,] input, int i, int j, int k, int filtersize)
        {
            double maxValue = 0;
            try
            {
                for (int a = 0; a < j + filtersize; a++)
                {
                    for (int b = 0; b < k + filtersize; b++)
                    {
                        maxValue = maxValue < input[i, a, b] ? input[i, a, b] : maxValue;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return maxValue;
        }
        public double[] FlatternLayer(double[,,] input)
        {

            int rgbChannel = input.GetLength(0);
            int rowPixel = input.GetLength(1);
            int columnPixel = input.GetLength(2);
            int length = rgbChannel * rowPixel * columnPixel;
            double[] output = new double[length];
            try
            {
                int count = 0;
                for (int i = 0; i < rgbChannel; i++)
                {
                    for (int j = 0; j < rowPixel; j++)
                    {
                        for (int k = 0; k < columnPixel; k++)
                        {
                            output[count] = input[i, j, k];
                            count = count + 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return output;
        }

        public double FullyConnectedLayer(double[] input, double[] weights)
        {
            double sum = 0;
            try
            {
                for (int i = 0; i < input.Length; i++)
                {
                    sum = sum + (input[i] * weights[i]);
                }
            }
            catch (Exception ex)
            {


            }
            return sum;
        }



        public double[,,,] Filter(int filter, int nooffilters, int pixelsize)
        {
            double[,,,] doubleFilter = new double[filter, nooffilters, pixelsize, pixelsize];
            Random random = new Random();
            for (int i = 0; i < filter; i++)
            {
                for (int j = 0; j < nooffilters; j++)
                {
                    for (int k = 0; k < pixelsize; k++)
                    {
                        for (int l = 0; l < pixelsize; l++)
                        {
                            doubleFilter[i, j, k, l] = random.NextDouble();
                        }
                    }
                }
            }

            return doubleFilter;
        }


        public double[] RandomWeights(int count)
        {
            double[] weights = new double[count];
            Random random = new Random();
            try
            {
                for (int i = 0; i < count; i++)
                {
                    weights[i] = random.NextDouble();
                }
            }
            catch (Exception ex)
            {

            }
            return weights;
        }

    }
}
