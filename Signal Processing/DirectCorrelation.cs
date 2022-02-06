using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }
        public List<float> Shift(List<float> Samples)
        {
            float FirstElement = Samples.ElementAt(0);
            Samples.RemoveAt(0);
            Samples.Add(FirstElement);
            return Samples;

        }
        public List<float> NormalizeSignal(Signal InputSig)
        {
            List<float> OutputSigSamples = new List<float>();
            Normalizer Normalizer = new Normalizer();
            Normalizer.InputMinRange = 0;
            Normalizer.InputMaxRange = 1;
            Normalizer.InputSignal = new Signal(OutputSigSamples, true);
            if (Normalizer.InputSignal.Samples.Count != 0)
            {
                Normalizer.InputSignal.Samples.Clear();
                Console.WriteLine("Not Clear");
            }

            
            for (int i = 0; i < InputSig.Samples.Count; i++)
            {
                Normalizer.InputSignal.Samples.Insert(i, InputSig.Samples[i]);
            }
            
            Normalizer.Run();
            OutputSigSamples = Normalizer.OutputNormalizedSignal.Samples;


            return OutputSigSamples;
        }
        public override void Run()
        {
            List<float> outputnonnormalizedcorrelation = new List<float>();
            List<float> outputnormalizedcorrelation = new List<float>();
            List<float>OutputNormalizedSamples1 = new List<float> ();
            List<float>OutputNormalizedSamples2 = new List<float> ();
            this.OutputNonNormalizedCorrelation = outputnonnormalizedcorrelation;
            this.OutputNormalizedCorrelation = outputnormalizedcorrelation;
            int Points = InputSignal1.Samples.Count + InputSignal2.Samples.Count - 1;
            float SumNonNormalized = 0.0f;
            float SumNormalized = 0.0f;
            //Padding Signals
            int PaddingCount1 = Points - InputSignal1.Samples.Count;
            int PaddingCount2 = Points - InputSignal2.Samples.Count;

            for (int i = 0; i <PaddingCount1 ; i++)
            {
                this.InputSignal1.Samples.Add(0);
            }

            for (int j = 0; j < PaddingCount2; j++)
            {
                this.InputSignal2.Samples.Add(0);
            }


            OutputNormalizedSamples1 = NormalizeSignal(InputSignal1);
            OutputNormalizedSamples2 = NormalizeSignal(InputSignal2);



            for (int i = 0; i < Points; i++)
            {
                for (int j = 0; j < Points; j++)
                {
                    SumNonNormalized += InputSignal1.Samples[j] * InputSignal2.Samples[j];
                    SumNormalized += OutputNormalizedSamples1[j] * OutputNormalizedSamples2[j];
                }

                outputnonnormalizedcorrelation.Add(SumNonNormalized / Points);
                outputnormalizedcorrelation.Add(SumNormalized / Points);
                SumNonNormalized = 0;
                SumNormalized = 0;
                InputSignal2.Samples = Shift(InputSignal2.Samples);
                OutputNormalizedSamples2 = Shift(OutputNormalizedSamples2);     
            }




            //Console.WriteLine("First");
            //foreach (float val1 in OutputNormalizedSamples1)
            //{
            //    Console.WriteLine(val1);
            //}

            //Console.WriteLine("Second");
            //foreach (float val2 in OutputNormalizedSamples2)
            //{
            //    Console.WriteLine(val2);
            //}

            foreach (float f in outputnormalizedcorrelation)
            {
                Console.WriteLine(f);
            }






            
        }
    }
}