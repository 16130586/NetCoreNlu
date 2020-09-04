using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using com.sun.tools.corba.se.idl.constExpr;
using jdk.nashorn.@internal.ir;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetProject.DataAccessor;
using NetProject.DbAccessor;
using NetProject.EntryParams;
using NetProject.Models;
using NetProject.Session;

namespace NetProject.Controllers
{
    public class HomePageController : Controller
    {
        private readonly ILogger<HomePageController> _logger;
        private readonly SliderData _sliderDataAcessor;
        private readonly CategoryData _categoryDataAcessor;
        private readonly TypeProductData _typeProductDataAcessor;
        private readonly ProductData _productDataAcessor;
        private readonly AddressData _addressData;
        private readonly BillData _billData;
        private readonly BillDetailData _billDetailData;
        public HomePageController(ILogger<HomePageController> logger
            , SliderData sliderDataAcessor
            , CategoryData categoryDataAcessor
            , TypeProductData typeProductDataAcessor
            , ProductData productDataAcessor
            , AddressData addressData
            , BillData billData
            , BillDetailData billDetailData)
        {
            _logger = logger;
            _sliderDataAcessor = sliderDataAcessor;
            _categoryDataAcessor = categoryDataAcessor;
            _typeProductDataAcessor = typeProductDataAcessor;
            _productDataAcessor = productDataAcessor;
            _addressData = addressData;
            _billData = billData;
            _billDetailData = billDetailData;
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

        [HttpGet]
        public IActionResult CheckOut()
        {
            var user = "";
            var cart = SessionFunction.GetCart(HttpContext.Session);
            if (user != null && cart.List.Count() > 0)
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

                ViewData["res_statusHomePage"] = "disible";
                ViewData["id_cateChose"] = 0;
                ViewData["res_statusAdmin"] = "disible";
                ViewData["cities"] = _addressData.Cities();
                return View();
            }
            else
            {
                return Redirect("ListCart");
            }
        }
        [HttpPost]
        public IActionResult Order(CheckoutParams entries)
        {


            Cart cart = SessionFunction.GetCart(HttpContext.Session);
            User user = SessionFunction.GetUser(HttpContext.Session);

            if (user == null || cart == null || cart.List.Count() <= 0)
            {
                return Redirect("/");
            }
            else
            {

                var rs_city = _addressData.GetCity(entries.Select_City);
                var rs_district = _addressData.GetDistrict(entries.Select_District);

                var yourAdress = entries.YourAddress + ",\t" + rs_district.Name + ",\t" + rs_city.Name;
                if (entries.YourNote == null) entries.YourNote = "";


                string dateNow = System.DateTime.Now.ToString("yyyy-MM-dd");

                var bill = _billData.AddBill(new Bill {
                    Active = 1,
                    Address = yourAdress,
                    DateOrder = dateNow,
                    IdUser = user.Id,
                    Note = entries.YourNote,
                    TotalPrice = cart.Total
                
                });

                foreach (var item in cart.List)
                {
                    _billDetailData.AddBillDetail(new BillDetail
                    {
                        Active = 1 ,
                        IdBill = bill.Id,
                        IdProduct = item.Id,
                        Quantity = cart.Quantities[item.Id + ""],
                        TotalPrice = cart.Quantities[item.Id + ""] * (item.PricePromotion != 0 ? item.PricePromotion : item.PriceListed)
                    });
                }
                SessionFunction.SetCart(HttpContext.Session, new Cart());
            }
            return Redirect("/");
        }
        [HttpGet]
        public IActionResult PageNotFound()
        {
            return View("NotFound");
        }
    }
}
