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

namespace Attributes_Employee_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Employee employee;
        public MainWindow()
        {
            InitializeComponent();
            employee = new Employee();
            this.DataContext = employee;
        }

        private void Validate_Click(object sender, RoutedEventArgs e)
        {
            var context = new ValidationContext(employee, null, null);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            bool isValid = Validator.TryValidateObject(employee, context, results, true);

            if (isValid)
            {
                ResultText.Text = $"Validation passed.\nEmployee: {employee.updatedGetValues()}";
                ResultText.Foreground = System.Windows.Media.Brushes.Green;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                foreach (var validationResult in results)
                {
                    sb.AppendLine(validationResult.ErrorMessage);
                }
                ResultText.Text = sb.ToString();
                ResultText.Foreground = System.Windows.Media.Brushes.Red;
            }
        }
    }
    }
