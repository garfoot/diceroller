using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using DiceRoller.Web.Services.Dice;
using DiceRoller.Web.Services.Players;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Infrastructure;

namespace DiceRoller.Web.Services.Rooms
{
    public class RoomService : IRoomService
    {
        private readonly IConnectionManager _connectionManager;
        private readonly ConcurrentDictionary<string, RoomInfo> _rooms = new ConcurrentDictionary<string, RoomInfo>();


        public RoomService(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }
        /// <summary>
        ///     Get a given room.
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public RoomInfo GetRoom(string roomId)
        {
            return _rooms.GetOrAdd(roomId, id => new RoomInfo {Id = id});
        }

        /// <summary>
        ///     Add a player to the room and notify everyone else. Will move them from the old room if they're in one.
        /// </summary>
        /// <param name="player">The player to add / move.</param>
        /// <param name="roomId">The new room.</param>
        /// <returns></returns>
        public async Task AddPlayerToRoom(PlayerInfo player, string roomId)
        {
            // Check if they're already in the room
            if (player.CurrentRoom == roomId)
            {
                return;
            }

            // Grab the dice hub context for sending notifications
            IHubContext hub = _connectionManager.GetHubContext<DiceHub>();

            // Check if they're in a room already and remove them
            if (player.CurrentRoom != null)
            {
                await RemovePlayer(player);
            }

            // Add the player into the room and notify everyone else
            var roomToJoin = GetRoom(roomId);
            roomToJoin.AddPlayer(player);
            await hub.Groups.Add(player.ConnectionId, roomId);
            hub.Clients.Group(roomId, player.ConnectionId, player.ConnectionId).playerJoined(player.Name);

            // Send the new player the current room player list
            hub.Clients.Client(player.ConnectionId).playerList(roomToJoin.GetPlayers());
        }

        /// <summary>
        ///     Remove a player from whatever room they're in.
        /// </summary>
        /// <param name="player">The player to remove.</param>
        /// <returns></returns>
        public Task RemovePlayer(PlayerInfo player, bool disconnected = false)
        {
            return RemovePlayerFromRoom(player, player.CurrentRoom, disconnected);
        }

        /// <summary>
        ///     Remove a player from whatever room they're in.
        /// </summary>
        /// <param name="player">The player to remove.</param>
        /// <param name="roomId">The room to remove the player from.</param>
        /// <param name="disconnected"></param>
        /// <returns></returns>
        public async Task RemovePlayerFromRoom(PlayerInfo player, string roomId, bool disconnected = false)
        {
            // Do nothing if the player is not in the room
            if (player.CurrentRoom != roomId)
            {
                return;
            }

            // Grab the dice hub for notifications
            IHubContext hub = _connectionManager.GetHubContext<DiceHub>();

            RoomInfo currentRoom = GetRoom(roomId);

            // Remove the player from the room and notify the room that the player left
            currentRoom.RemovePlayer(player);

            if (!disconnected)
            {
                await hub.Groups.Remove(player.ConnectionId, currentRoom.Id);
            }


            hub.Clients.Group(currentRoom.Id).playerLeft(player.Name);
        }
    }
}