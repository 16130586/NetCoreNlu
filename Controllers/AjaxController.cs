using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetProject.DataAccessor;
using NetProject.DbAccessor;
using NetProject.Models;
using NetProject.Session;

namespace NetProject.Controllers
{
    public class AjaxController : Controller
    {
        private readonly ProductData _productDataAccessor;
        private readonly AddressData _addressData;
        public AjaxController(ProductData productData , AddressData addressData)
        {
            _productDataAccessor = productData;
            _addressData = addressData;
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

        [HttpGet]
        public IActionResult GetDistrictOfCity([FromQuery] int id_city)
        {
            ViewData["Districts"] = _addressData.Districts(id_city);
            return View("GetDistrictOfCity");
        }
    }
}