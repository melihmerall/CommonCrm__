using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCrm.Data.Entities
{
	public class BaseEntity
	{
		public int Id { get; set; }
		public Guid? OwnerId { get; set; }

		public DateTime CreatedDate { get; set; } = DateTime.Now;

		public DateTime ModifiedDate { get; set; } = DateTime.Now;

		public string? CreatedBy { get; set; } 

		public string? ModifiedBy { get; set; } 
		public string? ImagePath { get; set; }
	}
}
