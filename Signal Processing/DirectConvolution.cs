using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            List<float> outputsignal = new List<float>();
            this.OutputConvolvedSignal = new Signal(outputsignal, false);
            int size = InputSignal1.Samples.Count + InputSignal2.Samples.Count - 1;
            float sum = 0.0f;
            for (int n = 0; n < size; n++)
            {
                for (int k = 0; k <= n; k++)
                {
                    if ( ((n - k) < InputSignal2.Samples.Count) && (k<InputSignal1.Samples.Count))
                    {
                        
                        sum += this.InputSignal1.Samples[k] * this.InputSignal2.Samples[n - k];
                        
                    }

                }
               
                outputsignal.Add(sum);
                sum = 0;
            }


        }
    }
}
