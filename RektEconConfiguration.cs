using Rocket.API;

namespace ExtraConcentratedJuice.RektEcon
{
    public class RektEconConfiguration : IRocketPluginConfiguration
    {
        public string Currency = "R";
        public decimal LootBoxPrice = 3500;
        public decimal ScratchcardPrice = 420;
        public bool UseSellColumn = true;

        public void LoadDefaults()
        {
            UseSellColumn = true;
            Currency = "R";
            LootBoxPrice = 3500;
            ScratchcardPrice = 420;
        }
    }
}
