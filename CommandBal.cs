using Rocket.API;
using System.Collections.Generic;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;

namespace ExtraConcentratedJuice.RektEcon
{
    public class CommandBal : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "balance";

        public string Help => "Retrieves your or another player's balance.";

        public string Syntax => "<player>";

        public List<string> Aliases => new List<string> {"bal", "money", "cash"};

        public List<string> Permissions => new List<string>() { "rektecon.bal" };

        public void Execute(IRocketPlayer caller, string[] args)
        {
            if (args.Length != 0)
            {
                if (caller.HasPermission("rektecon.bal.other"))
                {
                    if (args.Length != 1)
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
                    decimal otherAmt = RektEcon.Instance.Handler.GetBalance(otherPlayer.Id);
                    UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("command_balance_other", otherPlayer.CharacterName, otherAmt, RektEcon.Instance.Configuration.Instance.Currency));
                    return;
                }
                else
                {
                    UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("command_balance_invalid_perms"), UnityEngine.Color.red);
                    return;
                }

            }

            if (caller is ConsolePlayer)
            {
                return;
            }
            decimal amount = RektEcon.Instance.Handler.GetBalance(caller.Id);
            UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("command_balance", amount, RektEcon.Instance.Configuration.Instance.Currency));
        }
    }
}