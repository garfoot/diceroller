using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using DiceRoller.Web.Services.Dice;

namespace DiceRoller.Web.Services.Players
{
    public class PlayerService : IPlayerService
    {
        private readonly IDictionary<string, PlayerInfo> _playersByName = new Dictionary<string, PlayerInfo>();
        private readonly IDictionary<string, PlayerInfo> _playersById = new Dictionary<string, PlayerInfo>();
        private readonly object _playerLock = new object();

        public void AddPlayer(PlayerInfo player)
        {
            lock (_playerLock)
            {
                _playersById[player.ConnectionId] = player;
                _playersByName[player.Name] = player;
            }
        }

        public void RemovePlayer(PlayerInfo player)
        {
            lock (_playerLock)
            {
                _playersById.Remove(player.ConnectionId);
                _playersByName.Remove(player.Name);
            }
        }

        public PlayerInfo GetPlayerByName(string player)
        {
            lock (_playerLock)
            {
                _playersByName.TryGetValue(player, out var playerInfo);

                return playerInfo;
            }
        }

        public PlayerInfo GetPlayerById(string id)
        {
            lock (_playerLock)
            {
                _playersById.TryGetValue(id, out var playerInfo);

                return playerInfo;
            }
        }
    }
}