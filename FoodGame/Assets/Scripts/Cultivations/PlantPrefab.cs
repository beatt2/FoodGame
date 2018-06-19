using Boo.Lang.Environments;
using Events;
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
            //MyPlant = new Plant(Name, UpgradePrefabIndex, MoneyTick, ExpenseTick, MonthsToGrow, BuildingPrice,
               // MyCurrentState, MyFieldType, UpgradeValue, SpriteIndex,SidePanelSpriteIndex, EnviromentValue, Happiness, SizeRank,Upgrade,UpgradeDuration, MonthCount);

            //SyncValuesToMyPlant();
        }

        public void CustomAwake()
        {
            MyPlant = new Plant(Name, UpgradePrefabIndex, MoneyTick, ExpenseTick, MonthsToGrow, BuildingPrice,
                MyCurrentState, MyFieldType, UpgradeValue, SpriteIndex,SidePanelSpriteIndex, EnviromentValue, Happiness, SizeRank, Upgrade, UpgradeDuration, MonthCount);
            
            SyncValuesToMyPlant();
        }

        public bool FirstRun;

        private void Start()
        {
            AddCultivation();
        }

        public void RemoveUpgrade()
        {
            SimpleMoneyManager.Instance.RemoveValue(MyPlant);
            EventManager.Instance.AddEnviromentValue(MyFieldType,-MyPlant.EnviromentValue);
            EventManager.Instance.AddHappinessValue(MyFieldType,-MyPlant.Happiness);
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
            MoneyTick = MyPlant.MoneyTick;
            BuildingPrice = MyPlant.BuildPrice;
            MonthsToGrow = MyPlant.MonthsToGrow;
            MyCurrentState = MyPlant.MyCultivationState;
            MyFieldType = MyPlant.FieldType;
            Upgrade = MyPlant.Upgrade;
            UpgradeDuration = MyPlant.UpgradeDuration;
            UpgradePrefabIndex = MyPlant.UpgradePrefabIndex;
            UpgradeValue = MyPlant.UpgradeValue;
            EnviromentValue = MyPlant.EnviromentValue;
            SidePanelSpriteIndex = MyPlant.SidePanelSpriteIndex;
            SpriteIndex = MyPlant.SpriteIndex;
            MonthCount = MyPlant.MonthCount;
            ExpenseTick = MyPlant.ExpenseTick;
            Happiness = MyPlant.Happiness;
            SizeRank = MyPlant.SizeRank;

        }







        private void AddCultivation()
        {
            if (Upgrade && !FirstRun)
            {
                SimpleMoneyManager.Instance.RemoveValue(_savedPlant);
                CultivationManager.Instance.AddValue(MyPlant, GetSavedPlant(), this);
            }
            else if (!FirstRun)
            {
                CultivationManager.Instance.AddValue(MyPlant, GetSavedPlant(), this);
            }
            else
            {
                CultivationManager.Instance.AddValue(MyPlant, MyPlant, this);
                FirstRun = false;
            }
       
        }
    }
}
