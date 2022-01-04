using AdvWorksBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvWorksUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Welcome to Adventure Works");
                AdvWorksBusinessLayer blObj = new AdvWorksBusinessLayer();
                int result = blObj.ConnectToDB();
                if (result == 1)
                {
                    Console.WriteLine("Database Connection Established.");
                    //blObj.GetAllDepts();
                    blObj.GetAllProducts();
                }
                else
                    Console.WriteLine("Database Connection Failed.");
                //Console.WriteLine(blObj.ConnectToDB().ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Devlopers crashed, We will fix it...");
            }
        }
    }
}
