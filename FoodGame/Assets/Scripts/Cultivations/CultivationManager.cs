using System.Collections.Generic;
using System.Linq;
using Boo.Lang.Environments;
using Grid;
using Money;
using Node;
using Save;
using TimeSystem;
using Tools;
using UI;
using UnityEngine;

// ReSharper disable ConvertIfStatementToSwitchStatement

namespace Cultivations
{
    public class CultivationManager : Singleton<CultivationManager>
    {
        private readonly List<CultivationPrefabList> _activeUpgradedCultivations = new List<CultivationPrefabList>();
        private int _moneyValueTracker;
        private int _moneyValueCount;

        public SidePanel MySidePanel;


        private void Start()
        {
            _moneyValueCount = SaveManager.Instance.GetMoneyValueCount();
        }

        public void AddValue(Cultivation cultivation, Cultivation oldCultivation, CultivationPrefab cultivationPrefab)
        {
            if (!SimpleMoneyManager.Instance.EnoughMoney(cultivation.BuildPrice)) return;
            SimpleMoneyManager.Instance.RemoveMoney(cultivation.BuildPrice);
            if (cultivation.MyCultivationState == NodeState.CurrentStateEnum.EmptyField) return;
            SimpleMoneyManager.Instance.AddFinance(cultivation, oldCultivation != null ? oldCultivation.MonthCount : 0);
            _moneyValueTracker++;
            if (_moneyValueTracker == _moneyValueCount)
            {
                SimpleMoneyManager.Instance.SetPercentageValues(SaveManager.Instance.GetPercentageValues());
                TimeManager.Instance.CalculateMoney();
            }
            else if (cultivation.Upgrade)
            {
                AddUpgradedCultivation(cultivationPrefab);
                if (cultivationPrefab.MyCurrentState == NodeState.CurrentStateEnum.Farm)
                {
                    ((BuildingPrefab) cultivationPrefab).MyBuilding.UpgradeDuration = cultivationPrefab.UpgradeDuration;
                }
                else if (cultivationPrefab.MyCurrentState == NodeState.CurrentStateEnum.Field)
                {
                    ((PlantPrefab) cultivationPrefab).MyPlant.UpgradeDuration = cultivationPrefab.UpgradeDuration;
                }
            }
        }


        //TODO Maybe turn this into an event
        public void MonthlyTick()
        {

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < _activeUpgradedCultivations.Count; index++)
            {
                var upgrade = _activeUpgradedCultivations[index];
                var cultivationPrefab = upgrade.MyCultivationPrefab;
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
                    BuildingPlacement.UpgradeFarmFinished((BuildingPrefab) cultivationPrefab);
                    MySidePanel.SetPanel(((BuildingPrefab) cultivationPrefab).MyBuilding);
                    RemoveUpgradedCultivation(cultivationPrefab);
                }
                else if (cultivationPrefab.MyCurrentState == NodeState.CurrentStateEnum.Field)
                {
                    BuildingPlacement.UpgradeFieldFinished((PlantPrefab) cultivationPrefab);
                    MySidePanel.SetPanel(((PlantPrefab) cultivationPrefab).MyPlant);
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


        public void AddUpgradedCultivation(CultivationPrefab cultivationPrefab)
        {
            _activeUpgradedCultivations.Add(new CultivationPrefabList(cultivationPrefab));
        }

        // ReSharper disable once SuggestBaseTypeForParameter
        private void RemoveUpgradedCultivation(CultivationPrefab cultivationPrefab)
        {
            _activeUpgradedCultivations.RemoveAll
            (
                c => _activeUpgradedCultivations.Any
                    (
                        c2 => c2.MyCultivationPrefab == cultivationPrefab
                    )
            );
        }
    }
}
