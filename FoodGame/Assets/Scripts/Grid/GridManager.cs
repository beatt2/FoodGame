﻿using System.Collections.Generic;
using Node;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Grid
{
    public class GridManager : Singleton<GridManager>
    {
        private readonly List<GridList> _nodeList = new List<GridList>();
        private List<SingleGridList>[] _singleGridLists = new List<SingleGridList> [5];
        private NodeBehaviour[,] _nodeBehavioursGrid;
        private int _gridSizeX;
        private int _gridSizeY;
        public GridMaker MyGridMaker;
        private NodeBehaviour _selectedNode;
        public BuildButton MyBuildButton;
        public GameObject BuildObject;
        private bool sort = false;
        private int _selectionSize = 1;
        private int _totalEntries;
        private bool _inSelectionState = false;
        

        protected  override void Awake()
        {
            base.Awake();
            _gridSizeX = MyGridMaker.Size.x;
            _gridSizeY = MyGridMaker.Size.y;
            _totalEntries = _gridSizeX * _gridSizeY;
               
        }

        
        private void ConvertListToArray()
        {
 
            _nodeList.Sort();
            _singleGridLists = new List<SingleGridList>[_gridSizeX];
            _nodeBehavioursGrid = new NodeBehaviour[_gridSizeX, _gridSizeY];

            for (int x = 0; x < _gridSizeX; x++)
            {
                _singleGridLists[x] = new List<SingleGridList>();
                for (int y = 0; y < _gridSizeY; y++)
                {
                    _singleGridLists[x].Add(new SingleGridList(_nodeList[x * _gridSizeY + y].Node,_nodeList[x * _gridSizeY + y].GridLocations ));
                }
                _singleGridLists[x].Sort();
                for (int y = 0; y < _gridSizeY; y++)
                {
                    _nodeBehavioursGrid[x, y] = _singleGridLists[x][y].Node;          
                }
            }
            
        }


        public void AddNode(NodeBehaviour node)
        {
            _nodeList.Add(new GridList(node, node.GridLocation));
            if (_totalEntries == _nodeList.Count)
            {
                ConvertListToArray();
            }
        }

        public void SetSelectionSize()
        {
 
            _selectionSize = _selectionSize == 1 ? 4 : 1;
            if (_selectedNode == null) return;
            ChangeColorsToOld();
            ChangeColorsToBlue();
        }

        public void ConfirmBuilding(bool value)
        {
            _inSelectionState = value;
            Debug.Log(_inSelectionState ? "Selection state is true" : "Selection state is false");
            if (!_inSelectionState)
            {
                ChangeColorsToOld();
            }
            else if (_selectedNode == null)
            {
                Debug.Log("No node selected");
            }
        }
        

        public NodeBehaviour GetSelectedNode()
        {
            return _selectedNode != null ? _selectedNode : null;
        }

        public void BuildButtonPressed()
        {
            Instantiate(BuildObject, _selectedNode.BuildLocation, Quaternion.identity, _selectedNode.transform);
        }

        public void SetSelectedNode(NodeBehaviour nodeBehaviour)
        {
            if (_selectedNode == null)
            {
                if (_inSelectionState)
                {
                    if (!nodeBehaviour.IsSelected())
                    {
                        Debug.Log("This tile is currently not selected");
                        return;
                    }
                    ChangeColorsToGreen();
                }
                else
                {
                    _selectedNode = nodeBehaviour;
                    ChangeColorsToBlue();
                }
            }
            else
            {
                if (_inSelectionState && nodeBehaviour.IsSelected())
                {
                    ChangeColorsToBlue();
                }
                else if (_inSelectionState && !nodeBehaviour.IsSelected())
                {
                    Debug.Log("no blue or green tile clicked");
                    return;
                }
                else
                {
                    ChangeColorsToOld();
                }
             
                _selectedNode = nodeBehaviour;
                
                if (_inSelectionState)
                {
                    ChangeColorsToGreen();
                }
                else
                {
                    ChangeColorsToBlue();
                }            
            }
            //TODO REMOVE
            //MyBuildButton.SetButtonInteractable(true);

        }
        public void SetNodeToNull()
        {
            if (_selectedNode == null) return;
            ChangeColorsToOld();
            _selectedNode = null;
            //TODO REMOVE
            //MyBuildButton.SetButtonInteractable(false);

        }


        private void ChangeColorsToBlue()
        {
            _selectedNode.HighLight.ChangeColorBlue();
            if (_selectionSize != 4 || _inSelectionState) return;
            if (CheckGridForNull(0, -1))
            {
                _nodeBehavioursGrid[_selectedNode.GridLocation.x, _selectedNode.GridLocation.y - 1].HighLight.ChangeColorBlue();
            }
            if (CheckGridForNull(1, -1))
            {
                _nodeBehavioursGrid[_selectedNode.GridLocation.x + 1, _selectedNode.GridLocation.y - 1].HighLight.ChangeColorBlue();
            }
            if (CheckGridForNull(1, 0))
            {
                _nodeBehavioursGrid[_selectedNode.GridLocation.x + 1, _selectedNode.GridLocation.y ].HighLight.ChangeColorBlue();
            }

        }

        private void ChangeColorsToOld()
        {
            _selectedNode.HighLight.ChangeColorToOld();
            if (_selectionSize != 4) return;
            if (CheckGridForNull(0, -1))
            {
                _nodeBehavioursGrid[_selectedNode.GridLocation.x, _selectedNode.GridLocation.y - 1].HighLight.ChangeColorToOld();
            }
            if (CheckGridForNull(1, -1))
            {
                _nodeBehavioursGrid[_selectedNode.GridLocation.x + 1, _selectedNode.GridLocation.y - 1].HighLight.ChangeColorToOld();
            }
            if (CheckGridForNull(1, 0))
            {
                _nodeBehavioursGrid[_selectedNode.GridLocation.x + 1, _selectedNode.GridLocation.y ].HighLight.ChangeColorToOld();
            }
     
        }

        public bool CheckGridForNull(int  xValue, int yValue)
        {
            //return _nodeBehavioursGrid[_selectedNode.GridLocation.x + xValue, _selectedNode.GridLocation.y + yValue] != null;

            if (_nodeBehavioursGrid.GetLength(0) <= _selectedNode.GridLocation.x + xValue)
            {
                return false;
            }
            if (_nodeBehavioursGrid.GetLength(1) <= _selectedNode.GridLocation.y + yValue)
            {
                return false;
            }
            if (_selectedNode.GridLocation.x + xValue < 0)
            {
                return false;
            }
            
            return _selectedNode.GridLocation.y + yValue >= 0;
        }

        private void ChangeColorsToGreen()
        {
            _selectedNode.HighLight.ChangeColorGreen();
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


