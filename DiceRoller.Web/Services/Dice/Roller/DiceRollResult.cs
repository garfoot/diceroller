using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DiceRoller.Web.Services.Dice.Roller
{
    public class DiceRollResult
    {
        [JsonProperty("total")]
        public int Total { get; set; }
        [JsonProperty("rolls")]
        public IList<DiceRollInfo> Rolls { get; set; } = new List<DiceRollInfo>();
    }
}