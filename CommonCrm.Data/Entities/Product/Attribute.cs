using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCrm.Data.Entities.Product
{
	public class Attribute : BaseEntity
	{
		public int Width { get; set; }
		public int Height { get; set; }
		public int Depth { get; set; }
		public int Volume { get; set; }
        public int Weight { get; set; }
		public int Packet { get; set; }
		public int ProductId { get; set; }
		public Product Product { get; set; }



	}
}
