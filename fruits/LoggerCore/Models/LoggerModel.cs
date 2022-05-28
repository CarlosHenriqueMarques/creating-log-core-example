namespace LoggerCore.Models;


public class LoggerModel
{
    private LoggerModel()
    {
        CreatedOn = DateTimeOffset.UtcNow;
    }
    // Metadata
    public DateTimeOffset CreatedOn { get; }
    public string Message { get; private set; }
    // WHERE
    public string AppName { get; private set; }
    public string AppLayer { get; private set; }
    public string Location { get; private set; }
    public string Hostname { get; private set; }
    // WHO
    public string UserName { get; private set; }
    // EVERYTHING ELSE
    public long? ElapsedMilliseconds { get; private set; }
    public Exception Exception { get; private set; }
    public string CorrelationId { get; private set; }
    public Dictionary<string, object> AdditionalInfo { get; private set; }

    public static LoggerModel CreateLog(string message, string appName, string appLayer,
        string userName)
    {
        ValidateParameters(message, appName, appLayer, userName);
        return new LoggerModel()
        {
            Message = message,
            AppName = appName,
            AppLayer = appLayer,
            UserName = userName
        };

    }

    public static LoggerModel CreateExceptionLog(string message, string appName, string appLayer,
        string userName, Exception exception)
    {
        ValidateParameters(message, appName, appLayer, userName);
        if (exception == null)
            throw new ArgumentNullException(nameof(exception));

        return new LoggerModel()
        {
            Message = message,
            AppName = appName,
            AppLayer = appLayer,
            UserName = userName,
            Exception = exception
        };

    }

    private static void ValidateParameters(string message, string appName, string appLayer, string userName)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentNullException(nameof(message));
        if (string.IsNullOrWhiteSpace(appName))
            throw new ArgumentNullException(nameof(appName));
        if (string.IsNullOrWhiteSpace(appLayer))
            throw new ArgumentNullException(nameof(appLayer));
        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentNullException(nameof(userName));
    }
}