using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetProject.Models;

namespace NetProject.Controllers
{
    public class HomePageController : Controller
    {
        private readonly ILogger<HomePageController> _logger;

        public HomePageController(ILogger<HomePageController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var sliders = new List<Slider>();
            ViewData["sliders"] = sliders;
            var countPromotion = 10;
            ViewData["countPromotion"] = countPromotion;
            var promotionProducts = new List<Product>();
            ViewData["promotionProducts"]  = promotionProducts;
            var hotSmartPhones = new List<Product>();
            ViewData["hotSmartPhones"] = hotSmartPhones;
            var hotAccessProducts = new List<Product>();
            ViewData["hotAccessProducts"] = hotAccessProducts;
            ViewData["res_statusHomePage"] = "visible";
            ViewData["res_statusHomePage"] = "visible";

            var cateProduct = new List<Category>();
            ViewData["res_getCateProduct"] = cateProduct;
            foreach (var cate in cateProduct) {
                ViewData["res_getTypeProduct_" + cate.Id] = new List<TypeProduct>();
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
