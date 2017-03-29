using Autofac;

namespace DiceRoller.Web.Modules
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(ThisAssembly)
                .AsSelf()
                .AsImplementedInterfaces();
        }
    }
}