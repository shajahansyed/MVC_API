using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvWorksDAL
{
    public class AdvWorksDataAccessLayer
    {
        SqlConnection conObj;
        SqlCommand cmdObj;
        public AdvWorksDataAccessLayer()
        {
            conObj = new SqlConnection(ConfigurationManager.ConnectionStrings["AdvWorksConnectionStr"].ConnectionString);
        }

        public int ConnectionToDB()
        {
            try
            {
                string sqlConStr = ConfigurationManager.ConnectionStrings["AdvWorksConnectionStr"].ConnectionString;
                SqlConnection conObj = new SqlConnection(sqlConStr);
                conObj.Open();
                if (conObj.State.ToString() == "Open")
                    return 1;
                else
                    return -1;
            }
            catch (Exception ex)
            {
                return -99;
            }
        }
        public void FetchAllDept()
        {
            try
            {
                cmdObj = new SqlCommand(@"SELECT Name,GroupName FROM HumanResources.Department",conObj);
                conObj.Open();
                SqlDataReader drDept = cmdObj.ExecuteReader();
                while (drDept.Read())
                {
                    Console.WriteLine(drDept["Name"]+" | "+drDept["GroupName"]);
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                conObj.Close();
            }
        }
        public void ProductList()
        {
            try
            {
                cmdObj = new SqlCommand(@"SELECT ProductID,Name,ProductNumber,ListPrice FROM Production.Product where ListPrice > 10.0 ORDER BY ListPrice", conObj);
                conObj.Open();
                SqlDataReader drProduct = cmdObj.ExecuteReader();
                while (drProduct.Read())
                {
                    Console.WriteLine(drProduct["ProductID"] + " | " + drProduct["Name"] + " | " + drProduct["ProductNumber"] + " | " + drProduct["ListPrice"]);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conObj.Close();
            }
        }
    }
}
