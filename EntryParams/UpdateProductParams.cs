﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.EntryParams
{
    public class UpdateProductParams
    {
        public int Select_nameCate { get; set; }
        public string Select_nameType { get; set; }
        public int Select_Status { get; set; }
        public int Id_Product {get; set;}
        public string Name_Product { get; set; }
        public int Quantity_Product { get; set; }
        public int Price_listed { get; set; }
        public int Price_Product { get; set; }
        public string Screen { get; set; }
        public string Operating_System { get; set; }
        public string Back_Camera { get; set; }
        public string Front_Camera { get; set; }
        public string CPU { get; set; }
        public string Internal_Memory { get; set; }
        public string RAM { get; set; }
        public string Memory_Stick { get; set; }
        public string Sim_Stick { get; set; }
        public string Battery_Capacity { get; set; }
        public IFormFile Image_product { get; set; }
        public string Image_product_old { get; set; }
    }
}
