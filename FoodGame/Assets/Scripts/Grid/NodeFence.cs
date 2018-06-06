using System;
using System.Runtime.Remoting.Channels;
using Node;
using UnityEngine;

namespace Grid
{
    public class NodeFence : MonoBehaviour
    {
        public bool Left;
        public GameObject LeftGameObject;

        public bool Right;
        public GameObject RightGameObject;

        public bool Up;
        public GameObject UpGameObject;

        public bool Down;
        public GameObject DownGameObject;

        public enum SideEnum
        {
            Left,
            Right,
            Up,
            Down
        }


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
                case 0:
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


        private void CheckUpSpace()
        {
            if (Up) return;
            var neighBourNode =
                GridManager.Instance.GetNode(_nodeBehaviour.GridLocation.x, _nodeBehaviour.GridLocation.y + 1);
            if (neighBourNode != null)
            {
                neighBourNode.GetNodeFence().Down = true;
            }


            Up = true;
            UpGameObject = BuildFence(GridManager.Instance.FenceTwo, UpLocation, -1);
        }


        private void CheckRightSpace()
        {
            if (Right) return;
            var neighBourNode =
                GridManager.Instance.GetNode(_nodeBehaviour.GridLocation.x + 1, _nodeBehaviour.GridLocation.y);
            if (neighBourNode != null)
            {
                neighBourNode.GetNodeFence().Left = true;
            }


            Right = true;
            RightGameObject = BuildFence(GridManager.Instance.FenceOne, RightLocation, 0);
        }

        private void CheckLeftSpace()
        {
            if (Left) return;
            var neighBourNode =
                GridManager.Instance.GetNode(_nodeBehaviour.GridLocation.x - 1, _nodeBehaviour.GridLocation.y);
            if (neighBourNode != null)
            {
                neighBourNode.GetNodeFence().Right = true;
            }

            Left = true;
            LeftGameObject = BuildFence(GridManager.Instance.FenceOne, LeftLocation, 1);
            //build fence left
        }

        private void CheckDownSpace()
        {
            if (Down) return;
            var neighBourNode =
                GridManager.Instance.GetNode(_nodeBehaviour.GridLocation.x, _nodeBehaviour.GridLocation.y - 1);
            if (neighBourNode != null)
            {
                neighBourNode.GetNodeFence().Up = true;
            }

            Down = true;
            DownGameObject = BuildFence(GridManager.Instance.FenceTwo, DownLocation, 1);
        }

        private GameObject BuildFence(GameObject go, Vector2 loc, int layerIncrement)
        {
            go = Instantiate(go, loc, Quaternion.identity, transform);
            go.transform.localPosition = loc;
            go.GetComponent<SpriteRenderer>().sortingOrder = _nodeBehaviour.GetLayer() + layerIncrement;
            return go;
        }


        public void TryRemoveFence()
        {
            if (LeftGameObject != null)
            {
                if (!CheckNeighbour(-1, 0, SideEnum.Left))
                {
                    Destroy(LeftGameObject);
                    Left = false;
                }
            }

            if (RightGameObject != null)
            {
                if (!CheckNeighbour(1, 0, SideEnum.Right))
                {
                    Destroy(RightGameObject);
                    Right = false;
                }
            }

            if (UpGameObject != null)
            {
                if (!CheckNeighbour(0, 1, SideEnum.Up))
                {
                    Destroy(UpGameObject);
                    Up = false;
                }
            }

            if (DownGameObject != null)
            {
                if (!CheckNeighbour(0, -1, SideEnum.Down))
                {
                    
                    Destroy(DownGameObject);
                    Down = false;
                }
            }
        }


        private void RemoveFence()
        {
        }

        private bool CheckNeighbour(int x, int y, SideEnum side)
        {
            var neighBourNode =
                GridManager.Instance.GetNode(_nodeBehaviour.GridLocation.x + x, _nodeBehaviour.GridLocation.y + y);
            if (neighBourNode == null) return false;
         
            switch (side)
            {
                case SideEnum.Left:
                    if (neighBourNode.GetNodeFence().Right)
                    {
                        if (neighBourNode.GetCurrentState() != NodeState.CurrentStateEnum.Empty)
                        {
                            MoveFenceToNeighbour(neighBourNode.GetNodeFence(), SideEnum.Right);
                            return true;
                        }

                        neighBourNode.GetNodeFence().Right = false;

                    }

                    break;
                case SideEnum.Right:
                    if (neighBourNode.GetNodeFence().Left)
                    {
                        if (neighBourNode.GetCurrentState() != NodeState.CurrentStateEnum.Empty)
                        {
                            MoveFenceToNeighbour(neighBourNode.GetNodeFence(), SideEnum.Left);
                            return true;
                        }
                        neighBourNode.GetNodeFence().Left = false;
                    }

                    break;
                case SideEnum.Up:
                    if (neighBourNode.GetNodeFence().Down)
                    {
                        if (neighBourNode.GetCurrentState() != NodeState.CurrentStateEnum.Empty)
                        {
                            MoveFenceToNeighbour(neighBourNode.GetNodeFence(), SideEnum.Down);
                            return true;
                        }
                        neighBourNode.GetNodeFence().Down = false;
                    }

                    break;
                case SideEnum.Down:
                    
                    if (neighBourNode.GetNodeFence().Up)
                    {
                        if (neighBourNode.GetCurrentState() != NodeState.CurrentStateEnum.Empty)
                        {
                            MoveFenceToNeighbour(neighBourNode.GetNodeFence(), SideEnum.Up);
                            return true;
                        }

                        neighBourNode.GetNodeFence().Up = false;
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException("side", side, null);
            }

            return false;
        }

        private void MoveFenceToNeighbour(NodeFence nodeFence, SideEnum side)
            {
                switch (side)
                {
                    case SideEnum.Left:
                        nodeFence.LeftGameObject = RightGameObject;
                        nodeFence.LeftGameObject.transform.parent = nodeFence.transform;
                        RightGameObject = null;
                        Right = false;
                        break;
                    case SideEnum.Right:
                        nodeFence.RightGameObject = LeftGameObject;
                        nodeFence.RightGameObject.transform.parent = nodeFence.transform;
                        Left = false;
                        break;
                    case SideEnum.Up:
                        nodeFence.UpGameObject = DownGameObject;
                        nodeFence.UpGameObject.transform.parent = nodeFence.transform;
                        Down = false;
                        break;
                    case SideEnum.Down:
                        nodeFence.DownGameObject = UpGameObject;
                        nodeFence.DownGameObject.transform.parent = nodeFence.transform;
                        Up = false;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("side", side, null);
                }
            }
        }
    }