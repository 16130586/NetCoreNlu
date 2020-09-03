using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetProject.DbAccessor;
using NetProject.Models;

namespace NetProject.Controllers
{
    public class HomePageController : Controller
    {
        private readonly ILogger<HomePageController> _logger;
        private readonly SliderData _sliderDataAcessor;
        private readonly CategoryData _categoryDataAcessor;
        private readonly TypeProductData _typeProductDataAcessor;
        private readonly ProductData _productDataAcessor;
        public HomePageController(ILogger<HomePageController> logger
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

        public IActionResult Index()
        {
            ViewData["sliders"] = _sliderDataAcessor.GetSlidersIndex();
            ViewData["countPromotion"] = _productDataAcessor.CountPromotionProducts();
            ViewData["promotionProducts"] = _productDataAcessor.GetPromitionProduct();
            ViewData["hotSmartPhones"] = _productDataAcessor.GetProductByCategory(1);
            ViewData["hotAccessProducts"] = _productDataAcessor.GetProductByCategory(2);

            var cateProduct = _categoryDataAcessor.GetActiveCategoryProduct();
            ViewData["res_getCateProduct"] = cateProduct;
            foreach (var cate in cateProduct)
            {
                ViewData["res_getTypeProduct_" + cate.Id] = _typeProductDataAcessor.GetTypeProduct(cate.Id);
            }

            ViewData["res_statusHomePage"] = "visible";
            ViewData["id_cateChose"] = 0;
            ViewData["res_statusAdmin"] = "disible";

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

        [HttpGet]
        public IActionResult ListCart()
        {
            ViewData["sliders"] = _sliderDataAcessor.GetSlidersIndex();
            ViewData["countPromotion"] = _productDataAcessor.CountPromotionProducts();
            ViewData["promotionProducts"] = _productDataAcessor.GetPromitionProduct();
            ViewData["hotSmartPhones"] = _productDataAcessor.GetProductByCategory(1);
            ViewData["hotAccessProducts"] = _productDataAcessor.GetProductByCategory(2);

            var cateProduct = _categoryDataAcessor.GetActiveCategoryProduct();
            ViewData["res_getCateProduct"] = cateProduct;
            foreach (var cate in cateProduct)
            {
                ViewData["res_getTypeProduct_" + cate.Id] = _typeProductDataAcessor.GetTypeProduct(cate.Id);
            }

            ViewData["res_statusHomePage"] = "visible";
            ViewData["id_cateChose"] = 0;
            ViewData["res_statusAdmin"] = "disible";
            return View();
        }
        [HttpGet]
        public IActionResult TypeProduct([FromQuery] int type_product)
        {
            ViewData["sliders"] = _sliderDataAcessor.GetSlidersIndex();
            ViewData["countPromotion"] = _productDataAcessor.CountPromotionProducts();
            ViewData["promotionProducts"] = _productDataAcessor.GetPromitionProduct();
            ViewData["hotSmartPhones"] = _productDataAcessor.GetProductByCategory(1);
            ViewData["hotAccessProducts"] = _productDataAcessor.GetProductByCategory(2);

            var cateProduct = _categoryDataAcessor.GetActiveCategoryProduct();
            ViewData["res_getCateProduct"] = cateProduct;
            foreach (var cate in cateProduct)
            {
                ViewData["res_getTypeProduct_" + cate.Id] = _typeProductDataAcessor.GetTypeProduct(cate.Id);
            }
            if (_typeProductDataAcessor.GetTypeProductById(type_product) == null) return NotFound();

            ViewData["res_statusHomePage"] = "disible";
            ViewData["id_cateChose"] = _typeProductDataAcessor.GetTypeProductById(type_product).IdCategory;
            ViewData["res_statusAdmin"] = "disible";



            var requestedTP = _typeProductDataAcessor.GetTypeProductById(type_product);
            ViewData["res_getNameTypeProductSmartPhone"] = _typeProductDataAcessor.GetNameTypeProductSmartPhone(requestedTP.IdCategory);
            ViewData["res_getAllTypeProductPromotion"] = _productDataAcessor.GetAllTypeProductPromotion(type_product);
            ViewData["res_getAllTypeProduct"] = _productDataAcessor.GetAllTypeProduct(type_product, 8, 0);

            ViewData["activeType"] = type_product;

            if (_typeProductDataAcessor.GetTypeProductById(type_product).IdCategory == 1)
            {

                ViewData["res_getAllTypeProductSmartPhone"] = _typeProductDataAcessor.GetNameTypeProductSmartPhone(1);
                return View("CateSmartPhoneProduct");
            }
            else
                return NotFound();
        }
    }
}
