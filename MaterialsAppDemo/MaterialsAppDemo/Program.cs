using MaterialsAppDemo.Data;
using System;

namespace MaterialsAppDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
           Application application = new Application(new TxtDataSource());
            application.Run();
        }
    }
}
