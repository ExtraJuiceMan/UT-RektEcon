using System;
using System.Linq;
using Rocket.API;
using System.Collections.Generic;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using SDG.Unturned;

namespace ExtraConcentratedJuice.RektEcon
{
    public class CommandSell : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "sell";

        public string Help => "Sells items.";

        public string Syntax => "<item> <amount>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "rektecon.sell" };

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
            UnturnedPlayer uCaller = (UnturnedPlayer)caller;

            if (uCaller.Inventory.has(id) == null)
            {
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("item_not_have", itemName), UnityEngine.Color.red);
                return;
            }

            List<InventorySearch> list = uCaller.Inventory.search(id, true, true);

            if (list.Count < amount)
            {
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("item_not_enough_have", itemName), UnityEngine.Color.red);
                return;
            }

            decimal price = RektEcon.Instance.Handler.GetItemPrice(id) * Convert.ToDecimal(amount);

            if (price == 0)
            {
                UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("item_not_available", itemName), UnityEngine.Color.yellow);
                return;
            }

            // Code snippet modified from ZaupShop

            byte oamount = amount;

            while (amount > 0)
            {
                if (uCaller.Player.equipment.checkSelection(list[0].page, list[0].jar.x, list[0].jar.y))
                {
                    uCaller.Player.equipment.dequip();
                }
                uCaller.Inventory.removeItem(list[0].page, uCaller.Inventory.getIndex(list[0].page, list[0].jar.x, list[0].jar.y));
                list.RemoveAt(0);
                amount--;
            }

            string itemType = RektEcon.Instance.Handler.GetItemType(id);

            switch (itemType)
            {
                case "Gun":
                    price = price * Convert.ToDecimal(0.55);
                    break;
                case "Magazine":
                    price = price * Convert.ToDecimal(0.45);
                    break;
                case "Shirt":
                    price = price * Convert.ToDecimal(0.80);
                    break;
                case "Pants":
                    price = price * Convert.ToDecimal(0.80);
                    break;
                case "Vest":
                    price = price * Convert.ToDecimal(0.80);
                    break;
                case "Hat":
                    price = price * Convert.ToDecimal(0.80);
                    break;
                case "Backpack":
                    price = price * Convert.ToDecimal(0.80);
                    break;
                case "Mask":
                    price = price * Convert.ToDecimal(0.80);
                    break;
                default:
                    break;
            }

            RektEcon.Instance.Handler.AddBalance(caller.Id, price);
            UnturnedChat.Say(caller, RektEcon.Instance.Translations.Instance.Translate("command_sell", oamount, itemName, price, RektEcon.Instance.Configuration.Instance.Currency));
        }
    }
}