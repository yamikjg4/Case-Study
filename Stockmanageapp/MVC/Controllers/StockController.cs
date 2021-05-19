using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class StockController : Controller
    {
        // GET: Stock
        public ActionResult Index()
        {
            IEnumerable<mvcStockModel> stock;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Stocks").Result;
            stock = response.Content.ReadAsAsync<IEnumerable<mvcStockModel>>().Result;
            return View(stock);
        }
        public ActionResult AddOrEdit(int id=0 )
        {
            if (id == 0)
            {
                return View(new mvcStockModel());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Stocks/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcStockModel>().Result);
            }
            
        }
        [HttpPost]
        public ActionResult AddOrEdit(mvcStockModel st1)
        {
            if (st1.Stock_Id == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Stocks", st1).Result;
                TempData["SuccessMessage"] = "Save Sucessfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Stocks/" + st1.Stock_Id, st1).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");

        }
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Stocks/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}