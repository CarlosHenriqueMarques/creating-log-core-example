using Microsoft.Extensions.Hosting;
using Serilog;

namespace LoggerCore.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder UseLog(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog();
        return hostBuilder;
    }
}

