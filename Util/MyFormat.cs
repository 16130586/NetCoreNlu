using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetProject.Util
{
    public class MyFormat
    {
        public static string ToVietNameseCurrency(int number)
        {
            CultureInfo cultureInfo = new CultureInfo("vi-VN");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            NumberFormatInfo LocalFormat = (NumberFormatInfo)NumberFormatInfo.CurrentInfo.Clone();
            return number.ToString("#,###đ", LocalFormat);
        }
    }
}
