using Rocket.API;
using System.Collections.Generic;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using System;

namespace ExtraConcentratedJuice.RektEcon
{
    public class CommandKick : IRocketCommand
    {
        Random rnd = new Random();

        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "kick";

        public string Help => "Kicks a player";

        public string Syntax => "<player>";

        public List<string> Aliases => new List<string> { "rkick" };

        public List<string> Permissions => new List<string>() { "rektecon.kick" };

        public void Execute(IRocketPlayer caller, string[] args)
        {

            if (args.Length == 0 || args.Length > 2)
            {
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("invalid_args"), UnityEngine.Color.red);
                return;
            }

            UnturnedPlayer victim = UnturnedPlayer.FromName(args[0]);

            if (victim == null)
            {
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("invalid_player"), UnityEngine.Color.red);
                return;
            }
            if (victim.HasPermission("rektecon.nokick"))
            {
                UnturnedChat.Say(String.Format(RektEcon.Instance.Translations.Instance.Translate("command_kick_fail"), caller.DisplayName, victim.DisplayName), UnityEngine.Color.magenta);

            }

            string kickRsn;

            if (args.Length == 2)
            {
                kickRsn = args[1];
            }
            else
            {
                kickRsn = "ISSUER DID NOT SPECIFY A REASON.";
            }

            victim.Kick(kickRsn);
            UnturnedChat.Say(String.Format(RektEcon.Instance.Translations.Instance.Translate("command_kick"), caller.DisplayName, victim.DisplayName, kickRsn), UnityEngine.Color.magenta);
        }
    }
}