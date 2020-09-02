using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetProject.DbAccessor;
using NetProject.Models;
using NetProject.Session;

namespace NetProject.Controllers
{
    public class AjaxController : Controller
    {
        private readonly ProductData _productDataAccessor;
        public AjaxController(ProductData productData)
        {
            _productDataAccessor = productData;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetCartOnHeader()
        {
            var cart = NetProject.Session.SessionFunction.GetCart(HttpContext.Session);
            var a = 1;
            return View();
        }
        [HttpGet]
        public IActionResult GetInforProduct([FromQuery] int id_product, [FromQuery] int index)
        {
            ViewData["res_inforPro"] = _productDataAccessor.GetProductById(id_product);
            ViewData["index"] = index;
            return View();
        }
        [HttpGet]
        public IActionResult DelCartProduct([FromQuery] int id_product)
        {
            var cart = SessionFunction.GetCart(HttpContext.Session);
            cart.Remove(id_product);
            SessionFunction.SetCart(HttpContext.Session, cart);
            return View();
        }
        [HttpGet]
        public IActionResult SetQty([FromQuery] int id_product, int value)
        {
            Cart cart = SessionFunction.GetCart(HttpContext.Session);
            cart.Put(id_product, value);
            SessionFunction.SetCart(HttpContext.Session, cart);
            return View("DelCartProduct");
        }
    }
}