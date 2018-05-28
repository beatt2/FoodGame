using Cultivations;
using Money;
using UnityEngine;

namespace Grid
{
    public class BuildingPlacement : MonoBehaviour
    {
        public GameObject [] Farms;
        public GameObject[] Fields;
        public GameObject EmptyField;


        
        
        


        
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
                node.gameObject.AddComponent<PlantPrefab>();
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
            //TODO prototype only
            if(go == Farms[0])
            node.transform.position = new Vector3(node.transform.position.x,node.transform.position.y -0.09f,0);

        }
        
        
    }
}
