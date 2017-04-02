using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiceRoller.Web.Services.Dice
{
    public class DiceService : IDiceService
    {
        private readonly ConcurrentDictionary<string, RoomInfo> _rooms = new ConcurrentDictionary<string, RoomInfo>();

        /// <summary>
        ///     Get a given room.
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public ValueTask<RoomInfo> GetRoom(string roomId)
        {
            return new ValueTask<RoomInfo>(_rooms.GetOrAdd(roomId, id => new RoomInfo {Id = id}));
        }

        /// <summary>
        ///     Get a list of the rooms that have this connection Id.
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public ValueTask<IList<RoomInfo>> GetRoomsForConnectionId(string connectionId)
        {
            return new ValueTask<IList<RoomInfo>>(_rooms.Where(i => i.Value.HasConnection(connectionId))
                                                        .Select(i => i.Value)
                                                        .ToList());
        }
    }
}