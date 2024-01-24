using FatCat.Toolkit.Console;
using FatCat.Toolkit.Web;
using FatCat.Toolkit.WebServer;
using Microsoft.AspNetCore.Mvc;

namespace FatCat.TcpFake;

public class TestingEndpoint : Endpoint
{
    [HttpGet("test")]
    public WebResult DoTest()
    {
        ConsoleLog.WriteDarkCyan("Hit test endpoint");

        return Ok($"ACK | {DateTime.Now:h:mm:ss:fff tt}");
    }
}
