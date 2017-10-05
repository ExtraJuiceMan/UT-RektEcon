using System.Data.SQLite;
using System.IO;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Player;
using Rocket.API.Collections;

namespace ExtraConcentratedJuice.RektEcon
{
    public class RektEcon : RocketPlugin<RektEconConfiguration>
    {
        public DBHandler Handler;
        public static RektEcon Instance;

        protected override void Load()
        {
            Instance = this;
            Handler = new DBHandler();
            Logger.LogWarning("RektEcon Loaded!");
            U.Events.OnPlayerConnected += Events_OnPlayerConnected;

            if (!File.Exists("database/rekt_data.db"))
            {
                Logger.LogWarning("Database file does not exist; creating now.");
                SQLiteConnection.CreateFile("database/rekt_data.db");
            }

            Instance.Handler.InitializeDatabase();
        }
        private void Events_OnPlayerConnected(UnturnedPlayer player)
        {
            Instance.Handler.InitializePlayer(player.Id);
        }
        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList(){
                { "command_balance", "You have {0}{1}." },
                { "command_balance_other", "{0} has {1}{2}." },
                { "command_balance_invalid_perms", "You do not have the required permissions to view other balances." },
                { "command_setbalance", "You have set your balance to {0}{1}." },
                { "command_setbalance_other", "{0}'s balance has been set to {1}{2}." },
                { "command_pay_sent", "You have sent {0} {1}{2}." },
                { "command_pay_get", "You have received {0}{1} from {2}." },
                { "command_pay_invalid_self", "You can't pay yourself, idiot." },
                { "command_buy", "You've purchased {0}x {1} for {2}{3}." },
                { "command_sell", "You've sold {0}x {1} for {2}{3}." },
                { "command_price", "{0}x {1} would cost you {2}{3}." },
                { "invalid_cash", "You don't have enough cash to do that." },
                { "invalid_amount", "Invalid amount." },
                { "invalid_args", "Invalid arguments." },
                { "invalid_player", "Player not found." },
                { "item_not_found", "Item was not found." },
                { "item_not_available", "That item ({0}) isn't available for purchase or sale." },
                { "item_not_have", "You do not have any {0} to sell." },
                { "item_not_enough_have", "You do not have enough of the item {0} to sell." },
                { "command_buy_failed", "Failed to buy {0}. Reverting purchase. Please contact an administrator." },
                };
            }
        }

    }
} 
