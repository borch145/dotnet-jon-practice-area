using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceExercise
{
    class Receptionist : IEmployee
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public decimal WeeklySalary { get; set; }
        public decimal PaidToDate { get; set; }

        public Receptionist(string name, decimal weeklySalary)
        {
            Name = name;
            Title = "Receptionist";
            WeeklySalary = weeklySalary;
            PaidToDate = 0;
        }


        //    public void GetPaid()
        //    {
        //        PaidToDate = PaidToDate + WeeklySalary;
        //        Console.WriteLine(Title + " " + Name + " has been paid $" + WeeklySalary + ". They have been paid $" + PaidToDate + " to date.");
        //    }
    }
}
