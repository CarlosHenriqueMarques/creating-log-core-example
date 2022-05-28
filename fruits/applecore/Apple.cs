using LoggerCore.Logging;

namespace applecore;

public class Apple
{

    public Apple() { ConfigLogger.Configure(); }

    public void printApple(string msg)
    {
        Console.WriteLine($"This message is come from Apple class - {msg}");
        ConfigLogger.Info(msg);
    }
}
