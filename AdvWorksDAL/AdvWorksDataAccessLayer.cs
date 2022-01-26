using AdvWorksDTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Linq.SqlClient;
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
        AdventureWorks2012Context contextObj;
        public AdvWorksDataAccessLayer()
        {
            conObj = new SqlConnection(ConfigurationManager.ConnectionStrings["AdvWorksConnectionStr"].ConnectionString);
            contextObj = new AdventureWorks2012Context();
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
        public List<DeptDetailsDTO> FetchAllDept()
        {
            try
            {
                cmdObj = new SqlCommand(@"SELECT Name,GroupName FROM HumanResources.Department",conObj);
                conObj.Open();
                SqlDataReader drDept = cmdObj.ExecuteReader();
                //while (drDept.Read())
                //{
                //    Console.WriteLine(drDept["Name"]+" | "+drDept["GroupName"]);
                //}
                List<DeptDetailsDTO> lstDept = new List<DeptDetailsDTO>();
                while (drDept.Read())
                {
                    DeptDetailsDTO deptFromReader = new DeptDetailsDTO();
                    deptFromReader.DeptName = drDept["Name"].ToString();
                    deptFromReader.DeptGroupName = drDept["GroupName"].ToString();
                    lstDept.Add(deptFromReader);
                    //lstDept.Add(new DeptDetailsDTO()
                    //{
                    //    DeptName = drDept["Name"].ToString(),
                    //    DeptGroupName = drDept["GroupName"].ToString()
                    //});
                }
                return lstDept;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conObj.Close();
            }
        }

        public List<DeptDetailsDTO> DeptSearchList(string deptGroupName)
        {
            try
            {

                var lstDept = contextObj.Departments.Where(w => w.GroupName.Contains(deptGroupName)).ToList();
                //var result = (from dept in contextObj.Departments
                //                  //where dept.GroupName == deptGroupName
                //              where SqlMethods.Like(dept.GroupName,"%"+deptGroupName+"%")
                //              select dept).ToList();
                List<Department> lstDeptsFromDB = contextObj.Departments.ToList();
                List<DeptDetailsDTO> lstDepts = new List<DeptDetailsDTO>();
                foreach (var dept in lstDept)
                {
                    lstDepts.Add(new DeptDetailsDTO()
                    {
                        DeptName = dept.Name,
                        DeptGroupName = dept.GroupName,
                    });
                }
                return lstDepts;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int AddNewProduct(ProductsDTO newProdObj)
        {
            try
            {
                Product prodObj = new Product();
                prodObj.Name = newProdObj.ProdName;
                prodObj.ProductNumber = newProdObj.ProdNum;
                prodObj.MakeFlag = true;
                prodObj.FinishedGoodsFlag = false;
                prodObj.SafetyStockLevel = 1000;
                prodObj.ReorderPoint = 750;
                prodObj.StandardCost = 0.00m;
                prodObj.ListPrice = 100.00m;
                prodObj.DaysToManufacture = 5;
                prodObj.SellStartDate = DateTime.Now;
                prodObj.rowguid = Guid.NewGuid();
                prodObj.ModifiedDate = System.DateTime.Now;

                contextObj.Products.Add(prodObj);
                return contextObj.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ProductsDTO> FetchMinMaxProdDetails(int min,int max)
        {
            try
            {
                //var lstProdListPrice = contextObj.Products.Where(w => w.ListPrice>min&&w.ListPrice<max).ToList();
                var result = (from prod in contextObj.Products
                              where prod.ListPrice > min && prod.ListPrice < max
                              orderby prod.ListPrice ascending
                              select prod).ToList();
                List<Product> lstProductsFromDB = contextObj.Products.ToList();
                List<ProductsDTO> lstProducts = new List<ProductsDTO>();
                foreach (var prod in result)
                {
                    lstProducts.Add(new ProductsDTO()
                    {
                        ProdId = prod.ProductID,
                        ProdName = prod.Name,
                        ProdNum = prod.ProductNumber,
                        ProdListPrice = prod.ListPrice,
                    });
                }
                return lstProducts;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int AddNewDeptUsingEF(DeptDetailsDTO newDeptObj)
        {
            try
            {   
                Department deptObj = new Department();
                deptObj.Name = newDeptObj.DeptName;
                deptObj.GroupName = newDeptObj.DeptGroupName;
                deptObj.ModifiedDate = System.DateTime.Now;
                
                contextObj.Departments.Add(deptObj);
                return contextObj.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int AddNewDepartment(DeptDetailsDTO newDeptObj, out int newDeptId)
        {
            try
            {
                newDeptId = 0;
                cmdObj = new SqlCommand();
                cmdObj.CommandText = "uspAddNewDept";
                cmdObj.CommandType = System.Data.CommandType.StoredProcedure;
                cmdObj.Connection = conObj;
                cmdObj.Parameters.AddWithValue("@deptName", newDeptObj.DeptName);
                cmdObj.Parameters.AddWithValue("@deptGroupName", newDeptObj.DeptGroupName);
                cmdObj.Parameters.AddWithValue("@deptDate", System.DateTime.Now);

                SqlParameter prcReturnValue = new SqlParameter();
                prcReturnValue.Direction = ParameterDirection.ReturnValue;
                prcReturnValue.SqlDbType = SqlDbType.Int;
                cmdObj.Parameters.Add(prcReturnValue);

                SqlParameter prcDeptIDOut = new SqlParameter();
                prcDeptIDOut.Direction = ParameterDirection.Output;
                prcDeptIDOut.SqlDbType = SqlDbType.Int;
                prcDeptIDOut.ParameterName = "@deptID";
                cmdObj.Parameters.Add(prcDeptIDOut);

                conObj.Open();
                cmdObj.ExecuteNonQuery();
                newDeptId = Convert.ToInt32(prcDeptIDOut.Value);
                return Convert.ToInt32(prcReturnValue.Value);
            }
            catch (Exception ex)
            {
                newDeptId = 0;
                return -99;
            }
            finally { conObj.Close(); }
        }

        public List<ProductsDTO> ProductList()
        {
            cmdObj = new SqlCommand(@"SELECT ProductID,Name,ProductNumber,ListPrice FROM Production.Product", conObj);
            SqlDataAdapter daProducts = new SqlDataAdapter(cmdObj);
            DataTable dtProductsFromDB = new DataTable();
            daProducts.Fill(dtProductsFromDB);
            List<ProductsDTO> lstProducts = new List<ProductsDTO>();
            foreach (DataRow prod in dtProductsFromDB.Rows)
            {
                ProductsDTO newObj = new ProductsDTO();
                newObj.ProdId = Convert.ToInt32(prod["ProductId"]);
                newObj.ProdName = Convert.ToString(prod["Name"]);
                newObj.ProdNum = Convert.ToString(prod["ProductNumber"]);
                newObj.ProdListPrice = Convert.ToInt32(prod["ListPrice"]);
                lstProducts.Add(newObj);
            }
            return lstProducts;
        }
        public List<ProductsDTO> ProdSearchList(string input)
        {
            try
            {
                cmdObj = new SqlCommand($@"SELECT ProductID,Name,ProductNumber,ListPrice FROM Production.Product WHERE Name LIKE '%{input}%' ORDER BY Name", conObj);
                SqlDataAdapter daProducts = new SqlDataAdapter(cmdObj);
                DataTable dtProductsFromDB = new DataTable();
                daProducts.Fill(dtProductsFromDB);
                List<ProductsDTO> lstProducts = new List<ProductsDTO>();
                foreach (DataRow prod in dtProductsFromDB.Rows)
                {
                    ProductsDTO newObj = new ProductsDTO();
                    newObj.ProdId = Convert.ToInt32(prod["ProductId"]);
                    newObj.ProdName = Convert.ToString(prod["Name"]);
                    newObj.ProdNum = Convert.ToString(prod["ProductNumber"]);
                    newObj.ProdListPrice = Convert.ToInt32(prod["ListPrice"]);
                    lstProducts.Add(newObj);
                }
                return lstProducts;
            }
            catch (Exception ex)
            {
                Exception exp = new Exception("Invalid user input");
                throw exp;
            }
        }
        public List<ProductsDTO> FetchProductsListUsingEF()
        {
            try
            {
                //contextObj.Products.ToList();
                //var lstProdListPrice = contextObj.Products.Where(w => w.ListPrice>100).OrderBy(o => o.ListPrice).ToList();
                var result = (from prod in contextObj.Products
                              where prod.ListPrice>10
                              orderby prod.ListPrice ascending
                              select prod).ToList();
                List<Product> lstProductsFromDB = contextObj.Products.ToList();
                List<ProductsDTO> lstProducts = new List<ProductsDTO>();
                foreach(var prod in result)
                {
                    lstProducts.Add(new ProductsDTO()
                    {
                        ProdId = prod.ProductID,
                        ProdName = prod.Name,
                        ProdNum = prod.ProductNumber,
                        ProdListPrice = prod.ListPrice,
                    });
                }
                //foreach (var prod in lstProdListPrice)
                //{
                //    lstProducts.Add(new ProductsDTO()
                //    {
                //        ProdId = prod.ProductID,
                //        ProdName = prod.Name,
                //        ProdNum = prod.ProductNumber,
                //        ProdListPrice = prod.ListPrice,
                //    });
                //}
            return lstProducts;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
