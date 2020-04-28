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
            var filters = this.Filter(1, 3, 3);
            var output = this.ConvolutionLayer(pixelvalues, filters);
            output = this.ActivationLayer(output);
            output = this.MaxPoolingLayer(output, 2);
            this.ActivationLayer(output);
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
            double[,,] output = new double[input.GetLength(0), input.GetLength(1), input.GetLength(2)];
            try
            {
                //Formula for MaxPooling
                // newwidth = Width/filtersize and newheight = Height/filtersize
                // width+newwidth + width and height+newheight +heigth
                var newWidth = input.GetLength(1) / filtersize;
                var newHeight = input.GetLength(2) / filtersize;
                for (int j = 0; j < input.GetLength(0); j++)
                {
                    for (int k = 0; k < input.GetLength(1); k++)
                    {
                        var rowValue = input[j, k, 0] * newWidth + input[j, k, 0];
                        for (int l = 0; l < input.GetLength(2); l++)
                        {
                            var columnValue = input[j, k, l] * newHeight + input[j, k, l];
                            output[j, k, l] = input[j, k, l] > columnValue ? input[j, k, l] : columnValue; // using which is maximum value
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return output;
        }

        public void FlatternLayer(double[,,] input)
        {

        }

        public void FullyConnectedLayer(double[,,] input)
        {

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

    }
}
