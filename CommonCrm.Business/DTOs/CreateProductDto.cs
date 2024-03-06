using CommonCrm.Data.Entities.Product;
using Microsoft.AspNetCore.Mvc.Rendering;
using Attribute = CommonCrm.Data.Entities.Product.Attribute;

namespace CommonCrm.Business.DTOs;

public class CreateProductDto
{
        public string Name { get; set; }
        public string? EnglishName { get; set; }
        public string? Code { get; set; }
        public int[] CategoryIds { get; set; }
        public int UnitId { get; set; }
        public int AttributeId { get; set; }

        // TL
        public decimal SalesPriceTL { get; set; }
        public decimal KdvTL { get; set; }
        public int TotalTL { get; set; }

        // Dolar
        public decimal SalesPriceDolar { get; set; }
        public decimal KdvDolar { get; set; }
        public int TotalDolar { get; set; }

        // Euro
        public decimal SalesPriceEuro { get; set; }
        public decimal KdvEuro { get; set; }
        public int TotalEuro { get; set; }

        public int OfferQuantity { get; set; }
        public string? Description { get; set; }
        
        public List<SelectListItem?> Attributes { get; set; }
        public List<SelectListItem?> ProductUnits { get; set; }
        public List<SelectListItem?> Categories { get; set; }

    


}