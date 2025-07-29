using System;
using System.Collections.Generic;

namespace PROG2B_Lesson_One
{
    public class StudentGrades
    {
        private readonly Dictionary<string, string> grades;

        public StudentGrades()
        {
            grades = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) // Case-insensitive lookup
            {
                { "Math", "A" },
                { "Science", "B+" },
                { "History", "A-" }
            };
        }

        // Indexer to access grades by subject name
        public string this[string subject]
        {
            get
            {
                if (grades.TryGetValue(subject, out string grade))
                    return grade;
                else
                    throw new ArgumentException($"Subject '{subject}' not found.");
            }
            set
            {
                grades[subject] = value;
            }
        }

        public bool ContainsSubject(string subject)
        {
            return grades.ContainsKey(subject);
        }

        public Dictionary<string, string> GetAllGrades()
        {
            return new Dictionary<string, string>(grades); // Return a copy to protect internal state
        }
    }
}
