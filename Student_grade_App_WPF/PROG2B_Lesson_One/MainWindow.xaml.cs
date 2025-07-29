using System;
using System.Collections.Generic;
using System.Windows;

namespace PROG2B_Lesson_One
{
    public partial class MainWindow : Window
    {
        private Dictionary<string, Student> students;

        public MainWindow()
        {
            InitializeComponent();

            students = new Dictionary<string, Student>(StringComparer.OrdinalIgnoreCase)
            {
                { "Alice", new Student("Alice") },
                { "Bob", new Student("Bob") },
                { "Charlie", new Student("Charlie") },
                { "Dana", new Student("Dana") }
            };

            StudentSelector.ItemsSource = students.Keys;
            StudentSelector.SelectedIndex = 0;
        }

        private Student GetSelectedStudent()
        {
            string selectedName = StudentSelector.SelectedItem as string;
            return students.TryGetValue(selectedName, out var student) ? student : null;
        }

        private void LoadStudent_Click(object sender, RoutedEventArgs e)
        {
            var student = GetSelectedStudent();
            if (student != null)
                MessageBox.Show($"Student {student.Name} loaded with GPA {student.GPA:F2}");
            else
                MessageBox.Show("Please select a student.");
        }

        private void GetGrade_Click(object sender, RoutedEventArgs e)
        {
            var student = GetSelectedStudent();
            if (student == null)
            {
                GradeOutput.Text = "Please select a student.";
                return;
            }

            string subject = SubjectInput.Text.Trim();

            if (string.IsNullOrEmpty(subject))
            {
                GradeOutput.Text = "Please enter a subject.";
                return;
            }

            if (student.Grades.TryGetValue(subject, out string grade))
            {
                GradeOutput.Text = $"Grade in {subject}: {grade}";
            }
            else
            {
                GradeOutput.Text = $"No grade found for subject '{subject}'.";
            }
        }

        private void CompareStudents_Click(object sender, RoutedEventArgs e)
        {
            var selected = GetSelectedStudent();
            if (selected == null)
            {
                ComparisonOutput.Text = "Please select a student.";
                return;
            }

            if (!students.TryGetValue("Bob", out var bob))
            {
                ComparisonOutput.Text = "Bob is not in the student list.";
                return;
            }

            if (selected == bob)
            {
                ComparisonOutput.Text = "Please select a different student to compare with Bob.";
                return;
            }

            string result = selected > bob
                ? $"{selected.Name} has a higher GPA than Bob."
                : $"Bob has a higher or equal GPA compared to {selected.Name}.";

            ComparisonOutput.Text = result;
        }
    }
}
