using System.Text;
using FakeItEasy;
using FatCat.Fakes;
using FatCat.TcpFake;
using FatCat.Toolkit.Caching;
using FatCat.Toolkit.Communication;
using FatCat.Toolkit.WebServer.Testing;
using Xunit;

namespace Tests.FatCat.TcpFake;

public class StartTcpServerEndpointTests
{
    private readonly IFatCatCache<ServerCacheItem> cache = A.Fake<IFatCatCache<ServerCacheItem>>();
    private readonly StartTcpServerEndpoint endpoint;
    private readonly StartServerRequest serverRequest = Faker.Create<StartServerRequest>();
    private readonly ITcpFactory tcpFactory = A.Fake<ITcpFactory>();
    private readonly IFatTcpServer tcpServer = A.Fake<IFatTcpServer>();

    public StartTcpServerEndpointTests()
    {
        A.CallTo(() => cache.InCache(A<string>._)).Returns(false);

        A.CallTo(() => tcpFactory.CreateOpenTcpServer()).Returns(tcpServer);

        endpoint = new StartTcpServerEndpoint(tcpFactory, cache);
    }

    [Fact]
    public void BeAPost()
    {
        endpoint.Should().BePost(nameof(StartTcpServerEndpoint.StartServer), "server");
    }

    [Fact]
    public void CacheTcpServer()
    {
        endpoint.StartServer(serverRequest);

        var expectedCacheItem = new ServerCacheItem(serverRequest.Port, tcpServer);

        A.CallTo(() => cache.Add(expectedCacheItem, default)).MustHaveHappened();
    }

    [Fact]
    public void CreateOpenTcpServerFromFactory()
    {
        endpoint.StartServer(serverRequest);

        A.CallTo(() => tcpFactory.CreateOpenTcpServer()).MustHaveHappened();
    }

    [Fact]
    public void IfServerPortAlreadyInCacheDoNotCreateOrStart()
    {
        A.CallTo(() => cache.InCache(A<string>._)).Returns(true);

        endpoint.StartServer(serverRequest);

        A.CallTo(() => tcpFactory.CreateOpenTcpServer()).MustNotHaveHappened();
        A.CallTo(() => cache.InCache(serverRequest.Port.ToString())).MustHaveHappened();
    }

    [Fact]
    public void StartTcpServer()
    {
        endpoint.StartServer(serverRequest);

        A.CallTo(
                () =>
                    tcpServer.Start(
                        serverRequest.Port,
                        Encoding.Default,
                        serverRequest.ReceiveBufferSize
                    )
            )
            .MustHaveHappened();
    }
}
