using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DiceRoller.Web.Services.Dice
{
    public class RoomInfo
    {
        private readonly ConcurrentDictionary<string, PlayerInfo> _players = new ConcurrentDictionary<string, PlayerInfo>();
        public string Id { get; set; }

        public ValueTask<IReadOnlyDictionary<string, PlayerInfo>> GetPlayers()
        {
            return new ValueTask<IReadOnlyDictionary<string, PlayerInfo>>(new ReadOnlyDictionary<string, PlayerInfo>(_players));
        }

        /// <summary>
        ///     Add a new player to the room.
        /// </summary>
        /// <param name="player">The player to add.</param>
        /// <returns>True if the player was added, false if the player was already in the room.</returns>
        /// <exception cref="InvalidOperationException">Throws InvalidOperationException if a different player with that name already exists in the room.</exception>
        public ValueTask<bool> AddPlayer(PlayerInfo player)
        {
            bool isAdded = _players.TryAdd(player.Name, player);

            // If we couldn't add, try updating with the same details that are already there
            if (!isAdded)
            {
                if (!_players.TryUpdate(player.Name, player, player))
                {
                    throw new InvalidOperationException("A different player with name name already exists.");
                }
            }

            return new ValueTask<bool>(isAdded);
        }

        /// <summary>
        ///     Remove the specified player(s) from the room.
        /// </summary>
        /// <param name="predicate">The predicate to identify the players.</param>
        /// <returns>A list of the removed players.</returns>
        public ValueTask<IList<PlayerInfo>> RemovePlayers(Func<PlayerInfo, bool> predicate)
        {
            var players = _players
                .Select(i => i.Value)
                .Where(predicate)
                .ToList();

            foreach (PlayerInfo player in players)
            {
                _players.TryRemove(player.Name, out var _);
            }

            return new ValueTask<IList<PlayerInfo>>(players);
        }

        /// <summary>
        ///     Check to see if this room has a connection from the specified connection Id.
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public bool HasConnection(string connectionId)
        {
            return _players.Any(i => i.Value.ConnectionId == connectionId);
        }
    }
}