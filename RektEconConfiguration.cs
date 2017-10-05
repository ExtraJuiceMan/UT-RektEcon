using Rocket.API;

namespace ExtraConcentratedJuice.RektEcon
{
    public class RektEconConfiguration : IRocketPluginConfiguration
    {
        public string Currency = "R";

        public void LoadDefaults()
        {
            Currency = "R";
        }
    }
}
