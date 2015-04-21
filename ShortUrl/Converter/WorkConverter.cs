using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ShortUrl.Converter
{
    public class EnumConverter: IValueConverter
    {
        // 摘要:
        //     轉換值。
        //
        // 參數:
        //   value:
        //     由繫結來源所產生的值。
        //
        //   targetType:
        //     繫結目標屬性的型別。
        //
        //   parameter:
        //     要使用的轉換子參數。
        //
        //   culture:
        //     要在轉換子中使用的文化特性 (Culture)。
        //
        // 傳回:
        //     已轉換的值。 如果方法傳回 null，就會使用有效的 null 值。
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(parameter);
        }
        //
        // 摘要:
        //     轉換值。
        //
        // 參數:
        //   value:
        //     由繫結目標所產生的值。
        //
        //   targetType:
        //     要轉換成的型別。
        //
        //   parameter:
        //     要使用的轉換子參數。
        //
        //   culture:
        //     要在轉換子中使用的文化特性 (Culture)。
        //
        // 傳回:
        //     已轉換的值。 如果方法傳回 null，就會使用有效的 null 值。
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? parameter : ShortUrl.Model.EnumType.Shortener;
        }
    }
}
