using System;
using Node;
using UnityEngine;

namespace Grid
{
    public class NodeFence : MonoBehaviour
    {
        public bool Left;
        public int LeftSizeRank;
        public GameObject LeftGameObject;

        public bool Right;
        public int RightSizeRank;
        public GameObject RightGameObject;

        public bool Up;
        public int UpSizeRank;
        public GameObject UpGameObject;

        public bool Down;
        public int DownSizeRank;
        public GameObject DownGameObject;


        private enum SideEnum
        {
            Left,
            Right,
            Up,
            Down
        }


        private int _location = -1;

        public static readonly Vector2 DownLocation = new Vector2(1.39f, -0.93f);
        public static readonly Vector2 UpLocation = new Vector2(-1.46f, 0.58f);
        public static readonly Vector2 RightLocation = new Vector2(1.52f, 0.53f);
        public static readonly Vector2 LeftLocation = new Vector2(-1.38f, -0.92f);

        private NodeBehaviour _nodeBehaviour;

        private void Awake()
        {
            _nodeBehaviour = GetComponent<NodeBehaviour>();
        }

        public void SetLocation(int value)
        {
            _location = value;
        }

        public void ConfirmBuild(int sizeRank)
        {
            LeftSizeRank = sizeRank;
            RightSizeRank = sizeRank;
            DownSizeRank = sizeRank;
            UpSizeRank = sizeRank;
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
            if (Up && UpSizeRank < 3 || Up && UpSizeRank > 2 && UpGameObject != null) return;
            var neighBourNode =GridManager.Instance.GetNode(_nodeBehaviour.GridLocation.x, _nodeBehaviour.GridLocation.y + 1);
            if (neighBourNode != null)
            {
                if (neighBourNode.GetNodeFence().DownSizeRank < 3 && neighBourNode.GetNodeFence().DownGameObject != null)
                {
                    HardRemoveFence(SideEnum.Down, neighBourNode.GetNodeFence());
                }
                neighBourNode.GetNodeFence().Down = true;

            }
            Up = true;
            UpGameObject = BuildFence(UpSizeRank < 3 ? GridManager.Instance.FenceTwo : GridManager.Instance.FenceTwoBig, UpLocation, -1);
        }


        private void CheckRightSpace()
        {
            if (Right && RightSizeRank < 3 || Right && RightSizeRank > 2 && RightGameObject != null) return;
            var neighBourNode = GridManager.Instance.GetNode(_nodeBehaviour.GridLocation.x + 1, _nodeBehaviour.GridLocation.y);
            if (neighBourNode != null)
            {
                if (neighBourNode.GetNodeFence().LeftSizeRank < 3 && neighBourNode.GetNodeFence().LeftGameObject != null)
                {
                    HardRemoveFence(SideEnum.Left, neighBourNode.GetNodeFence());
                }
                neighBourNode.GetNodeFence().Left = true;
            }

            Right = true;
            RightGameObject = BuildFence(RightSizeRank < 3 ? GridManager.Instance.FenceOne : GridManager.Instance.FenceOneBig, RightLocation, -1);
        }

        private void CheckLeftSpace()
        {
            if (Left && LeftSizeRank < 3 || Left && LeftSizeRank > 2 && LeftGameObject != null) return;
            var neighBourNode = GridManager.Instance.GetNode(_nodeBehaviour.GridLocation.x - 1, _nodeBehaviour.GridLocation.y);
            if (neighBourNode != null)
            {
                if (neighBourNode.GetNodeFence().RightSizeRank < 3 && neighBourNode.GetNodeFence().RightGameObject != null)
                {
                    HardRemoveFence(SideEnum.Right, neighBourNode.GetNodeFence());
                }
                neighBourNode.GetNodeFence().Right = true;
            }

            Left = true;
            LeftGameObject = BuildFence(LeftSizeRank < 3 ? GridManager.Instance.FenceOne : GridManager.Instance.FenceOneBig, LeftLocation, 1);

        }

        private void CheckDownSpace()
        {
            if (Down && DownSizeRank < 3 || Down && DownSizeRank > 2 && DownGameObject != null) return;
            var neighBourNode = GridManager.Instance.GetNode(_nodeBehaviour.GridLocation.x, _nodeBehaviour.GridLocation.y - 1);
            if (neighBourNode != null)
            {
                if (neighBourNode.GetNodeFence().UpSizeRank < 3 && neighBourNode.GetNodeFence().DownGameObject != null)
                {
                    HardRemoveFence(SideEnum.Up, neighBourNode.GetNodeFence());
                }
                neighBourNode.GetNodeFence().Up = true;
            }

            Down = true;
            DownGameObject = BuildFence(DownSizeRank < 3 ? GridManager.Instance.FenceTwo : GridManager.Instance.FenceTwoBig, DownLocation, 1);
        }

