using Cultivations;
using Grid;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Node
{
    public class NodeBehaviour : MonoBehaviour, IPointerDownHandler//, IPointerEnterHandler
    {
        [HideInInspector] public HighLight HighLight;

        private bool _isSelected;

        public Vector2Int GridLocation;


        private SpriteRenderer _spriteRenderer;


        public int ListIndex = -1;
        private bool _emptyCultivationField;

        private NodeState _nodeState;
        private NodeFence _nodeFence;


        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _nodeState = GetComponent<NodeState>();
            _nodeFence = GetComponent<NodeFence>();
        }


        private void Start()
        {
            HighLight = GetComponent<HighLight>();
            GridManager.Instance.AddNode(this);
        }

        public NodeFence GetNodeFence()
        {
            return _nodeFence;
        }

        public int GetLayer()
        {
            return _spriteRenderer.sortingOrder;
        }


        public bool GetCultivationField()
        {
            return _emptyCultivationField;
        }



//        public void OnPointerEnter(PointerEventData eventData)
//        {
//        #if !UNITY_EDITOR
//			GridManager.Instance.SetSelectedNode(this);
//		#endif
//        }


        public void OnPointerDown(PointerEventData eventData)
        {
            GridManager.Instance.SetSelectedNode(this);
        }

        public void SetCultivationListIndex(int index)
        {
            ListIndex = index;
        }


        public void SetEmptyCultivationField(bool value)
        {
            _emptyCultivationField = value;
            if (value)
            {
                HighLight.ChangeColorToOld();
            }
        }



        public bool IsSelected()
        {
            return HighLight.IsSelected();
        }

        public NodeState.CurrentStateEnum GetCurrentState()
        {
            return _nodeState.CurrentState;
        }

        public NodeState.FieldTypeEnum GetFieldType()
        {
            return _nodeState.FieldType;
        }


        public int GetListIndex()
        {
            return ListIndex;
        }

        private void RemoveCultivationTile()
        {
            if (GetComponent<CultivationPrefab>() != null)
            {
                Destroy(GetComponent<CultivationPrefab>());
            }
        }


        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        public void ResetNode(bool isPlant, bool resetListIndex)
        {
            _spriteRenderer.sprite = GridManager.Instance.BuildingPlacement.GetOriginalSprite();
            if (!isPlant)
            {
                GetComponent<NodeState>().ResetSate();
            }
            else
            {
                GetComponent<NodeState>().CurrentState = NodeState.CurrentStateEnum.EmptyField;
                
            }

            HighLight.ResetActiveColor();
            HighLight.ChangeColorToOld();
            if (resetListIndex)
            {
                ListIndex = -1;

            }
            if(!isPlant)
            RemoveCultivationTile();
        }
    }
}
