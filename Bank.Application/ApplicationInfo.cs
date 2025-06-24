using System.Globalization;
using System.Reflection;

namespace Bank.Application;

public static class ApplicationInfo
{
    public static class Build
    {
        public static string   Version          { get; }
        public static string   Configuration    { get; }
        public static string   SourceRevisionId { get; }
        public static DateTime BuildDate        { get; }

        static Build()
        {
            var assembly = Assembly.GetEntryAssembly();

            var infoVersion = assembly?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                                      ?.InformationalVersion;

            Version          = infoVersion ?? "Unknown";
            SourceRevisionId = "Unknown";
            Configuration    = "Unknown";
            BuildDate        = DateTime.UtcNow;

            if (infoVersion is null)
                return;
            
            if (DateTime.TryParseExact(Version[10..25], "yyyyMMdd.HHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                BuildDate = date;

            Configuration    = Version.Split(' ')[0];
            SourceRevisionId = Version.Split('+')[1];
        }
    }
}
