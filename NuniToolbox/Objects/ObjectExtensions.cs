using NuniToolbox.Collections;

namespace NuniToolbox.Objects;

/// <summary>
/// A class with generic extension methods for objects.
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    /// Returns a shallow copy of the speficied object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj">The object to copy</param>
    /// <param name="excludedProperties">Collection of property names to exclude</param>
    /// <returns></returns>
    public static T ShallowCopy<T>(this T obj, ICollection<string> excludedProperties = null) where T : new()
    {
        if (excludedProperties == null)
        {
            excludedProperties = new HashSet<string>();
        }

        T clonedObj = new();

        clonedObj.GetType().GetProperties()
            .Where(propertyInfo => propertyInfo.CanRead && propertyInfo.CanWrite && !excludedProperties.Contains(propertyInfo.Name))
            .Foreach(propertyInfo => propertyInfo.SetValue(clonedObj, propertyInfo.GetValue(obj)));

        return clonedObj;
    }

    /// <summary>
    /// Returns a deep copy of the speficied object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj">The object to copy</param>
    /// <param name="excludedPropertiesByType">Collection of property names to exclude by fully qualified type name (namespace + class name)</param>
    /// <returns></returns>
    public static T DeepCopy<T>(this T obj, IDictionary<string, ICollection<string>> excludedPropertiesByType = null) where T : new()
    {
        if (obj is not null)
        {
            return (T)DeepCopy(obj, obj.GetType(), excludedPropertiesByType);
        }
        else
        {
            return default;
        }
    }

    private static object DeepCopy(object obj, Type type, IDictionary<string, ICollection<string>> excludedPropertiesByType)
    {
        if (obj is not null)
        {
            if (excludedPropertiesByType == null)
            {
                excludedPropertiesByType = new Dictionary<string, ICollection<string>>();
            }

            excludedPropertiesByType.TryGetValue(type.FullName, out ICollection<string> excludedProperties);

            excludedProperties ??= new HashSet<string>();

            object clonedObj = type.GetConstructor(Type.EmptyTypes).Invoke(Type.EmptyTypes);

            type.GetProperties()
                .Where(propertyInfo => propertyInfo.CanRead && propertyInfo.CanWrite && !excludedProperties.Contains(propertyInfo.Name))
                .Foreach(propertyInfo =>
                {
                    if (propertyInfo.PropertyType.IsValueType || propertyInfo.PropertyType == typeof(string))
                    {
                        propertyInfo.SetValue(clonedObj, propertyInfo.GetValue(obj));
                    }
                    else
                    {
                        object propertyValue = propertyInfo.GetValue(obj);

                        if (propertyValue is not null)
                        {
                            object clonedProperty = DeepCopy(propertyValue, propertyValue.GetType(), excludedPropertiesByType);
                            propertyInfo.SetValue(clonedObj, clonedProperty);
                        }
                        else
                        {
                            propertyInfo.SetValue(clonedObj, null);
                        }
                    }
                });

            return clonedObj;
        }
        else
        {
            return null;
        }
    }
}
