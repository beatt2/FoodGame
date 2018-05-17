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

        public void ChangeValues(BuildingPrefab buildingPrefab)
        {
            Name = buildingPrefab.name;
            Sustainability = buildingPrefab.Sustainability;
            MoneyTick = buildingPrefab.MoneyTick;
            TickDelay = buildingPrefab.TickDelay;
            UpgradeRank = buildingPrefab.UpgradeRank;

        }

        protected void Update()
        {
            //TODO DAFUQ?
//          Vector3 tempVect = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            transform.position = (new Vector3(tempVect.x, tempVect.y, 0));
        }
    }
}
