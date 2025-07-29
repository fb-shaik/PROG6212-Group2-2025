using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health_tracker_app
{
    public static unsafe class PointerUtils
    {
        //unsafe pointer method to boost each step count by a given amount
        public static void BoostSteps(int* steps, int length, int boostAmount)
        {
            for (int i = 0; i < length; i++)
            {
                steps[i] += boostAmount;
            }
        }
        
    }
}
