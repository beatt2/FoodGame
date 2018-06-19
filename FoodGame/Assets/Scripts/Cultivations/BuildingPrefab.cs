using System;
using System.Runtime.InteropServices;
using Boo.Lang.Environments;
using Events;
using JetBrains.Annotations;
using Money;
using Node;
using UnityEngine;

namespace Cultivations
{
    public class BuildingPrefab : CultivationPrefab
    {
        public Building MyBuilding;
        private Building _savedBuilding;


        public void Start()
        {
//            MyBuilding.FieldType = GetComponent<NodeState>().FieldType;
//            MyBuilding.MyCultivationState = GetComponent<NodeState>().CurrentState;
//            MyCurrentState = MyBuilding.MyCultivationState;
//            MyFieldType = MyBuilding.FieldType;
//            //TODO this is kinda wonky
//            AddCultivation();
        }

        public bool FirstRun;

        public void CustomAwake()
        {
            var tempSprite = GetComponent<SpriteRenderer>().sprite;
            MyBuilding = new Building
            (
                Name,
                UpgradePrefabIndex,
                MoneyTick,
                ExpenseTick,
                MonthsToGrow,
                BuildingPrice,
                MyCurrentState,
                MyFieldType,
                UpgradeValue,
                SpriteIndex,
                SidePanelSpriteIndex,
                EnviromentValue,
                Happiness,
                SizeRank,
                Upgrade,
                UpgradeDuration,
                MonthCount
            );

            SyncValuesToMyBuidling();
        }

        public void RemoveUpgrade()
        {
            SimpleMoneyManager.Instance.RemoveValue(MyBuilding);
            EventManager.Instance.AddEnviromentValue(MyFieldType,-MyBuilding.EnviromentValue);
            EventManager.Instance.AddHappinessValue(MyFieldType,-MyBuilding.Happiness);
            MyBuilding = _savedBuilding;
            SyncValuesToMyBuidling();
            AddCultivation();
        }

        public void ChangeValues(Building building)
        {
            if (MyBuilding != null)
            {
                _savedBuilding = MyBuilding;
                SimpleMoneyManager.Instance.RemoveValue(MyBuilding);
            }

            MyBuilding = building;
            SyncValuesToMyBuidling();
            AddCultivation();
        }


        [CanBeNull]
        public Building GetSavedBuilding()
        {
            return _savedBuilding;
        }

        public void SetSavedBuilding(Building building)
        {
            _savedBuilding = building;
        }


        // Nodestate is set by gridmanager
        //TODO probably rework that..
        private void SyncValuesToMyBuidling()
        {
            Name = MyBuilding.Name;
            MyCurrentState = MyBuilding.MyCultivationState;
            MyFieldType = MyBuilding.FieldType;
            UpgradePrefabIndex = MyBuilding.UpgradePrefabIndex;
            MoneyTick = MyBuilding.MoneyTick;
            ExpenseTick = MyBuilding.ExpenseTick;
            UpgradeValue = MyBuilding.UpgradeValue;
            SpriteIndex = MyBuilding.SpriteIndex;
            SidePanelSpriteIndex = MyBuilding.SidePanelSpriteIndex;
            EnviromentValue = MyBuilding.EnviromentValue;
            Happiness = MyBuilding.Happiness;
            SizeRank = MyBuilding.SizeRank;
            BuildingPrice = MyBuilding.BuildPrice;
            MonthsToGrow = MyBuilding.MonthsToGrow;
            Upgrade = MyBuilding.Upgrade;
            UpgradeDuration = MyBuilding.UpgradeDuration;
            UpgradePrefabIndex = MyBuilding.UpgradePrefabIndex;
            MonthCount = MyBuilding.MonthCount;
        }

        private void AddCultivation()
        {
            if (Upgrade && !FirstRun)
            {
                SimpleMoneyManager.Instance.RemoveValue(_savedBuilding);
            }

            if (FirstRun)
            {
                CultivationManager.Instance.AddValue(MyBuilding, MyBuilding, this);
                FirstRun = false;
            }
            else
            {
                CultivationManager.Instance.AddValue(MyBuilding, GetSavedBuilding(), this);
            }
        }
    }
}