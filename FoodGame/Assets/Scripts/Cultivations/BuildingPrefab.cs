using System;
using System.Runtime.InteropServices;
using Boo.Lang.Environments;
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
        }

        public void RemoveUpgrade()
        {
            CultivationManager.Instance.RemoveEntry(MyBuilding);
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
            Debug.Log(MyCurrentState);
            MyFieldType = MyBuilding.FieldType;
            Debug.Log(MyFieldType);
            UpgradePrefabIndex = MyBuilding.UpgradePrefabIndex;
            MoneyTick = MyBuilding.MoneyTick;
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
                CultivationManager.Instance.RemoveEntry(_savedBuilding);
            }
            else if (!FirstRun)
            {
                CultivationManager.Instance.AddValue(MyBuilding, GetSavedBuilding());
            }
            else
            {
                CultivationManager.Instance.AddValue(MyBuilding, MyBuilding);
                FirstRun = false;
            }

 
        }
    }
}
