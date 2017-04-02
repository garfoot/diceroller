﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis;

namespace DiceRoller.Web.Services.Dice
{
    public class DiceHub : Hub
    {
        private readonly IDiceService _diceService;

        public DiceHub(IDiceService diceService)
        {
            _diceService = diceService;
        }

        public async Task Connect(string roomId, string player)
        {
            RoomInfo room = await _diceService.GetRoom(roomId);

            try
            {
                // Get the players from the room
                var players = await room.GetPlayers();

                // Try and add the new player, if this returns true then we have added them as a new player, false and
                // they were already in there
                if (await room.AddPlayer(new PlayerInfo {Name = player, Address = GetCallerAddress(), ConnectionId = Context.ConnectionId}))
                {
                    // Notify all of the other members of the group that this player joined
                    Clients.Group(roomId, Context.ConnectionId).playerJoined(player);
                }

                // Add the player to a group with this roomId name, may already be in there but also might have a new connection ID
                await Groups.Add(Context.ConnectionId, roomId);

                // Send them the current room player list
                Clients.Caller.playerList(players.Select(i => i.Value.Name));
            }
            catch (InvalidOperationException)
            {
                Clients.Caller.error("A player with that name already exists in this room.");
            }
        }

        public override async Task OnDisconnected(bool stopCalled)
        {
            var rooms =  await _diceService.GetRoomsForConnectionId(Context.ConnectionId);

            foreach (RoomInfo room in rooms)
            {
                var players = await room.RemovePlayers(i => i.ConnectionId == Context.ConnectionId);

                foreach (PlayerInfo player in players)
                {
                    Clients.Group(room.Id).playerLeft(player.Name);
                }
            }
        }

        private string GetCallerAddress()
        {
            return Context.Request.HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }
}