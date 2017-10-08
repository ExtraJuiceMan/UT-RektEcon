using System;
using System.Linq;
using Rocket.API;
using System.Collections.Generic;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using SDG.Unturned;

namespace ExtraConcentratedJuice.RektEcon
{
    public class CommandScratch : IRocketCommand
    {

        Random rnd = new Random();

        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "scratch";

        public string Help => "Scratchcard command to get CASH.";

        public string Syntax => "";

        public List<string> Aliases => new List<string> { "scratchcard" };

        public List<string> Permissions => new List<string>() { "rektecon.scratch" };

        public void Execute(IRocketPlayer caller, string[] args)
        {
           if ( args.Length > 0)
            {
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("invalid_args"), UnityEngine.Color.red);
                return;
            }

           decimal price = RektEcon.Instance.Configuration.Instance.ScratchcardPrice;

           if (RektEcon.Instance.Handler.GetBalance(caller.Id) - price < 0)
            {
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("invalid_cash"), UnityEngine.Color.red);
                return;
            }

           RektEcon.Instance.Handler.AddBalance(caller.Id, -price);
           int randomNumber = rnd.Next(1, 100);

           if (randomNumber == 1)
            {
                decimal amt = 10000;
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("command_scratch_jackpot", amt, RektEcon.Instance.Configuration.Instance.Currency), UnityEngine.Color.cyan);
                UnturnedChat.Say(RektEcon.Instance.Translations.Instance.Translate("command_scratch_jackpot_global", caller.DisplayName, amt, RektEcon.Instance.Configuration.Instance.Currency), UnityEngine.Color.cyan);
                RektEcon.Instance.Handler.AddBalance(caller.Id, amt);
                return;
            }
           else if (randomNumber < 50)
            {
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("command_scratch_nowin"));
                return;

            }
           else if (randomNumber < 70)
           {
                decimal amt = rnd.Next(1, 600);
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("command_scratch_win", amt, RektEcon.Instance.Configuration.Instance.Currency), UnityEngine.Color.cyan);
                RektEcon.Instance.Handler.AddBalance(caller.Id, amt);
                return;
            }
           else if (randomNumber < 85)
           {
                decimal amt = rnd.Next(600, 1100);
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("command_scratch_win", amt, RektEcon.Instance.Configuration.Instance.Currency), UnityEngine.Color.cyan);
                RektEcon.Instance.Handler.AddBalance(caller.Id, amt);
                return;
            }
           else if (randomNumber < 90)
           {
                decimal amt = rnd.Next(1000, 2800);
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("command_scratch_win", amt, RektEcon.Instance.Configuration.Instance.Currency), UnityEngine.Color.cyan);
                RektEcon.Instance.Handler.AddBalance(caller.Id, amt);
                return;
           }
           else if (randomNumber <= 100)
           {
                decimal amt = rnd.Next(1, 4500);
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("command_scratch_win", amt, RektEcon.Instance.Configuration.Instance.Currency), UnityEngine.Color.cyan);
                RektEcon.Instance.Handler.AddBalance(caller.Id, amt);
                return;
           }

        }
    }
}