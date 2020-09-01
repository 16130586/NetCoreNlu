using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetProject.DbAccessor;
using NetProject.Session;

namespace NetProject.Controllers
{
    public class CartController : Controller
    {
        ProductData _productDataAccessor;
        public CartController(ProductData productData)
        {
            _productDataAccessor = productData;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddCart([FromQuery] int id_product , [FromQuery] int quantity)
        {
            var cart = SessionFunction.GetCart(HttpContext.Session);
            if (cart == null)
            {
                cart = new Models.Cart();
            }
            cart.Put(_productDataAccessor.GetProductById(id_product), quantity);
            SessionFunction.SetCart(HttpContext.Session, cart);
            return RedirectToActionPreserveMethod("GetCartOnHeader", "Ajax");
        }
        [HttpGet]
        public IActionResult AjaxSetQtyOnHeader([FromQuery] int id_product, [FromQuery] int value)
        {
            var cart = SessionFunction.GetCart(HttpContext.Session);
            if (cart == null)
            {
                cart = new Models.Cart();
            }
            cart.Put(id_product , value);
            SessionFunction.SetCart(HttpContext.Session, cart);
            return RedirectToActionPreserveMethod("GetCartOnHeader", "Ajax");
        }
        [HttpGet]
        public IActionResult AjaxDelCartOnHeader([FromQuery] int id_product) {
            var cart = SessionFunction.GetCart(HttpContext.Session);
            if (cart == null)
            {
                cart = new Models.Cart();
            }
            cart.Remove(id_product);
            SessionFunction.SetCart(HttpContext.Session, cart);
            return RedirectToActionPreserveMethod("GetCartOnHeader", "Ajax");
        }
    }
}