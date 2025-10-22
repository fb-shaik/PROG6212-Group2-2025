using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attributes_Demo_WPF
{
    public class Student
    {
        [Required(ErrorMessage ="Name is required.")]
        [StringLength(50, ErrorMessage ="Name must be at most 50 characters")]
        public string Name { get; set; }
        
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Invalid email")]
        public string Email { get; set; }

        [Range(16,35, ErrorMessage ="Age must be between 16 & 35")]
        public int Age { get; set; }
    }
}
