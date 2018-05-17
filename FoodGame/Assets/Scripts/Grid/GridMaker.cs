using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using Node;
using Tools;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace Grid
{
    [CreateAssetMenu]
    public class GridMaker : ScriptableObject
    {

        public GameObject RedBlock;
        public GameObject WhiteBlock;

        public GameObject RedSideBlock;
        public GameObject WhiteSideBlock;

        public GameObject BlueBlock;






        private const float XOffset = 1.463f;
        private const float YOffset = 0.74f;

        private const float XRowOffset = 1.463f;
        private const float YRowOffset = 0.74f;

        public Vector2Int Size;

        public void OnButtonPressed()
        {
            var parent = GameObject.FindGameObjectWithTag("Grid");
            if (parent != null)
            {
             
                var tempChildList = parent.transform.Cast<Transform>().ToList();
                foreach (var child in tempChildList)
                {
                    DestroyImmediate(child.gameObject);
                }
    
            }
            else
            {
                Debug.LogError("Object with tag Grid not found");
            }

            bool white = false;  
            
        
            int oldLayerCount = Size.x + 2;

            for (int x = 0; x < Size.x; x++)
            {    
                
                var currentLayerCount = oldLayerCount - x;
                var currentPosition = Vector3.zero;
                currentPosition.x = XRowOffset * x;
                currentPosition.y = YRowOffset * x;
                for (int y = 0; y < Size.y; y++)
                {

                    GameObject go;
                    GameObject node;
                    
                  
                    if (x == 0 || y == 0)
                    {
                        go = Instantiate(white ? WhiteSideBlock : RedSideBlock, currentPosition, Quaternion.identity, parent.transform);
             
                    }
                    else
                    {
                        go = Instantiate(white ? WhiteBlock : RedBlock, currentPosition, Quaternion.identity, parent.transform); 
                    }
                    Vector3 goPlace = new Vector3(go.transform.position.x, go.transform.position.y + 0.83f, 0);
                    
                    go.GetComponent<NodeBehaviour>().GridLocation = new Vector2Int(x,y);
                    go.GetComponent<SpriteRenderer>().sortingOrder = currentLayerCount;
             
                    go.name = "Node "+ x + " " + y;
             
                    white = !white;
                    currentPosition.x -= XOffset;
                    currentPosition.y += YOffset;
                    currentLayerCount--;
                }
                white = x % 2 == 0;
              
            }

        }
        
        
        
    }

}
