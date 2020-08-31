using NetProject.Context;
using NetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.DbAccessor
{
    public class ProductData : DataProvider
    {
        public ProductData(OurDbContext context) : base(context)
        {
        }
        public void AdminAddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public List<Product> GetPromitionProduct() {
            try
            {
                return _context.Products.Where(pd => pd.Active == 1 && pd.PricePromotion > 0).OrderBy(x => Guid.NewGuid()).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Product>();
            }
        }

        public int CountPromotionProducts(){
            try
            {
                return _context.Products.Where(pd => pd.Active == 1 && pd.PricePromotion > 0).Count();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }
        public List<Product> GetProductByCategory(int idCate)
        {
            try
            {
                return (from products in _context.Products
                        join types in _context.TypeProducts
                        on products.IdType equals types.Id
                        join categories in _context.Categories
                        on types.IdCategory equals categories.Id
                        where categories.Id == idCate
                        select new Product
                        {
                            Id = products.Id,
                            NameType = products.NameType,
                            CategoryName = categories.NameCategory,
                            NameProduct = products.NameProduct,
                            ImageProduct = products.ImageProduct,
                            PriceListed = products.PriceListed,
                            PricePromotion = products.PricePromotion,
                            Parameter = products.Parameter,
                            ImageDetailProduct = products.ImageDetailProduct
                        }).ToList();
                        
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Product>();
            }
        }
        public Product GetProductById(int id)
        {
            try
            {
                return _context.Products.Where(tpd => tpd.Id == id).FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool UpdateProduct(Product product)
        {
            try
            {
                var requested = GetProductById(product.Id);
                if (requested == null) return false;
                requested.Id = product.Id;
                requested.Active = product.Active;
                requested.IdCategory = product.IdCategory;
                requested.IdType = product.IdType;
                requested.NameType = product.NameType;
                requested.NameProduct = product.NameProduct;
                requested.ImageProduct = product.ImageProduct;
                requested.ImageDetailProduct = product.ImageDetailProduct;
                requested.Quantity = product.Quantity;
                requested.PriceListed = product.PriceListed;
                requested.PricePromotion = product.PricePromotion;
                requested.Internal_Memory = product.Internal_Memory;
                requested.Memory_Stick = product.Memory_Stick;
                requested.Operating_System = product.Operating_System;
                requested.RAM = product.RAM;
                requested.Screen = product.Screen;
                requested.Sim_Stick = product.Sim_Stick;
                requested.Back_Camera = product.Back_Camera;
                requested.Front_Camera = product.Front_Camera;
                requested.CPU = product.CPU;
                requested.Battery_Capacity = product.Battery_Capacity;
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public bool Delete(int id_product)
        {
            try
            {
                var requested = GetProductById(id_product);
                _context.Products.Remove(requested);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }


        }
    }
}
