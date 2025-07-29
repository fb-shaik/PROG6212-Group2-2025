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

namespace Health_tracker_app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //list a hold all step-counting devices entered by the user via the WPF
        //make use of type T for the Step-Counter class
        private List<StepCounter> devices = new();

        public MainWindow()
        {
            InitializeComponent();
            RefreshList(); //populates the UI with the current list of devices
        }

        //create a method to refresh the ListBoxUI with the output of all devices
        private void RefreshList() 
        { 
            DeviceList.Items.Clear(); //remove the current lists items
            //loop through each device & add its formatted string to the ListBox
            foreach (var device in devices)
            {
                DeviceList.Items.Add(device.Format());
            }
        
        }

        private void AddDevice_Click(object sender, RoutedEventArgs e)
        {
            /*try-catch to capture the info of the user input input
             *  
                device name
                validate the step count
                validate the confidence 
                create a new step counter device with the given input
                update the UI with the new device
                clear the input      

             */

            try
            {
                string name = DeviceNameBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(name))
                    throw new Exception("Device name is required.");

                if (!int.TryParse(StepCountBox.Text, out int steps) || steps < 0)
                    throw new Exception("Steps must be a non-negative integer.");

                if (!float.TryParse(ConfidenceBox.Text, out float conf) || conf < 0 || conf > 1)
                    throw new Exception("Confidence must be a number between 0 and 1.");

                var device = new StepCounter(name, steps, conf);
                devices.Add(device);
                RefreshList();

                DeviceNameBox.Clear();
                StepCountBox.Clear();
                ConfidenceBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void CombineDevices_Click(object sender, RoutedEventArgs e)
        {

            /*
                try-catch
                    - must have at least 2 devices to combine step count (list view)
                    - start with the first device as a base [0]
                    - use the operator overloading to combine all the devices into one
                    - display the result in the summary text block
                    - error msg if combining fails 
             */

            try
            {
                if (devices.Count < 2)
                    throw new Exception("Add at least 2 devices to combine.");

                var combined = devices[0];
                for (int i = 1; i < devices.Count; i++)
                {
                    combined += devices[i];
                }

                SummaryText.Text = $"Combined: {combined.StepCount:N0} steps | Confidence: {combined.Confidence * 100:F1}%";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Combine Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BoostSteps_Click(object sender, RoutedEventArgs e)
        {

            /* Try-catch
             * Check if there any devices on the list view
             * Create an array of step counts
             * Use unsafe code to boost step count via pointer manipulation
             * Update each device with the adjusted count
             * Show message success
             * Error message 
             */

            try {
                if (devices.Count == 0)
                    throw new Exception("No devices to boost");

                int[] stepsArray = new int[devices.Count];
                for (int i = 0; i < devices.Count; i++)
                    stepsArray[i] = devices[i].StepCount;
            
                //unsafe code
                unsafe
                {
                    fixed (int* ptr = stepsArray)
                    { 
                        //Boost by count
                        PointerUtils.BoostSteps(ptr, stepsArray.Length, 500);
                    }
                }

                for (int i = 0; i < devices.Count; i++)
                    devices[i].StepCount = stepsArray[i];

                RefreshList();
                MessageBox.Show(" Steps boosted by 500 for all devices!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            catch (Exception ex){ 
                MessageBox.Show($"!{ ex.Message}", "Boost Error", MessageBoxButton.OK, MessageBoxImage.Error);
            
            
            }













        }
    }
}