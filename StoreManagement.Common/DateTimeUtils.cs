namespace StoreManagement.Common
{
    public static class DateTimeUtils
    {
        private static readonly Lazy<TimeZoneInfo> _brazilTimeZone =
            new(GetBrazilTimeZone);

        private static TimeZoneInfo BrazilTimeZone => _brazilTimeZone.Value;

        private static TimeZoneInfo GetBrazilTimeZone()
        {
            const string windowsId = "E. South America Standard Time";
            const string ianaId = "America/Sao_Paulo";

            try
            {
                return TimeZoneInfo.FindSystemTimeZoneById(windowsId);
            }
            catch (TimeZoneNotFoundException)
            {
                try
                {
                    return TimeZoneInfo.FindSystemTimeZoneById(ianaId);
                }
                catch (TimeZoneNotFoundException)
                {
                    return TimeZoneInfo.CreateCustomTimeZone(
                        "BR_UTC_MINUS_3",
                        TimeSpan.FromHours(-3),
                        "Brazil (fallback)",
                        "Brazil (fallback)");
                }
            }
        }

        public static DateTimeOffset NowInBrazil() =>
            TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, BrazilTimeZone);

        public static DateTimeOffset ToBrazilTime(DateTimeOffset source) =>
            TimeZoneInfo.ConvertTime(source.ToUniversalTime(), BrazilTimeZone);

        public static DateTimeOffset ToUtc(DateTimeOffset source) =>
            source.ToUniversalTime();
    }
}
