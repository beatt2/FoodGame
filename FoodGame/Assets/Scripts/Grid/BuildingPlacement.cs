using Cultivations;
using Money;
using UnityEngine;

namespace Grid
{
    public class BuildingPlacement : MonoBehaviour
    {
        public GameObject [] CultivationPrefabs;
        public GameObject EmptyField;


        
        
        
        public bool BuildFarm(int index)
        {
            if (SimpleMoneyManager.Instance.EnoughMoney(CultivationPrefabs[index].GetComponent<BuildingPrefab>().BuildingPrice))
            {
                ChangeTile(CultivationPrefabs[index]);
                return true;
            }
            Debug.Log("Sorry not enough money");
            return false;
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

 
        public void BuildField()
        {
            if (SimpleMoneyManager.Instance.EnoughMoney(CultivationPrefabs[1].GetComponent<BuildingPrefab>().BuildingPrice))
            {
                ChangeTile(CultivationPrefabs[1]);
            }
            else
            {
                Debug.Log("Sorry not enough money");
            }
        }

        private void ChangeTile(GameObject go)
        {
            if (GridManager.Instance.GetSelectedNode() == null) return;
            var node = GridManager.Instance.GetSelectedNode();
            node.SetSprite(go.GetComponent<SpriteRenderer>().sprite);
            node.gameObject.AddComponent<BuildingPrefab>();
            go.GetComponent<BuildingPrefab>().CustomAwake();
            node.GetComponent<BuildingPrefab>().ChangeValues(go.GetComponent<BuildingPrefab>().MyBuilding);
            node.GetComponent<NodeState>().ChangeValues(go.GetComponent<NodeState>());
            //TODO prototype only
            if(go == CultivationPrefabs[0])
            node.transform.position = new Vector3(node.transform.position.x,node.transform.position.y -0.09f,0);

        }
        
        
    }
}
