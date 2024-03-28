using System.ComponentModel.DataAnnotations;
using CommonCrm.Business.Extensions.Utilities;
using CommonCrm.Data.Entities.Product;
using Microsoft.AspNetCore.Mvc.Rendering;
using Attribute = CommonCrm.Data.Entities.Product.Attribute;

namespace CommonCrm.Business.DTOs;

public class CreateProductDto
{
        [Required(ErrorMessage = Constants.Required)]
        public string Name { get; set; }
        
        [Required(ErrorMessage = Constants.Required)]
        public string? EnglishName { get; set; }
        [Required(ErrorMessage = Constants.Required)]
        
        public string? Code { get; set; }
        
        public decimal? CurrencyDollar { get; set; }
        public decimal? CurrencyEuro { get; set; }

        public int[]? CategoryIds { get; set; }

        public int[]? CollectionIds { get; set; }

        public int? UnitId { get; set; }

        // TL
        public decimal? SalesPriceTL { get; set; }
        public int? KdvTL { get; set; }
        public decimal? TotalTL { get; set; }
        public bool IsTlDefault { get; set; }


        // Dolar
        public decimal? SalesPriceDolar { get; set; }
        public int? KdvDolar { get; set; }
        public decimal? TotalDolar { get; set; }
        public bool IsDolarDefault { get; set; }

        // Euro
        public decimal? SalesPriceEuro { get; set; }
        public int? KdvEuro { get; set; }
        public decimal? TotalEuro { get; set; }
        public bool IsEuroDefault { get; set; }

        public int? OfferQuantity { get; set; } = 1;
        public string? OfferDescription { get; set; }
        public string? Description { get; set; }
        public string? EnglishDescription { get; set; }

        
        public List<SelectListItem?> ProductCollections { get; set; }
        public List<SelectListItem?> Categories { get; set; }
        public List<SelectListItem>? Units { get; set; }
        
        public string? ImagePath { get; set; }
        
        public ProductUnit ProductUnit { get; set; }
        
        //Attribute
        public int? Width { get; set; } //Genişlik cm
        public int? Height { get; set; }// Yükseklik cm
        public int? Depth { get; set; } // Derinlik cm
        public int? Volume { get; set; } //Hacim m3
        public int? Weight { get; set; } //ağırlık kg
        public int? Packet { get; set; } //paket

    


}