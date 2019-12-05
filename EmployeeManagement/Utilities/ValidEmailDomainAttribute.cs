﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Utilities
{
    public class ValidEmailDomainAttribute : ValidationAttribute
    {
        private readonly string _allowedDomain;

        public ValidEmailDomainAttribute(string allowedDomain)
        {
            _allowedDomain = allowedDomain;
        }
        public override bool IsValid(object value)
        {
            //return base.IsValid(value);
            string[] strings = value.ToString().Split("@");
            return strings[1].ToUpper() == _allowedDomain.ToUpper();
        }
    }
}
