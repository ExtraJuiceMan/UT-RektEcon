using System;
using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;

namespace ExtraConcentratedJuice.RektEcon
{
    public class CommandPay : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "pay";

        public string Help => "Pays cash to another player.";

        public string Syntax => "<player> <amount>";

        public List<string> Aliases => new List<string> {"transfer"};

        public List<string> Permissions => new List<string>() { "rektecon.pay" };

        public void Execute(IRocketPlayer caller, string[] args)
        {
            if (args.Length != 2)
            {
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("invalid_args"), UnityEngine.Color.red);
                return;
            }

            UnturnedPlayer receiver = UnturnedPlayer.FromName(args[0]);

            if (receiver == null)
            {
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("invalid_player"), UnityEngine.Color.red);
                return;
            }
            if (caller.Id == receiver.Id)
            {
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("command_pay_invalid_self"), UnityEngine.Color.red);
                return;
            }

            decimal amt;

            if (!Decimal.TryParse(args[1], out amt) || amt <= 0)
            {
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("invalid_amount"), UnityEngine.Color.red);
                return;
            }

            decimal playerBalance = RektEcon.Instance.Handler.GetBalance(caller.Id);

            if (playerBalance - amt < 0)
            {
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("invalid_cash"), UnityEngine.Color.red);
                return;
            }

            RektEcon.Instance.Handler.AddBalance(caller.Id, -amt);
            RektEcon.Instance.Handler.AddBalance(receiver.Id, amt);
            UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("command_pay_sent", receiver.CharacterName, amt, RektEcon.Instance.Configuration.Instance.Currency));
            UnturnedChat.Say(receiver, RektEcon.Instance.Translations.Instance.Translate("command_pay_get", amt, RektEcon.Instance.Configuration.Instance.Currency, caller.DisplayName), UnityEngine.Color.yellow);
        }
    }
}