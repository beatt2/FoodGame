
using UnityEditor;

namespace Cultivations
{
    public abstract class Cultivation
    {
        public string Name;
        public int Sustainability;
        public int MoneyTick;
        public float TickDelay;
        public int UpgradeRank;

        protected Cultivation(string name, int sustainability, int moneyTick, float tickDelay, int upgradeRank)
        {
            Name = name;
            Sustainability = sustainability;
            MoneyTick = moneyTick;
            TickDelay = tickDelay;
            UpgradeRank = upgradeRank;
        }

    }
}
