﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalBook.Models.ViewModels
{
    public class ProductVM
    {
        public Product Products { get; set; }
        public string? Dealer { get; set; }
        public string? UserId { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
