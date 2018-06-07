using System;
using Boo.Lang.Environments;
using Node;
using UnityEngine;

namespace Cultivations
{
    [Serializable]
    public class PlantPrefab :CultivationPrefab
    {
        [HideInInspector] public Plant MyPlant;


        protected void Awake()
        {

            MyPlant = new Plant(Name,Sustainability,MoneyTick, ExpenseTick,UpgradeRank,BuildingPrice,MyCurrentState, MyFieldType, UpgradeValue,SpriteIndex, EnviromentValue, Happiness);

        }
        public  void CustomAwake()
        {
        
            MyPlant = new Plant(Name, Sustainability, MoneyTick, ExpenseTick, UpgradeRank, BuildingPrice, MyCurrentState, MyFieldType,  UpgradeValue, SpriteIndex, EnviromentValue, Happiness);

        }

        private void Start()
        {
            AddCultivation();
        }
        
 
        //TODO WHY IS THIS DIFFERENT THAN BUILDINGPREFAB????
        public void ChangeValues(Plant plant)
        {

            MyPlant = plant;
            MyPlant.FieldType = GetComponent<NodeState>().FieldType;
            MyPlant.MyCultivationState = GetComponent<NodeState>().CurrentState;
            Name = MyPlant.Name;
            Sustainability = MyPlant.Sustainability;
            MoneyTick = MyPlant.MoneyTick;
            BuildingPrice = MyPlant.BuildPrice;
            UpgradeRank = MyPlant.UpgradeRank;
            MyCurrentState = MyPlant.MyCultivationState;
            MyFieldType = MyPlant.FieldType;
            AddCultivation();
        }

        private void AddCultivation()
        {
            CultivationManager.Instance.AddValue(MyPlant);
        }

    }
}
