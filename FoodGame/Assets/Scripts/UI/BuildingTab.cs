using Cultivations;
using Grid;
using Money;
using UnityEngine;

namespace UI
{
    public class BuildingTab : MonoBehaviour 
    {
        public static Sprite StatBarFill;
        private BuildingPrefab [] _buildingPrefabs;
        private FarmButtons[] _farmButtons;
        public bool BuildingTabActive = true;

        private int _highestPrice;
        

        private void Start()
        {
            
            GridManager.Instance.gameObject.GetComponent<Selection>().ToggleBuildPanel(true);
            var tempFarms = GridManager.Instance.BuildingPlacement.Farms;
            _buildingPrefabs = new BuildingPrefab[tempFarms.Length];
            var tempGameObjects = GameObject.FindGameObjectsWithTag("FarmButton");
            _farmButtons = new FarmButtons[tempFarms.Length];
//            foreach (var farm in tempFarms)
//            {
//                if (farm.GetComponent<BuildingPrefab>().MyBuilding.BuildPrice > _highestPrice)
//                {
//                    _highestPrice =farm.GetComponent<BuildingPrefab>().MyBuilding.BuildPrice;
//                }
//            }
            for (int i = 0; i < _buildingPrefabs.Length; i++)
            {
                _buildingPrefabs[i] = tempFarms[i].GetComponent<BuildingPrefab>();
                _buildingPrefabs[i].CustomAwake();
                _farmButtons[i] = tempGameObjects[i].GetComponent<FarmButtons>();
            }
            GridManager.Instance.gameObject.GetComponent<Selection>().ToggleBuildPanel(false);
        }
        
        private void Update()
        {
            if (!BuildingTabActive) return;
            foreach (var t in _farmButtons)
            {
                // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                if (_buildingPrefabs[t.IndexNumber].BuildingPrice > SimpleMoneyManager.Instance.GetCurrentMoney())
                {
                    t.SetInteractable(false);
                }
                else
                {
                    t.SetInteractable(true);
                }
            }
        }
        
        

    }
}
