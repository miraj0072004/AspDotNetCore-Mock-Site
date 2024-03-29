﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class EditRoleViewModel
    {

        public EditRoleViewModel()
        {
            this.Users=new List<string>();
        }
        public string Id { get; set; }

        [Required(ErrorMessage = "The role name is required")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }

    }
}
