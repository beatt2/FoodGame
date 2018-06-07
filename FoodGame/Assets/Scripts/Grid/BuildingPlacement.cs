using Cultivations;
using Money;
using Node;
using UnityEngine;
using UnityEngine.UI;

namespace Grid
{
    public class BuildingPlacement : MonoBehaviour
    {
        public GameObject [] Farms;
        private BuildingPrefab [] _buildingPrefabs;

        private FarmButtons[] _farmButtons;


        public GameObject[] Fields;
        public GameObject EmptyField;
        private Sprite _emptyFieldSprite;

        public bool BuildingTabActive = true;




        private void Awake()
        {
            GridManager.Instance.gameObject.GetComponent<Selection>().ToggleBuildPanel(true);
            _emptyFieldSprite = EmptyField.GetComponent<SpriteRenderer>().sprite;
            _buildingPrefabs = new BuildingPrefab[Farms.Length];
            //TODO Probably can remove the others now
            var tempGameObjects = GameObject.FindGameObjectsWithTag("FarmButton");
            _farmButtons = new FarmButtons[Farms.Length];
            for (int i = 0; i < _buildingPrefabs.Length; i++)
            {
                _buildingPrefabs[i] = Farms[i].GetComponent<BuildingPrefab>();
                _buildingPrefabs[i].CustomAwake();
                _farmButtons[i] = tempGameObjects[i].GetComponent<FarmButtons>();
            }


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
                return true;
            }
            Debug.Log("Sorry not enough money");
            return false;
        }

        public bool BuildField(int index)
        {
            Debug.Log(Fields[index].GetComponent<PlantPrefab>().BuildingPrice);
            if (SimpleMoneyManager.Instance.EnoughMoney(Fields[index].GetComponent<PlantPrefab>().BuildingPrice))
            {
                ChangeTile(Fields[index], true);
                return true;
            }
            Debug.Log("Sorry not enough money");
            return false;

        }

        private void ChangeTile(GameObject go, bool field)
        {
            if (GridManager.Instance.GetSelectedNode() == null) return;
            var node = GridManager.Instance.GetSelectedNode();
            node.SetSprite(go.GetComponent<SpriteRenderer>().sprite);
            if (field)
            {
               
                go.GetComponent<PlantPrefab>().CustomAwake();
                node.GetComponent<PlantPrefab>().ChangeValues(go.GetComponent<PlantPrefab>().MyPlant);
            }
            else
            {
                node.gameObject.AddComponent<BuildingPrefab>();
                go.GetComponent<BuildingPrefab>().CustomAwake();
                node.GetComponent<BuildingPrefab>().ChangeValues(go.GetComponent<BuildingPrefab>().MyBuilding);
            }
            node.GetComponent<NodeState>().ChangeValues(go.GetComponent<NodeState>());
        }

        public Sprite GetOriginalSprite()
        {
            return _emptyFieldSprite;
        }


        public void RemoveTiles(NodeBehaviour node)
        {
            if (node.GetListIndex() == -1) return;

            int nodeCount = GridManager.Instance.GetCultivationLocationList()[node.GetListIndex()].Count;
            int nodeIndex = node.GetListIndex();
            for (int i = 0; i < nodeCount; i++)
            {
                var nodeBehaviour = GridManager.Instance.GetCultivationLocationList()[nodeIndex][i];
                nodeBehaviour.GetNodeFence().TryRemoveFence();
                if (nodeBehaviour.gameObject.GetComponent<PlantPrefab>() != null)
                {
                    CultivationManager.Instance.RemoveEntry(nodeBehaviour.gameObject.GetComponent<PlantPrefab>().MyPlant);

                }
                else if (nodeBehaviour.gameObject.GetComponent<BuildingPrefab>() != null)
                {
                    CultivationManager.Instance.RemoveEntry(nodeBehaviour.gameObject.GetComponent<BuildingPrefab>().MyBuilding);
                }
                nodeBehaviour.ResetNode();
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
