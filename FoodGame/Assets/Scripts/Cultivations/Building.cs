namespace Cultivations
{
    public class Building : Cultivation
    {
        public Building(string name, int sustainability, int moneyTick, float tickDelay, int upgradeRank, CultivationManager.CultivationType cultivationType, int buildingPrice) 
            : base(name, sustainability, moneyTick, tickDelay, upgradeRank, cultivationType, buildingPrice)
        {
            
        }
        
    }
}