        public GameObject BuildFence(GameObject go, Vector2 loc, int layerIncrement)
        {
            go = Instantiate(go, loc, Quaternion.identity, transform);
            go.transform.localPosition = loc;
            go.GetComponent<SpriteRenderer>().sortingOrder = _nodeBehaviour.GetLayer() + layerIncrement;
            return go;
        }

        private static void HardRemoveFence(SideEnum sideEnum, NodeFence node)
        {
            switch (sideEnum)
            {
                case SideEnum.Left:
                    Destroy(node.LeftGameObject);
                    break;
                case SideEnum.Right:
                    Destroy(node.RightGameObject);
                    break;
                case SideEnum.Up:
                    Destroy(node.UpGameObject);
                    break;
                case SideEnum.Down:
                    Destroy(node.DownGameObject);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("sideEnum", sideEnum, null);
            }
        }


        public void TryRemoveFence()
        {
            if (LeftGameObject != null)
            {
                if (!CheckNeighbour(-1, 0, SideEnum.Left))
                {
                    Destroy(LeftGameObject);
                    Left = false;
                    LeftSizeRank = 0;
                }
            }
            else
            {
                LeftSizeRank = 0;
            }

            if (RightGameObject != null)
            {
                if (!CheckNeighbour(1, 0, SideEnum.Right))
                {
                    Destroy(RightGameObject);
                    Right = false;
                    RightSizeRank = 0;
                }

            }
            else
            {
                RightSizeRank = 0;
            }

            if (UpGameObject != null)
            {
                if (!CheckNeighbour(0, 1, SideEnum.Up))
                {
                    Destroy(UpGameObject);
                    Up = false;
                    UpSizeRank = 0;
                }

            }
            else
            {
                UpSizeRank = 0;
            }

            if (DownGameObject != null)
            {
                if (!CheckNeighbour(0, -1, SideEnum.Down))
                {

                    Destroy(DownGameObject);
                    Down = false;
                    DownSizeRank= 0;
                }

            }
            else
            {
                DownSizeRank = 0;
            }
        }


        private bool CheckNeighbour(int x, int y, SideEnum side)
        {
            var neighBourNode = GridManager.Instance.GetNode(_nodeBehaviour.GridLocation.x + x, _nodeBehaviour.GridLocation.y + y);
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
                        if (nodeFence.LeftSizeRank < 3 && RightSizeRank > 2)
                        {
                            nodeFence.LeftGameObject = nodeFence.BuildFence(
                                GridManager.Instance.FenceOne,
                                LeftLocation,
                                1
                            );


                        }
                        else
                        {
                            nodeFence.LeftGameObject = RightGameObject;
                        }
                        Destroy(RightGameObject);
                        nodeFence.LeftGameObject.transform.parent = nodeFence.transform;
                        RightGameObject = null;
                        Right = false;
                        RightSizeRank = 0;
                        break;
                    case SideEnum.Right:
                        if (nodeFence.RightSizeRank < 3 && LeftSizeRank > 2)
                        {
                            nodeFence.RightGameObject = nodeFence.BuildFence(
                                GridManager.Instance.FenceOne,
                                RightLocation,
                                -1
                            );

                        }
                        else
                        {
                            nodeFence.RightGameObject = LeftGameObject;
                        }
                        Destroy(LeftGameObject);
                        nodeFence.RightGameObject.transform.parent = nodeFence.transform;

                        LeftGameObject = null;
                        Left = false;
                        LeftSizeRank = 0;
                        break;
                    case SideEnum.Up:
                        if (nodeFence.UpSizeRank < 3 && DownSizeRank > 2)
                        {
                            nodeFence.UpGameObject = nodeFence.BuildFence(
                                GridManager.Instance.FenceTwo,
                                UpLocation,
                                -1
                            );

                        }
                        else
                        {
                            nodeFence.UpGameObject = DownGameObject;
                        }
                        Destroy(DownGameObject);
                        nodeFence.UpGameObject.transform.parent = nodeFence.transform;
                        DownGameObject = null;
                        Down = false;
                        DownSizeRank = 0;
                        break;
                    case SideEnum.Down:
                        if (nodeFence.DownSizeRank < 3 && UpSizeRank > 2)
                        {
                            nodeFence.DownGameObject = nodeFence.BuildFence(
                                GridManager.Instance.FenceTwo,
                                DownLocation,
                                0
                            );

                        }
                        else
                        {
                            nodeFence.DownGameObject = DownGameObject;
                        }
                        nodeFence.DownGameObject.transform.parent = nodeFence.transform;
                        Destroy(UpGameObject);
                        UpGameObject = null;
                        Up = false;
                        UpSizeRank = 0;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("side", side, null);
                }


            }
        }
    }
