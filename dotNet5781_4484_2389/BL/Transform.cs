using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class Transform
    {
        public static object trans<S>(this S original, Type tp) //transform BO to DO and back
        {
            object copyToObject = Activator.CreateInstance(tp);
            foreach (PropertyInfo propertyInfo in tp.GetProperties())
            {
                PropertyInfo propFrom = typeof(S).GetProperty(propertyInfo.Name);
                if (propFrom != null)
                {
                    var value = propFrom.GetValue(original, null);
                    if (value is ValueType || value is string)
                        propertyInfo.SetValue(copyToObject, value);
                }
            }
            return copyToObject;
        }
    }
}
