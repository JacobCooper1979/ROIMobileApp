using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using SQLite;

namespace ROI_app
{
    //employees class
    public class Employee
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }

        [System.ComponentModel.DataAnnotations.MaxLength(50)]
        public string FirstName { get; set; }

        [System.ComponentModel.DataAnnotations.MaxLength(50)]
        public string LastName { get; set; }
    }

    // sets employees to the database
    public class EmployeeDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure the database path for SQLite
            string databasePath = Path.Combine(FileSystem.AppDataDirectory, "employees.db");
            optionsBuilder.UseSqlite($"Filename={databasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the constraints and properties for Employee 
            modelBuilder.Entity<Employee>()
                .Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Employee>()
                .Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);
        }
    }

    // Initializes the database and performs migration if needed
    public static class DatabaseInitializer
    {
        public static void Initialize()
        {
            using (var db = new EmployeeDbContext())
            {
                db.Database.Migrate();
            }
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
            return await _context.Employees.ToListAsync();
        }

        public async Task<int> SaveEmployeeAsync(Employee employee)
        {
            // Save or update an employee based on the provided Id
            if (employee.Id == 0)
            {
                _context.Employees.Add(employee);
            }
            else
            {
                _context.Employees.Update(employee);
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteEmployeeAsync(Employee employee)
        {
            // Delete an employee from the database
            _context.Employees.Remove(employee);
            return await _context.SaveChangesAsync();
        }
    }
}


