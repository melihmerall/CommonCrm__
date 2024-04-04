using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCrm.Data.Entities.Product
{
	public class Product: BaseEntity
	{
        public  string? Name { get; set; }
		public  string? EnglishName { get; set; }
		public  string? Code { get; set; }
		public ICollection<Category> Categories { get; set; } = new HashSet<Category>();
		public int? UnitId { get; set; }
		public ProductUnit Unit { get; set; }
		
		public ICollection<Collection> Collections { get; set; } = new HashSet<Collection>();

		//Tl
		public decimal? SalesPriceTL { get; set; }
		public int? KdvTL { get; set; }
		public decimal? TotalTL { get; set; }
		public bool IsTlDefault { get; set; }


		// Dolar
		public decimal? SalesPriceDolar { get; set; }
		public int? KdvDolar { get; set; }
		public decimal? TotalDolar { get; set; }
		public bool IsDolarDefault { get; set; }
		public decimal? CurrencyDollar { get; set; }
		public decimal? CurrencyEuro { get; set; }

		// Euro
		public decimal? SalesPriceEuro { get; set; }
		public int? KdvEuro { get; set; }
		public decimal? TotalEuro { get; set; }
		public bool IsEuroDefault { get; set; }

        public int? OfferQuantity { get; set; } //
        public string? OfferDescription { get; set; } //

        public string? Description { get; set; }
        public string? EnglishDescription { get; set; }

        
        
        
        //Attribute
        public int? Width { get; set; } //Genişlik cm
        public int? Height { get; set; }// Yükseklik cm
        public int? Depth { get; set; } // Derinlik cm
        public int? Volume { get; set; } //Hacim 
        public int? Weight { get; set; } //ağırlık
        public int? Packet { get; set; } //paket
    }
}
