using FatCat.Toolkit;

namespace FatCat.TcpFake;

public class StartServerRequest : EqualObject
{
    public ushort Port { get; set; }

    public int ReceiveBufferSize { get; set; } = 1024;
}
