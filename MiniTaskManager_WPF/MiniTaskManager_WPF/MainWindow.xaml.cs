using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MiniTaskManager_WPF
{
    public partial class MainWindow : Window
    {
        private List<ProcessInfo> allProcesses = new List<ProcessInfo>();
        private readonly string logFilePath = "ProcessLog.txt";

        public MainWindow()
        {
            InitializeComponent();
            LoadProcesses();
        }

        private void LoadProcesses()
        {
            try
            {
                allProcesses = Process.GetProcesses()
                    .OrderBy(p => p.ProcessName)
                    .Select(p =>
                    {
                        double memory = 0;
                        try { memory = p.WorkingSet64 / 1024.0 / 1024.0; } catch { }
                        return new ProcessInfo
                        {
                            Id = p.Id,
                            ProcessName = p.ProcessName,
                            MemoryMB = Math.Round(memory, 2),
                            BaseProcess = p
                        };
                    }).ToList();

                dgProcesses.ItemsSource = allProcesses;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load processes: {ex.Message}", "Error");
            }
        }

        private void RefreshProcessList_Click(object sender, RoutedEventArgs e)
        {
            LoadProcesses();
        }

        private void StartProcess_Click(object sender, RoutedEventArgs e)
        {
            string processName = txtProcessName.Text.Trim();
            if (string.IsNullOrWhiteSpace(processName))
            {
                MessageBox.Show("Please enter a process name.");
                return;
            }

            try
            {
                Process.Start(processName);
                LogAction($"Started process: {processName}");
                LoadProcesses();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not start process: {ex.Message}", "Error");
            }
        }

        private void KillSelected_Click(object sender, RoutedEventArgs e)
        {
            if (dgProcesses.SelectedItem is ProcessInfo info)
            {
                try
                {
                    string pname = info.ProcessName;
                    info.BaseProcess.Kill();
                    info.BaseProcess.WaitForExit();
                    LogAction($"Killed process: {pname}");
                    LoadProcesses();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to kill process: {ex.Message}", "Error");
                }
            }
            else
            {
                MessageBox.Show("Please select a process to kill.");
            }
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = txtSearch.Text.ToLower();
            dgProcesses.ItemsSource = allProcesses
                .Where(p => p.ProcessName.ToLower().Contains(query))
                .ToList();
        }

        private void dgProcesses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgProcesses.SelectedItem is ProcessInfo info)
            {
                txtMemoryUsage.Text = $"Selected: {info.ProcessName} | Memory: {info.MemoryMB} MB";
            }
        }

        private void LogAction(string message)
        {
            try
            {
                File.AppendAllText(logFilePath, $"{DateTime.Now}: {message}{Environment.NewLine}");
            }
            catch
            {
                // silently fail
            }
        }
    }

    public class ProcessInfo
    {
        public int Id { get; set; }
        public string ProcessName { get; set; }
        public double MemoryMB { get; set; }
        public Process BaseProcess { get; set; }
    }
}
