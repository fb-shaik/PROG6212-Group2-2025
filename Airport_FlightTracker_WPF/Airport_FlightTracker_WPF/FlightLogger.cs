using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_FlightTracker_WPF
{ /// <summary>
  /// FlightLogger handles writing passenger info to files using method overloading.
  /// </summary>
    public class FlightLogger
    {
        private readonly string boardedFilePath;
        private readonly string cancelledFilePath;

        public FlightLogger(string boardedPath, string cancelledPath)
        {
            boardedFilePath = boardedPath;
            cancelledFilePath = cancelledPath;
        }

        // Synchronous log for boarded passengers
        public void LogBoardedPassenger(string name, string seat)
        {
            string entry = $"{name},{seat},{DateTime.Now},Boarded{Environment.NewLine}";
            File.AppendAllText(boardedFilePath, entry);
        }

        // Asynchronous log for boarded passengers
        public async Task LogBoardedPassengerAsync(string name, string seat)
        {
            string entry = $"{name},{seat},{DateTime.Now},Boarded{Environment.NewLine}";
            await File.AppendAllTextAsync(boardedFilePath, entry);
        }

        // Asynchronous log for cancelled passengers
        public async Task LogCancelledPassengerAsync(string name, string seat)
        {
            string entry = $"{name},{seat},Cancelled{Environment.NewLine}";
            await File.AppendAllTextAsync(cancelledFilePath, entry);
        }
    }
}