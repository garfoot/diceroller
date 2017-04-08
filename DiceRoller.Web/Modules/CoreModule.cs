using Autofac;
using DiceRoller.Web.Services.Dice;
using DiceRoller.Web.Services.Players;
using DiceRoller.Web.Services.Rooms;

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
                .RegisterType<RoomService>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .RegisterType<PlayerService>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}