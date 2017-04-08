using System;
using System.Collections.Generic;

namespace DiceRoller.Web.Services.Dice.Roller
{
    public class DiceRollResult
    {
        public int Total { get; set; }
        public IList<DiceRollInfo> Rolls { get; set; } = new List<DiceRollInfo>();
    }
}