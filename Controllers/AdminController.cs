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
            var cateProduct = _categoryDataAcessor.GetActiveCategoryProduct();
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
            var cateProduct = _categoryDataAcessor.GetActiveCategoryProduct();
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
            var cateProduct = _categoryDataAcessor.GetActiveCategoryProduct();
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
                var categories = _categoryDataAcessor.GetALL();
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
        public IActionResult AdminListCate()
        {
            var cateProduct = _categoryDataAcessor.GetActiveCategoryProduct();
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
                var categories = _categoryDataAcessor.GetALL();
                if (categories == null) return NotFound();
                ViewData["res_listCateProduct"] = categories;
                return View();
            }

            return Redirect("/");
        }

        [HttpGet]
        public IActionResult AdminEditCate([FromQuery(Name = "id_cate")] int id_cate)
        {
            var cateProduct = _categoryDataAcessor.GetActiveCategoryProduct();
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
                var result = _categoryDataAcessor.GetOne(id_cate);
                if (result == null) return NotFound();
                ViewData["res_CateEdit"] = result;
                return View();
            }

            return Redirect("AdminListCate");
        }
        [HttpPost]
        public IActionResult UpdateCateToDB(UpdateCateParams entry)
        {

            if (string.IsNullOrEmpty(entry.icon_cate)) entry.icon_cate = "";
            if (string.IsNullOrEmpty(entry.name_cate)) entry.name_cate = "";

            var update = new Category
            {
                NameCategory = entry.name_cate,
                Active =entry.select_Status,
                Id = entry.id_cate,
                IconCategory = entry.icon_cate
            };
            _categoryDataAcessor.Update(update);
            SessionFunction.SetString(HttpContext.Session, "mes_err", "Sửa thành công");
            return Redirect("AdminListCate");
        }

        [HttpGet]
        public IActionResult DeleteCateToDB([FromQuery(Name = "id_cate")] int id_cate)
        {
            _categoryDataAcessor.Delete(id_cate);
            return Redirect("AdminListCate");
        }

        [HttpGet]
        public IActionResult AdminAddCate()
        {
            var cateProduct = _categoryDataAcessor.GetActiveCategoryProduct();
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

            return Redirect("AdminListCate");
        }
        [HttpPost]
        public IActionResult AddCateToDB(AddCateParams entry) {

            if (string.IsNullOrEmpty(entry.Icon_cate)) entry.Icon_cate = "";
            if (string.IsNullOrEmpty(entry.Name_cate)) entry.Name_cate = "";

            _categoryDataAcessor.AddCategory(new Category
            {
                Active = 1 , 
                IconCategory = entry.Icon_cate,
                NameCategory = entry.Name_cate
            });
            SessionFunction.SetString(HttpContext.Session, "mes_err", "Thêm thành công");
            return Redirect("AdminListCate");
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
            var cateProduct = _categoryDataAcessor.GetActiveCategoryProduct();
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
        public IActionResult AddProductToDB()
        {
            return null;
        }
    }
}