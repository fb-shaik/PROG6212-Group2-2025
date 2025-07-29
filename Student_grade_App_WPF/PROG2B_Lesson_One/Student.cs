using System.Collections.Generic;

namespace PROG2B_Lesson_One
{
    public class Student
    {
        public string Name { get; }
        public double GPA { get; }
        public Dictionary<string, string> Grades { get; }

        public Student(string name)
        {
            Name = name;
            Grades = GenerateRandomGrades();
            GPA = CalculateGPA();
        }

        private Dictionary<string, string> GenerateRandomGrades()
        {
            // Simulate some default grades
            return new Dictionary<string, string>
            {
                { "Math", "A" },
                { "Science", "B+" },
                { "History", "A-" },
                { "English", "B" }
            };
        }

        private double CalculateGPA()
        {
            double total = 0;
            foreach (var grade in Grades.Values)
            {
                total += grade switch
                {
                    "A+" => 4.3,
                    "A" => 4.0,
                    "A-" => 3.7,
                    "B+" => 3.3,
                    "B" => 3.0,
                    "B-" => 2.7,
                    "C+" => 2.3,
                    "C" => 2.0,
                    "D" => 1.0,
                    _ => 0.0
                };
            }

            return Grades.Count > 0 ? total / Grades.Count : 0.0;
        }

        public static bool operator >(Student s1, Student s2) => s1.GPA > s2.GPA;
        public static bool operator <(Student s1, Student s2) => s1.GPA < s2.GPA;
    }
}
