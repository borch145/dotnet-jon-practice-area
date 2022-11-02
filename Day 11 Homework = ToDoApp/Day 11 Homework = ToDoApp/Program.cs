using Day_11_Homework___ToDoApp.UI;
using System;

namespace Day_11_Homework___ToDoApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu startup = new Menu();
            startup.MainMenu();

        }
    }
}

/*
 
    UI Layer (Presentation)
        -A user can create, access, and modify ToDo lists of some nature
         Create new lists
                >create new or escape
         Create Task or Modify Task/List
                >Choose list or escape 
                    > create task or select Tasks or Rename List or Escape 
                        > Mark Complete/Edit/Delete
         View Lists and Tasks
                >View List >view Incomplete from all lists >View complete from all lists > 
                       |                     |                         |
               |duesoon|create|Del|  |duesoon|markcomp|Del|     |unmark|Del|Del All|
               |ESC|Mark|Rename|     |ESC|                      |ESC|

    Logic Layer (BLL)
        -The orchestration of workflows (create/read/update/delete -> CRUD)

    Models (Data Representation through classes)
        -The classes that represent the data being manipulated
                
                -ToDoItem Class (int id; string title; string body; DateTime created, DateTime complete by)
                -ToDoList (int id; string title; List<ToDoItem> toDoList; DateTime created)
                -ListTracker (List<ToDoList>;

            **example: class ToDoItem {int id; string contents;}
            
    Data Layer (Persistence Layer)
        -The layer responsible for accessing/storing/modifying data


*/
