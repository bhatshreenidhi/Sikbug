using System;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;

namespace Sikbug.Utils
{
    public class Parser
    {
        public static DateTime ParseDateTime(string date)
        {
            var dayOfWeek = date.Substring(0, 3).Trim();
            var month = date.Substring(4, 3).Trim();
            var dayInMonth = date.Substring(8, 2).Trim();
            var time = date.Substring(11, 9).Trim();
            var offset = date.Substring(20, 5).Trim();
            var year = date.Substring(25, 5).Trim();
            var dateTime = string.Format("{0}-{1}-{2} {3}", dayInMonth, month, year, time);
            var ret = DateTime.Parse(dateTime).ToLocalTime();
            return ret;
        }

        public static T Deserialize<T>(string json)
        {
            T result = default(T);

            // load json into memorystream and deserialize
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(T));
            result = (T)s.ReadObject(ms);
            ms.Close();

            return result;
        }
    }
}
