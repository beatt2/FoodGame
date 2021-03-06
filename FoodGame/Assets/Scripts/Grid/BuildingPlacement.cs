﻿using Cultivations;
using Events;
using Money;
using Node;
using Save;
using UnityEngine;
// ReSharper disable PossibleNullReferenceException

namespace Grid
{
    public class BuildingPlacement : MonoBehaviour
    {
        public GameObject[] Farms;
        public GameObject[] Fields;
        public GameObject[] FarmUpgrades;
        public GameObject[] FieldUpgrades;
        public GameObject EmptyField;
        private Sprite _emptyFieldSprite;


        private void Awake()
        {
            GridManager.Instance.gameObject.GetComponent<Selection>().ToggleBuildPanel(true);
            _emptyFieldSprite = EmptyField.GetComponent<SpriteRenderer>().sprite;
        }


        /// <summary>
        /// Sets and ads the empty field component
        /// </summary>
        /// <param name="node"></param>
        /// <param name="sizeRank"></param>
        public void SetEmptyField(GameObject node, int sizeRank)
        {
            if (GridManager.Instance.GetSelectedNode() == null) return;
            EmptyField.GetComponent<PlantPrefab>().CustomAwake();
            node.AddComponent<PlantPrefab>();
            node.GetComponent<PlantPrefab>().ChangeValues(EmptyField.GetComponent<PlantPrefab>().MyPlant);
            node.GetComponent<PlantPrefab>().MyPlant.SizeRank = sizeRank;
        }

        public bool BuildFarm(int index)
        {
            if (SimpleMoneyManager.Instance.EnoughMoney(Farms[index].GetComponent<BuildingPrefab>().BuildingPrice))
            {
                ChangeTile(Farms[index], false);
                SoundManager.Instance.FarmPlacementSound();
                return true;
            }

            return false;
        }

        public bool BuildField(int index)
        {
            if (SimpleMoneyManager.Instance.EnoughMoney(Fields[index].GetComponent<PlantPrefab>().BuildingPrice))
            {
                ChangeTile(Fields[index], true);
                SoundManager.Instance.PlayFieldPlacementSound();
                return true;
            }

            return false;
        }

        public int GetFieldBuildPrice(int index)
        {
            return Fields[index].GetComponent<PlantPrefab>().BuildingPrice;
        }
        public bool UpgradeField(int index)
        {
            if (SimpleMoneyManager.Instance.EnoughMoney(FieldUpgrades[index].GetComponent<PlantPrefab>().MyPlant.UpgradeValue))
            {
                ChangeTile(FieldUpgrades[index], true);
                CultivationManager.Instance.AddUpgradedCultivation(GridManager.Instance.GetSelectedNode().GetComponent<PlantPrefab>());
                SoundManager.Instance.PlayUpgradeSound();
                return true;
            }

            return false;
        }

        public bool UpgradeFarm(int index)
        {
            Debug.Log(FarmUpgrades[index].GetComponent<BuildingPrefab>().MyBuilding.UpgradeValue);
            if (SimpleMoneyManager.Instance.EnoughMoney(FarmUpgrades[index].GetComponent<BuildingPrefab>().MyBuilding.UpgradeValue))
            {
                FarmUpgrades[index].GetComponent<BuildingPrefab>().CustomAwake();
                ChangeTile(FarmUpgrades[index], false);
                CultivationManager.Instance.AddUpgradedCultivation(GridManager.Instance.GetSelectedNode().GetComponent<BuildingPrefab>());
                SoundManager.Instance.PlayUpgradeSound();
                return true;
            }

            return false;
        }

        public static void UpgradeFarmFinished(BuildingPrefab buildingPrefab)
        {
       
                var node = buildingPrefab.GetComponent<NodeBehaviour>();
                node.SetSprite(SaveManager.Instance.GetSprite(buildingPrefab.GetSavedBuilding().SpriteIndex));

                buildingPrefab.RemoveUpgrade();
            
   

        }

        public static void UpgradeFieldFinished(PlantPrefab plantPrefab)
        {

                var node = plantPrefab.GetComponent<NodeBehaviour>();
                node.SetSprite(SaveManager.Instance.GetSprite(plantPrefab.GetSavedPlant().SpriteIndex));
                plantPrefab.RemoveUpgrade();
         
     
        }


