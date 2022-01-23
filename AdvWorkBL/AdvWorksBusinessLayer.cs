using AdvWorksDAL;
using AdvWorksDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvWorksBL
{
    public class AdvWorksBusinessLayer
    {
        AdvWorksDataAccessLayer dalObj;
        public AdvWorksBusinessLayer()
        {
            dalObj = new AdvWorksDataAccessLayer();
        }
        public int ConnectToDB()
        {
            try
            {
                AdvWorksDataAccessLayer dalObj = new AdvWorksDataAccessLayer();
                return dalObj.ConnectionToDB();
            }
            catch (Exception ex)
            {
                return -89;
            }
        }
        public List<DeptDetailsDTO> GetAllDepts()
        {
            AdvWorksDataAccessLayer dalObj = new AdvWorksDataAccessLayer();
            List<DeptDetailsDTO> deptLstFromDAL = dalObj.FetchAllDept();
            //dalObj.FetchAllDept();
            return deptLstFromDAL;
        }
        public List<ProductsDTO> GetAllProducts()
        {
            AdvWorksDataAccessLayer dalObj = new AdvWorksDataAccessLayer();
            List<ProductsDTO> prodLstFromDAL = dalObj.ProductList();
            return prodLstFromDAL;
        }

        public List<DeptDetailsDTO> SearchDept(string deptGroupName)
        {
            List<DeptDetailsDTO> searchedDeptLstFromDAL = dalObj.DeptSearchList(deptGroupName);
            return searchedDeptLstFromDAL;
        }

        public int AddNewDeptEF(DeptDetailsDTO newDeptObj)
        {
            try
            {
                if (String.IsNullOrEmpty(newDeptObj.DeptName))
                    return -1;
                if (String.IsNullOrEmpty(newDeptObj.DeptGroupName))
                    return -2;
                else
                    return dalObj.AddNewDeptUsingEF(newDeptObj);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ProductsDTO> SearchedProd(string input)
        {
            List<ProductsDTO> searchedProdLstFromDAL = dalObj.ProdSearchList(input);
            return searchedProdLstFromDAL;
        }

        public int AddNewDept(DeptDetailsDTO newDeptObj, out int newDeptId)
        {
            if (String.IsNullOrEmpty(newDeptObj.DeptName))
            {
                newDeptId = 0;
                return -1;
            }
            if (String.IsNullOrEmpty(newDeptObj.DeptGroupName))
            {
                newDeptId = 0;
                return -2;
            }
            else
                return dalObj.AddNewDepartment(newDeptObj, out newDeptId);
        }
        public List<ProductsDTO> FetchAllProductsUsingEF()
        {
            List<ProductsDTO> prodLstFromDAL = dalObj.FetchProductsListUsingEF();
            return prodLstFromDAL;
        }

        public List<ProductsDTO> FetchMinMaxProducts(int min, int max)
        {
            List<ProductsDTO> prodLstFromDAL = dalObj.FetchMinMaxProdDetails(min,max);
            return prodLstFromDAL;
        }

        public int AddNewProduct(ProductsDTO newProdObj)
        {
            return dalObj.AddNewProduct(newProdObj);
        }
    }
}
