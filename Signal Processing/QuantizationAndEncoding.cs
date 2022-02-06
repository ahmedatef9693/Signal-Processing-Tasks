using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class QuantizationAndEncoding : Algorithm
    {
        // You will have only one of (InputLevel or InputNumBits), the other property will take a negative value
        // If InputNumBits is given, you need to calculate and set InputLevel value and vice versa
        public int InputLevel { get; set; }
        public int InputNumBits { get; set; }
        public Signal InputSignal { get; set; }
        public Signal OutputQuantizedSignal { get; set; }
        public List<int> OutputIntervalIndices { get; set; }
        public List<string> OutputEncodedSignal { get; set; }
        public List<float> OutputSamplesError { get; set; }
        public float GetMinAmplitude(Signal s)
        {
            float Min = s.Samples[0];
            for (int i = 1; i < s.Samples.Count; i++)
            {
                if (s.Samples[i] < Min)
                {
                    Min = s.Samples[i];
                }
            }
            return Min;
        }
        public float GetMaxAmplitude(Signal s)
        {
            float Max = s.Samples[0];
            for (int i = 1; i < s.Samples.Count; i++)
            {
                if (s.Samples[i] > Max)
                {
                    Max = s.Samples[i];
                }
            }
            return Max;
        }
        public override void Run()
        {
            List<float> outputquantizedsignal = new List<float>();
            List<int> outputintervalindices = new List<int>();
            List<string> outputencodedsignal = new List<string>();
            List<float> outputsampleserror = new List<float>();

            this.OutputQuantizedSignal = new Signal(outputquantizedsignal, false);
            this.OutputIntervalIndices = outputintervalindices;
            this.OutputEncodedSignal = outputencodedsignal;
            this.OutputSamplesError = outputsampleserror;




            float MaxAmplitude = GetMaxAmplitude(this.InputSignal);
            float MinAmplitude = GetMinAmplitude(this.InputSignal);
            float DeltaValue = 0.0f;
            double levels = 0;
            if (this.InputLevel != 0)
            {
                this.InputNumBits = Convert.ToInt32(Math.Log(this.InputLevel, 2));
                DeltaValue = (MaxAmplitude - MinAmplitude) / this.InputLevel;
                levels = Math.Pow(2, this.InputNumBits);
            }
            else
            {

                levels = Math.Pow(2, this.InputNumBits);
                this.InputLevel = Convert.ToInt32(levels);
                DeltaValue = (MaxAmplitude - MinAmplitude) / this.InputLevel;

            }


            //Making Ranges
            var MyRanges = new List<Tuple<float, float>>(Convert.ToInt32(levels));
            float StartRange = MinAmplitude + DeltaValue;
            MyRanges.Insert(0, new Tuple<float, float>(MinAmplitude, StartRange));

            for (int m = 1; m < MyRanges.Capacity; m++)
            {
                float Next = MyRanges[m - 1].Item2 + DeltaValue;
                MyRanges.Insert(m, new Tuple<float, float>(MyRanges[m - 1].Item2, Next));

            }


            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                for (int j = 0; j < MyRanges.Count; j++)
                {

                    if (InputSignal.Samples[i] >= MyRanges[j].Item1 && (Convert.ToSingle(Math.Round(InputSignal.Samples[i], 2)) <= Convert.ToSingle(Math.Round(MyRanges[j].Item2, 2))))
                    {


                        float MidPoint = 0;
                        outputintervalindices.Add(j+1);
                        outputencodedsignal.Add(Convert.ToString(j, 2).PadLeft(this.InputNumBits, '0'));

                        MidPoint = (MyRanges[j].Item1 + MyRanges[j].Item2) / 2;
                        outputquantizedsignal.Add(Convert.ToSingle(MidPoint));


                    }

                }
                float Error = 0;

                Error = outputquantizedsignal[i] - InputSignal.Samples[i];
                outputsampleserror.Add(Convert.ToSingle(Math.Round(Error, 3)));
            }

            /*-----------------------------------------------------------------------*/
            //Console.WriteLine("Output Quantized");
            //foreach (var output in outputquantizedsignal)
            //{
            //    Console.WriteLine(output);
            //}
            //Console.WriteLine("Output Ranges");
            //foreach (var output in MyRanges)
            //{
            //    Console.WriteLine(output);
            //}
            //Console.WriteLine("Output Errors");
            //foreach (var output in outputsampleserror)
            //{
            //    Console.WriteLine(output);
            //}
            //Console.WriteLine("Output Encodeing");
            //foreach (var output in outputencodedsignal)
            //{
            //    Console.WriteLine(output);
            //}
            //Console.WriteLine("Output Intervals Indices");
            //foreach (var output in outputintervalindices)
            //{
            //    Console.WriteLine(output);
            //}
            












        }
    }
}
