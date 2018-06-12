using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Grid;
using Money;
using Node;
using Tools;
using UnityEngine;

namespace Cultivations
{
    public class CultivationManager : Singleton<CultivationManager>
    {

        private readonly Dictionary<Enum,List<Cultivation>> _cultivations = new Dictionary<Enum,List<Cultivation>>();
        private readonly List<CultivationPrefab> _activeUpgradedCultivations = new List<CultivationPrefab>();

        public Dictionary<Enum, List<Cultivation>> GetCultivations()
        {
            return _cultivations;
        }

        public void AddValue(Cultivation cultivation, Cultivation oldCultivation)
        {
            if (!SimpleMoneyManager.Instance.EnoughMoney(cultivation.BuildPrice)) return;
            SimpleMoneyManager.Instance.RemoveMoney(cultivation.BuildPrice);
            SimpleMoneyManager.Instance.AddMonthlyIncome(cultivation.MoneyTick);
            //TODO CHANGE THE WAY TO DO THIS
            if (cultivation.MyCultivationState != NodeState.CurrentStateEnum.EmptyField)
            {
                SimpleMoneyManager.Instance.AddFinance(cultivation,
                    oldCultivation != null ? oldCultivation.MonthCount : 0);
            }
            SimpleMoneyManager.Instance.AddMonthlyExpenses(10);
            CheckForNull(cultivation.MyCultivationState);
            _cultivations[cultivation.MyCultivationState].Add(cultivation);
        }


        //TODO Maybe turn this into an event
        public void MonthlyTick()
        {
            for (var index = 0; index < _activeUpgradedCultivations.Count; index++)
            {
                var cultivationPrefab = _activeUpgradedCultivations[index];
                cultivationPrefab.UpgradeDuration--;
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


        public void AddUpgradedCultivation(CultivationPrefab cultivationPrefab)
        {
            _activeUpgradedCultivations.Add(cultivationPrefab);
        }

        public void RemoveUpgradedCultivation(CultivationPrefab cultivationPrefab)
        {
            _activeUpgradedCultivations.Remove(cultivationPrefab);
        }

        private void CheckForNull(NodeState.CurrentStateEnum currentState)
        {
            if (_cultivations.ContainsKey(currentState)) return ;
            _cultivations.Add(currentState, new List<Cultivation>());
        }



        public void RemoveEntry(Cultivation cultivation)
        {
            //TODO STILL HAS TO LINK TO MONEYMANAGER
            _cultivations[cultivation.MyCultivationState].Remove(cultivation);
        }


    }
}
