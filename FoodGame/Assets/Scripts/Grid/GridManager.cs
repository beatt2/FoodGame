using System.Collections.Generic;
using System.Linq;
using Cultivations;
using Node;
using Save;
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
        private bool sort = false;
        private int _selectionSize = 1;
        private int _totalEntries;
        private bool _inSelectionState = false;

        private Selection _selection;
        
        public GameObject FenceOne;
        public GameObject FenceTwo;

        [SerializeField] public BuildingPlacement BuildingPlacement;


        private  List<List<NodeBehaviour>> _cultivationLocationList = new List<List<NodeBehaviour>>();


        protected override void Awake()
        {
            Screen.SetResolution(1920,1080,true);
            base.Awake();
            _selection = GetComponent<Selection>();
            _gridSizeX = MyGridMaker.Size.x;
            _gridSizeY = MyGridMaker.Size.y;
            _totalEntries = _gridSizeX * _gridSizeY;
        }

      

        public NodeBehaviour[,] GetNodeGrid()
        {
            return _nodeBehavioursGrid;
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
                    _singleGridLists[x].Add(new SingleGridList(_nodeList[x * _gridSizeY + y].Node,
                        _nodeList[x * _gridSizeY + y].GridLocations));
                }
                _singleGridLists[x].Sort();
                for (int y = 0; y < _gridSizeY; y++)
                {
                    _nodeBehavioursGrid[x, y] = _singleGridLists[x][y].Node;
                }
            }
            
            //LoadNodes();
        }
        
        private void LoadNodes()
        {
            _nodeBehavioursGrid = SaveManager.Instance.LoadNodes(_nodeBehavioursGrid);
        }

        public List<List<NodeBehaviour>> GetCultivationLocationList()
        {
            return _cultivationLocationList;
        }

        public void SetCultivationList(List<List<NodeBehaviour>> nodes)
        {
            _cultivationLocationList = nodes;
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
            if(_selectionSize == 4)
            ChangeColorsToBlue();
      
        }

        public bool ConfirmLocation(bool value)
        {
            if (_nodeBehavioursGrid.Cast<NodeBehaviour>().Any(node => node.HighLight.IsRed()))
            {
                Debug.Log("Cant build in the red");
                return false;
            }    
            _inSelectionState = value;
            Debug.Log(_inSelectionState ? "Selection state is true" : "Selection state is false");
            if (!_inSelectionState)
            {
                SetTilesToAlpha(false);
                _selectionSize = 1;
                ChangeColorsToOld();  
            }
            else if (_selectedNode != null) 
            {
                SetTilesToAlpha(true);
                _selectedNode.HighLight.ChangeColorGreen();
            }
            if (_selectedNode == null)
            {
                Debug.Log("No node selected");
            }

            return true;
        }

        public NodeBehaviour GetSelectedNode()
        {
            return _selectedNode != null ? _selectedNode : null;
        }

        public NodeBehaviour GetNode(int x, int y)
        {
            if (x < GridSizeX() && x > 0 && y <GridSizeY() && y > 0)
            {
                return _nodeBehavioursGrid[x, y];
            }
            return null;

        }

        public int GridSizeY()
        {
            return _nodeBehavioursGrid.GetLength(1);
        }

        public int GridSizeX()
        {
            return _nodeBehavioursGrid.GetLength(0);
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

            if (!_inSelectionState)
            {
                _selection.SetYesNoLocation(Camera.main.WorldToScreenPoint(_selectedNode.transform.position));
            }
            else
            {
                _selection.SetYesNoBuildLocation(Camera.main.WorldToScreenPoint(_selectedNode.transform.position));
            }

            if (_selectedNode.GetComponent<BuildingPrefab>() != null)
            {
                _selection.SetSidePanel(_selectedNode.GetComponent<BuildingPrefab>().MyBuilding);
            }
            else if (_selectedNode.GetComponent<PlantPrefab>() != null)
            {
                _selection.SetSidePanel(_selectedNode.GetComponent<PlantPrefab>().MyPlant);
            }
            else if (_selection.SidePannelActive())
            {
                _selection.ToggleSidePanel();    
            }
            if (_selectionSize != 4 || _inSelectionState) return;
            if (!_selection.YesNoActive())
            {
                _selection.ToggleYesNo(true);
            }
   
        }

        public void SetNodeToNull()
        {
            if (_selectedNode == null || _selectionSize == 4) return;
            ChangeColorsToOld();
            _selectedNode = null;

        }

        public void SetTilesToAlpha(bool value)
        {
            foreach (var node in _nodeBehavioursGrid)
            {
                if (!node.IsSelected())
                {
                    node.HighLight.SetAlpha(value);
                }
            }
        }


        private void ChangeColorsToBlue()
        {
            if (CheckGridForBuildSpace(0, 0))
            {
                _selectedNode.HighLight.ChangeColorBlue();
                _nodeBehavioursGrid[_selectedNode.GridLocation.x, _selectedNode.GridLocation.y].GetNodeFence().SetLocation(0);
            }
            else
            {
                _selectedNode.HighLight.ChangeColorRed();
            }

            if (_selectionSize != 4 || _inSelectionState) return;
            if (CheckGridForNull(0, -1))
            {
                if (CheckGridForBuildSpace(0, -1))
                {
                    _nodeBehavioursGrid[_selectedNode.GridLocation.x, _selectedNode.GridLocation.y - 1].HighLight.ChangeColorBlue();
                    _nodeBehavioursGrid[_selectedNode.GridLocation.x, _selectedNode.GridLocation.y - 1].GetNodeFence().SetLocation(3);
                }
                else
                {
                    _nodeBehavioursGrid[_selectedNode.GridLocation.x, _selectedNode.GridLocation.y - 1].HighLight.ChangeColorRed();
                }
            }

            if (CheckGridForNull(1, -1))
            {
                if (CheckGridForBuildSpace(1, -1))
                {
                    _nodeBehavioursGrid[_selectedNode.GridLocation.x + 1, _selectedNode.GridLocation.y - 1].HighLight.ChangeColorBlue();
                    _nodeBehavioursGrid[_selectedNode.GridLocation.x + 1, _selectedNode.GridLocation.y - 1].GetNodeFence().SetLocation(2);
                }
                else
                {
                    _nodeBehavioursGrid[_selectedNode.GridLocation.x + 1, _selectedNode.GridLocation.y - 1].HighLight.ChangeColorRed();
          
                }
            }

            if (CheckGridForNull(1, 0))
            {
                if (CheckGridForBuildSpace(1, 0))
                {
                    _nodeBehavioursGrid[_selectedNode.GridLocation.x + 1, _selectedNode.GridLocation.y].HighLight.ChangeColorBlue();
                    _nodeBehavioursGrid[_selectedNode.GridLocation.x + 1, _selectedNode.GridLocation.y].GetNodeFence().SetLocation(1);
                }
                else
                {
                    _nodeBehavioursGrid[_selectedNode.GridLocation.x + 1, _selectedNode.GridLocation.y].HighLight.ChangeColorRed();
                }
            }
        }

        private void ChangeColorsToOld()
        {
            _selectedNode.HighLight.ChangeColorToOld();


            //if (_selectionSize != 4) return;
            foreach (var node in _nodeBehavioursGrid)
            {
                if (node.IsSelected())
                {
                    node.HighLight.ChangeColorToOld();
                    node.GetNodeFence().SetLocation(-1);
                }
            }
        }

        private bool CheckGridForNull(int xValue, int yValue)
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

        private bool CheckGridForBuildSpace(int xValue, int yValue)
        {
            return _nodeBehavioursGrid[_selectedNode.GridLocation.x + xValue, _selectedNode.GridLocation.y + yValue].GetListIndex() == -1;
        }

        private void ChangeColorsToGreen()
        {
            _selectedNode.HighLight.ChangeColorGreen();
            //_selectionButton.SetActiveConfirmButton();
        }

        public void ConfirmBuildFarmButtonPressed(int target)
        {
            if (!BuildingPlacement.BuildFarm(target)) return;
      
            SetTilesToAlpha(false);
            int listCount = _cultivationLocationList.Count;
            _selectedNode.GetComponent<NodeFence>().ConfirmBuild();
            _cultivationLocationList.Add(new List<NodeBehaviour>());
            _cultivationLocationList[listCount].Add(_selectedNode);
            _cultivationLocationList[listCount][0].SetCultivationListIndex(listCount);
            SaveManager.Instance.SetHighestCultivationIndex(listCount);
            _selectedNode.HighLight.ChangeColorToOld();
            var tempNodeState = _selectedNode.GetComponent<NodeState>();
            
            foreach (var node in _nodeBehavioursGrid)
            {
                if (!node.HighLight.IsBlue()) continue;
                node.GetComponent<NodeFence>().ConfirmBuild();
                //TODO ARE THESE STILL Necessary??
                node.SetCultivationListIndex(listCount);
                node.SetEmptyCultivationField(true);
                node.GetComponent<NodeState>().ChangeValues(NodeState.CurrentStateEnum.EmptyField, tempNodeState.FieldType);
                BuildingPlacement.SetEmptyField(node.gameObject);
                _cultivationLocationList[listCount].Add(node);

            }

            _selectionSize = 1;
            _inSelectionState = false;

        }

        public void CancelBuildState()
        {
            SetTilesToAlpha(false);
            _inSelectionState = false;
            ChangeColorsToBlue();
        }
        
        
        

        private void Update()
        {
#if UNITY_EDITOR
//            if (!Input.GetMouseButtonDown(0)) return; 
//            if (!EventSystem.current.IsPointerOverGameObject())
//            {
//                SetNodeToNull();
//            }
#endif
//#if UNITY_ANDROID && !UNITY_EDITOR
//
//            if(Input.GetTouch(0).fingerId)
//            if (!EventSystem.current.IsPointerOverGameObject())
//            {
//                SetNodeToNull();
//            }
//#endif
        }
    }
}