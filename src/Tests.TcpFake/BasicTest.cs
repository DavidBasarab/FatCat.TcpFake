using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Tests.FatCat.TcpFake;

public class BasicTest
{
    [Fact]
    public void BeTrue()
    {
        true.Should().BeTrue();
    }
}
