using Autofac;
using FatCat.Toolkit.Caching;

namespace FatCat.TcpFake;

public class FakeModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(FatCatCache<>)).As(typeof(IFatCatCache<>)).SingleInstance();
    }
}
