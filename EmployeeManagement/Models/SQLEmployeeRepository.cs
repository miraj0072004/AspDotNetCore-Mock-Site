using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Models
{
    public class SqlEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        public SqlEmployeeRepository(AppDbContext context,ILogger<SqlEmployeeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public Employee AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public Employee DeleteEmployee(int id)
        {
            Employee employee = _context.Employees.Find(id);
            if (employee!=null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _context.Employees;
        }

        public Employee GetEmployee(int Id)
        {
            _logger.LogTrace("Trace Log");
            _logger.LogDebug("Debug Log");
            _logger.LogInformation("Information Log");
            _logger.LogWarning("Warning Log");
            _logger.LogError("Error Log");
            _logger.LogCritical("Critical Log");
            return _context.Employees.Find(Id);
        }

        public Employee UpdateEmployee(Employee employeeChanges)
        {
           var employee = _context.Employees.Attach(employeeChanges);
           employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
           _context.SaveChanges();
           return employeeChanges;
        }
    }
}
