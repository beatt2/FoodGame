﻿using Cultivations;
using Grid;
using UnityEngine;

namespace UI
{
    public class BuildingTab : MonoBehaviour 
    {
        public static Sprite StatBarFill;
        private BuildingPrefab [] _buildingPrefabs;
        private FarmButtons[] _farmButtons;
        
        
        private void Awake()
        {
            GridManager.Instance.gameObject.GetComponent<Selection>().ToggleBuildPanel(true);
            var tempFarms = GridManager.Instance.BuildingPlacement.Farms;
            _buildingPrefabs = new BuildingPrefab[tempFarms.Length];
            var tempGameObjects = GameObject.FindGameObjectsWithTag("FarmButton");
            _farmButtons = new FarmButtons[tempFarms.Length];
            for (int i = 0; i < _buildingPrefabs.Length; i++)
            {
                _buildingPrefabs[i] = tempFarms[i].GetComponent<BuildingPrefab>();
                _buildingPrefabs[i].CustomAwake();
                _farmButtons[i] = tempGameObjects[i].GetComponent<FarmButtons>();
            }

            
       
   
        }
        
        

    }
}