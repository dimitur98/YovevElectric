﻿using System;
using System.Collections.Generic;
using System.Text;
using YovevElectric.Data.Models;

namespace YovevElectric.Web.ViewModels.Product
{
    public class EditProductViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Categories Category { get; set; }

        public decimal Price { get; set; }
    }
}