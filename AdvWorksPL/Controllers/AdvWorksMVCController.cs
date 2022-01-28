using AdvWorksBL;
using AdvWorksDTO;
using AdvWorksPL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AdvWorksPL.Controllers
{
    public class AdvWorksMVCController : Controller
    {
        AdvWorksBusinessLayer blObj;
        public AdvWorksMVCController()
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
        [HttpGet]
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
        [HttpGet]
        public ActionResult AddNewDept()
        {
            try
            {
                return  View();
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
                DeptDetailsDTO deptObj = new DeptDetailsDTO();
                deptObj.DeptName = deptModelObj.DeptName;
                deptObj.DeptGroupName = deptModelObj.DeptGroupName;
                int deptId = 0;
                int retVal = blObj.AddNewDept(deptObj, out deptId);
                if(retVal == 1)
                {
                    return RedirectToAction("DisplayDeptDetails");
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }
        public async Task<ActionResult> DisplayDeptDetailsWebAPI()
        {
            try
            {
                string baseURL = $"https://localhost:44304/";
                string routeURL = $"api/AdvWorksAPI/GetAllDeptDetails";
                var apiClient = new HttpClient();
                apiClient.BaseAddress = new Uri(baseURL);
                apiClient.DefaultRequestHeaders.Clear();
                apiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage apiResponse = await apiClient.GetAsync(routeURL);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var result = apiResponse.Content.ReadAsStringAsync().Result;
                    //List<DepartmentModel> lstDepts = new List<DepartmentModel>();
                    var finalResult = JsonConvert.DeserializeObject<List<DepartmentModel>>(result);
                    return View(finalResult);
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }
        public async Task<ActionResult> DisplayDeptGroupDetailsWebAPI(string deptGroupName)
        {
            try
            {
                string baseURL = $"https://localhost:44304/";
                string routeURL = $"api/AdvWorksAPI/GetDeptDetails/{deptGroupName}";
                var apiClient = new HttpClient();
                apiClient.BaseAddress = new Uri(baseURL);
                apiClient.DefaultRequestHeaders.Clear();
                apiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage apiResponse = await apiClient.GetAsync(routeURL);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var result = apiResponse.Content.ReadAsStringAsync().Result;
                    var finalResult = JsonConvert.DeserializeObject<List<DepartmentModel>>(result);
                    return View(finalResult);
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }
        public async Task<ActionResult> DisplayProdDetailsWebAPI()
        {
            try
            {
                string baseURL = $"https://localhost:44304/";
                string routeURL = $"api/AdvWorksAPI/GetAllProductDetails";
                var apiClient = new HttpClient();
                apiClient.BaseAddress = new Uri(baseURL);
                apiClient.DefaultRequestHeaders.Clear();
                apiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage apiResponse = await apiClient.GetAsync(routeURL);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var result = apiResponse.Content.ReadAsStringAsync().Result;
                    var finalResult = JsonConvert.DeserializeObject<List<ProductModel>>(result);
                    return View(finalResult);
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }
        public async Task<ActionResult> DisplayProdMinMaxDetailsWebAPI(int min,int max)
        {
            try
            {
                string baseURL = $"https://localhost:44304/";
                string routeURL = $"api/AdvWorksAPI/GetMinMaxProductDetails/{min}/{max}";
                var apiClient = new HttpClient();
                apiClient.BaseAddress = new Uri(baseURL);
                apiClient.DefaultRequestHeaders.Clear();
                apiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage apiResponse = await apiClient.GetAsync(routeURL);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var result = apiResponse.Content.ReadAsStringAsync().Result;
                    var finalResult = JsonConvert.DeserializeObject<List<ProductModel>>(result);
                    return View(finalResult);
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }
    }
}