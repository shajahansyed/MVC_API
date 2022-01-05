using AdvWorksBL;
using AdvWorksDTO;
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
                    //blObj.GetAllProducts();
                    //List<DeptDetailsDTO> deptLstFromBL = blObj.GetAllDepts();
                    //foreach (var dept in deptLstFromBL)
                    //{
                    //    Console.WriteLine(dept.DeptName+ " | "+dept.DeptGroupName);
                    //}
                    //List<ProductsDTO> prodLstFromBL = blObj.GetAllProducts();
                    //foreach (var prod in prodLstFromBL)
                    //{
                    //    Console.WriteLine(prod.ProdId + " | " + prod.ProdName + " | " + prod.ProdNum + " | " + prod.ProdListPrice);
                    //}
                    //Console.Write("Enter product name: ");
                    //string input = Console.ReadLine();
                    //List<ProductsDTO> searchedProdLstFromBL = blObj.SearchedProd(input);
                    //foreach (var prod in searchedProdLstFromBL)
                    //{
                    //    Console.WriteLine(prod.ProdId + " | " + prod.ProdName + " | " + prod.ProdNum + " | " + prod.ProdListPrice);
                    //}
                    Console.Write("Enter Department Name: ");
                    string deptName = Console.ReadLine();
                    Console.Write("Enter Department Group Name: ");
                    string deptGroupName = Console.ReadLine();
                    DeptDetailsDTO newDeptObj = new DeptDetailsDTO();
                    newDeptObj.DeptName = deptName;
                    newDeptObj.DeptGroupName = deptGroupName;
                    int returnValue = blObj.AddNewDept(newDeptObj);
                    if (returnValue == 1)
                        Console.WriteLine("Department Added Sucessfully");
                    else if (returnValue == -1)
                        Console.WriteLine("Department Name Cannot be NULL");
                    else if (returnValue == -2)
                        Console.WriteLine("Department Group Name Cannot be NULL");
                    else
                        Console.WriteLine("ERROR 101: Something went worng.... We will fix it");
                }
                else
                    Console.WriteLine("Database Connection Failed.");
                //Console.WriteLine(blObj.ConnectToDB().ToString());
            }
            catch (Exception)
            {
                Console.WriteLine("Devlopers crashed, We will fix it...");
            }
        }
    }
}
