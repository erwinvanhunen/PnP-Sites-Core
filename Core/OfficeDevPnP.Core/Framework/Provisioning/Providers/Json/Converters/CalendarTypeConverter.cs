using Microsoft.SharePoint.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class CalendarTypeConverter : JsonConverter<CalendarType>
    {
        public override CalendarType ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, CalendarType existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            /**
             *  "None",
                "Gregorian",
                "Japan",
                "Taiwan",
                "Korea",
                "Hijri",
                "Thai",
                "Hebrew",
                "Gregorian Middle East French Calendar",
                "Gregorian Arabic Calendar",
                "Gregorian Transliterated English Calendar",
                "Gregorian Transliterated French Calendar",
                "Korea and Japanese Lunar",
                "Chinese Lunar",
                "Saka Era",
                "Umm al-Qura"
               */
            var value = serializer.Deserialize<string>(reader);
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

        public override void WriteJson(JsonWriter writer, CalendarType value, JsonSerializer serializer)
        {
            switch (value)
            {
                case CalendarType.None:
                    {
                        serializer.Serialize(writer, "None");
                        break;
                    }
                case CalendarType.Gregorian:
                    {
                        serializer.Serialize(writer, "Gregorian");
                        break;
                    }
                case CalendarType.Japan:
                    {
                        serializer.Serialize(writer, "Japan");
                        break;
                    }
                case CalendarType.Taiwan:
                    {
                        serializer.Serialize(writer, "Taiwan");
                        break;
                    }
                case CalendarType.Korea:
                    {
                        serializer.Serialize(writer, "Korea");
                        break;
                    }
                case CalendarType.Hijri:
                    {
                        serializer.Serialize(writer, "Hijri");
                        break;
                    }
                case CalendarType.Thai:
                    {
                        serializer.Serialize(writer, "Thai");
                        break;
                    }
                case CalendarType.Hebrew:
                    {
                        serializer.Serialize(writer, "Hebrew");
                        break;
                    }
                case CalendarType.GregorianMEFrench:
                    {
                        serializer.Serialize(writer, "Gregorian Middle East French Calendar");
                        break;
                    }
                case CalendarType.GregorianArabic:
                    {
                        serializer.Serialize(writer, "Gregorian Arabic Calendar");
                        break;
                    }
                case CalendarType.GregorianXLITEnglish:
                    {
                        serializer.Serialize(writer, "Gregorian Transliterated English Calendar");
                        break;
                    }
                case CalendarType.GregorianXLITFrench:
                    {
                        serializer.Serialize(writer, "Gregorian Transliterated French Calendar");
                        break;
                    }
                case CalendarType.KoreaJapanLunar:
                    {
                        serializer.Serialize(writer, "Korea and Japanese Lunar");
                        break;
                    }
                case CalendarType.ChineseLunar:
                    {
                        serializer.Serialize(writer, "Chinese Lunar");
                        break;
                    }
                case CalendarType.SakaEra:
                    {
                        serializer.Serialize(writer, "Saka Era");
                        break;
                    }
                case CalendarType.UmAlQura:
                    {
                        serializer.Serialize(writer, "Umm al-Qura");
                        break;
                    }
            }
        }
    }
}
