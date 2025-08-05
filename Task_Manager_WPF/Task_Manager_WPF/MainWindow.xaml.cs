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

namespace Task_Manager_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Store all current process in the UI (List<ProcessInfo>)
        private List<ProcessInfo> allProcesses = new();

        //File path for logging the start/kill actions
        private readonly string logFilePath = "ProcessLog_Outcome.txt";

        //Logs the process action(start/kill) to a file with a timestamp
        //File.AppendAllText 
        private void LogAction(string message)
        {
            try 
            {
                File.AppendAllText(logFilePath,$"{DateTime.Now:G}: {message}{Environment.NewLine}");    
            }
            catch (Exception ex) 
            { }
        }

        //Retrieve the running system processes & populate the DataGrid
        private void LoadProcesses() 
        {
            try {

                allProcesses = Process.GetProcesses()
                    .OrderBy(p => p.ProcessName)
                    .Select(p => CreateProcessInfo(p))
                    .Where(p => p != null)
                    .ToList();

                dgProcesses.ItemsSource = allProcesses;
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Failed to load processes:{ex.Message}", "Error");
            }
            
        }

        private ProcessInfo? CreateProcessInfo(Process process)
        {
            try {
                double memoryMB = Math.Round(process.WorkingSet64 /1024.0 /1024.0, 2);

                return new ProcessInfo
                {
                    Id = process.Id,
                    ProcessName = process.ProcessName,
                    MemoryMB = memoryMB,
                    BaseProcess = process
                };
            
            }

            catch (Exception ex) 
            {
                return null; //Handle inaccesible process
            }
        }


        public MainWindow()
        {
            InitializeComponent();
            LoadProcesses();
        }

        //Start a process with the name specified in the textbox
        private void StartProcess_Click(object sender, RoutedEventArgs e)
        {
            string processName = txtProcessName.Text.Trim();
            if (string.IsNullOrEmpty(processName)) 
            {
                MessageBox.Show("Please enter a process name.");
                return;
            }
            try {
                Process.Start(processName);
                LogAction($"Started a process: {processName}"); //keep a log in the text file 
                LoadProcesses();
                
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Could not start the process: {ex.Message}", "Error");
            }

        }

        private void RefreshProcessList_Click(object sender, RoutedEventArgs e)
        {
            LoadProcesses(); //Reloads the process list
        }

        private void KillSelected_Click(object sender, RoutedEventArgs e)
        {
            if (dgProcesses.SelectedItem is not ProcessInfo selectedProcess)
            { MessageBox.Show("Please select a process to kill.");
                return;
            }
            try
            {
                selectedProcess.BaseProcess.Kill();
                selectedProcess.BaseProcess.WaitForExit();
                LogAction($"Killed process: {selectedProcess.ProcessName}");
                LoadProcesses();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to kill process: {ex.Message}", "Error");
            }
        }

        private void dgProcesses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgProcesses.SelectedItem is ProcessInfo info)
            {
                txtMemoryUsage.Text = $"Selected: {info.ProcessName} | Memory: {info.MemoryMB} MB";
            }
        }
        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {// Filters the process list based on search input.
            string query = txtSearch.Text.Trim().ToLower();

            dgProcesses.ItemsSource = allProcesses
                .Where(p => p.ProcessName.ToLower().Contains(query))
                .ToList();
        }


    }
}