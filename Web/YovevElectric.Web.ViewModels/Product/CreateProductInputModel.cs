﻿namespace YovevElectric.Web.ViewModels.Product
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using YovevElectric.Data.Models;

    public class CreateProductInputModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public Categories Category { get; set; }

        public decimal Price { get; set; }

        public string ImgPath { get; set; }
    }
}
