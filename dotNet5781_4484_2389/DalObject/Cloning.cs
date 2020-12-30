using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DL
{
    static class Cloning
    {

        internal static T Clone<T>(this T original) where T : new()
        {
            T copyToObject = new T();
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                propertyInfo.SetValue(copyToObject, propertyInfo.GetValue(original, null), null);
            return copyToObject;
        }
        
        //public static object transDOtoBO<S>(this S original, Type tp) //transform DO to BO struct
        //{
        //    object copyToObject = Activator.CreateInstance(tp);
        //    foreach (PropertyInfo propertyInfo in typeof(S).GetProperties())
        //    {
        //        PropertyInfo propFrom = typeof(S).GetProperty(propertyInfo.Name);
        //        if (propFrom != null)
        //        {
        //            var value = propFrom.GetValue(original, null);
        //            if (value is ValueType || value is string)
        //                propertyInfo.SetValue(copyToObject, value);
        //        }
        //    }
        //    return copyToObject;
        //}

        //internal static T Clone<T>(this T original)//דרך שלישית - בונוס
        //{
        //    T target = (T)Activator.CreateInstance(original.GetType());
        //    //...
        //    target = original; //+copy c-tor for each class  ??
        //    return target;
        //}

        //internal static IClonable Clone(this IClonable original)//דרך שניה - בונוס (יש להשתמש בממשק)
        //{
        //    IClonable target = (IClonable)Activator.CreateInstance(original.GetType());
        //    //...
        // target= original; //+copy c-tor for each class
        //    return target;
        //}

        //internal static T Clone<T>(this T original)//דרך שלישית - בונוס
        //{
        //    T target = (T)Activator.CreateInstance(original.GetType());
        //    //...
        // target= original; //+copy c-tor for each class
        //    return target;
        //}

        //internal static WindDirection Clone(this WindDirection original) //דרך ראשונה
        //{
        //    WindDirection target = new WindDirection();
        //    target.direction = original.direction;
        //    return target;
        //}
    }
}