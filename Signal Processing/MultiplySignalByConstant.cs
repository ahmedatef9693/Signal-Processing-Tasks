using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MultiplySignalByConstant : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputConstant { get; set; }
        public Signal OutputMultipliedSignal { get; set; }

        public override void Run()
        {
            List<float> Mylist = new List<float>();


            this.OutputMultipliedSignal = new Signal(Mylist, false);
            
            int SignalLength = this.InputSignal.Samples.Count;

            for (int i = 0; i < SignalLength; i++)
            {

                float temp = this.InputSignal.Samples.ElementAt(i) * this.InputConstant;
                Mylist.Insert(i, temp);
                

            }





        }
    }
}
