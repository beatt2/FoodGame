using Node;
using UnityEngine;

namespace Grid
{
    public class NodeFence : MonoBehaviour
    {
        public bool Left;
        public bool Right;
        public bool Up;
        public bool Down;

        
        private int _location = -1;
        
        private static Vector2 _downLocation = new Vector2(1.39f, -0.93f);
        private static Vector2 _upLocation = new Vector2(-1.46f, 0.58f);
        private static Vector2 _rightLocation = new Vector2(1.52f, 0.53f);
        private static Vector2  _leftLocation = new Vector2(-1.38f, -0.92f);

        private NodeBehaviour _nodeBehaviour;

        private void Awake()
        {
            _nodeBehaviour = GetComponent<NodeBehaviour>();
        }

        public void SetLocation(int value)
        {
            _location = value;
        }

        public void ConfirmBuild()
        {
            switch (_location)
            {
                case 0 :
                    
                    //if()
          
                    if (CheckUpSpace(GridManager.Instance.GetNode(_nodeBehaviour.GridLocation.x,_nodeBehaviour.GridLocation.y + 1)))
                    {
                        //buildfence up
                    }

                    break;
                case 1:
                    if (!Up)
                    {
                        if (_nodeBehaviour.GridLocation.y + 1 < GridManager.Instance.GridSizeY())
                        {
                     
                        }  
                    }
                    
                    break;
                case 2:

                    break;
                case 3:
                    
                    break;
            }
        }

        private bool CheckUpSpace(NodeBehaviour node)
        {
            if (Up) return false;
            if (node.GridLocation.y + 1 < GridManager.Instance.GridSizeY())
            {
               node.GetNodeFence().Down = true;
            }
            return true;
        }

        private bool CheckLeftSpace(NodeBehaviour node)
        {
            if (Left) return false;
            if (node.GridLocation.x  > 0)
            {
                node.GetNodeFence().Left = true;
            }

            return true;
        }

        private void BuildFence(GameObject go, Vector2 loc)
        {
            
        }




    }
}
