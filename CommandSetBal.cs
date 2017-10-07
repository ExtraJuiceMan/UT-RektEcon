using System;
using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;

namespace ExtraConcentratedJuice.RektEcon
{
    public class CommandSetBal : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "setbalance";

        public string Help => "Set a player's balance.";

        public string Syntax => "<player>";

        public List<string> Aliases => new List<string> {"setbal", "setmoney", "setcash"};

        public List<string> Permissions => new List<string>() { "rektecon.setbal" };

        public void Execute(IRocketPlayer caller, string[] args)
        {
            if (args.Length == 0)
            {
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("invalid_args"), UnityEngine.Color.red);
                return;
            }

            if (args.Length != 1)
            {
                if (args.Length != 2)
                {
                    UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("invalid_args"), UnityEngine.Color.red);
                    return;
                }

                UnturnedPlayer otherPlayer = UnturnedPlayer.FromName(args[0]);
                if (otherPlayer == null)
                {
                    UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("invalid_player"), UnityEngine.Color.red);
                    return;
                }

                decimal amt;

                if (!Decimal.TryParse(args[1], out amt) || amt <= 0)
                {
                    UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("invalid_amount"), UnityEngine.Color.red);
                    return;
                }
                RektEcon.Instance.Handler.SetBalance(otherPlayer.Id, amt);
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("command_setbalance_other", otherPlayer.CharacterName, amt, RektEcon.Instance.Configuration.Instance.Currency));
                return;

            }

            if (caller is ConsolePlayer)
            {
                return;
            }

            decimal amount;
            if (!Decimal.TryParse(args[0], out amount) || amount <= 0)
            {
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("invalid_amount"), UnityEngine.Color.red);
                return;
            }
            RektEcon.Instance.Handler.SetBalance(caller.Id, amount);
            UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("command_setbalance", amount, RektEcon.Instance.Configuration.Instance.Currency));
        }
    }
}