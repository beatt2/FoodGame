using Cultivations;
using Money;
using UnityEngine;

namespace Grid
{
    public class BuildingPlacement : MonoBehaviour
    {
        public GameObject [] CultivationPrefabs;


        public bool BuildFarm(int index)
        {
            if (SimpleMoneyManager.Instance.EnoughMoney(CultivationPrefabs[index].GetComponent<BuildingPrefab>()
                .BuildingPrice))
            {
                ChangeTile(CultivationPrefabs[index]);
                return true;
            }
            else
            {
                
                Debug.Log("Sorry not enough money");
                return false;
            }
         
        }

 
        public void BuildField()
        {
            if (SimpleMoneyManager.Instance.EnoughMoney(CultivationPrefabs[1].GetComponent<BuildingPrefab>()
                .BuildingPrice))
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
