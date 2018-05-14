using UnityEngine;

namespace Cultivations
{
    public class BuildingPrefab : CultivationPrefab
    {
        [HideInInspector] public Building MyBuilding;
        
        protected void Awake()
        {
            MyBuilding = new Building(Name,Sustainability,MoneyTick,TickDelay,UpgradeRank);
        }

        protected void Update()
        {
          Vector3 tempVect = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = (new Vector3(tempVect.x, tempVect.y, 0));
        }
    }
}
