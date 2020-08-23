using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetProject.DataAccessor;
using NetProject.DbAccessor;
using NetProject.EntryParams;
using NetProject.Models;
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
        private readonly FileData _fileData;
        public AdminController(ILogger<AdminController> logger
            , SliderData sliderDataAcessor
            , CategoryData categoryDataAcessor
            , TypeProductData typeProductDataAcessor
            , ProductData productDataAcessor
            , FileData fileData)
        {
            _logger = logger;
            _sliderDataAcessor = sliderDataAcessor;
            _categoryDataAcessor = categoryDataAcessor;
            _typeProductDataAcessor = typeProductDataAcessor;
            _productDataAcessor = productDataAcessor;
            _fileData = fileData;
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
            if (HttpContext.User.Identity.IsAuthenticated && User.CheckLevel())
            {
                return View();
            }

            return Redirect("/");
        }
        [HttpPost]
        public IActionResult AddTypeToDB([FromForm] AddTypeParams entry)
        {

            if (string.IsNullOrEmpty(entry.Name_type)) entry.Name_type = "";
            var url = "";
            if (entry.Image_type != null)
            {

                url = _fileData.Save(entry.Image_type);
            }
            _typeProductDataAcessor.AddType(new Models.TypeProduct
            {
                NameType = entry.Name_type,
                Active = 1,
                IdCategory = entry.Select_nameCate,
                ImageType = url
            });
            SessionFunction.SetString(HttpContext.Session, "mes_err", "Thêm thành công");
            return Redirect("AddType");
        }
        [HttpGet]
        public IActionResult AdminEditType([FromQuery(Name = "id_type")] int id_type)
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
            if (HttpContext.User.Identity.IsAuthenticated && User.CheckLevel())
            {
                var result = _typeProductDataAcessor.GetTypeProductById(id_type);
                if (result == null) return NotFound();
                ViewData["res_TypeEdit"] = result;
                return View();
            }

            return Redirect("/");
        }
        [HttpPost]
        public IActionResult AdminEditTypeToDB([FromForm] UpdateTypeParams entry)
        {

            if (string.IsNullOrEmpty(entry.Name_type)) entry.Name_type = "";

            var url = entry.Image_type_old;
            if (entry.Image_type != null)
            {

                url = _fileData.Save(entry.Image_type);
            }
            _typeProductDataAcessor.UpdateType(new Models.TypeProduct
            {
                Id = entry.Id_type,
                NameType = entry.Name_type,
                Active = entry.Select_Status,
                IdCategory = entry.Select_nameCate,
                ImageType = url

            });
            SessionFunction.SetString(HttpContext.Session, "mes_err", "Sửa thành công");
            return Redirect("AdminListType");
        }

        [HttpGet]
        public IActionResult AdminListType()
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
            if (HttpContext.User.Identity.IsAuthenticated && User.CheckLevel())
            {
                var typeProducts = _typeProductDataAcessor.GetAll();
                var categories = _categoryDataAcessor.getAll();
                var result = typeProducts.Join(categories, tp => tp.IdCategory, ct => ct.Id, (tp, ct) => new TypeProduct
                {
                    NameType = tp.NameType,
                    ImageType = tp.ImageType,
                    Id = tp.Id,
                    CategoryName = ct.NameCategory,
                    Active = tp.Active
                }).OrderBy(rs => rs.Id).ToList();

                if (result == null) return NotFound();
                ViewData["res_listTypeProduct"] = result;
                return View();
            }

            return Redirect("/");
        }
        [HttpGet]
        public IActionResult DeleteTypeToDB([FromQuery(Name = "id_type")] int id_type)
        {
            _typeProductDataAcessor.Delete(id_type);
            return Redirect("AdminListType");
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            var cateProduct = _categoryDataAcessor.GetCategoryProduct();
            ViewData["res_getCateProduct"] = cateProduct;
            var typeProduct = _typeProductDataAcessor.GetTypeProduct();
            ViewData["res_getTypeProduct_"] = typeProduct;
            foreach (var cate in cateProduct)
            {
                ViewData["res_getProduct_" + cate.Id] = _productDataAcessor.GetProductByCategory(cate.Id);
            }
            ViewData["res_statusHomePage"] = "disible";
            ViewData["id_cateChose"] = 0;
            ViewData["id_typeChose"] = 0;
            ViewData["res_statusAdmin"] = "visible";

            var User = SessionFunction.GetUser(HttpContext.Session);
            if (HttpContext.User.Identity.IsAuthenticated && User.CheckLevel())
            {
                return View();
            }

            return Redirect("/");
        }
        [HttpPost]
        public IActionResult AddProductToDB([FromForm] AddProductParams entry)
        {
            if (string.IsNullOrEmpty(entry.Name_Product)) entry.Name_Product = "";
            var url = "";
            if (entry.Image_product != null)
            {

                url = _fileData.Save(entry.Image_product);
            }
            _productDataAcessor.AddProduct(new Models.Product
            {
                NameProduct = entry.Name_Product,
                Active= 1,
                IdCategory = entry.Select_nameCate,
                IdType = entry.Select_nameType,
                ImageProduct = url,
                ImageDetailProduct = url,
                Quantity = entry.Quantity_Product,
                PricePromotion = entry.Price_Product,
                Screen = entry.Screen,
                Operating_System = entry.Operating_System,
                Back_Camera = entry.Back_Camera,
                Front_Camera = entry.Front_Camera,
                CPU = entry.CPU,
                RAM = entry.RAM,
                Memory_Stick = entry.Memory_Stick,
                Sim_Stick = entry.Sim_Stick,
                Battery_Capacity = entry.Battery_Capacity
               
            });
            SessionFunction.SetString(HttpContext.Session, "mes_err", "Thêm thành công");
            return Redirect("AddProduct");
        }
        public IActionResult AdminEditProduct([FromQuery(Name = "id_product")]int id_product)
        {
            var cateProduct = _categoryDataAcessor.GetCategoryProduct();
            ViewData["res_getCateProduct"] = cateProduct;
            var typeProduct = _typeProductDataAcessor.GetTypeProduct();
            ViewData["res_getTypeProduct_"] = typeProduct;
            foreach (var cate in cateProduct)
            {
                ViewData["res_getProduct_" + cate.Id] = _productDataAcessor.GetProductByCategory(cate.Id);
            }
            ViewData["res_statusHomePage"] = "disible";
            ViewData["id_cateChose"] = 0;
            ViewData["id_typeChose"] = 0;
            ViewData["res_statusAdmin"] = "visible";

            var User = SessionFunction.GetUser(HttpContext.Session);
            if (HttpContext.User.Identity.IsAuthenticated && User.CheckLevel())
            {
                return View();
            }

            return Redirect("/");
        
    }
        [HttpPost]
        public IActionResult AdminEditProductToDB([FromForm] UpdateProductParams entry)
        {

            if (string.IsNullOrEmpty(entry.Name_Product)) entry.Name_Product = "";

            var url = entry.Image_product_old;
            if (entry.Image_product != null)
            {

                url = _fileData.Save(entry.Image_product);
            }
            _productDataAcessor.UpdateProduct(new Models.Product
            {
                NameProduct = entry.Name_Product,
                Active = 1,
                IdCategory = entry.Select_nameCate,
                IdType = entry.Select_nameType,
                ImageProduct = url,
                ImageDetailProduct = url,
                Quantity = entry.Quantity_Product,
                PricePromotion = entry.Price_Product,
                Screen = entry.Screen,
                Operating_System = entry.Operating_System,
                Back_Camera = entry.Back_Camera,
                Front_Camera = entry.Front_Camera,
                CPU = entry.CPU,
                RAM = entry.RAM,
                Memory_Stick = entry.Memory_Stick,
                Sim_Stick = entry.Sim_Stick,
                Battery_Capacity = entry.Battery_Capacity
            });
            SessionFunction.SetString(HttpContext.Session, "mes_err", "Sửa thành công");
            return Redirect("AdminListProduct");
        }
        [HttpGet]
        public IActionResult AdminListProduct()
        {
            var cateProduct = _categoryDataAcessor.GetCategoryProduct();
            ViewData["res_getCateProduct"] = cateProduct;
            var typeProduct = _typeProductDataAcessor.GetTypeProduct();
            ViewData["res_getTypeProduct_"] = typeProduct;
            foreach (var cate in cateProduct)
            {
                ViewData["res_getProduct_" + cate.Id] = _productDataAcessor.GetProductByCategory(cate.Id);
            }
            ViewData["res_statusHomePage"] = "disible";
            ViewData["id_cateChose"] = 0;
            ViewData["id_typeChose"] = 0;
            ViewData["res_statusAdmin"] = "visible";

            var User = SessionFunction.GetUser(HttpContext.Session);
            if (HttpContext.User.Identity.IsAuthenticated && User.CheckLevel())
            {
                var products = _productDataAcessor.GetAll();
                var categories = _categoryDataAcessor.getAll();
                var result =products.Join(categories, tp => tp.IdCategory, ct => ct.Id, (tp, ct) => new Product
                {
                    NameProduct = tp.NameProduct,
                    ImageProduct = tp.ImageProduct,
                    Id = tp.Id,
                    CategoryName = ct.NameCategory,
                    Active = tp.Active,
                    ImageDetailProduct = tp.ImageProduct,
                    Quantity = tp.Quantity,
                    PricePromotion = tp.PricePromotion,
                    Screen = tp.Screen,
                    Operating_System = tp.Operating_System,
                    Back_Camera = tp.Back_Camera,
                    Front_Camera = tp.Front_Camera,
                    CPU = tp.CPU,
                    RAM = tp.RAM,
                    Memory_Stick = tp.Memory_Stick,
                    Sim_Stick = tp.Sim_Stick,
                    Battery_Capacity = tp.Battery_Capacity
                }).OrderBy(rs => rs.Id).ToList();

                if (result == null) return NotFound();
                ViewData["res_listProduct"] = result;
                return View();
            }

            return Redirect("/");
        }
    }

    }

