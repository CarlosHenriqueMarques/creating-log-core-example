using Microsoft.Extensions.Configuration;

namespace LoggerCore.Logging;

public class LogConfigOptions
{
    private LogConfigOptions(bool isConsole, bool isRollingFile, bool isApplicationInsights)
    {
        IsConsole = isConsole;
        IsRollingFile = isRollingFile;
        IsApplicationInsights = isApplicationInsights;
    }

    public bool IsRollingFile { get; private set; }
    public bool IsApplicationInsights { get; private set; }
    public bool IsConsole { get; private set; }
    public string InstrumentationKey { get; private set; }
    public string RollingFilePath { get; private set; }

    public static LogConfigOptions Create(IConfiguration configuration, string configSection)
    {
        LogConfigOptions options;
        var quicLogSection = configuration.GetSection(configSection);
        if (quicLogSection == null)
            throw new InvalidOperationException("Section QuicLog is missing.");
        if (bool.TryParse(quicLogSection["IsConsole"], out var isConsole) &&
            bool.TryParse(quicLogSection["IsRollingFile"], out var isRollingFile) &&
            bool.TryParse(quicLogSection["IsApplicationInsights"], out var isApplicationInsights))
        {
            options = new LogConfigOptions(isConsole, isRollingFile, isApplicationInsights);
        }
        else
            throw new InvalidOperationException("Unable to parse boolean values in QuicLog section");

        if (isApplicationInsights)
        {
            var instrumentationKey = configuration.GetValue<string>("ApplicationInsights:InstrumentationKey");
            if (string.IsNullOrWhiteSpace(instrumentationKey))
            {
                throw new InvalidOperationException(
                    "Instrumentation key cannot be null. It should be set under ApplicationInsights:InstrumentationKey");
            }

            options.InstrumentationKey = instrumentationKey;
        }

        if (isRollingFile)
        {
            options.RollingFilePath = quicLogSection["RollingFilePath"];
        }
        return options;
    }
}
