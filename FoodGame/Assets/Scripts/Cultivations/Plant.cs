namespace Cultivations
{
    public class Plant : Cultivation
    {
        public Plant(string name, int sustainability, int moneyTick, float tickDelay, int upgradeRank, CultivationManager.CultivationType cultivationType) 
            : base(name, sustainability, moneyTick, tickDelay, upgradeRank, cultivationType)
        {
            
        }
    }
}
