using UnityEngine;

namespace Cultivations
{
    public class PlantPrefab :CultivationPrefab
    {
        [HideInInspector] public Plant MyPlant;
    
        protected void Awake()
        {
            MyPlant = new Plant(Name,Sustainability,MoneyTick,TickDelay,UpgradeRank);
        }

    }
}
