using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health_tracker_app
{
    public static class StepExtensions
    {
        //Extension method to format a StepCounter object into a readable string
        public static string Format(this StepCounter sc) 
        {
            return $"{sc.DeviceName}: {sc.StepCount:0} steps ({sc.Confidence * 100}% confidence)";
        
        }
    
    }
}
