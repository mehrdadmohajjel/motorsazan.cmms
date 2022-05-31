using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml.Serialization;
using Persia;

namespace Motorsazan.CMMS.Shared.Utilities
{
    public static class Converter
    {
        public static string GetHostName()
        {
            return HttpContext.Current.Request.UserHostName;
        }

        public static string UniqueString()
        {
            return Guid.NewGuid().ToString();
        }

        public static string GetCalledUrl()
        {
            return HttpContext.Current.Request.HttpMethod + " " + HttpContext.Current.Request.Url.OriginalString;
        }

        public static string GetUserAgent()
        {
            return HttpContext.Current.Request.UserAgent;
        }

        public static string ToString(Enum enumValue)
        {
            return Enum.GetName(enumValue.GetType(), enumValue);
        }

        public static T ToEnum<T>(string stringValue) where T : struct
        {
            return (T) Enum.Parse(typeof(T), stringValue, true);
        }

        public static string ToPersianString(string number)
        {
            return PersianWord.ToPersianString(number);
        }

        public static string ToEnglishString(string number)
        {
            var arabic = new string[10] {"٠", "١", "٢", "٣", "٤", "٥", "٦", "٧", "٨", "٩"};
            var persian = new string[10] {"۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹"};

            if (string.IsNullOrEmpty(number) == false)
                for (var i = 0; i < persian.Length; i++)
                {
                    number = number.Replace(persian[i], i.ToString());
                    number = number.Replace(arabic[i], i.ToString());
                }

            var englishString = number;

            return englishString;
        }

        public static string ToSHA256Hash(string plainTextValue)
        {
            string hashValue;

            var data = Encoding.UTF8.GetBytes(plainTextValue);
            using (var shaM = new SHA256Managed())
            {
                var result = shaM.ComputeHash(data);
                hashValue = BitConverter.ToString(result).Replace("-", "");
            }

            return hashValue;
        }

        public static T[] RemoveAt<T>(this T[] source, int index)
        {
            var instance = new T[source.Length - 1];
            if (index > 0) Array.Copy(source, 0, instance, 0, index);

            if (index < source.Length - 1) Array.Copy(source, index + 1, instance, index, source.Length - index - 1);

            return instance;
        }

        public static T XmlDeserializer<T>(string xmlString)
        {
            var sw = Stopwatch.StartNew();

            try
            {
                var instance = default(T);
                var xmlSerializer = new XmlSerializer(typeof(T));
                using (var stringreader = new StringReader(xmlString))
                {
                    instance = (T) xmlSerializer.Deserialize(stringreader);
                }

                //Log.Trace("desalinizing xml string successfully completed.", sw.Elapsed.TotalMilliseconds);
                return instance;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public static string XmlSerializer<T>(T xmlObject)
        {
            var sw = Stopwatch.StartNew();

            try
            {
                var xmlString = string.Empty;
                var xmlSerializer = new XmlSerializer(xmlObject.GetType());
                using (var textWriter = new StringWriter())
                {
                    xmlSerializer.Serialize(textWriter, xmlObject);
                    xmlString = textWriter.ToString();
                }

                //Log.Trace("serializing xml object successfully completed.", sw.Elapsed.TotalMilliseconds);
                return xmlString;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}