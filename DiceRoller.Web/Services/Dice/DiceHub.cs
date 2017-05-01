using System;
using System.Linq;
using System.Threading.Tasks;
using DiceRoller.Web.Services.Players;
using DiceRoller.Web.Services.Rooms;
using Microsoft.AspNetCore.SignalR;
using DiceRoller.Web.Services.Dice.Roller;

namespace DiceRoller.Web.Services.Dice
{
    public class DiceHub : Hub
    {
        private readonly IPlayerService _playerService;
        private readonly IRoomService _roomService;

        public DiceHub(
            IPlayerService playerService,
            IRoomService roomService
            )
        {
            _playerService = playerService;
            _roomService = roomService;
        }

        public async Task Connect(string roomId, string player)
        {
            PlayerInfo playerInfo = _playerService.GetPlayerByName(player);

            // If we don't have a player create one
            if (playerInfo == null)
            {
                playerInfo = new PlayerInfo
                {
                    Name = player,
                    Address = Context.Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    ConnectionId = Context.ConnectionId
                };

                _playerService.AddPlayer(playerInfo);
            }

            await _roomService.AddPlayerToRoom(playerInfo, roomId);
        }

        public override async Task OnDisconnected(bool stopCalled)
        {
            PlayerInfo player = _playerService.GetPlayerById(Context.ConnectionId);

            _playerService.RemovePlayer(player);
            await _roomService.RemovePlayer(player, true);
        }

        public void GetDiceList()
        {
            Clients.Caller.updateDiceList(new[]
            {
                "d4", "d6", "d8", "d10", "d12", "d20", "d100"
            });
        }

        public void AddDie(string die)
        {
            PlayerInfo player = _playerService.GetPlayerById(Context.ConnectionId);

            if (player != null)
            {
                player.Dice.Add(new DiceInfo(die));

                // Notify the players in the room of the dice for this player
                Clients.Group(player.CurrentRoom).selectedDice(player.Name, player.Dice);
            }
        }

        public void RemoveDie(DiceInfo die)
        {
            PlayerInfo player = _playerService.GetPlayerById(Context.ConnectionId);

            player.Dice.Remove(die);

            Clients.Group(player.CurrentRoom).selectedDice(player.Name, player.Dice);
        }

        public void RollDice()
        {
            PlayerInfo player = _playerService.GetPlayerById(Context.ConnectionId);

            var diceRoller = new DiceRollerService();

            var results = diceRoller.Roll( player.Dice.Select(i => $"1{i.ToString()}" ));   //   map { "1$_" }  @sassa

            Clients.Group(player.CurrentRoom).rolledDice(player.Name, results ); // https://xkcd.com/221/

        }

    }
}