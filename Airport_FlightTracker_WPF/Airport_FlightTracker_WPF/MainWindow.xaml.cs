using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Airport_FlightTracker_WPF
{ /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
    public partial class MainWindow : Window
    {
        // File paths for boarded and cancelled logs
        private readonly string flightLogPath;
        private readonly string cancelledLogPath;

        // Logger instance to handle file writing
        private readonly FlightLogger logger;

        public MainWindow()
        {
            InitializeComponent();

            // Define file paths relative to AppDomain
            flightLogPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "flight_log.txt");
            cancelledLogPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cancelled_log.txt");

            // Initialize logger
            logger = new FlightLogger(flightLogPath, cancelledLogPath);

            try
            {
                // Ensure both files exist
                if (!File.Exists(flightLogPath))
                    File.Create(flightLogPath).Close();

                if (!File.Exists(cancelledLogPath))
                    File.Create(cancelledLogPath).Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing files: " + ex.Message);
            }

            // Button event handlers
            btnCanceled.Click += BtnCanceled_Click;
            btnViewFlight.Click += BtnViewFlight_Click;
            btnViewCancelled.Click += BtnViewCancelled_Click;
        }

        /// <summary>
        /// Handles the "Boarded" button click (synchronous)
        /// </summary>
        private void btnBoarded_Click(object sender, RoutedEventArgs e)
        {
            string name = txtPassengerName.Text.Trim();
            string seat = txtSeatNumber.Text.Trim();

            if (IsValidInput(name, seat))
            {
                try
                {
                    logger.LogBoardedPassenger(name, seat);
                    ShowMessage.Text = "Passenger marked as Boarded.";
                }
                catch (Exception ex)
                {
                    ShowMessage.Text = "Error logging boarded passenger: " + ex.Message;
                }
            }
        }

        /// <summary>
        /// Handles the "Boarded Async" button click
        /// </summary>
        private async void btnBoardedAsync_Click(object sender, RoutedEventArgs e)
        {
            string name = txtPassengerName.Text.Trim();
            string seat = txtSeatNumber.Text.Trim();

            if (IsValidInput(name, seat))
            {
                try
                {
                    await logger.LogBoardedPassengerAsync(name, seat);
                    ShowMessage.Text = "Passenger (Async) marked as Boarded.";
                }
                catch (Exception ex)
                {
                    ShowMessage.Text = "Async error: " + ex.Message;
                }
            }
        }

        /// <summary>
        /// Handles the "Cancelled" button click
        /// </summary>
        private async void BtnCanceled_Click(object sender, RoutedEventArgs e)
        {
            string name = txtPassengerName.Text.Trim();
            string seat = txtSeatNumber.Text.Trim();

            if (IsValidInput(name, seat))
            {
                try
                {
                    await logger.LogCancelledPassengerAsync(name, seat);
                    ShowMessage.Text = "Passenger marked as Cancelled.";
                }
                catch (Exception ex)
                {
                    ShowMessage.Text = "Error cancelling passenger: " + ex.Message;
                }
            }
        }

        /// <summary>
        /// Opens the flight log file using the default text editor (e.g., Notepad)
        /// </summary>
        private void BtnViewFlight_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = flightLogPath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                ShowMessage.Text = "Could not open flight log: " + ex.Message;
            }
        }

        /// <summary>
        /// Opens the cancelled log file
        /// </summary>
        private void BtnViewCancelled_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = cancelledLogPath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                ShowMessage.Text = "Could not open cancelled log: " + ex.Message;
            }
        }

        private void btnVisaCheck_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Launching the Emirates visa/passport info page
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://www.emirates.com/za/english/before-you-fly/visa-passport-information/",
                    UseShellExecute = true // Required to open URL in default browser
                });
            }
            catch (Exception ex)
            {
                ShowMessage.Text = "Could not open visa website: " + ex.Message;
            }
        }


        /// <summary>
        /// Validates user input for passenger name and seat
        /// </summary>
        private bool IsValidInput(string name, string seat)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(seat))
            {
                ShowMessage.Text = "Please enter both name and seat number.";
                return false;
            }
            return true;
        }
    }
}
