using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using NuniToolbox.Collections;

namespace NuniToolbox.Objects
{
    /// <summary>
    /// A class with generic extension methods for objects.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Returns a shallow copy of the speficied object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ShallowCopy<T>(this T obj) where T : new()
        {
            T clonedObj = new T();

            clonedObj.GetType().GetProperties()
                .Where(propertyInfo => propertyInfo.CanRead && propertyInfo.CanWrite)
                .Foreach(propertyInfo => propertyInfo.SetValue(clonedObj, propertyInfo.GetValue(obj)));

            return clonedObj;
        }

        /// <summary>
        /// Returns a deep copy of the speficied object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepCopy<T>(this T obj) where T : new()
        {
            return (T)DeepCopy(obj, obj.GetType());
        }

        private static object DeepCopy(object obj, Type type)
        {
            if (obj != null)
            {
                object clonedObj = type.GetConstructor(Type.EmptyTypes).Invoke(Type.EmptyTypes);

                type.GetProperties()
                    .Where(propertyInfo => propertyInfo.CanRead && propertyInfo.CanWrite)
                    .Foreach(propertyInfo =>
                    {
                        if (propertyInfo.PropertyType.IsValueType || propertyInfo.PropertyType == typeof(string))
                        {
                            propertyInfo.SetValue(clonedObj, propertyInfo.GetValue(obj));
                        }
                        else
                        {
                            object clonedProperty = DeepCopy(propertyInfo.GetValue(obj), propertyInfo.PropertyType);
                            propertyInfo.SetValue(clonedObj, clonedProperty);
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
}
