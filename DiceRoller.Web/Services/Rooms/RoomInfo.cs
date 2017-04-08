using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DiceRoller.Web.Services.Players;

namespace DiceRoller.Web.Services.Rooms
{
    public class RoomInfo
    {
        private readonly IList<string> _players = new List<string>();

        public string Id { get; set; }

        /// <summary>
        ///     Get a list of the players currently in the room.
        /// </summary>
        /// <returns>A read only snapshot of the players.</returns>
        public IReadOnlyList<string> GetPlayers()
        {
            // Snapshot the player list
            lock (_players)
            {
                return new ReadOnlyCollection<string>(new List<string>(_players));
            }
        }

        /// <summary>
        ///     Add a new player to the room.
        /// </summary>
        /// <param name="player">The player to add.</param>
        public void AddPlayer(PlayerInfo player)
        {
            lock (_players)
            {
                _players.Add(player.Name);
                player.CurrentRoom = Id;
            }
        }

        /// <summary>
        ///     Remove the specified player(s) from the room.
        /// </summary>
        public void RemovePlayer(PlayerInfo playerName)
        {
            lock (_players)
            {
                _players.Remove(playerName.Name);
            }
        }
    }
}