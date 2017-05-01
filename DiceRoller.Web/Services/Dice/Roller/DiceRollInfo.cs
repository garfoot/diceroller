using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DiceRoller.Web.Services.Dice.Roller
{
    public class DiceRollInfo
    {
        [JsonProperty("mod",NullValueHandling=NullValueHandling.Ignore)]
        public int? Mod { get; set; }
        [JsonProperty("nums")]
        public IList<int> Nums { get; set; }
        [JsonProperty("total")]
        public int Total { get; set; }
        [JsonProperty("spec")]
        public string Spec { get; set; }
    }
}