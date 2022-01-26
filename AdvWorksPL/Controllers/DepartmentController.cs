using AdvWorksBL;
using AdvWorksDTO;
using AdvWorksPL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdvWorksPL.Controllers
{
    public class DepartmentController : Controller
    {
        AdvWorksBusinessLayer blObj;
        public DepartmentController()
        {
            blObj = new AdvWorksBusinessLayer();
        }
        // GET: Department
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DisplayDeptDetails()
        {
            try
            {
                List<DeptDetailsDTO> lstOfDept = blObj.GetAllDepts();
                List<DepartmentModel> lstOfDeptsModel = new List<DepartmentModel>();
                foreach (var dept in lstOfDept)
                {
                    DepartmentModel deptModelObj = new DepartmentModel();
                    deptModelObj.DeptName = dept.DeptName;
                    deptModelObj.DeptGroupName = dept.DeptGroupName;
                    lstOfDeptsModel.Add(deptModelObj);
                }
                return View(lstOfDeptsModel);
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }
        public ActionResult DisplayProductDetails()
        {
            try
            {
                List<ProductsDTO> lstOfProd = blObj.GetAllProducts();
                List<ProductModel> lstOfProdsModel = new List<ProductModel>();
                foreach (var prod in lstOfProd)
                {
                    ProductModel prodModelObj = new ProductModel();
                    prodModelObj.ProdName = prod.ProdName;
                    prodModelObj.ProdNum = prod.ProdNum;
                    prodModelObj.ProdListPrice = prod.ProdListPrice;
                    lstOfProdsModel.Add(prodModelObj);
                }
                return View(lstOfProdsModel);
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }
        public ActionResult DisplayDeptGroupDetails(string deptGroupName)
        {
            try
            {
                List<DeptDetailsDTO> lstOfDept = blObj.SearchDept(deptGroupName);
                List<DepartmentModel> lstOfDeptsModel = new List<DepartmentModel>();
                foreach (var dept in lstOfDept)
                {
                    DepartmentModel deptModelObj = new DepartmentModel();
                    deptModelObj.DeptName = dept.DeptName;
                    deptModelObj.DeptGroupName = dept.DeptGroupName;
                    lstOfDeptsModel.Add(deptModelObj);
                }
                return View(lstOfDeptsModel);
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }
        public ActionResult DisplayProdDetails()
        {
            try
            {
                List<ProductsDTO> lstOfProd = blObj.FetchAllProductsUsingEF();
                List<ProductModel> lstOfProdsModel = new List<ProductModel>();
                foreach (var prod in lstOfProd)
                {
                    ProductModel prodModelObj = new ProductModel();
                    prodModelObj.ProdName = prod.ProdName;
                    prodModelObj.ProdNum = prod.ProdNum;
                    prodModelObj.ProdListPrice = prod.ProdListPrice;
                    lstOfProdsModel.Add(prodModelObj);
                }
                return View(lstOfProdsModel);
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult AddNewDept(DepartmentModel deptModelObj)
        {
            try
            {
                int newDeptId = 0;
                DeptDetailsDTO deptDtoObj = new DeptDetailsDTO();
                deptModelObj.DeptName = deptDtoObj.DeptName;
                deptModelObj.DeptGroupName = deptDtoObj.DeptGroupName;
                int retValue = blObj.AddNewDept(deptDtoObj, out newDeptId);
                if (retValue == 1)
                    return  View("Departement added sucessfully " + newDeptId);
                else
                    return View("Dept not added/saved.");
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }
    }
}