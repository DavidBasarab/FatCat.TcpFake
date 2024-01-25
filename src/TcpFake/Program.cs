using System.Reflection;
using FatCat.Toolkit.Communication;
using FatCat.Toolkit.Console;
using FatCat.Toolkit.Web.Api;
using FatCat.Toolkit.WebServer;

namespace FatCat.TcpFake;

public static class Program
{
    private static ToolkitWebApplicationSettings applicationSettings;

    public static void Main(params string[] args)
    {
        applicationSettings = new ToolkitWebApplicationSettings
        {
            Options = WebApplicationOptions.CommonOptions,
            ContainerAssemblies =
            [
                Assembly.GetExecutingAssembly(),
                typeof(ITcpFactory).Assembly,
                typeof(ToolkitWebApplication).Assembly
            ],
            OnWebApplicationStarted = Started,
            Args = args,
            BasePath = "/api"
        };

        ToolkitWebApplication.Run(applicationSettings);
    }

    private static void Started()
    {
        try
        {
            ConsoleLog.WriteDarkCyan("Application has started");
        }
        catch (Exception e)
        {
            ConsoleLog.WriteException(e);
        }
    }
}
