using System;
using UnityEngine;

namespace Cultivations
{
    [Serializable]
    public class BuildingPrefab : CultivationPrefab
    {
        [HideInInspector] public Building MyBuilding;



        protected void Awake()
        {
            var tempSprite = GetComponent<SpriteRenderer>().sprite;
            MyBuilding = new Building(Name,Sustainability,MoneyTick,ExpenseTick,UpgradeRank,BuildingPrice,MyCurrentState, MyFieldType,UpgradeValue,SpriteIndex, EnviromentValue, Happiness);
        }

        public void CustomAwake()
        {
            var tempSprite = GetComponent<SpriteRenderer>().sprite;
            MyBuilding = new Building(Name,Sustainability,MoneyTick,ExpenseTick,UpgradeRank,BuildingPrice,MyCurrentState, MyFieldType,UpgradeValue, SpriteIndex, EnviromentValue, Happiness);
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


    }
}
