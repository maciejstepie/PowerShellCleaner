using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PowerShellCleaner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string path = @"%userprofile%\AppData\Roaming\Microsoft\Windows\PowerShell\PSReadline\ConsoleHost_history.txt";
        string[] lines;
        public MainWindow()
        {
            InitializeComponent();
            LoadCommandHistory();
        }

        private void LoadCommandHistory()
        {
            string systemPath = Environment.ExpandEnvironmentVariables(path);
            lines = File.ReadAllLines(systemPath);

            foreach (string line in lines)
            {
                HistoryCommandsList.Items.Add(line);
            }
           
        }

        private void RemoveDuplicateButton_Click(object sender, RoutedEventArgs e)
        {
            //Double reverse to keep recent ones 
            lines = lines.Reverse().Distinct().Reverse().ToArray();

            HistoryCommandsList.Items.Clear();

            foreach (string line in lines)
            {
                HistoryCommandsList.Items.Add(line);
            }
        }

        private void RemoveLineButton_Click(object sender, RoutedEventArgs e)
        {
            HistoryCommandsList.Items.Remove(((Button)sender).DataContext);
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            string systemPath = Environment.ExpandEnvironmentVariables(path);
            await File.WriteAllLinesAsync(systemPath, lines);
        }

        private void RealodButton_Click(object sender, RoutedEventArgs e)
        {
            string systemPath = Environment.ExpandEnvironmentVariables(path);
            lines = File.ReadAllLines(systemPath);

            HistoryCommandsList.Items.Clear();

            foreach (string line in lines)
            {
                HistoryCommandsList.Items.Add(line);
            }
        }
    }
}
