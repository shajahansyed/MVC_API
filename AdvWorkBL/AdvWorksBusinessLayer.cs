using AdvWorksDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvWorksBL
{
    public class AdvWorksBusinessLayer
    {
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
        public void GetAllDepts()
        {
            AdvWorksDataAccessLayer dalObj = new AdvWorksDataAccessLayer();
            dalObj.FetchAllDept();
        }
        public void GetAllProducts()
        {
            AdvWorksDataAccessLayer dalObj = new AdvWorksDataAccessLayer();
            dalObj.ProductList();
        }
    }
}
