using System;
using Boo.Lang.Environments;
using Node;
using UnityEngine;

namespace Cultivations
{
    [Serializable]
    public class PlantPrefab : CultivationPrefab
    {
        [HideInInspector] public Plant MyPlant;

        private Plant _savePlant;


        protected void Awake()
        {
            MyPlant = new Plant(Name, Sustainability, MoneyTick, ExpenseTick, MonthsToGrow, BuildingPrice,
                MyCurrentState, MyFieldType, UpgradeValue, SpriteIndex, EnviromentValue, Happiness, SizeRank,Upgrade,UpgradeDuration);
            
            
        }

        public void CustomAwake()
        {
            MyPlant = new Plant(Name, Sustainability, MoneyTick, ExpenseTick, MonthsToGrow, BuildingPrice,
                MyCurrentState, MyFieldType, UpgradeValue, SpriteIndex, EnviromentValue, Happiness, SizeRank, Upgrade, UpgradeDuration);
        }

        private void Start()
        {
            AddCultivation();
        }


        //TODO WHY IS THIS DIFFERENT THAN BUILDINGPREFAB????
        public void ChangeValues(Plant plant, NodeState.CurrentStateEnum currentStateEnum, NodeState.FieldTypeEnum fieldTypeEnum)
        {
            if (MyPlant != null)
            {
                _savePlant = MyPlant;
            }
            MyPlant = plant;
            MyPlant.FieldType = GetComponent<NodeState>().FieldType;
            MyPlant.MyCultivationState = GetComponent<NodeState>().CurrentState;
            Name = MyPlant.Name;
            Sustainability = MyPlant.Sustainability;
            MoneyTick = MyPlant.MoneyTick;
            BuildingPrice = MyPlant.BuildPrice;
            MonthsToGrow = MyPlant.MonthsToGrow;
            MyCurrentState = MyPlant.MyCultivationState;
            MyFieldType = MyPlant.FieldType;
            Upgrade = MyPlant.Upgrade;
            UpgradeDuration = MyPlant.UpgradeDuration;
            AddCultivation();
        }
        
      

        private void AddCultivation()
        {
            if (Upgrade)
            {
                CultivationManager.Instance.RemoveEntry(_savePlant);
            }
            
            CultivationManager.Instance.AddValue(MyPlant);
        }
    }
}