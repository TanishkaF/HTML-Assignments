using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System;

namespace SchoolEntity.Extension
{
    public static class Helper
    {
        public static List<T> ConvertDataTableToList<T>(this DataTable dt) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in dt.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);

                            // Handle nullable types
                            if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            {
                                if (row[prop.Name] == DBNull.Value)
                                {
                                    propertyInfo.SetValue(obj, null, null);
                                }
                                else
                                {
                                    propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], Nullable.GetUnderlyingType(propertyInfo.PropertyType)), null);
                                }
                            }
                            else
                            {
                                propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }
    }
}
  