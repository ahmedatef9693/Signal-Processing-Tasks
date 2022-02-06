using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Normalizer : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputMinRange { get; set; }
        public float InputMaxRange { get; set; }
        public Signal OutputNormalizedSignal { get; set; }

        public float GetMin(Signal Sig)
        {
            float Min = Sig.Samples[0];
            
            for (int i = 1; i < Sig.Samples.Count; i++)
            {
                
                if(Sig.Samples[i] < Min)
                {
                    Min = Sig.Samples[i];
                    
                }
            }
            return Min;
        }
        public float GetMax(Signal Sig)
        {
            float Max = Sig.Samples[0];
            Console.WriteLine("Hello");
            for (int i = 1; i < Sig.Samples.Count; i++)
            {
                

                if (Sig.Samples[i] > Max)
                {
                    Max = Sig.Samples[i];
                }
                
            }
            return Max;
        }
        public override void Run()
        {
            //Equation Of Normalization to range from 0 to 1
            //((X[:,i]-min(X[:,i]))/(max(X[:,i])-min(X[:,i])))*(b-a)+a

            List<float> Normalized = new List<float>();
            this.OutputNormalizedSignal = new Signal(Normalized, false);
            //getting min value for the signal column
            float Min = GetMin(InputSignal);
            //getting max value for the signal column
            float Max = GetMax(InputSignal);
            float Diff = Max - Min;
            
            float MinRange = this.InputMinRange;
            float MaxRange = this.InputMaxRange;
            Console.WriteLine(MinRange);
            Console.WriteLine(MaxRange);
            if (MinRange == 0 && MaxRange == 1)
            {
                for (int i = 0; i < InputSignal.Samples.Count; i++)
                {

                    float Value = (InputSignal.Samples[i] - Min) / (Diff);
                    Value *= (MaxRange - MinRange) + MinRange;
                    Normalized.Insert(i, Value);
                    Value = 0;
                }
            }
            else if(MinRange == -1 && MaxRange == 1)
            {
                for (int i = 0; i < InputSignal.Samples.Count; i++)
                {

                    float Value = (InputSignal.Samples[i] - Min) / (Diff);
                    Value *= 2;
                    Value -= 1;
                    Normalized.Insert(i, Value);
                    Value = 0;
                }
            }




        }
    }
}
