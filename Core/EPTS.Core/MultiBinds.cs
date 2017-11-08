using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace EPTS.Core
{
    public class MultiBinds : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            object[] result = null;
            Array.Resize(ref result, values.Length);
            for (int x = 0; x < values.Length; x++)
            {
                result[x] = values[x];
            }
            //try
            //{
            //    result = new object[] 
            //    {
            //        values[0],
            //        values[1]
            //    };
            //}
            //catch
            //{

            //}
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            object[] splitValues = ((string)value).Split(' ');
            return splitValues;
        }
    }
}
