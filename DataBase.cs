using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using SQLite;

namespace ROI_app
{
    // Employees class
    public class Employee
    {
        //sets Id as the primary key
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(10)]
        public string EmployeeID { get; set; }
    }
    

    public class EmployeeDbContext
    {
        private SQLiteAsyncConnection _connection;

        public EmployeeDbContext()
        {
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "employees.db");

            // Check if the database file already exists
            if (!File.Exists(databasePath))
            {
                _connection = new SQLiteAsyncConnection(databasePath);
            }
            else
            {
                _connection = new SQLiteAsyncConnection(databasePath);
            }
        }

        //Creates a table for employee
        public async Task InitializeDatabaseAsync()
        {
            await _connection.CreateTableAsync<Employee>();
        }

        //Gets all employees from the Employee table
        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _connection.Table<Employee>().ToListAsync();
        }

        //Saves employee If the employee doesnt already exists 
        public async Task<int> SaveEmployeeAsync(Employee employee)
        {
            if (employee.Id == 0)
            {
                return await _connection.InsertAsync(employee);
            }
            else
            {
                //If employee exists the employee is updated
                return await _connection.UpdateAsync(employee);
            }
        }

        //Deletes an employee from the empployee table
        public async Task<int> DeleteEmployeeAsync(Employee employee)
        {
            return await _connection.DeleteAsync(employee);
        }
    }


    //Employee repository 
    public class EmployeeRepository
    {
        private readonly EmployeeDbContext _context;

        public EmployeeRepository()
        {
            _context = new EmployeeDbContext();
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _context.GetEmployeesAsync();
        }

        public async Task<int> SaveEmployeeAsync(Employee employee)
        {
            return await _context.SaveEmployeeAsync(employee);
        }

        public async Task<int> DeleteEmployeeAsync(Employee employee)
        {
            return await _context.DeleteEmployeeAsync(employee);
        }
    }
}
