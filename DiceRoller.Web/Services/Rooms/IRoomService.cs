using System;
using System.Threading.Tasks;
using DiceRoller.Web.Services.Players;

namespace DiceRoller.Web.Services.Rooms
{
    public interface IRoomService
    {
        /// <summary>
        ///     Get the room info for the given room.
        /// </summary>
        /// <param name="roomId">The room ID to get.</param>
        /// <returns></returns>
        RoomInfo GetRoom(string roomId);

        /// <summary>
        ///     Add a player to the room and notify everyone else. Will move them from the old room if they're in one.
        /// </summary>
        /// <param name="player">The player to add / move.</param>
        /// <param name="roomId">The new room.</param>
        /// <returns></returns>
        Task AddPlayerToRoom(PlayerInfo player, string roomId);

        /// <summary>
        ///     Remove a player from whatever room they're in.
        /// </summary>
        /// <param name="player">The player to remove.</param>
        /// <param name="isDisconnected">True if the player has disconnected.</param>
        /// <returns></returns>
        Task RemovePlayer(PlayerInfo player, bool isDisconnected = false);

        /// <summary>
        ///     Remove a player from whatever room they're in.
        /// </summary>
        /// <param name="player">The player to remove.</param>
        /// <param name="roomId">The room to remove the player from.</param>
        /// <param name="isDisconnected">True if the player has disconnected.</param>
        /// <returns></returns>
        Task RemovePlayerFromRoom(PlayerInfo player, string roomId, bool isDisconnected = false);
    }
}