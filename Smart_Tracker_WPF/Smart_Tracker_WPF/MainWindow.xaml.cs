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

namespace Smart_Tracker_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Handle the Combine Parcels button
        //Demonstrates operator overloading as defined in the Parcel class
        private void CombineParcels_Click(object sender, RoutedEventArgs e)
        {
            //Convert UI text inputs for dimensions
            var p1 = new Parcel
                (
                    Convert.ToInt32(Length1.Text),
                    Convert.ToInt32(Width1.Text),
                    Convert.ToInt32(Height1.Text)
                );

            var p2 = new Parcel
               (
                   Convert.ToInt32(Length2.Text),
                   Convert.ToInt32(Width2.Text),
                   Convert.ToInt32(Height2.Text)
               );

            //Use overloaded + operator to "combine" two parcels 
            var combined = p1 + p2;

            //Display the combined parcel dimensions
            OutputBox.Text = $"Combined Parcel Dimensions:\n" +
                             $"Lenght: {combined.Lenght}, Width: {combined.Width}, Height: {combined.Height}";   
        }

        //Handles the Process info button
        //Demonstrates the use of 'dynamic'
        private void ProcessDynamic_Click(object sender, RoutedEventArgs e)
        {
            //Declare a dynamic variable to allow flexiable typing
            dynamic info = DynamicInput.Text;
            //Show the intitial tyoe & value
            OutputBox.Text = $"Original type: {info.GetType().Name}, value: {info}\n";

            //try to treat the input as an integer (e.g. weight)
            if (int.TryParse(info.ToString(), out int weight))
            {
                info = weight + 5; //add 5kg to simulate processing
                OutputBox.Text += $"Uploaded weight (+5kg) : {info}";
            }
            else
            {
                //Treat it as a text note if the input is not numeric
                info = info.ToString().ToUpper();
                OutputBox.Text += $"\nProcessed as message note: {info}";
            }
        }

        //Handles the Generate Summary button
        //Demonstrate the creation of an anonymous object for quick data summary
        private void GenerateSummary_Click(object sender, RoutedEventArgs e)
        {
            //create a temp object with custom properties
            var summary = new
            {
                Receiver = "Charlie",
                ETA = DateTime.Now.AddDays(3).ToShortDateString(),
                From = "Store Room 39",
                Status = "Pending"
            };

            // Display the summary — anonymous types are great for 1-off displays
            OutputBox.Text = $"Delivery Summary:\nTo: " +
                $"{summary.Receiver}\nETA: " +
                $"{summary.ETA}\nFrom: {summary.From}\nStatus: {summary.Status}";



        }
    }
}