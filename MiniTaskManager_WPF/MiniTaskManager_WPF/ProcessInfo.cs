using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTaskManager_WPF
{
    public class ProcessInfo
    {
        public int Id { get; set; }
        public string ProcessName { get; set; }
        public double MemoryMB { get; set; }
        public Process BaseProcess { get; set; } // to reference actual process
    }
}
