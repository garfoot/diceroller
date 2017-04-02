using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiceRoller.Web.Services.Dice
{
    public interface IDiceService
    {
        ValueTask<RoomInfo> GetRoom(string roomId);
        ValueTask<IList<RoomInfo>> GetRoomsForConnectionId(string connectionId);
    }
}