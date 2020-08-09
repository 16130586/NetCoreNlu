using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetProject.DbAccessor;
using NetProject.Session;

namespace NetProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly SliderData _sliderDataAcessor;
        private readonly CategoryData _categoryDataAcessor;
        private readonly TypeProductData _typeProductDataAcessor;
        private readonly ProductData _productDataAcessor;
        public AdminController(ILogger<AdminController> logger
            , SliderData sliderDataAcessor
            , CategoryData categoryDataAcessor
            , TypeProductData typeProductDataAcessor
            , ProductData productDataAcessor)
        {
            _logger = logger;
            _sliderDataAcessor = sliderDataAcessor;
            _categoryDataAcessor = categoryDataAcessor;
            _typeProductDataAcessor = typeProductDataAcessor;
            _productDataAcessor = productDataAcessor;
        }

        [HttpGet]
        public IActionResult AddType()
        {
            var cateProduct = _categoryDataAcessor.GetCategoryProduct();
            ViewData["res_getCateProduct"] = cateProduct;
            foreach (var cate in cateProduct)
            {
                ViewData["res_getTypeProduct_" + cate.Id] = _typeProductDataAcessor.GetTypeProduct(cate.Id);
            }
            ViewData["res_statusHomePage"] = "disible";
            ViewData["id_cateChose"] = 0;
            ViewData["res_statusAdmin"] = "visible";

            var User = SessionFunction.GetUser(HttpContext.Session);
            if (HttpContext.User.Identity.IsAuthenticated && User.CheckLevel()) {
                return View();
            }

            return Redirect("/");
        }
        [HttpPost]
        public IActionResult AddTypeToDB() {
            return null;
        }
    }
}