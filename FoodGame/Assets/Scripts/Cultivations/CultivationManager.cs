using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Grid;
using Money;
using Node;
using Save;
using TimeSystem;
using Tools;
using UnityEngine;

namespace Cultivations
{
    public class CultivationManager : Singleton<CultivationManager>
    {

        //private readonly Dictionary<Enum,List<Cultivation>> _cultivations = new Dictionary<Enum,List<Cultivation>>();
        private List<CultivationPrefabList> _activeUpgradedCultivations = new List<CultivationPrefabList>();
        private int _moneyValueTracker = 0;
        private int _moneyValueCount;

   
        private void Start()
        {
             _moneyValueCount = SaveManager.Instance.GetMoneyValueCount();
            
        }

        public void AddValue(Cultivation cultivation, Cultivation oldCultivation, CultivationPrefab cultivationPrefab)
        {
            if (!SimpleMoneyManager.Instance.EnoughMoney(cultivation.BuildPrice)) return;
            SimpleMoneyManager.Instance.RemoveMoney(cultivation.BuildPrice);
            //TODO CHANGE THE WAY TO DO THIS
            if (cultivation.MyCultivationState != NodeState.CurrentStateEnum.EmptyField)
            {
                SimpleMoneyManager.Instance.AddFinance(cultivation,oldCultivation != null ? oldCultivation.MonthCount : 0);
                _moneyValueTracker++;
                if (_moneyValueTracker == _moneyValueCount)
                {
                    SimpleMoneyManager.Instance.SetPercentageValues(SaveManager.Instance.GetPercentageValues());
                    TimeManager.Instance.CalculateMoney();
                }
                else if(cultivation.Upgrade)
                {
                    AddUpgradedCultivation(cultivationPrefab);
                }
            }
        }


        //TODO Maybe turn this into an event
        public void MonthlyTick()
        {
            for (var index = 0; index < _activeUpgradedCultivations.Count; index++)
            {
                var cultivationPrefab = _activeUpgradedCultivations[index].MyCultivationPrefab;
                cultivationPrefab.UpgradeDuration--;
                if (cultivationPrefab.MyCurrentState == NodeState.CurrentStateEnum.Farm)
                {
                    ((BuildingPrefab) cultivationPrefab).MyBuilding.UpgradeDuration = cultivationPrefab.UpgradeDuration;
                }
                else if (cultivationPrefab.MyCurrentState == NodeState.CurrentStateEnum.Field)
                {
                    ((PlantPrefab) cultivationPrefab).MyPlant.UpgradeDuration = cultivationPrefab.UpgradeDuration;
                    
                }
                if (cultivationPrefab.UpgradeDuration >= 1) continue;
                if (cultivationPrefab.MyCurrentState == NodeState.CurrentStateEnum.Farm)
                {
                    Debug.Log("Building type = buidling prefab");
                    GridManager.Instance.BuildingPlacement.UpgradeFarmFinished((BuildingPrefab) cultivationPrefab);
                    RemoveUpgradedCultivation(cultivationPrefab);
                }
                else if (cultivationPrefab.MyCurrentState == NodeState.CurrentStateEnum.Field)
                {
                    GridManager.Instance.BuildingPlacement.UpgradeFieldFinished((PlantPrefab) cultivationPrefab);
                    RemoveUpgradedCultivation(cultivationPrefab);
                    Debug.Log("Building type = plant prefab");
                }
                else
                {
                    Debug.LogError("Type not found");
                }
            }
        }

        public List<CultivationPrefabList> GetActiveCultivationPrefabLists()
        {
            return _activeUpgradedCultivations;
        }

        public void SetActiveCultivationPrefabList(List<CultivationPrefabList> getActiveCultivationPrefabLists)
        {
            _activeUpgradedCultivations = getActiveCultivationPrefabLists;
        }


        public void AddUpgradedCultivation(CultivationPrefab cultivationPrefab)
        {
            _activeUpgradedCultivations.Add(new CultivationPrefabList(cultivationPrefab));
        }

        public void RemoveUpgradedCultivation(CultivationPrefab cultivationPrefab)
        {
            _activeUpgradedCultivations.RemoveAll(
                c =>
                _activeUpgradedCultivations.Any
                    (c2 => c2.MyCultivationPrefab == cultivationPrefab));
        }






//        public void RemoveEntry(Cultivation cultivation)
//        {
//            //TODO STILL HAS TO LINK TO MONEYMANAGER
//            //if(_cultivations.ContainsKey(cultivation.MyCultivationState))
//            //_cultivations[cultivation.MyCultivationState].Remove(cultivation);
//        }


    }
}
