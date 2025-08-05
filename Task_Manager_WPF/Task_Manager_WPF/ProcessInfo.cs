using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager_WPF
{
    //Display the process data in the UI
    public class ProcessInfo
    {
        public int Id { get; set; }
        public string ProcessName { get; set; } = string.Empty;
        public double MemoryMB { get; set; }
        public Process BaseProcess { get; set; } = null!; 
    }
}
