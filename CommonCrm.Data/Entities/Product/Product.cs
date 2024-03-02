using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCrm.Data.Entities.Product
{
	public class Product: BaseEntity
	{
        public required string Name { get; set; }
		public  string? EnglishName { get; set; }
		public  string? Code { get; set; }
		public ICollection<Category> Categories { get; set; } = new HashSet<Category>();
		public int UnitId { get; set; }
		public ProductUnit Unit { get; set; }

		public int AttributeId { get; set; }
		public Attribute Attribute { get; set; }

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

        public int OfferQuantity { get; set; } //

        public string Description{ get; set; }






    }
}
