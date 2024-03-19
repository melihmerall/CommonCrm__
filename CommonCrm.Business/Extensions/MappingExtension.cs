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
    if (source == null)
        throw new ArgumentNullException(nameof(source), "Source object cannot be null.");

    var destination = Activator.CreateInstance<TDestination>();

    var sourceProperties = source.GetType().GetProperties();
    var destinationProperties = destination?.GetType().GetProperties();

    foreach (var sourceProperty in sourceProperties)
    {
        var destinationProperty = destinationProperties?.FirstOrDefault(p => p.Name == sourceProperty.Name);

        if (destinationProperty != null && destinationProperty.CanWrite)
        {
            // İki özellik tipi aynı ise veya kaynak özelliğin değeri null ise
            // Doğrudan atama yapabiliriz
            if (destinationProperty.PropertyType == sourceProperty.PropertyType || sourceProperty.GetValue(source) == null)
            {
                var sourceValue = sourceProperty.GetValue(source);
                destinationProperty.SetValue(destination, sourceValue);
            }
            else
            {
                // Eğer iki özellik tipi farklıysa ve kaynak özellik değeri null değilse
                // Tip dönüşümü yaparak atama yapabiliriz
                if (sourceProperty.PropertyType == typeof(string))
                {
                    var sourceValue = sourceProperty.GetValue(source)?.ToString();
                    destinationProperty.SetValue(destination, Convert.ChangeType(sourceValue, destinationProperty.PropertyType));
                }
                else if (destinationProperty.PropertyType == typeof(string))
                {
                    var sourceValue = sourceProperty.GetValue(source);
                    destinationProperty.SetValue(destination, Convert.ChangeType(sourceValue, destinationProperty.PropertyType));
                }
                // Burada diğer tip dönüşümlerini de ekleyebilirsiniz
                // Örneğin: int, decimal, DateTime vs.
            }
        }
    }

    return destination;
}
	}
}
