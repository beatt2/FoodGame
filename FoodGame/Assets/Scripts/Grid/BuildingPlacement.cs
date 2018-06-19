using Cultivations;
using Events;
using Money;
using Node;
using Save;
using UnityEngine;

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
        public void SetEmptyField(GameObject node)
        {
            if (GridManager.Instance.GetSelectedNode() == null) return;
            EmptyField.GetComponent<PlantPrefab>().CustomAwake();
            node.AddComponent<PlantPrefab>();
            node.GetComponent<PlantPrefab>().ChangeValues(EmptyField.GetComponent<PlantPrefab>().MyPlant);
        }

        public bool BuildFarm(int index)
        {
            if (SimpleMoneyManager.Instance.EnoughMoney(Farms[index].GetComponent<BuildingPrefab>().BuildingPrice))
            {
                ChangeTile(Farms[index], false);
                SoundManager.Instance.FarmPlacementSound();
                return true;
            }

            SimpleMoneyManager.Instance.Waarschuwing("Sorry not enough money");
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

            SimpleMoneyManager.Instance.Waarschuwing("Sorry not enough money");
            return false;
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

            SimpleMoneyManager.Instance.Waarschuwing("Sorry not enough money");
            return false;

        }

        public bool UpgradeFarm(int index)
        {
            if (SimpleMoneyManager.Instance.EnoughMoney(FarmUpgrades[index].GetComponent<BuildingPrefab>().MyBuilding.UpgradeValue))
            {
                FarmUpgrades[index].GetComponent<BuildingPrefab>().CustomAwake();
                ChangeTile(FarmUpgrades[index], false);
                CultivationManager.Instance.AddUpgradedCultivation(GridManager.Instance.GetSelectedNode().GetComponent<BuildingPrefab>());
                SoundManager.Instance.PlayUpgradeSound();
                return true;
            }
            SimpleMoneyManager.Instance.Waarschuwing("Sorry not enough money");
            return false;
        }

        public void UpgradeFarmFinished(BuildingPrefab buildingPrefab)
        {
            var node = buildingPrefab.GetComponent<NodeBehaviour>();
            node.SetSprite(SaveManager.Instance.GetSprite(buildingPrefab.GetSavedBuilding().SpriteIndex));

            buildingPrefab.RemoveUpgrade();
        }

        public void UpgradeFieldFinished(PlantPrefab plantPrefab)
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
                }
                else
                {
                    node.GetComponent<BuildingPrefab>().ChangeValues(go.GetComponent<BuildingPrefab>().MyBuilding);
                }
                GetComponent<Selection>().SetSidePanel(node.GetComponent<BuildingPrefab>().MyBuilding);
            }
        }


        public Sprite GetOriginalSprite()
        {
            return _emptyFieldSprite;
        }


        public void RemoveTiles(NodeBehaviour node)
        {
            if (node.GetListIndex() == -1) return;
            if (node.GetCurrentState() == NodeState.CurrentStateEnum.Field)
            {
                bool isPlant = false;
                if (node.gameObject.GetComponent<PlantPrefab>() != null)
                {
                    SimpleMoneyManager.Instance.RemoveValue(node.gameObject.GetComponent<PlantPrefab>().MyPlant);
                    isPlant = true;
                }
                else if (node.gameObject.GetComponent<BuildingPrefab>() != null)
                {
                    SimpleMoneyManager.Instance.RemoveValue(node.gameObject.GetComponent<BuildingPrefab>().MyBuilding);
                }


                node.ResetNode(isPlant);
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

                }
                else if (nodeBehaviour.gameObject.GetComponent<BuildingPrefab>() != null)
                {
                    SimpleMoneyManager.Instance.RemoveValue(nodeBehaviour.gameObject.GetComponent<BuildingPrefab>().MyBuilding);
                }

                nodeBehaviour.ResetNode(false);
            }
            GridManager.Instance.GetCultivationLocationDictionary()[nodeIndex].Clear();

  
        }
    }
}
