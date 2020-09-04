using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.Context
{
    public class DbInitializer
    {
        private readonly string _deployUrl;
        public DbInitializer(IHostingEnvironment env, IConfiguration appConfig)
        {
            _deployUrl = appConfig["DeployUrl"];
        }
        public void Initialize(OurDbContext context)
        {
            context.Database.EnsureCreated();
            context.Database.Migrate();
            if (context.Users.Count() <= 0) {
                // seeding for user
                User admin = new User
                {
                    NameUser = "ADMIN",
                    Email = "admin@gmail.com",
                    Password = "123456",
                    Gender = "male",
                    Level = "admin",
                    Active = 1,
                    NumberPhone = "0382702841"
                };

                context.Users.Add(admin);
                context.SaveChanges();
            }
            if (context.Categories.Count() <= 0) {
                context.Categories.Add(new Category
                {
                    NameCategory = "Điện thoại",
                    Active = 1
                });
                context.Categories.Add(new Category
                {
                    NameCategory = "Máy tính bảng",
                    Active = 1
                });
                context.SaveChanges();
            }
            if (context.TypeProducts.Count() <= 0) {
                var mobileCate = context.Categories.Where(ct => ct.NameCategory.Equals("Điện thoại")).FirstOrDefault();
                var tabletCate = context.Categories.Where(ct => ct.NameCategory.Equals("Máy tính bảng")).FirstOrDefault();

                context.TypeProducts.Add(new TypeProduct
                {
                    Active = 1,
                    CategoryName = mobileCate.NameCategory,
                    IdCategory = mobileCate.Id,
                    NameType = "Apple",
                    ImageType = _deployUrl + "/project/image/type/logo_iphone.png"
                }) ;

                context.TypeProducts.Add(new TypeProduct
                {
                    Active = 1,
                    CategoryName = mobileCate.NameCategory,
                    IdCategory = mobileCate.Id,
                    NameType = "Samsung",
                    ImageType = _deployUrl + "/project/image/type/logo_samsung.png"
                });

                context.TypeProducts.Add(new TypeProduct
                {
                    Active = 1,
                    CategoryName = mobileCate.NameCategory,
                    IdCategory = mobileCate.Id,
                    NameType = "Readmi",
                    ImageType = _deployUrl + "/project/image/type/readmi.png"
                });

                context.TypeProducts.Add(new TypeProduct
                {
                    Active = 1,
                    CategoryName = mobileCate.NameCategory,
                    IdCategory = mobileCate.Id,
                    NameType = "Xiaomi",
                    ImageType = _deployUrl + "/project/image/type/dienthoai-xiaomi.png"
                });

                context.TypeProducts.Add(new TypeProduct
                {
                    Active = 1,
                    CategoryName = mobileCate.NameCategory,
                    IdCategory = mobileCate.Id,
                    NameType = "Huawei",
                    ImageType = _deployUrl + "/project/image/type/huawei.png"
                });
                context.SaveChanges();
            }

            if (context.Products.Count() <= 0) {
                var mobileCate = context.Categories.Where(ct => ct.NameCategory.Equals("Điện thoại")).FirstOrDefault();
                var huaweiType = context.TypeProducts.Where(type => type.NameType.Equals("Huawei")).FirstOrDefault();
                var appleType = context.TypeProducts.Where(type => type.NameType.Equals("Apple")).FirstOrDefault();
                var samsungType = context.TypeProducts.Where(type => type.NameType.Equals("Samsung")).FirstOrDefault();
                var readmiType = context.TypeProducts.Where(type => type.NameType.Equals("Readmi")).FirstOrDefault();
                var XiaomiType = context.TypeProducts.Where(type => type.NameType.Equals("Xiaomi")).FirstOrDefault();

                #region huawei
                context.Products.Add(new Product
                {
                    Active = 1,
                    IdCategory = mobileCate.Id,
                    CategoryName = mobileCate.NameCategory,
                    IdType = huaweiType.Id,
                    NameType = huaweiType.NameType,
                    NameProduct = "Huawei P40 Pro",
                    ImageProduct = _deployUrl + "/project/image/product/huawei/p40pro/main.jpg",
                    ImageDetailProduct = _deployUrl+ "/project/image/product/huawei/p40pro/main.jpg",/*them */
                    Quantity = 10,
                    PriceListed = 23900000,
                    PricePromotion = 21000000,
                });

                context.Products.Add(new Product
                {
                    Active = 1,
                    IdCategory = mobileCate.Id,
                    CategoryName = mobileCate.NameCategory,
                    IdType = huaweiType.Id,
                    NameType = huaweiType.NameType,
                    NameProduct = "Huawei P40",
                    ImageProduct = _deployUrl + "/project/image/product/huawei/p40/main.jpg",
                    ImageDetailProduct=_deployUrl+ "/project/image/product/huawei/p40/main.jpg",
                    Quantity = 10,
                    PriceListed = 19900000,
                    PricePromotion = 17000000,
                });
                #endregion
                #region apple
                context.Products.Add(new Product
                {
                    Active = 1,
                    IdCategory = mobileCate.Id,
                    CategoryName = mobileCate.NameCategory,
                    IdType = appleType.Id,
                    NameType = appleType.NameType,
                    NameProduct = "iPhone 11 128GB",
                    ImageProduct = _deployUrl + "/project/image/product/apple/iphone/iphone11/main.jpg",
                    ImageDetailProduct=_deployUrl + "/project/image/product/apple/iphone/iphone11/main.jpg",
                    Quantity = 10,
                    PriceListed = 23900000,
                    PricePromotion = 19700000,
                });

                context.Products.Add(new Product
                {
                    Active = 1,
                    IdCategory = mobileCate.Id,
                    CategoryName = mobileCate.NameCategory,
                    IdType = appleType.Id,
                    NameType = appleType.NameType,
                    NameProduct = "iPhone Xs 64GB",
                    ImageProduct = _deployUrl + "/project/image/product/apple/iphone/iphonexs/main.jpg",
                    ImageDetailProduct=_deployUrl+ "/project/image/product/apple/iphone/iphonexs/main.jpg",
                    Quantity = 10,
                    PriceListed = 19900000,
                    PricePromotion = 17300000,
                });
                #endregion
                #region samsung
                context.Products.Add(new Product
                {
                    Active = 1,
                    IdCategory = mobileCate.Id,
                    CategoryName = mobileCate.NameCategory,
                    IdType = samsungType.Id,
                    NameType = samsungType.NameType,
                    NameProduct = "Samsung Galaxy S20+",
                    ImageProduct = _deployUrl + "/project/image/product/samsung/s20p/main.jpg",
                    ImageDetailProduct=_deployUrl + "/project/image/product/samsung/s20p/main.jpg",
                    Quantity = 10,
                    PriceListed = 23900000,
                    PricePromotion = 19700000,
                });

                context.Products.Add(new Product
                {
                    Active = 1,
                    IdCategory = mobileCate.Id,
                    CategoryName = mobileCate.NameCategory,
                    IdType = samsungType.Id,
                    NameType = samsungType.NameType,
                    NameProduct = "Samsung Galaxy Note 20",
                    ImageProduct = _deployUrl + "/project/image/product/samsung/note20/main.jpg",
                    ImageDetailProduct = _deployUrl + "/project/image/product/samsung/note20/main.jpg",
                    Quantity = 10,
                    PriceListed = 19900000,
                    PricePromotion = 17300000,
                });
                #endregion
                context.SaveChanges();
            }
        }
    }
}
