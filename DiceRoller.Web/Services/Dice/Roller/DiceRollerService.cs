using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DiceRoller.Web.Services.Dice.Roller
{
    public class DiceRollerService
    {
        public DiceRollResult Roll(params string[] diceList)
        {
            return Roll((IEnumerable<string>) diceList);
        }

        public DiceRollResult Roll(IEnumerable<string> diceList)
        {
            var random = new Random((int) DateTime.UtcNow.Ticks);

            var diceSpec = new Regex("(?\'times\'\\d+)d(?\'num\'\\d+)((?\'modSign\'[+-]{1})(?\'modAmount\'\\d+))?");

            var nums = new List<DiceRollInfo>();

            foreach (string diceInfo in diceList)
            {
                Match match = diceSpec.Match(diceInfo);
                if (match.Success)
                {
                    int num = int.Parse(match.Groups["num"].Value);
                    int mul = int.Parse(match.Groups["times"].Value);

                    var current = new List<int>();
                    for (var i = 0; i < mul; i++)
                    {
                        current.Add(random.Next(1, num + 1));
                    }

                    int? mod = null;
                    if (match.Groups["modSign"].Success
                        && match.Groups["modAmount"].Success)
                    {
                        string sign = match.Groups["modSign"].Value;
                        int modAmount = int.Parse(match.Groups["modAmount"].Value);
                        mod = modAmount * (sign == "+" ? 1 : -1);
                    }

                    var info = new DiceRollInfo
                    {
                        Nums = current,
                        Spec = diceInfo,
                        Total = current.Aggregate(0, (tot, cur) => tot + cur) + (mod ?? 0),
                        Mod = mod
                    };


                    nums.Add(info);
                }
            }

            return new DiceRollResult
            {
                Total = nums.Aggregate(0, (c, i) => c + i.Total),
                Rolls = nums
            };
        }
    }
}