using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceExercise
{
    interface IEmployee
    {
        public string Name { get; set; } 
        public string Title { get; set; }
        public decimal WeeklySalary { get; set; }
        public decimal PaidToDate { get; set; }

        
    }

    

}
