using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetProject.DbAccessor;

namespace NetProject.Controllers
{
    public class AjaxController : Controller
    {
        private readonly ProductData _productDataAccessor;
        public AjaxController(ProductData productData) {
            _productDataAccessor = productData;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetCartOnHeader() {
            var cart = NetProject.Session.SessionFunction.GetCart(HttpContext.Session);
            var a = 1;
            return View();
        }
        [HttpGet]
        public IActionResult GetInforProduct([FromQuery] int id_product , [FromQuery] int index) {
            ViewData["res_inforPro"] = _productDataAccessor.GetProductById(id_product);
            ViewData["index"] = index;
            return View();
        }
    }
}