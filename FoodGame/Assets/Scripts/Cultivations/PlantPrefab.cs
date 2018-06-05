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
            var tempSprite = GetComponent<SpriteRenderer>().sprite;
            MyPlant = new Plant(Name,Sustainability,MoneyTick, ExpenseTick, BuildingPrice,UpgradeRank,MyCurrentState, MyFieldType, tempSprite, UpgradeValue, UpgradeOptions,SpriteIndex);

        }
        public  void CustomAwake()
        {
            var tempSprite = GetComponent<SpriteRenderer>().sprite;
            MyPlant = new Plant(Name,Sustainability,MoneyTick,ExpenseTick,BuildingPrice,UpgradeRank,MyCurrentState, MyFieldType, tempSprite, UpgradeValue, UpgradeOptions,SpriteIndex);

        }

        private void Start()
        {
            AddCultivation();
        }

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
