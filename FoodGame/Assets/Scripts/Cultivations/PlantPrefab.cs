using System;
using Boo.Lang.Environments;
using JetBrains.Annotations;
using Money;
using Node;
using UnityEngine;

namespace Cultivations
{

    public class PlantPrefab : CultivationPrefab
    {
        [HideInInspector] public Plant MyPlant;

        private Plant _savedPlant;


        protected void Awake()
        {
            MyPlant = new Plant(Name, UpgradePrefabIndex, MoneyTick, ExpenseTick, MonthsToGrow, BuildingPrice,
                MyCurrentState, MyFieldType, UpgradeValue, SpriteIndex,SidePanelSpriteIndex, EnviromentValue, Happiness, SizeRank,Upgrade,UpgradeDuration, MonthCount);


        }

        public void CustomAwake()
        {
            MyPlant = new Plant(Name, UpgradePrefabIndex, MoneyTick, ExpenseTick, MonthsToGrow, BuildingPrice,
                MyCurrentState, MyFieldType, UpgradeValue, SpriteIndex,SidePanelSpriteIndex, EnviromentValue, Happiness, SizeRank, Upgrade, UpgradeDuration, MonthCount);
        }

        private void Start()
        {
            AddCultivation();
        }

        public void RemoveUpgrade()
        {
            CultivationManager.Instance.RemoveEntry(MyPlant);
            MyPlant = _savedPlant;
            SyncValuesToMyPlant();
            AddCultivation();

        }

        [CanBeNull]
        public Plant GetSavedPlant()
        {
            return _savedPlant;

        }

        public void SetSavedPlant(Plant plant)
        {
            _savedPlant = plant;
        }


        //TODO WHY IS THIS DIFFERENT THAN BUILDINGPREFAB????
        public void ChangeValues(Plant plant)
        {
            if (MyPlant != null)
            {
                _savedPlant = MyPlant;
                //SimpleMoneyManager.Instance.RemoveValue(_savedPlant);

            }
            MyPlant = plant;
            SyncValuesToMyPlant();
            AddCultivation();
        }

        private void SyncValuesToMyPlant()
        {
            Name = MyPlant.Name;
            MyPlant.FieldType = GetComponent<NodeState>().FieldType;
            MyPlant.MyCultivationState = GetComponent<NodeState>().CurrentState;
            Name = MyPlant.Name;
            UpgradePrefabIndex = MyPlant.UpgradePrefabIndex;
            MoneyTick = MyPlant.MoneyTick;
            BuildingPrice = MyPlant.BuildPrice;
            MonthsToGrow = MyPlant.MonthsToGrow;
            MyCurrentState = MyPlant.MyCultivationState;
            MyFieldType = MyPlant.FieldType;
            Upgrade = MyPlant.Upgrade;
            UpgradeDuration = MyPlant.UpgradeDuration;
            UpgradePrefabIndex = MyPlant.UpgradePrefabIndex;
            MonthCount = MyPlant.MonthCount;
        }







        private void AddCultivation()
        {
            if (Upgrade)
            {
                CultivationManager.Instance.RemoveEntry(_savedPlant);
            }
            CultivationManager.Instance.AddValue(MyPlant, GetSavedPlant());
        }
    }
}
