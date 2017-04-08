using System;
using System.Collections.Generic;
using DiceRoller.Web.Services.Dice;

namespace DiceRoller.Web.Services.Players
{
    public interface IPlayerService
    {
        PlayerInfo GetPlayerByName(string player);
        PlayerInfo GetPlayerById(string id);
        void AddPlayer(PlayerInfo player);
        void RemovePlayer(PlayerInfo player);
    }
}