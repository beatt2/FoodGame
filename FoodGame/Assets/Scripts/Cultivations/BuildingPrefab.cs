
using UnityEngine;

namespace Cultivations
{
    public class BuildingPrefab : CultivationPrefab
    {
        [HideInInspector] public Building MyBuilding;



        protected void Awake()
        {
            var tempSprite = GetComponent<SpriteRenderer>().sprite;
            MyBuilding = new Building(Name,Sustainability,MoneyTick,ExpenseTick,UpgradeRank,BuildingPrice,MyCurrentState, MyFieldType,tempSprite,UpgradeValue, UpgradeOptions, PlacementIndex, EnviromentValue, Happyness);
        }

        public void CustomAwake()
        {
            var tempSprite = GetComponent<SpriteRenderer>().sprite;
            MyBuilding = new Building(Name,Sustainability,MoneyTick,ExpenseTick,UpgradeRank,BuildingPrice,MyCurrentState, MyFieldType, tempSprite, UpgradeValue, UpgradeOptions, PlacementIndex, EnviromentValue, Happyness);
        }

        public void ChangeValues(Building building)
        {
            MyBuilding = building;
            AddCultivation();
        }

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
