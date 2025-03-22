using GeoTimeZone;
using System;
using TimeZoneConverter;

namespace TemplateService.Business.Helper
{
    public static class LocalDateHelper
    {
        public static DateTime GetLocalDateTime(double latitude, double longitude, DateTime utcDate)
        {
            string tz1 = TimeZoneLookup.GetTimeZone(latitude, longitude).Result;
            TimeZoneInfo tmeZoneInfo = TZConvert.GetTimeZoneInfo(tz1);
            var localTime = TimeZoneInfo.ConvertTimeFromUtc(utcDate, tmeZoneInfo);
            return localTime;
        }
        public static DateTime GetEgyptLocalDateTime(DateTime utcDate)
        {
            string tz1 = "Africa/Cairo";
            TimeZoneInfo tmeZoneInfo = TZConvert.GetTimeZoneInfo(tz1);
            var localTime = TimeZoneInfo.ConvertTimeFromUtc(utcDate, tmeZoneInfo);
            return localTime;
        }
        
    }
}
