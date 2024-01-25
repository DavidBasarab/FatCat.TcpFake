using System.Text;
using FatCat.Toolkit.Caching;
using FatCat.Toolkit.Communication;
using FatCat.Toolkit.WebServer;
using Microsoft.AspNetCore.Mvc;

namespace FatCat.TcpFake;

public class StartTcpServerEndpoint(
    ITcpFactory tcpFactory,
    IFatCatCache<ServerCacheItem> serverCache
) : Endpoint
{
    [HttpPost("server")]
    public WebResult StartServer([FromBody] StartServerRequest request)
    {
        if (serverCache.InCache(request.Port.ToString()))
        {
            return Ok();
        }

        var server = tcpFactory.CreateOpenTcpServer();

        server.Start(request.Port, Encoding.Default, request.ReceiveBufferSize);

        var cacheItem = new ServerCacheItem(request.Port, server);

        serverCache.Add(cacheItem);

        return NotImplemented();
    }
}
