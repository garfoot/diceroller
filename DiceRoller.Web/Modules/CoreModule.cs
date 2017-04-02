using Autofac;
using DiceRoller.Web.Services.Dice;

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

            builder
                .RegisterType<DiceService>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}