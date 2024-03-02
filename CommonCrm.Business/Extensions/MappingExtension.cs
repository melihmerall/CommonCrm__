using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCrm.Business.Extensions
{
	public static class MappingExtension
	{
		public static TDestination MapTo<TDestination>(this object source)
		{
			var destination = Activator.CreateInstance<TDestination>();

			var sourceProperties = source.GetType().GetProperties();
			var destinationProperties = destination?.GetType().GetProperties();

			foreach (var sourceProperty in sourceProperties)
			{
				var destinationProperty = destinationProperties?.FirstOrDefault(p => p.Name == sourceProperty.Name);

				if (destinationProperty != null && destinationProperty.CanWrite)
				{
					var sourceValue = sourceProperty.GetValue(source);
					destinationProperty.SetValue(destination, sourceValue);
				}
			}
	
			return destination;
		}
	}
}
