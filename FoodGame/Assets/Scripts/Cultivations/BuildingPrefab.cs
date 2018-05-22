using UnityEngine;

namespace Cultivations
{
    public class BuildingPrefab : CultivationPrefab
    {
        [HideInInspector] public Building MyBuilding;


        
        protected void Awake()
        {
            MyBuilding = new Building(Name,Sustainability,MoneyTick,UpgradeRank,BuildingPrice,MyCurrentState, MyFieldType);
        }

        public void CustomAwake()
        {
            MyBuilding = new Building(Name,Sustainability,MoneyTick,UpgradeRank,BuildingPrice,MyCurrentState, MyFieldType);
        }

        public void ChangeValues(Building building)
        {
            MyBuilding = building;
            AddCultivation();   
        }

        
        //TODO moving this to nodestate class
//        public Building.BuildingType GetBuildingType()
//        {
//            return MyBuildingType;
//        }

        private void AddCultivation()
        {
            CultivationManager.Instance.AddValue(MyBuilding);
        }

        protected void Update()
        {
            //TODO DAFUQ?
//          Vector3 tempVect = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            transform.position = (new Vector3(tempVect.x, tempVect.y, 0));
        }
    }
}
