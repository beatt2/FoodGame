using System;
using Node;
using UnityEngine;

namespace Grid
{
    public class NodeFence : MonoBehaviour
    {
        public bool Left;
        public bool LeftOwner;
        public bool Right;
        public bool RightOwner;
        public bool Up;
        public bool UpOwner;
        public bool Down;
        public bool DownOwner;

        
        private int _location = -1;
        
        private static readonly Vector2 DownLocation = new Vector2(1.39f, -0.93f);
        private static readonly Vector2 UpLocation = new Vector2(-1.46f, 0.58f);
        private static readonly Vector2 RightLocation = new Vector2(1.52f, 0.53f);
        private static readonly Vector2 LeftLocation = new Vector2(-1.38f, -0.92f);

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
                    CheckLeftSpace();
                    CheckUpSpace();
                    break;
                case 1:
                    CheckUpSpace();
                    CheckRightSpace();
                    break;
                case 2:
                    CheckRightSpace();
                    CheckDownSpace();
                    break;
                case 3:
                    CheckLeftSpace();
                    CheckDownSpace();
                    break;
            }
        }

        public void RemoveFence()
        {
            if (LeftOwner)
            {
                
            }
        }

        private bool CheckNeighbour(int x, int y)
        {
            var neighBourNode = GridManager.Instance.GetNode(_nodeBehaviour.GridLocation.x + x,_nodeBehaviour.GridLocation.y + y);
            if (neighBourNode != null)
            {
                    neighBourNode.GetNodeFence().Down = true;
               
            }
        }

        private void CheckUpSpace()
        {
            if (Up) return;
            var neighBourNode = GridManager.Instance.GetNode(_nodeBehaviour.GridLocation.x,_nodeBehaviour.GridLocation.y + 1);
            if (neighBourNode != null)
            {
                  neighBourNode.GetNodeFence().Down = true;
                if (neighBourNode.GridLocation.y + 1 < GridManager.Instance.GridSizeY())
                {
                  
                }
            }
        

            Up = true;
            UpOwner = true;
            BuildFence(GridManager.Instance.FenceTwo, UpLocation,  -1);
        }
        
        

        private void CheckRightSpace()
        {
            if (Right) return;
            var neighBourNode = GridManager.Instance.GetNode(_nodeBehaviour.GridLocation.x + 1,_nodeBehaviour.GridLocation.y );
            if (neighBourNode != null)
            {
                if (neighBourNode.GridLocation.x + 1 < GridManager.Instance.GridSizeX())
                {
                    neighBourNode.GetNodeFence().Left = true;
                }
            }
     
            
            Right = true;
            RightOwner = true;
            BuildFence(GridManager.Instance.FenceOne, RightLocation, 0);
            //build fence right

        }

        private void CheckLeftSpace()
        {
            if (Left) return;
            var neighBourNode = GridManager.Instance.GetNode(_nodeBehaviour.GridLocation.x  -1,_nodeBehaviour.GridLocation.y);
            if (neighBourNode != null)
            {
                if (_nodeBehaviour.GridLocation.x > 0)
                {
                    neighBourNode.GetNodeFence().Right = true;
                }
            }

            Left = true;
            LeftOwner = true;
            BuildFence(GridManager.Instance.FenceOne, LeftLocation,1);
            //build fence left
        }

        private void CheckDownSpace()
        {
            if (Down) return;
            var neighBourNode = GridManager.Instance.GetNode(_nodeBehaviour.GridLocation.x ,_nodeBehaviour.GridLocation.y -1);
            if (neighBourNode != null)
            {
                if (neighBourNode.GridLocation.y > 0)
                {
                    neighBourNode.GetNodeFence().Up = true;
                }
            }

            Down = true;
            DownOwner = true;
            //buildfence down
            BuildFence(GridManager.Instance.FenceTwo, DownLocation, 1);
        }

        private void BuildFence(GameObject go, Vector2 loc, int layerIncrement)
        {
            go = Instantiate(go, loc, Quaternion.identity, transform);
            go.transform.localPosition = loc;
            go.GetComponent<SpriteRenderer>().sortingOrder = _nodeBehaviour.GetLayer() + layerIncrement;
        }




    }
}
