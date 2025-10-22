using System.ComponentModel.DataAnnotations;
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

namespace Attributes_Demo_WPF
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

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var student = new Student();
            {
                student.Name = NameBox.Text.Trim();
                student.Email = EmailBox.Text.Trim();
                student.Age = int.TryParse(AgeBox.Text , out var age) ? age : 0;
            }
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            bool isValid = Validator.TryValidateObject(student, new ValidationContext(student), results, true);


            if (isValid)
            {
                ResultBlock.Text = "✅ Registration successful!";
                ResultBlock.Foreground = Brushes.Green;
            }
            else
            {
                var errorMessages = results.ConvertAll(r => "- " + r.ErrorMessage);
                ResultBlock.Text = "❌ Errors:\n" + string.Join("\n", errorMessages);
                ResultBlock.Foreground = Brushes.Red;
            }

        }
    }
}