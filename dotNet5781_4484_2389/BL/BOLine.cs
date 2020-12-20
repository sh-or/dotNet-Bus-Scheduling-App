using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
/*מידע על קו אוטובוס : כולל אוסף התחנות במסלול הקו (על פי סדר ההגעה לתחנות) עם מידע רלוונטי (בלבד) לתצוגת קו אוטובוס
 * (קוד ושם תחנה - אך אין צורך במיקום לפי קו אורך וקו רוחב ופרטים נוספים על התחנה), ובתוספת מידע על מרחקים וזמני נסיעה בין התחנות. 
לכך בניתם בתרגיל ישות של תחנת קו - נדרשת עבודה על הישות להתאים אותה למה שכתוב כאן.
מידע על תחנת אוטובוס: כולל רשימת הקווים שעוברים בתחנה - 
מזהה קו, מספר קו, תחנת סיום. לבונוס - זמני הגעה לתחנה ומידע רלוונטי נוסף שאינו מיותר לתצוגת מידע על תחנת אוטובוס.
*/
{
    public class Line : StationLine //include code,line and last station
    {
        public AreaEnum Area { get; set; }
        public int FirstStation { get; set; }
        public bool IsExist { get; set; } //???
        //public bool IsIntercity { get; set; }
        public List<LineStation> Stations { get; set; }
    }
}
    //public class Weather
    //{
    //    public int Feeling { get; set; }
    //}

    // public class BO
    //{
    // }

