using System;
using Node;
using UnityEngine;

namespace Cultivations
{
    [Serializable]
    public class BuildingPrefab : CultivationPrefab
    {
        public Building MyBuilding;
        private Building _savedBuilding;




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
            MyBuilding = new Building( Name,Sustainability,MoneyTick,ExpenseTick,MonthsToGrow,BuildingPrice,
                MyCurrentState, MyFieldType,UpgradeValue, SpriteIndex, EnviromentValue, Happiness, SizeRank, Upgrade, UpgradeDuration);
        }

        public void ChangeValues(Building building)
        {
            if (MyBuilding != null)
            {
                _savedBuilding = MyBuilding;
            }
            MyBuilding = building;
            Name = MyBuilding.Name;
            Sustainability = MyBuilding.Sustainability;
            MoneyTick = MyBuilding.MoneyTick;
            BuildingPrice = MyBuilding.BuildPrice;
            MonthsToGrow = MyBuilding.MonthsToGrow;
            Upgrade = MyBuilding.Upgrade;
            UpgradeDuration = MyBuilding.UpgradeDuration;
        }

        private void AddCultivation()
        {
            if (Upgrade)
            {
                CultivationManager.Instance.RemoveEntry(_savedBuilding);

            }
            CultivationManager.Instance.AddValue(MyBuilding);
        }


    }
}
