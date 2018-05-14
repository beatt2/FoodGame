namespace Cultivations
{
    public class Plant : Cultivation
    {
        public Plant(string name, int sustainability, int moneyTick, float tickDelay, int upgradeRank) 
            : base(name, sustainability, moneyTick, tickDelay, upgradeRank)
        {
            
        }
    }
}
