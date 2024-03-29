﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee(){Id=1,Name="Miraj",Email="miraj0072004@gmail.com",Department=Dept.HR},
                new Employee(){Id=2,Name="Nadia",Email="nadia0072004@gmail.com",Department=Dept.Maintenance},
                new Employee(){Id=3,Name="Rizna",Email="rizna0072004@gmail.com",Department=Dept.Payroll}
            };
        }

        public Employee AddEmployee(Employee employee)
        {
            employee.Id = _employeeList.Max(e=>e.Id) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        public Employee UpdateEmployee(Employee employeeChanges)
        {
            Employee employee = _employeeList.Find(e => e.Id == employeeChanges.Id);
            if (employee != null)
            {
                employee.Name = employeeChanges.Name;
                employee.Email = employeeChanges.Email;
                employee.Department = employeeChanges.Department;
            }

            return employee;
        }

        public Employee DeleteEmployee(int id)
        {
            Employee employee = _employeeList.Find(e => e.Id == id);
            if (employee!=null)
            {
                _employeeList.Remove(employee);
            }

            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int Id)
        {
            return this._employeeList.Find(e => e.Id == Id);
        }
    }
}
