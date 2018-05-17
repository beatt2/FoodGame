
using UnityEditor;

namespace Cultivations
{
    public abstract class Cultivation
    {
        public string Name;
        public int Sustainability;
        public int MoneyTick;// = value
        public float TickDelay;
        public int UpgradeRank;


        public CultivationManager.CultivationType MyCultivationType;

        protected Cultivation(string name, int sustainability, int moneyTick, float tickDelay, int upgradeRank,CultivationManager.CultivationType cultivationType)
        {
            Name = name;
            Sustainability = sustainability;
            MoneyTick = moneyTick;
            TickDelay = tickDelay;
            UpgradeRank = upgradeRank;
            MyCultivationType = cultivationType;
        }

    }
}
