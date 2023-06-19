using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using RetroAdventureCreator.Infrastructure.Game.Enums;

namespace RetroAdventureCreator.Core.Extensions;

/// <summary>
/// .NET Object extension methods
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    /// Method for get all values for properties of specified type in a object (search recursive in depth mode)
    /// </summary>
    /// <typeparam name="T">Type of properties for get values</typeparam>
    /// <param name="object">Main object instance</param>
    /// <returns>Enumerable whith all properties found</returns>
    public static IEnumerable<T> GetDepthPropertyValuesOfType<T>(this object @object) where T : class
    {
        if (@object == null)
        {
            return Enumerable.Empty<T>();
        }

        var properties = @object.GetType().GetProperties().AsEnumerable();

        var result = GetPropertiesValuesOfType<T>(@object, properties).ToList();

        properties = properties.Where(item => item.PropertyType != typeof(T) || item.PropertyType == typeof(IEnumerable<T>));
        var depthResult = LoopObjectProperties<T>(@object, properties);

        result.AddRange(depthResult);
        return result;
    }

    private static IEnumerable<T> LoopObjectProperties<T>(object @object, IEnumerable<PropertyInfo> properties) where T : class
    {
        var result = new List<T>();
        var propertyObjects = FindPropertiesOfClassType(properties);
        foreach (var propertyObject in propertyObjects)
        {
            var propertyObjectValue = propertyObject.GetValue(@object);
            if (propertyObjectValue == null)
            {
                continue;
            }

            if (propertyObject.PropertyType.IsGenericType && propertyObject.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                foreach (var property in (System.Collections.IEnumerable)propertyObjectValue)
                {
                    result.AddRange(GetDepthPropertyValuesOfType<T>(property));
                }

            }
            else
            {
                result.AddRange(GetDepthPropertyValuesOfType<T>(propertyObjectValue));
            }
        }

        return result;
    }

    private static IEnumerable<T> GetPropertiesValuesOfType<T>(object @object, IEnumerable<PropertyInfo> properties) where T : class
    {
        // get properties values of IEnumerable type
        var propertiesOfTypeList = properties.Where(item => item.PropertyType == typeof(IEnumerable<T>))
            .Select(item => item.GetValue(@object))
            .Where(item => item != null).OfType<IEnumerable<T>>()
            .SelectMany(item => item);

        // get properties values of T type
        var propertiesOfType = properties.Where(item => item.PropertyType == typeof(T))
            .Select(item => item.GetValue(@object))
            .Where(item => item != null).OfType<T>();

        return propertiesOfTypeList.Concat(propertiesOfType).ToList();
    }

    private static IEnumerable<PropertyInfo> FindPropertiesOfClassType(IEnumerable<PropertyInfo> properties) => 
        properties.Where(item =>
        {
            var propertyType = item.PropertyType;
            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                propertyType = propertyType.GetGenericArguments()[0];
            }

            return propertyType.IsClass && propertyType != typeof(string);
        })
        .Select(item => item);
}
