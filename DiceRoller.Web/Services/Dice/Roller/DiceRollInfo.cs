using System;
using System.Collections.Generic;

namespace DiceRoller.Web.Services.Dice.Roller
{
    public class DiceRollInfo
    {
        public int? Mod { get; set; }
        public IList<int> Nums { get; set; }
        public int Total { get; set; }
        public string Spec { get; set; }
    }
}