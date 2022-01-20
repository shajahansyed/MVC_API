using AdvWorksBL;
using AdvWorksDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AdvWorksAPI.Controllers
{
    public class DepartmentController : ApiController
    {
        AdvWorksBusinessLayer blObj;
        public DepartmentController()
        {
            blObj = new AdvWorksBusinessLayer();
        }
        [HttpGet]
        public HttpResponseMessage GetAllDeptDetails()
        {
            try
            {
                List<DeptDetailsDTO> lstOfDept = blObj.GetAllDepts();
                if (lstOfDept.Count > 0)
                    return Request.CreateResponse(HttpStatusCode.OK, lstOfDept);
                else
                    return Request.CreateResponse(HttpStatusCode.OK, "No Dept Details");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet]
        public HttpResponseMessage GetAllProductDetails()
        {
            try
            {
                List<ProductsDTO> lstOfProd = blObj.GetAllProducts();
                if (lstOfProd.Count > 0)
                    return Request.CreateResponse(HttpStatusCode.OK, lstOfProd);
                else
                    return Request.CreateResponse(HttpStatusCode.OK, "No Dept Details");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet]
        public HttpResponseMessage GetDeptDetails(string deptGroupName)
        {
            try
            {
                List<DeptDetailsDTO> lstOfDept = blObj.SearchDept(deptGroupName);
                if (lstOfDept.Count > 0)
                    return Request.CreateResponse(HttpStatusCode.OK, lstOfDept);
                else
                    return Request.CreateResponse(HttpStatusCode.OK, "No Dept Details");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet]
        public HttpResponseMessage GetProductDetails()
        {
            try
            {
                List<ProductsDTO> lstOfProd = blObj.FetchAllProductsUsingEF();
                if (lstOfProd.Count > 0)
                    return Request.CreateResponse(HttpStatusCode.OK, lstOfProd);
                else
                    return Request.CreateResponse(HttpStatusCode.OK, "No Dept Details");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        public HttpResponseMessage AddNewDept(DeptDetailsDTO deptObj)     
        {
            try
            {
                int newDeptId = 0;
                int retValue = blObj.AddNewDept(deptObj, out newDeptId);
                if(retValue==1)
                    return Request.CreateResponse(HttpStatusCode.OK, "Departement added sucessfully " + newDeptId);
                else
                    return Request.CreateResponse(HttpStatusCode.OK, "Dept not added/saved.");
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }
        }
    }
}
