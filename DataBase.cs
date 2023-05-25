﻿using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using SQLite;

namespace ROI_app
{
    // employees class
    public class Employee
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }
    }

    // sets employees to the database
    public class EmployeeDbContext
    {
        private SQLiteAsyncConnection _connection;

        public EmployeeDbContext()
        {
            string databasePath = Path.Combine(FileSystem.AppDataDirectory, "employees.db");
            _connection = new SQLiteAsyncConnection(databasePath);
            _connection.CreateTableAsync<Employee>().Wait();
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            // Retrieve all employees from the database asynchronously
            return await _connection.Table<Employee>().ToListAsync();
        }

        public async Task<int> SaveEmployeeAsync(Employee employee)
        {
            // Save or update an employee based on the provided Id
            if (employee.Id == 0)
            {
                return await _connection.InsertAsync(employee);
            }
            else
            {
                return await _connection.UpdateAsync(employee);
            }
        }

        public async Task<int> DeleteEmployeeAsync(Employee employee)
        {
            // Delete an employee from the database
            return await _connection.DeleteAsync(employee);
        }
    }

    // Initializes the database and performs migration if needed
    public static class DatabaseInitializer
    {
        public static void Initialize()
        {
            // No migration needed for SQLite
        }
    }

    // Repository class for performing CRUD operations on Employee
    public class EmployeeRepository
    {
        private readonly EmployeeDbContext _context;

        public EmployeeRepository()
        {
            _context = new EmployeeDbContext();
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            // Retrieve all employees from the database asynchronously
            return await _context.GetEmployeesAsync();
        }

        public async Task<int> SaveEmployeeAsync(Employee employee)
        {
            // Save or update an employee based on the provided Id
            return await _context.SaveEmployeeAsync(employee);
        }

        public async Task<int> DeleteEmployeeAsync(Employee employee)
        {
            // Delete an employee from the database
            return await _context.DeleteEmployeeAsync(employee);
        }
    }
}
