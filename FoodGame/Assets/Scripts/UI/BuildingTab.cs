using System.Security.Policy;
using Cultivations;
using Grid;
using Money;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BuildingTab : MonoBehaviour 
    {
        public static Sprite StatBarFill;
        private BuildingPrefab [] _buildingPrefabs;
        private FarmButtons[] _farmButtons;
        public Text[] MonthTexts;
        
        public bool BuildingTabActive = false;

        private float _highestPrice;
        

        private void Start()
        {
            
            GridManager.Instance.gameObject.GetComponent<Selection>().ToggleBuildPanel(true);
            var tempFarms = GridManager.Instance.BuildingPlacement.Farms;
            _buildingPrefabs = new BuildingPrefab[tempFarms.Length];
            var tempGameObjects = GameObject.FindGameObjectsWithTag("FarmButton");
            _farmButtons = new FarmButtons[tempFarms.Length];
            foreach (var farm in tempFarms)
            {
                farm.GetComponent<BuildingPrefab>().CustomAwake();
                if (farm.GetComponent<BuildingPrefab>().MyBuilding.BuildPrice > _highestPrice)
                {
                    _highestPrice =farm.GetComponent<BuildingPrefab>().MyBuilding.BuildPrice;
                }
            }
            for (int i = 0; i < _buildingPrefabs.Length; i++)
            {
                _buildingPrefabs[i] = tempFarms[i].GetComponent<BuildingPrefab>();
                _buildingPrefabs[i].CustomAwake();
                _farmButtons[i] = tempGameObjects[i].GetComponent<FarmButtons>();
                SetButton(_farmButtons[i].GetComponent<Button>(), _buildingPrefabs[i]) ;
                
            }

            //TODO FIX BUG
            for (int i = 0; i < GridManager.Instance.BuildingPlacement.Fields.Length; i++)
            {
               // MonthTexts[i].text = GridManager.Instance.BuildingPlacement.Fields[i].GetComponent<PlantPrefab>()
                   // .MyPlant.MonthsToGrow + " maanden";
            }
            GridManager.Instance.gameObject.GetComponent<Selection>().ToggleBuildPanel(false);
        }

        private void SetButton(Button button, BuildingPrefab prefab)
        {
            StatBar statBar = button.gameObject.transform.parent.GetComponentInChildren<StatBar>();
            if (statBar == null) return;
            for (int i = 0; i < prefab.Happiness; i++)
            {
                statBar.Happiness[i].gameObject.SetActive(true);
            }

            for (int i = 0; i < prefab.EnviromentValue; i++)
            {
                statBar.Enviroment[i].gameObject.SetActive(true);
            }

            float onePercent = _highestPrice / 100;
            int moneyValue =  prefab.MyBuilding.BuildPrice / (int)onePercent;
            int finalValue = moneyValue / 10;
            for (int i = 0; i < finalValue; i++)    
            {
                statBar.Money[i].gameObject.SetActive(true);
            }

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
