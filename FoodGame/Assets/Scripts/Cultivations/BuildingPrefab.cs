using System;
using Node;
using UnityEngine;

namespace Cultivations
{
    [Serializable]
    public class BuildingPrefab : CultivationPrefab
    {
        public Building MyBuilding;




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
            MyBuilding = new Building(Name,Sustainability,MoneyTick,ExpenseTick,MonthsToGrow,BuildingPrice,MyCurrentState, MyFieldType,UpgradeValue, SpriteIndex, EnviromentValue, Happiness, SizeRank);
        }

        public void ChangeValues(Building building)
        {
            MyBuilding = building;
            Name = MyBuilding.Name;
            Sustainability = MyBuilding.Sustainability;
            MoneyTick = MyBuilding.MoneyTick;
            BuildingPrice = MyBuilding.BuildPrice;
            MonthsToGrow = MyBuilding.MonthsToGrow;
        }

        private void AddCultivation()
        {
            CultivationManager.Instance.AddValue(MyBuilding);
        }


    }
}
