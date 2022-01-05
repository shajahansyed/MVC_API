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
        public List<ProductsDTO> SearchedProd(string input)
        {
            List<ProductsDTO> searchedProdLstFromDAL = dalObj.ProdSearchList(input);
            return searchedProdLstFromDAL;
        }

        public int AddNewDept(DeptDetailsDTO newDeptObj)
        {
            return dalObj.AddNewDepartment(newDeptObj);
        }
    }
}
