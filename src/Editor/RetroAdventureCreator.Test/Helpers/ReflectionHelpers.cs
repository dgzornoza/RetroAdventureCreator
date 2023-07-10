using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RetroAdventureCreator.Test.Helpers;

internal static class ReflectionHelpers
{
    public static object? InvokeMethod<T>(this T instance, string methodName, params object[] args)
    {
        var method = typeof(T).GetTypeInfo()
            .GetMethod(methodName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

        try
        {
            return method?.Invoke(instance, args);
        }
        catch (TargetInvocationException ex)
        {
            throw ex.InnerException ?? ex;
        }
    }

    public static void SetPropertyValue<T>(this T instance, string propertyName, object value)
    {
        var property = typeof(T).GetTypeInfo().GetDeclaredProperty(propertyName);
        try
        {
            property?.SetValue(instance, value);
        }
        catch (TargetInvocationException ex)
        {
            throw ex.InnerException ?? ex;
        }
    }
}
