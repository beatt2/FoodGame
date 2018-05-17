using UnityEngine;

namespace Cultivations
{
    public abstract class CultivationPrefab : MonoBehaviour
    {
        public string Name;
        public int Sustainability;
        public int MoneyTick;
        public float TickDelay;
        public int UpgradeRank;
        public CultivationManager.CultivationType MyCultivationType;



    }
}
