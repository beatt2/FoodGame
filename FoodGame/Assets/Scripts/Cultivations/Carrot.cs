using UnityEngine;

namespace Cultivations
{
    public class Carrot : Cultivation {
        public Carrot(string name, int sustainability, int moneyTick, float tickDelay, int upgradeRank, CultivationManager.CultivationType cultivationType) : 
            base(name, sustainability, moneyTick, tickDelay, upgradeRank, cultivationType)
        {

        }
    }
}
