﻿using System;
using SWLOR.Game.Server.Legacy.ChatCommand.Contracts;
using SWLOR.Game.Server.Legacy.Enumeration;
using SWLOR.Game.Server.Legacy.GameObject;
using SWLOR.Game.Server.Legacy.Service;
using SWLOR.Game.Server.Service;

namespace SWLOR.Game.Server.Legacy.ChatCommand
{
    [CommandDetails("Rolls dice. Use /dice help for more information", CommandPermissionType.Player | CommandPermissionType.DM | CommandPermissionType.Admin)]
    public class Dice : IChatCommand
    {
        
        
        

        private string GenericError;

        public Dice(
            
            
            )
        {
            
            
            

            GenericError = ColorToken.Red("Please enter /dice help for more information on how to use this command.");
        }

        public void DoAction(NWPlayer user, NWObject target, NWLocation targetLocation, params string[] args)
        {
            var command = args[0].ToLower();

            switch (command)
            {
                case "help":
                    DoHelp(user);
                    break;
                case "d2":
                case "d4":
                case "d6":
                case "d8":
                case "d10":
                case "d20":
                case "d100":
                    var sides = Convert.ToInt32(command.Substring(1));
                    var count = 1;
                    if (args.Length > 1)
                    {
                        if (!int.TryParse(args[1], out count))
                        {
                            count = 1;
                        }
                    }

                    DoDiceRoll(user, sides, count);
                    break;

                default:
                    user.SendMessage(GenericError);
                    return;
            }

        }

        public bool RequiresTarget => false;


        private void DoHelp(NWPlayer user)
        {
            string[] commands =
            {
                "help: Displays this help text.",
                "d2: Rolls a 2-sided die. Follow with a number between 1-10 to roll multiple dice. Example: /dice d2 4",
                "d4: Rolls a 4-sided die. Follow with a number between 1-10 to roll multiple dice. Example: /dice d4 4",
                "d6: Rolls a 6-sided die. Follow with a number between 1-10 to roll multiple dice. Example: /dice d6 4",
                "d8: Rolls an 8-sided die. Follow with a number between 1-10 to roll multiple dice. Example: /dice d8 4",
                "d10: Rolls a 10-sided die. Follow with a number between 1-10 to roll multiple dice. Example: /dice d10 4",
                "d20: Rolls a 20-sided die. Follow with a number between 1-10 to roll multiple dice. Example: /dice d20 4" ,
                "d100: Rolls a 100-sided die. Follow with a number between 1-10 to roll multiple dice. Example: /dice d100 4",

            };

            var message = string.Join("\n", commands);

            user.SendMessage(message);
        }

        private void DoDiceRoll(NWPlayer user, int sides, int number)
        {
            int value;

            if (number < 1)
                number = 1;
            else if (number > 10)
                number = 10;

            switch (sides)
            {
                case 2:
                    value = SWLOR.Game.Server.Service.Random.D2(number);
                    break;
                case 4:
                    value = SWLOR.Game.Server.Service.Random.D4(number);
                    break;
                case 6:
                    value = SWLOR.Game.Server.Service.Random.D6(number);
                    break;
                case 8:
                    value = SWLOR.Game.Server.Service.Random.D8(number);
                    break;
                case 10:
                    value = SWLOR.Game.Server.Service.Random.D10(number);
                    break;
                case 20:
                    value = SWLOR.Game.Server.Service.Random.D20(number);
                    break;
                case 100:
                    value = SWLOR.Game.Server.Service.Random.D100(number);
                    break;
                default:
                    value = 0;
                    break;
            }

            var dieRoll = number + "d" + sides;
            var message = ColorToken.SkillCheck("Dice Roll: ") + dieRoll + ": " + value;
            user.SpeakString(message);
        }

        public string ValidateArguments(NWPlayer user, params string[] args)
        {
            if (args.Length < 1)
            {
                return GenericError;
            }

            return string.Empty;
        }
    }
}