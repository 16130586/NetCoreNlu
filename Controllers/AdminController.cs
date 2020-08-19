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
            }) ;
            SessionFunction.SetString(HttpContext.Session, "mes_err", "Thêm thành công");
            return Redirect("AddType");
        }
        [HttpGet]
        public IActionResult AdminEditType([FromQuery(Name = "id_type")] int id_type) {
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
        public IActionResult AdminListType() {
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
                var result = typeProducts.Join(categories , tp => tp.IdCategory , ct => ct.Id , (tp , ct) => new TypeProduct{
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
    }
}