using System;
using System.Xml.Linq;

namespace InterfaceExercise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             -Create an interface for Employee, with properties for Salary and CurrentSavings, and a method GetPaid()

            -Create a few different classes of employee that implement the Employee interface (etc- Cashier : Employee, Driver : Employee)
             with varying salaries, and implement logic for handling an employee's CurrentSavings, as well as implementing the GetPaid() method

            */
            bool payDay = true;

            IEmployee employeeCEO = new CEO("Patty", 10000);
            IEmployee employeeReceptionist = new Receptionist("Bob", 1500);

            while (payDay)
            {
                GetPaid(employeeCEO);
                GetPaid(employeeReceptionist);
            }
        }

        public static void GetPaid(IEmployee employee)
        {
            employee.PaidToDate = employee.PaidToDate + employee.WeeklySalary;
            Console.WriteLine(employee.Title + " " + employee.Name + " has been paid $" + employee.WeeklySalary + ". They have been paid $" + employee.PaidToDate + " to date.");
            Console.ReadKey();
        }
    }
}
