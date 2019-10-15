using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class CalendarTypeConverter : JsonConverter<CalendarType>
    {
        public override CalendarType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            switch (value)
            {
                case "None":
                    return CalendarType.None;
                case "Gregorian":
                    return CalendarType.Gregorian;
                case "Japan":
                    return CalendarType.Japan;
                case "Taiwan":
                    return CalendarType.Taiwan;
                case "Korea":
                    return CalendarType.Korea;
                case "Hijri":
                    return CalendarType.Hijri;
                case "Thai":
                    return CalendarType.Thai;
                case "Hebrew":
                    return CalendarType.Hebrew;
                case "Gregorian Middle East French Calendar":
                    return CalendarType.GregorianMEFrench;
                case "Gregorian Arabic Calendar":
                    return CalendarType.GregorianArabic;
                case "Gregorian Transliterated English Calendar":
                    return CalendarType.GregorianXLITEnglish;
                case "Gregorian Transliterated French Calendar":
                    return CalendarType.GregorianXLITFrench;
                case "Korea and Japanese Lunar":
                    return CalendarType.KoreaJapanLunar;
                case "Chinese Lunar":
                    return CalendarType.ChineseLunar;
                case "Saka Era":
                    return CalendarType.SakaEra;
                case "Umm al-Qura":
                    return CalendarType.UmAlQura;
                default:
                    return CalendarType.Gregorian;
            }
        }

        
        public override void Write(Utf8JsonWriter writer, CalendarType value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case CalendarType.None:
                    {
                        writer.WriteStringValue("None");
                        break;
                    }
                case CalendarType.Gregorian:
                    {
                        writer.WriteStringValue("Gregorian");
                        break;
                    }
                case CalendarType.Japan:
                    {
                        writer.WriteStringValue("Japan");
                        break;
                    }
                case CalendarType.Taiwan:
                    {
                        writer.WriteStringValue("Taiwan");
                        break;
                    }
                case CalendarType.Korea:
                    {
                        writer.WriteStringValue("Korea");
                        break;
                    }
                case CalendarType.Hijri:
                    {
                        writer.WriteStringValue("Hijri");
                        break;
                    }
                case CalendarType.Thai:
                    {
                        writer.WriteStringValue("Thai");
                        break;
                    }
                case CalendarType.Hebrew:
                    {
                        writer.WriteStringValue("Hebrew");
                        break;
                    }
                case CalendarType.GregorianMEFrench:
                    {
                        writer.WriteStringValue("Gregorian Middle East French Calendar");
                        break;
                    }
                case CalendarType.GregorianArabic:
                    {
                        writer.WriteStringValue("Gregorian Arabic Calendar");
                        break;
                    }
                case CalendarType.GregorianXLITEnglish:
                    {
                        writer.WriteStringValue("Gregorian Transliterated English Calendar");
                        break;
                    }
                case CalendarType.GregorianXLITFrench:
                    {
                        writer.WriteStringValue("Gregorian Transliterated French Calendar");
                        break;
                    }
                case CalendarType.KoreaJapanLunar:
                    {
                        writer.WriteStringValue("Korea and Japanese Lunar");
                        break;
                    }
                case CalendarType.ChineseLunar:
                    {
                        writer.WriteStringValue("Chinese Lunar");
                        break;
                    }
                case CalendarType.SakaEra:
                    {
                        writer.WriteStringValue("Saka Era");
                        break;
                    }
                case CalendarType.UmAlQura:
                    {
                        writer.WriteStringValue("Umm al-Qura");
                        break;
                    }
            }
        }
    }
}
