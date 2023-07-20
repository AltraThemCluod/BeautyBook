using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyBook.Common
{
    public static class UnitsConverter
    {
        // unit names from the UnitOfMeasure table
        private const string Altitude = @"Altitude";
        private const string Celsius = @"Celsius";
        private const string Fahrenheit = @"Fahrenheit";
        private const string Kelvin = @"Kelvin";
        private const string Rankine = "@Rankine";
        private const string RelHumidity = @"% RH";
        private const string Siemens = @"Siemens/cm";
        private const string Microsiemens = @"MicroSiemens/cm";
        private const string Millibars = @"Millibars";
        private const string InchesHg = @"Inches Mercury";
        private const string MillimetersHg = @"Millimeters Mercury";
        private const string CO2PPM = @"CARBON DIOXIDE";
        private const string CO2PCT = @"CARBON DIOXIDE - %CO₂";
        private const string Feet = @"Feet";
        private const string Meters = @"Meters";

        public static double CelsiusToFahrenheit(double input, int resolution)
        {
            return Math.Round(1.8 * input + 32, resolution, MidpointRounding.AwayFromZero);
        }

        public static double FahrenheitToCelsius(double input, int resolution)
        {
            return Math.Round(5 * (input - 32) / 9, resolution, MidpointRounding.AwayFromZero);
        }

        public static double MillibarsToInchesHg(double input, int resolution)
        {
            // 1 mb = .0295300586 inHg (32 degrees F)
            return Math.Round((input * .0295300586), resolution, MidpointRounding.AwayFromZero);
        }

        public static double InchesHgToMillibars(double input, int resolution)
        {
            // 1 mb = .0295300586 inHg (32 degrees F)
            return Math.Round((input / .0295300586), resolution, MidpointRounding.AwayFromZero);
        }

        public static double MillibarsToMmHg(double input, int resolution)
        {
            //1 mb = .7500637554 mmHg(0 degrees C)
            return Math.Round((input * .7500637554), resolution, MidpointRounding.AwayFromZero);
        }

        public static double MmHgToMillibars(double input, int resolution)
        {
            //1 mb = .7500637554 mmHg(0 degrees C)
            return Math.Round((input / .7500637554), resolution, MidpointRounding.AwayFromZero);
        }

        public static double CelsiusToKelvin(double input, int resolution)
        {
            return Math.Round(input - 273.15, resolution, MidpointRounding.AwayFromZero);
        }


        public static double KelvinToCelsius(double input, int resolution)
        {
            return Math.Round(input + 273.15, resolution, MidpointRounding.AwayFromZero);
        }

        public static double VoltToPercent(double current, double max, double min)
        {
            return Math.Max(Math.Min((int)((1 - (max - current) /
                  (max - min)) * 100), 100), 1);
        }

        public static double MetersToFeet(double input, int resolution)
        {
            //1 mt = 3.28084 ft
            return Math.Round((input * 3.28084), resolution, MidpointRounding.AwayFromZero);
        }

        private static double FeetToMeters(double input, int resolution)
        {
            return Math.Round((input / 3.28084), resolution, MidpointRounding.AwayFromZero);
        }

        private static double CelsiusToRankine(double input, int resolution)
        {
            return Math.Round(1.8 * (input + 273.15), resolution, MidpointRounding.AwayFromZero);
        }

        private static double RankineToCelsius(double input, int resolution)
        {
            return Math.Round((input / 1.8) - 273.15, resolution, MidpointRounding.AwayFromZero);
        }


        /// <summary>
        /// Use this to convert units from the database units to user preference units
        /// </summary>
        /// <param name="datatype"></param>
        /// <param name="tounits"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static double Convert(string datatype, string tounits, double data, int resolution)
        {
            switch (datatype.ToUpper())
            {
                case "TEMPERATURE":
                    if (tounits.ToUpper() == Fahrenheit.ToUpper())
                        return CelsiusToFahrenheit(data, resolution);
                    if (tounits.ToUpper() == Kelvin.ToUpper())
                        return CelsiusToKelvin(data, resolution);
                    if (tounits.ToUpper() == Rankine.ToUpper())
                        return CelsiusToRankine(data, resolution);
                    return data;

                case "PRESSURE":
                    if (tounits.ToUpper() == InchesHg.ToUpper())
                        return MillibarsToInchesHg(data, resolution);
                    if (tounits.ToUpper() == MillimetersHg.ToUpper())
                        return MillibarsToMmHg(data, resolution);
                    return data;

                case "ALTITUDE":
                    if (tounits.ToUpper() == Feet.ToUpper())
                        return MetersToFeet(data, resolution);
                    return data;

                case CO2PCT:
                case "HUMIDITY":
                case "CONDUCTIVITY":
                    break;
            }
            return data;
        }

        /// <summary>
        /// Use this for incoming data conversions from user preferences to database units
        /// </summary>
        /// <param name="datatype"></param>
        /// <param name="tounits"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static double ConvertFrom(string datatype, string tounits, double data)
        {
            switch (datatype.ToUpper())
            {
                case "TEMPERATURE":
                    if (tounits.ToUpper() == Fahrenheit.ToUpper())
                        return FahrenheitToCelsius(data, 2);
                    if (tounits.ToUpper() == Kelvin.ToUpper())
                        return KelvinToCelsius(data, 2);
                    if (tounits.ToUpper() == Rankine.ToUpper())
                        return RankineToCelsius(data, 2);
                    return data;

                case "PRESSURE":
                    if (tounits.ToUpper() == InchesHg.ToUpper())
                        return InchesHgToMillibars(data, 0);
                    if (tounits.ToUpper() == MillimetersHg.ToUpper())
                        return MmHgToMillibars(data, 0);
                    return data;

                case "ALTITUDE":
                    if (tounits.ToUpper() == Meters.ToUpper())
                        return FeetToMeters(data, 0);
                    return data;

                case CO2PCT:
                case "HUMIDITY":
                case "CONDUCTIVITY":
                    break;
            }
            return data;
        }

        public static DateTime ConvertToLocalTime(DateTime timestamp, int? unadjustedTimeZone)
        {
            var specifiedTimestamp = DateTime.SpecifyKind(timestamp, DateTimeKind.Unspecified);
            var timeZone = unadjustedTimeZone.HasValue
                ? TimeZoneHelper.Timezones[AdjustTimezone(unadjustedTimeZone.Value)]
                : TimeZoneInfo.Utc;

            var localtime = TimeZoneInfo.ConvertTimeFromUtc(specifiedTimestamp, timeZone);
            return localtime;
        }

        public static DateTime ConvertToUtcTime(DateTime timestamp, int? unadjustedTimeZone)
        {
           
            var specifiedTimestamp = DateTime.SpecifyKind(timestamp, DateTimeKind.Unspecified);
            var timeZone = unadjustedTimeZone.HasValue
                ? TimeZoneHelper.Timezones[AdjustTimezone(unadjustedTimeZone.Value)]
                : TimeZoneInfo.Utc;
            var localtime = TimeZoneInfo.ConvertTimeToUtc(specifiedTimestamp, timeZone);
            return localtime;
        }

        public static string TimeZoneOffset(int? unadjustedTimeZone)
        {
            var timeZone = TimeZoneHelper.Timezones[AdjustTimezone(unadjustedTimeZone.Value)];

            return timeZone.BaseUtcOffset.ToString();
        }

        private static int AdjustTimezone(int timezone)
        {
            if (timezone == 2)
                return timezone + 1;
            else if (timezone == 3)
                return timezone + 2;
            else if (timezone == 4)
                return timezone + 3;
            else if (timezone >= 5 && timezone <= 12)
                return timezone + 4;
            else if (timezone >= 13 && timezone <= 16)
                return timezone + 5;
            else if (timezone >= 18 && timezone < 20)
                return timezone + 6;
            else if ((timezone >= 20 && timezone < 22))
                return timezone + 7;
            else if (timezone == 17 || timezone == 22  || timezone == 25 || timezone == 34 || timezone == 35 || timezone == 49) 
                return timezone + 9;
            else if (timezone == 23 || timezone == 26 || timezone == 27 ||(timezone >= 30 && timezone <= 33 ) ||(timezone >= 36 && timezone <= 47 ) )
                return timezone + 10;
            else if (timezone == 53 || timezone == 28 || timezone == 48 || (timezone >= 50 && timezone <= 62 ))
                return timezone + 11;
            else if ((timezone >= 63 && timezone <= 79 ))
                return timezone + 12;
            else if (timezone == 80)
                return timezone + 14;
            else if ((timezone >= 81 && timezone <= 86 ))
                return timezone + 15;
            else if ((timezone >= 87 && timezone <=  95))
                return timezone + 18;
            else if ((timezone ==  98))
                return timezone + 19;
            else if (timezone == 97)
                return timezone + 21;
            else if (timezone == 96 || (timezone >= 99 && timezone <=  107))
                return timezone + 22;
            else
                return timezone;
        }
    }

    public static class TimeZoneHelper
    {
        public static List<TimeZoneInfo> Timezones { get; set; }
    }
}
