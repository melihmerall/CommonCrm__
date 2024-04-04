using System.ComponentModel.DataAnnotations;
using CommonCrm.Data.Entities.Product;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CommonCrm.Business.DTOs;

public class GetProductsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
        
    public string? EnglishName { get; set; }
        
    public string? Code { get; set; }

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
    public string? ImagePath { get; set; }
        
    public List<Collection> ProductCollections { get; set; }
    public List<Category> Categories { get; set; }
    public ProductUnit Unit { get; set; }
        
    //Attribute
    public int? Width { get; set; } //Genişlik cm
    public int? Height { get; set; }// Yükseklik cm
    public int? Depth { get; set; } // Derinlik cm
    public int? Volume { get; set; } //Hacim m3
    public int? Weight { get; set; } //ağırlık kg
    public int? Packet { get; set; } //paket
}