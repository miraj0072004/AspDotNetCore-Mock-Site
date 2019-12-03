﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required,MaxLength(50,ErrorMessage ="The name cannot exceed 50 characters")]
        
        public string Name { get; set; }
        [Required]
        [Display(Name="Office Email")]
        [EmailAddress(ErrorMessage ="Invalid Email Format")]
        public string Email { get; set; }
        [Required]
        public Dept? Department { get; set; }

        public string PhotoPath { get; set; }
        
    }
}