        private void ChangeTile(GameObject go, bool field)
        {
            if (GridManager.Instance.GetSelectedNode() == null) return;
            var node = GridManager.Instance.GetSelectedNode();
            node.SetSprite(go.GetComponent<SpriteRenderer>().sprite);
            node.GetComponent<NodeState>().ChangeValues(go.GetComponent<NodeState>());
            if (field)
            {
                go.GetComponent<PlantPrefab>().CustomAwake();
                node.GetComponent<PlantPrefab>().ChangeValues(go.GetComponent<PlantPrefab>().MyPlant);
                EventManager.Instance.AddEnviromentValue
                (
                    node.GetComponent<NodeState>().FieldType,
                    node.GetComponent<PlantPrefab>().MyPlant.EnviromentValue
                );
                EventManager.Instance.AddHappinessValue
                (
                    node.GetComponent<NodeState>().FieldType,
                    node.GetComponent<PlantPrefab>().MyPlant.Happiness
                );
                GetComponent<Selection>().SetSidePanel(node.GetComponent<PlantPrefab>().MyPlant);
            }
            else
            {
                if (!go.GetComponent<BuildingPrefab>().MyBuilding.Upgrade)
                {
                    node.gameObject.AddComponent<BuildingPrefab>().ChangeValues(go.GetComponent<BuildingPrefab>().MyBuilding);
                    if (node.GetComponent<BuildingPrefab>().SizeRank > 2)
                    {
                        EventManager.Instance.AddFactoryReview();
                    }
                }
                else
                {
                    node.GetComponent<BuildingPrefab>().ChangeValues(go.GetComponent<BuildingPrefab>().MyBuilding);
                }

                EventManager.Instance.AddEnviromentValue
                (
                    node.GetComponent<NodeState>().FieldType,
                    node.GetComponent<BuildingPrefab>().MyBuilding.EnviromentValue
                );
                EventManager.Instance.AddHappinessValue
                (
                    node.GetComponent<NodeState>().FieldType,
                    node.GetComponent<BuildingPrefab>().MyBuilding.Happiness
                );

                GetComponent<Selection>().SetSidePanel(node.GetComponent<BuildingPrefab>().MyBuilding);
            }
        }


        public Sprite GetOriginalSprite()
        {
            return _emptyFieldSprite;
        }


        public static void RemoveTiles(NodeBehaviour node)
        {
            if (node.GetListIndex() == -1) return;
            if (node.GetCurrentState() == NodeState.CurrentStateEnum.Field)
            {
                bool isPlant = false;
                if (node.gameObject.GetComponent<PlantPrefab>() != null)
                {
                    SimpleMoneyManager.Instance.RemoveValue(node.gameObject.GetComponent<PlantPrefab>().MyPlant);
                    
                    EventManager.Instance.AddEnviromentValue
                    (
                        node.GetComponent<NodeState>().FieldType,
                        -node.GetComponent<PlantPrefab>().MyPlant.EnviromentValue
                    );
                    EventManager.Instance.AddHappinessValue
                    (
                        node.GetComponent<NodeState>().FieldType,
                        -node.GetComponent<PlantPrefab>().MyPlant.Happiness
                    );
                    if (node.GetComponent<PlantPrefab>().MyPlant.Upgrade)
                    {
                        CultivationManager.Instance.RemoveUpgradedCultivation(node.GetComponent<PlantPrefab>());
                        node.ResetNode(true, false);
                        node.HighLight.ChangeColorToOld();
                        node.GetComponent<PlantPrefab>().ResetValues();
                    }                    
                    isPlant = true;
                }


                node.ResetNode(isPlant, false);
                return;
            }

            int nodeCount = GridManager.Instance.GetCultivationLocationDictionary()[node.GetListIndex()].Count;
            int nodeIndex = node.GetListIndex();
            for (int i = 0; i < nodeCount; i++)
            {
                var nodeBehaviour = GridManager.Instance.GetCultivationLocationDictionary()[nodeIndex][i];
                nodeBehaviour.GetNodeFence().TryRemoveFence();
                if (nodeBehaviour.gameObject.GetComponent<PlantPrefab>() != null)
                {
                    SimpleMoneyManager.Instance.RemoveValue(nodeBehaviour.gameObject.GetComponent<PlantPrefab>().MyPlant);
                    EventManager.Instance.AddEnviromentValue
                    (
                        nodeBehaviour.GetComponent<NodeState>().FieldType,
                        -nodeBehaviour.GetComponent<PlantPrefab>().MyPlant.EnviromentValue
                    );
                    EventManager.Instance.AddHappinessValue
                    (
                        nodeBehaviour.GetComponent<NodeState>().FieldType,
                        -nodeBehaviour.GetComponent<PlantPrefab>().MyPlant.Happiness
                    );
                    if (nodeBehaviour.GetComponent<PlantPrefab>().MyPlant.Upgrade)
                    {
                        CultivationManager.Instance.RemoveUpgradedCultivation(nodeBehaviour.GetComponent<PlantPrefab>());
                    }

                    nodeBehaviour.GetComponent<PlantPrefab>().ResetValues();
                }
                else if (nodeBehaviour.gameObject.GetComponent<BuildingPrefab>() != null)
                {
                    SimpleMoneyManager.Instance.RemoveValue(nodeBehaviour.gameObject.GetComponent<BuildingPrefab>().MyBuilding);
                    EventManager.Instance.AddEnviromentValue
                    (
                        nodeBehaviour.GetComponent<NodeState>().FieldType,
                        -nodeBehaviour.GetComponent<BuildingPrefab>().MyBuilding.EnviromentValue
                    );
                    EventManager.Instance.AddHappinessValue
                    (
                        nodeBehaviour.GetComponent<NodeState>().FieldType,
                        -nodeBehaviour.GetComponent<BuildingPrefab>().MyBuilding.Happiness
                    );
                    if (node.GetComponent<BuildingPrefab>().MyBuilding.Upgrade)
                    {
                        CultivationManager.Instance.RemoveUpgradedCultivation(node.GetComponent<BuildingPrefab>());
                    }  
                }

                nodeBehaviour.ResetNode(false, true);
            }

            GridManager.Instance.GetCultivationLocationDictionary()[nodeIndex].Clear();
        }
    }
}
