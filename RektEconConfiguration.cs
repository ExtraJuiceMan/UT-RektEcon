using Rocket.API;

namespace ExtraConcentratedJuice.RektEcon
{
    public class RektEconConfiguration : IRocketPluginConfiguration
    {
        public string Currency = "R";
        public decimal LootBoxPrice = 3500;

        public void LoadDefaults()
        {
            Currency = "R";
            LootBoxPrice = 3500;
        }
    }
}
