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
    public class DetailProductController : Controller
    {
        private readonly ILogger<DetailProductController> _logger;
        private readonly CategoryData _categoryDataAcessor;
        private readonly TypeProductData _typeProductDataAcessor;
        private readonly ProductData _productDataAcessor;

        public DetailProductController(ILogger<DetailProductController> logger
           , CategoryData categoryDataAcessor
           , TypeProductData typeProductDataAcessor
           , ProductData productDataAcessor)
        {
            _logger = logger;
            _categoryDataAcessor = categoryDataAcessor;
            _typeProductDataAcessor = typeProductDataAcessor;
            _productDataAcessor = productDataAcessor;
        }



        public IActionResult DetailProduct()
        {
            ViewData["countPromotion"] = _productDataAcessor.CountPromotionProducts();
            ViewData["promotionProducts"] = _productDataAcessor.GetPromitionProduct();
            ViewData["relatedProduct"] = _productDataAcessor.GetProductByCategory(1);
            
           
            var cateProduct = _categoryDataAcessor.GetCategoryProduct();
            ViewData["res_getCateProduct"] = cateProduct;
            foreach (var cate in cateProduct)
            {
                ViewData["res_getTypeProduct_" + cate.Id] = _typeProductDataAcessor.GetTypeProduct(cate.Id);
            }

            ViewData["res_statusDetailProduct"] = "visible";
            ViewData["res_statusDetailProduct"] = "visible";
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
    }
}
