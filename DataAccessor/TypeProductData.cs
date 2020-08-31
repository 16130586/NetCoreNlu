using Microsoft.VisualBasic;
using NetProject.Context;
using NetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.DbAccessor
{
    public class TypeProductData : DataProvider
    {
        public TypeProductData(OurDbContext context) : base(context)
        {
        }
        public void AddType(TypeProduct typeProduct)
        {
            _context.TypeProducts.Add(typeProduct);
            _context.SaveChanges();
        }
        public List<TypeProduct> GetTypeProduct(int idCate)
        {
            try
            {
                return _context.TypeProducts.Where(tpd => tpd.Active == 1 && tpd.IdCategory == idCate).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<TypeProduct>();
            }
        }
        public TypeProduct GetTypeProductById(int id)
        {
            try
            {
                return _context.TypeProducts.Where(tpd => tpd.Id == id).FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool UpdateType(TypeProduct typeProduct)
        {
            try
            {
                var requested = GetTypeProductById(typeProduct.Id);
                if (requested == null) return false;
                requested.Id = typeProduct.Id;
                requested.Active = typeProduct.Active;
                requested.IdCategory = typeProduct.IdCategory;
                requested.NameType = typeProduct.NameType;
                requested.ImageType = typeProduct.ImageType;
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<TypeProduct> GetAll()
        {
            return _context.TypeProducts.ToList();
        }

        public bool Delete(int id_type)
        {
            try
            {
                var requested = GetTypeProductById(id_type);
                _context.TypeProducts.Remove(requested);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }


        }
        public List<TypeProduct> GetTypeProduct()
        {
            var rs = _context.TypeProducts.Where(tp => tp.Active > 0).ToList();
            return  rs == null ? new List<TypeProduct>() : rs;
        }

    }
}