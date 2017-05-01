using System;

namespace DiceRoller.Web.Services.Dice
{
    public class DiceInfo : IEquatable<DiceInfo>
    {
        public string Die { get; }
        public string Id { get; set; }


        public DiceInfo()
        {
        }

        public DiceInfo(string die)
        {
            Die = die;
            Id = Guid.NewGuid().ToString("N");
        }

        public bool Equals(DiceInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DiceInfo) obj);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }

        public static bool operator ==(DiceInfo left, DiceInfo right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DiceInfo left, DiceInfo right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return Die;
        }

    }
}