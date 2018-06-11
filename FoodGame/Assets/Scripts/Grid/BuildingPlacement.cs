using Cultivations;
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


        //TODO MOVE TO ANOTHER SCRIPT
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
            node.GetComponent<PlantPrefab>().ChangeValues(
                EmptyField.GetComponent<PlantPrefab>().MyPlant, NodeState.CurrentStateEnum.EmptyField,
                NodeState.FieldTypeEnum.Nothing);
        }

        public bool BuildFarm(int index)
        {
            if (SimpleMoneyManager.Instance.EnoughMoney(Farms[index].GetComponent<BuildingPrefab>().BuildingPrice))
            {
                ChangeTile(Farms[index], false);
                return true;
            }

            Debug.Log("Sorry not enough money");
            return false;
        }

        public bool BuildField(int index)
        {
            if (SimpleMoneyManager.Instance.EnoughMoney(Fields[index].GetComponent<PlantPrefab>().BuildingPrice))
            {
                ChangeTile(Fields[index], true);

                return true;
            }

            Debug.Log("Sorry not enough money");
            return false;
        }

        public bool UpgradeField(int index)
        {
            if (SimpleMoneyManager.Instance.EnoughMoney(FieldUpgrades[index].GetComponent<PlantPrefab>().UpgradeValue))
            {
                ChangeTile(FieldUpgrades[index], true);
                return true;
            }

            Debug.Log("Sorry not enough money");
            return false;

        }

        public bool UpgradeFarm(int index)
        {
            if (SimpleMoneyManager.Instance.EnoughMoney(FarmUpgrades[index].GetComponent<BuildingPrefab>().UpgradeValue))
            {
                ChangeTile(FarmUpgrades[index], false);
                return true;
            }
            Debug.Log("Sorry not enough money");
            return false;
        }

        public void UpgradeFarmFinished(BuildingPrefab buildingPrefab)
        {
            var node = buildingPrefab.GetComponent<NodeBehaviour>();
            node.SetSprite(SaveManager.Instance.GetSprite(buildingPrefab.SpriteIndex));
            buildingPrefab.RemoveUpgrade();
        }

        public void UpgradeFieldFinished(PlantPrefab plantPrefab)
        {
            var node = plantPrefab.GetComponent<NodeBehaviour>();
            node.SetSprite(SaveManager.Instance.GetSprite(plantPrefab.SpriteIndex));
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
                node.GetComponent<PlantPrefab>().ChangeValues(go.GetComponent<PlantPrefab>().MyPlant,
                    go.GetComponent<NodeState>().CurrentState, go.GetComponent<NodeState>().FieldType);
                GetComponent<Selection>().SetSidePanel(node.GetComponent<PlantPrefab>().MyPlant);
            }
            else
            {
                go.GetComponent<BuildingPrefab>().CustomAwake();

                if (!go.GetComponent<BuildingPrefab>().MyBuilding.Upgrade)
                {
                    node.gameObject.AddComponent<BuildingPrefab>()
                        .ChangeValues(go.GetComponent<BuildingPrefab>().MyBuilding);
                }
                else
                {
                    node.GetComponent<BuildingPrefab>().ChangeValues(go.GetComponent<BuildingPrefab>().MyBuilding);
                }
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
                if (node.gameObject.GetComponent<PlantPrefab>() != null)
                {
                    CultivationManager.Instance.RemoveEntry(node.gameObject.GetComponent<PlantPrefab>().MyPlant);
                }
                else if (node.gameObject.GetComponent<BuildingPrefab>() != null)
                {
                    CultivationManager.Instance.RemoveEntry(node.gameObject.GetComponent<BuildingPrefab>().MyBuilding);
                }

                GridManager.Instance.GetCultivationLocationList()[node.GetListIndex()].Remove(node);
                node.ResetNode();
                return;
            }

            int nodeCount = GridManager.Instance.GetCultivationLocationList()[node.GetListIndex()].Count;
            int nodeIndex = node.GetListIndex();
            for (int i = 0; i < nodeCount; i++)
            {
                var nodeBehaviour = GridManager.Instance.GetCultivationLocationList()[nodeIndex][i];
                nodeBehaviour.GetNodeFence().TryRemoveFence();
                if (nodeBehaviour.gameObject.GetComponent<PlantPrefab>() != null)
                {
                    CultivationManager.Instance.RemoveEntry(
                        nodeBehaviour.gameObject.GetComponent<PlantPrefab>().MyPlant);
                }
                else if (nodeBehaviour.gameObject.GetComponent<BuildingPrefab>() != null)
                {
                    CultivationManager.Instance.RemoveEntry(nodeBehaviour.gameObject.GetComponent<BuildingPrefab>()
                        .MyBuilding);
                }

                nodeBehaviour.ResetNode();
            }

            GridManager.Instance.GetCultivationLocationList()[node.GetListIndex()].Clear();
        }
    }
}
