using StudentManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementSystem.Logic
{
    public class Manager
    {
        public IDataSource DataSource { get; set; }
        public Manager() 
        {
            DataSource = Settings.DataSource;
        }
    }
}
