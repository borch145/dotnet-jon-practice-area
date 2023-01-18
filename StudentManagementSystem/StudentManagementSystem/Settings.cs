using Microsoft.Extensions.Configuration;
using StudentManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementSystem
{
    public static class Settings
    {
        public static string SaveFilePath { get; set; }
        public static IDataSource DataSource { get; set; }

        static Settings()
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .Build();

            string filePath = config.GetSection("Settings:SaveFilePath").Value;
            SetFilePath(filePath);

            string dataSource = config.GetSection("Settings:DataMode").Value;
            SetDataSource(dataSource);
        }

        public static void SetFilePath(string filePath)
        {
            SaveFilePath= filePath;
        }
        public static void SetDataSource(string dataSource)
        {
            switch(dataSource) 
            {
                case "InMemoryData":
                    DataSource = new InMemoryData();
                    break;
                case "TextDataSource":
                    DataSource = new TextData();
                    break;
                default:
                    throw new Exception("Data source could not be configured.");
            }
        }
    }
}
