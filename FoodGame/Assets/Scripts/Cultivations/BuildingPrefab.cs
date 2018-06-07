using System;
using Node;
using UnityEngine;

namespace Cultivations
{
    [Serializable]
    public class BuildingPrefab : CultivationPrefab
    {
        public Building MyBuilding;


//
//        protected void Awake()
//        {
//            var tempSprite = GetComponent<SpriteRenderer>().sprite;
//           //MyBuilding = new Building(Name,Sustainability,MoneyTick,ExpenseTick,UpgradeRank,BuildingPrice,MyCurrentState, MyFieldType,UpgradeValue,SpriteIndex, EnviromentValue, Happiness);
//        }

        public void Start()
        {
            MyBuilding.FieldType = GetComponent<NodeState>().FieldType;
            MyBuilding.MyCultivationState = GetComponent<NodeState>().CurrentState;
            
            MyCurrentState = MyBuilding.MyCultivationState;
            MyFieldType = MyBuilding.FieldType;
            //TODO this is kinda wonky
            AddCultivation();

        }

        public void CustomAwake()
        {
            
            var tempSprite = GetComponent<SpriteRenderer>().sprite;
            MyBuilding = new Building(Name,Sustainability,MoneyTick,ExpenseTick,UpgradeRank,BuildingPrice,MyCurrentState, MyFieldType,UpgradeValue, SpriteIndex, EnviromentValue, Happiness);
        }

        public void ChangeValues(Building building)
        {
            MyBuilding = building;
            Name = MyBuilding.Name;
            Sustainability = MyBuilding.Sustainability;
            MoneyTick = MyBuilding.MoneyTick;
            BuildingPrice = MyBuilding.BuildPrice;
            UpgradeRank = MyBuilding.UpgradeRank;

            
            //TODO POSSIBLE BUG
//            MyCurrentState = MyBuilding.MyCultivationState;
//            MyFieldType = MyBuilding.FieldType;
            
            
        }

        private void AddCultivation()
        {
            CultivationManager.Instance.AddValue(MyBuilding);
        }


    }
}
