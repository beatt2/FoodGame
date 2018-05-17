using Cultivations;
using UnityEngine;
using UnityScript.Macros;

namespace Grid
{
    public class BuildingPlacement : MonoBehaviour
    {
        public GameObject [] CultivationPrefabs;


        public void BuildFarm()
        {
            ChangeTile(CultivationPrefabs[0]);
        }

        public void BuildFiled()
        {
            ChangeTile(CultivationPrefabs[1]);
        }

        private void ChangeTile(GameObject go)
        {
            if (GridManager.Instance.GetSelectedNode() == null) return;
            var node = GridManager.Instance.GetSelectedNode();
            node.SetSprite(go.GetComponent<SpriteRenderer>().sprite);
            node.gameObject.AddComponent<BuildingPrefab>();
            node.GetComponent<BuildingPrefab>().ChangeValues(go.GetComponent<BuildingPrefab>());

        }
        
        
    }
}
