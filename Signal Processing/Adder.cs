using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Adder : Algorithm
    {
        public List<Signal> InputSignals { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            int NumberOfSignals = InputSignals.Count;
            List<float> output = new List<float>();
            this.OutputSignal = new Signal(output, false);
            int SignalLength = 0;

            for (int i = 0; i < InputSignals.Count; i++)
            {
                if (i + 1 < InputSignals.Count)
                {

                    if ((InputSignals[i].Samples.Count - InputSignals[i + 1].Samples.Count) == 0)
                    {

                        //Signals Are equal
                        SignalLength = InputSignals[0].Samples.Count;
                        for (int sample = 0; sample < SignalLength; sample++)
                        {
                            output.Insert(sample, InputSignals[i].Samples[sample] + InputSignals[i + 1].Samples[sample]);

                        }
                    }

                }


            }

        }
    }
}
