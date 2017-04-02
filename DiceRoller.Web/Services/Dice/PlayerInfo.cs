using System;
using System.Collections.Generic;

namespace DiceRoller.Web.Services.Dice
{
    public class PlayerInfo : IEquatable<PlayerInfo>
    {
        /// <summary>
        ///     The name of the connected user
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The address from which they have connected
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///     Their connection ID in SignalR
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        ///     A list of the dice for this player
        /// </summary>
        public IList<string> Dice { get; set; } = new List<string>();

        public bool Equals(PlayerInfo other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return string.Equals(Name, other.Name) && string.Equals(Address, other.Address);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((PlayerInfo) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (Address != null ? Address.GetHashCode() : 0);
            }
        }

        public static bool operator ==(PlayerInfo left, PlayerInfo right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PlayerInfo left, PlayerInfo right)
        {
            return !Equals(left, right);
        }
    }
}