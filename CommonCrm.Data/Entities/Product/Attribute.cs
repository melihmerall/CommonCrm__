using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCrm.Data.Entities.Product
{
	public class Attribute : BaseEntity
	{
		
		public int ProductId { get; set; }
		public Product Product { get; set; }



	}
}
