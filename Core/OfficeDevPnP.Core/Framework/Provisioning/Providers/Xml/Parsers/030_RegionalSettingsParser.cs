using System;
using System.Xml.Linq;
using OfficeDevPnP.Core.Framework.Provisioning.Model;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.Parsers
{
    [BaseElementParser(
        SupportedSchemas = XMLPnPSchemaVersion.V201605,
        Sequence = 30)]
    internal class RegionalSettingsParser : IBaseElementParser
    {
        public ProvisioningTemplate ParseElement(XMLPnPSchemaVersion schema, ProvisioningTemplate outgoingTemplate, IProvisioningTemplate incomingTemplate)
        {
            switch (schema)
            {
                case XMLPnPSchemaVersion.V201605:
                    {
                        var source = incomingTemplate as V201605.ProvisioningTemplate;
                        if (source.RegionalSettings != null)
                        {
                            outgoingTemplate.RegionalSettings = new Model.RegionalSettings()
                            {
                                AdjustHijriDays = source.RegionalSettings.AdjustHijriDaysSpecified ? source.RegionalSettings.AdjustHijriDays : 0,
                                AlternateCalendarType = source.RegionalSettings.AlternateCalendarTypeSpecified ? source.RegionalSettings.AlternateCalendarType.FromSchemaToTemplateCalendarTypeV201605() : Microsoft.SharePoint.Client.CalendarType.None,
                                CalendarType = source.RegionalSettings.CalendarTypeSpecified ? source.RegionalSettings.CalendarType.FromSchemaToTemplateCalendarTypeV201605() : Microsoft.SharePoint.Client.CalendarType.None,
                                Collation = source.RegionalSettings.CollationSpecified ? source.RegionalSettings.Collation : 0,
                                FirstDayOfWeek = source.RegionalSettings.FirstDayOfWeekSpecified ?
                                    (DayOfWeek)Enum.Parse(typeof(DayOfWeek), source.RegionalSettings.FirstDayOfWeek.ToString()) : System.DayOfWeek.Sunday,
                                FirstWeekOfYear = source.RegionalSettings.FirstWeekOfYearSpecified ? source.RegionalSettings.FirstWeekOfYear : 0,
                                LocaleId = source.RegionalSettings.LocaleIdSpecified ? source.RegionalSettings.LocaleId : 1033,
                                ShowWeeks = source.RegionalSettings.ShowWeeksSpecified && source.RegionalSettings.ShowWeeks,
                                Time24 = source.RegionalSettings.Time24Specified && source.RegionalSettings.Time24,
                                TimeZone = !String.IsNullOrEmpty(source.RegionalSettings.TimeZone) ? Int32.Parse(source.RegionalSettings.TimeZone) : 0,
                                WorkDayEndHour = source.RegionalSettings.WorkDayEndHourSpecified ? source.RegionalSettings.WorkDayEndHour.FromSchemaToTemplateWorkHourV201605() : Model.WorkHour.PM0600,
                                WorkDays = source.RegionalSettings.WorkDaysSpecified ? source.RegionalSettings.WorkDays : 5,
                                WorkDayStartHour = source.RegionalSettings.WorkDayStartHourSpecified ? source.RegionalSettings.WorkDayStartHour.FromSchemaToTemplateWorkHourV201605() : Model.WorkHour.AM0900,
                            };
                        }
                        else
                        {
                            outgoingTemplate.RegionalSettings = null;
                        }
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(schema), schema, null);
            }
            return outgoingTemplate;
        }
        public IProvisioningTemplate ParseTemplate(XMLPnPSchemaVersion schema, IProvisioningTemplate outgoingTemplate, ProvisioningTemplate incomingTemplate)
        {
            switch (schema)
            {
                case XMLPnPSchemaVersion.V201605:
                    {
                        var result = outgoingTemplate as V201605.ProvisioningTemplate;
                        outgoingTemplate = Parse201605Object(result, incomingTemplate);
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(schema), schema, null);
            }
            return outgoingTemplate;
        }

        private static IProvisioningTemplate Parse201605Object(V201605.ProvisioningTemplate result, ProvisioningTemplate template)
        {
            if (template.RegionalSettings != null)
            {
                result.RegionalSettings = new V201605.RegionalSettings()
                {
                    AdjustHijriDays = template.RegionalSettings.AdjustHijriDays,
                    AdjustHijriDaysSpecified = true,
                    AlternateCalendarType = template.RegionalSettings.AlternateCalendarType.FromTemplateToSchemaCalendarTypeV201605(),
                    AlternateCalendarTypeSpecified = true,
                    CalendarType = template.RegionalSettings.CalendarType.FromTemplateToSchemaCalendarTypeV201605(),
                    CalendarTypeSpecified = true,
                    Collation = template.RegionalSettings.Collation,
                    CollationSpecified = true,
                    FirstDayOfWeek = (V201605.DayOfWeek)Enum.Parse(typeof(V201605.DayOfWeek), template.RegionalSettings.FirstDayOfWeek.ToString()),
                    FirstDayOfWeekSpecified = true,
                    FirstWeekOfYear = template.RegionalSettings.FirstWeekOfYear,
                    FirstWeekOfYearSpecified = true,
                    LocaleId = template.RegionalSettings.LocaleId,
                    LocaleIdSpecified = true,
                    ShowWeeks = template.RegionalSettings.ShowWeeks,
                    ShowWeeksSpecified = true,
                    Time24 = template.RegionalSettings.Time24,
                    Time24Specified = true,
                    TimeZone = template.RegionalSettings.TimeZone.ToString(),
                    WorkDayEndHour = template.RegionalSettings.WorkDayEndHour.FromTemplateToSchemaWorkHourV201605(),
                    WorkDayEndHourSpecified = true,
                    WorkDays = template.RegionalSettings.WorkDays,
                    WorkDaysSpecified = true,
                    WorkDayStartHour = template.RegionalSettings.WorkDayStartHour.FromTemplateToSchemaWorkHourV201605(),
                    WorkDayStartHourSpecified = true,
                };
            }
            else
            {
                result.RegionalSettings = null;
            }
            return result;
        }
    }
}
