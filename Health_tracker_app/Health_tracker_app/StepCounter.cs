using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health_tracker_app
{
    public class StepCounter
    {
        // 3 fields (DeviceName, StepCount, Confidence
        public string DeviceName { get; set; }
        public int StepCount { get; set; }
        public double Confidence { get; set; }

        //Parameterized Constructor
        public StepCounter(string name, int steps, double confidence) 
        {
            DeviceName = name;
            StepCount = steps >=0 ? steps : 0;
            //Cofidence = Math.Clamp(confidence, 0f, 1f);
            Confidence = confidence <0f ? 0f : (confidence > 1f ? 1f : confidence);
            // condition ? true : false
        }

        //Overload + operator to combine 2 devices
        public static StepCounter operator +(StepCounter a, StepCounter b) 
        {
            return new StepCounter(
                    a.DeviceName + " + " + b.DeviceName,
                    a.StepCount + b.StepCount,
                    (a.Confidence + b.Confidence) /2
                );
        
        }

    }
}
