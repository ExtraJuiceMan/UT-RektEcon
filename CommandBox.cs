using System;
using System.Linq;
using Rocket.API;
using System.Collections.Generic;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using SDG.Unturned;

namespace ExtraConcentratedJuice.RektEcon
{
    public class CommandBox : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "gunbox";

        public string Help => "Gives you a random gun.";

        public string Syntax => "<amount>";

        public List<string> Aliases => new List<string> { "lootbox" };

        public List<string> Permissions => new List<string>() { "rektecon.box" };

        public void Execute(IRocketPlayer caller, string[] args)
        {
           if ( args.Length > 0)
            {
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("invalid_args"), UnityEngine.Color.red);
                return;
            }

            if (RektEcon.Instance.Handler.GetBalance(caller.Id) - RektEcon.Instance.Configuration.Instance.LootBoxPrice < 0)
            {
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("invalid_cash"), UnityEngine.Color.red);
                return;
            }

            ushort id = RektEcon.Instance.Handler.RandomItem("Gun"); ;
            Asset item = SDG.Unturned.Assets.find(EAssetType.ITEM, id);
            string itemName = ((ItemAsset)item).itemName;

            RektEcon.Instance.Handler.AddBalance(caller.Id, -RektEcon.Instance.Configuration.Instance.LootBoxPrice);
            UnturnedPlayer uCaller = (UnturnedPlayer)caller;

            if (uCaller.GiveItem(id, 1))
            {
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("command_unbox", itemName), UnityEngine.Color.yellow);
            }
            else
            {
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("command_buy_failed", itemName), UnityEngine.Color.red);
                RektEcon.Instance.Handler.AddBalance(caller.Id, RektEcon.Instance.Configuration.Instance.LootBoxPrice);
            }
        }
    }
}