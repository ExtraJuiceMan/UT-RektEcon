using System;
using System.Linq;
using Rocket.API;
using System.Collections.Generic;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using SDG.Unturned;

namespace ExtraConcentratedJuice.RektEcon
{
    public class CommandBuy : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "buy";

        public string Help => "Buys items.";

        public string Syntax => "<item> <amount>";

        public List<string> Aliases => new List<string> { "purchase" };

        public List<string> Permissions => new List<string>() { "rektecon.buy" };

        public void Execute(IRocketPlayer caller, string[] args)
        {
           if (args.Length == 0 || args.Length > 2)
            {
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("invalid_args"), UnityEngine.Color.red);
                return;
            }

            byte amount = 1;

            if (args.Length == 2)
            {
                if (!byte.TryParse(args[1], out amount))
                {
                    UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("invalid_amount"), UnityEngine.Color.red);
                    return;
                }
            }

            ushort id;
            string itemString = args[0].ToString();

            // Code snippet stolen from RocketMod's /i command :^)

            if (!ushort.TryParse(itemString, out id))
            {
                List<ItemAsset> sortedAssets = new List<ItemAsset>(SDG.Unturned.Assets.find(EAssetType.ITEM).Cast<ItemAsset>());
                ItemAsset asset = sortedAssets.Where(i => i.itemName != null).OrderBy(i => i.itemName.Length).Where(i => i.itemName.ToLower().Contains(itemString.ToLower())).FirstOrDefault();
                if (asset != null) id = asset.id;
                if (String.IsNullOrEmpty(itemString.Trim()) || id == 0)
                {
                    UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("item_not_found"), UnityEngine.Color.red);
                    return;
                }
            }

            Asset item = SDG.Unturned.Assets.find(EAssetType.ITEM, id);
            string itemName = ((ItemAsset)item).itemName;
            decimal price = RektEcon.Instance.Handler.GetItemPrice(id) * Convert.ToDecimal(amount);

            if (price == 0)
            {
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("item_not_available", itemName), UnityEngine.Color.yellow);
                return;
            }

            if (RektEcon.Instance.Handler.GetBalance(caller.Id) - price < 0)
            {
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("invalid_cash"), UnityEngine.Color.red);
                return;
            }

            RektEcon.Instance.Handler.AddBalance(caller.Id, -price);
            UnturnedPlayer uCaller = (UnturnedPlayer)caller;

            if (uCaller.GiveItem(id, amount))
            {
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("command_buy", amount, itemName, price, RektEcon.Instance.Configuration.Instance.Currency));
            }
            else
            {
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("command_buy_failed", itemName), UnityEngine.Color.red);
                RektEcon.Instance.Handler.AddBalance(caller.Id, price);
            }
        }
    }
}