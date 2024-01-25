using FatCat.Toolkit;
using FatCat.Toolkit.Caching;
using FatCat.Toolkit.Communication;

namespace FatCat.TcpFake;

public class ServerCacheItem(ushort port, IFatTcpServer tcpServer) : EqualObject, ICacheItem
{
    public string CacheId
    {
        get => Port.ToString();
    }

    public ushort Port { get; set; } = port;

    public IFatTcpServer TcpServer { get; set; } = tcpServer;
}
