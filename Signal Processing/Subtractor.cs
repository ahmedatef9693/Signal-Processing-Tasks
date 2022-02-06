using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Subtractor : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputSignal { get; set; }

        /// <summary>
        /// To do: Subtract Signal2 from Signal1 
        /// i.e OutSig = Sig1 - Sig2 
        /// </summary>
        public override void Run()
        {
            List<float> output = new List<float>();
            this.OutputSignal = new Signal(output, false);

            int InputSignal1Length = InputSignal1.Samples.Count;
            int InputSignal2Length = InputSignal2.Samples.Count;

            if((InputSignal1Length-InputSignal2Length)==0)
            {
                //Same Signals Counter
                for (int i = 0; i < InputSignal1Length; i++)
                {
                    output.Insert(i, InputSignal1.Samples[i] - InputSignal2.Samples[i]);
                }


            }
            else
            {
                int Difference = Math.Abs(InputSignal1Length - InputSignal2Length);
                if (InputSignal1Length > InputSignal2Length)
                {
                    
                    for (int i = 0; i < Difference; i++)
                    {
                        InputSignal2.Samples.Add(0.0f);
                        
                    }

                }
                else if (InputSignal1Length < InputSignal2Length)
                {
                    for (int i = 0; i < Difference; i++)
                    {
                        InputSignal1.Samples.Add(0.0f);

                    }

                }
              
                

            }
            











        }
    }
}