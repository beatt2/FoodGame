using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using MathExt;
using Node;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Grid
{
    public class GridManager : Singleton<GridManager>
    {
        private List<NodeBehaviour> _nodeBehaviours = new List<NodeBehaviour>();

        private NodeBehaviour[,] _nodeBehavioursGrid;
        private int _gSizeX;
        private int _gSizeY;

        public GridMaker MyGridMaker;

        private NodeBehaviour _selectedNode;

        public BuildButton MyBuildButton;

        public GameObject BuildObject;

        private bool sort = false;

        private void Start()
        {
            _nodeBehavioursGrid = MyGridMaker.NodeBehaviours;
            Debug.Log(MyGridMaker.Size);
            _gSizeX = _nodeBehavioursGrid.GetLength(0);
            _gSizeY = _nodeBehavioursGrid.GetLength(1);
        }


        public void AddNode(NodeBehaviour node)
        {
            _nodeBehaviours.Add(node);
        }

        public NodeBehaviour GetSelectedNode()
        {
            return _selectedNode != null ? _selectedNode : null;
        }

        public void BuildButtonPressed()
        {
            Instantiate(BuildObject,_selectedNode.BuildLocation, Quaternion.identity,_selectedNode.transform);
            NeighBourCheck(1);

      
        }

        public void NeighBourCheck(int SquareSize)
        {
            bool xSmall = false;
            bool xBig = false;
            bool ySmall = false;
            bool yBig = false;
            if (_selectedNode.GridLocation.x - SquareSize > 0)
            {
                xSmall = true;             
            }
            if (_selectedNode.GridLocation.x + SquareSize < _gSizeX)
            {
                xBig = true;
            }
            if (_selectedNode.GridLocation.y - SquareSize > 0)
            {
                ySmall = true;
            }
            if (_selectedNode.GridLocation.y + SquareSize < _gSizeY)
            {
                yBig = true;
            }

            if (!xSmall && !xBig)
            {
                Debug.Log("No X location found");
            }

            if (!ySmall && !yBig)
            {
                Debug.Log("No Y location found");
            }

            _nodeBehavioursGrid[_selectedNode.GridLocation.x, _selectedNode.GridLocation.y - 1].HighLight
                .ChangeColorGreen();
                
            _nodeBehavioursGrid[_selectedNode.GridLocation.x + 1, _selectedNode.GridLocation.y - 1].HighLight
                .ChangeColorGreen();
            _nodeBehavioursGrid[_selectedNode.GridLocation.x + 1, _selectedNode.GridLocation.y].HighLight
                .ChangeColorGreen();
            
           // if (xBig && ySmall)
//            {
//
//            }
//            else if (xSmall && ySmall)
//            {
//                _nodeBehavioursGrid[_selectedNode.GridLocation.x, _selectedNode.GridLocation.y - 1].HighLight
//                    .ChangeColorGreen();
//                
//                _nodeBehavioursGrid[_selectedNode.GridLocation.x - 1, _selectedNode.GridLocation.y - 1].HighLight
//                    .ChangeColorGreen();
//                _nodeBehavioursGrid[_selectedNode.GridLocation.x - 1, _selectedNode.GridLocation.y].HighLight
//                    .ChangeColorGreen();
//            }
//            else if (xBig && yBig)
//            {
//                _nodeBehavioursGrid[_selectedNode.GridLocation.x, _selectedNode.GridLocation.y + 1].HighLight
//                    .ChangeColorGreen();
//                
//                _nodeBehavioursGrid[_selectedNode.GridLocation.x + 1, _selectedNode.GridLocation.y + 1].HighLight
//                    .ChangeColorGreen();
//                
//                _nodeBehavioursGrid[_selectedNode.GridLocation.x + 1, _selectedNode.GridLocation.y].HighLight
//                    .ChangeColorGreen();
//            }
//            else if(xBig)
          
            
            
            
        }
       

        public void SetSelectedNode(NodeBehaviour nodeBehaviour)
        {
            if (_selectedNode == null)
            {
                _selectedNode = nodeBehaviour;
              
                _selectedNode.HighLight.ChangeColorBlue();
            }
            else
            {
                _selectedNode.HighLight.ChangeColorBlue();
                _selectedNode = nodeBehaviour;
                _selectedNode.HighLight .ChangeColorBlue();
            }
            MyBuildButton.SetButtonInteractable(true);
        }
        public void SetNodeToNull()
        {
            if (_selectedNode == null) return;
            _selectedNode.HighLight.ChangeColorBlue();
            _selectedNode = null;
            MyBuildButton.SetButtonInteractable(false);


        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                SetNodeToNull();
            }


        }

        
    }
}


