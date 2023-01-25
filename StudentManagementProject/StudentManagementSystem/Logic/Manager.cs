using StudentManagementSystem.Data;
using StudentManagementSystem.Models;
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

        public Categorey ParseEnumIndexToCategorey(int categoreyIndex)
        {
            switch (categoreyIndex)
            {
                case 0:
                    return Categorey.Science;
                case 1:
                    return Categorey.Art;
                case 2:
                    return Categorey.Math;
                default:
                    return Categorey.Invalid;

            }
        }
    }
}
