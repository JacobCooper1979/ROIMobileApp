using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ROI_app.Models
{
    // Represents an employee
    public class Employee
    {
        private const string DefaultImage = "roi_logo.png";

        private ObservableCollection<Models.Employee> employees{ get; set; }

        public ObservableCollection<Models.Employee> Employees { get { return Employees; } }
        private string image;
        public string Image{get { return string.IsNullOrEmpty(image) ? DefaultImage : image; }set { image = value; }}

        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

namespace ROI_app
{
    // ViewEmployees content page
    public partial class ViewEmployees : ContentPage
    {
        private ObservableCollection<Models.Employee> Employees { get; set; }

        public ObservableCollection<Models.Employee> ShowEmployees { get { return Employees; } }

        public ViewEmployees()
        {
            InitializeComponent();
            //Initializes employees collection
            Employees = new ObservableCollection<Models.Employee>();
            //loads employees from their database
            LoadEmployees();
            //sets the binding for the grid
            BindingContext = this;
        }

        // Event handler for the Home button click
        private async void OnHomeButtonClicked(object sender, EventArgs e)
        {
            // Navigate back to the previous page
            await Navigation.PopAsync();
        }

        // Event handler for the Refresh button click
        private void OnRefreshButtonClicked(object sender, EventArgs e)
        {
            LoadEmployees();
        }


        private async void LoadEmployees()
        {
            // Clear existing employees
            Employees.Clear();

            // Loads employees from the EmployeeRepository
            EmployeeRepository employeeRepository = new EmployeeRepository();
            List<Employee> employees = await employeeRepository.GetEmployeesAsync();

            // Add employees to the Employees collection
            foreach (var employee in employees)
            {
                Employees.Add(new Models.Employee
                {
                    ID = employee.EmployeeID,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName
                });
            }
        }

    }
}

