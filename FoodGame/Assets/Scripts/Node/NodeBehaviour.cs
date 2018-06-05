using Cultivations;
using Grid;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Node
{
	public class NodeBehaviour : MonoBehaviour , IPointerDownHandler
	{
		[HideInInspector]
		public HighLight HighLight;

		private const float YBuildingOffset = 0.83f;

		private bool _isSelected;

		public Vector2Int GridLocation;

		private SpriteRenderer _spriteRenderer;


		private int _listIndex = -1;
		private bool _emptyCultivationField;

		private NodeState _nodeState;
		private NodeFence _nodeFence;
		

		public Vector3 BuildLocation
		{
			get
			{
				return new Vector3(transform.position.x, transform.position.y + YBuildingOffset, 0);
			}
		}

		private void Awake()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_nodeState = GetComponent<NodeState>();
			_nodeFence = GetComponent<NodeFence>();
		}

		public NodeFence GetNodeFence()
		{
			return _nodeFence;
		}


		public bool GetCultivationField()
		{
			return _emptyCultivationField;
		}

		public void SetCultivationField(bool value)
		{
			_emptyCultivationField = value;
		}
		public void OnPointerDown(PointerEventData eventData)
		{
			GridManager.Instance.SetSelectedNode(this);
		}

		public void SetCultivationListIndex(int index)
		{
			_listIndex = index;
		}

		public bool IsFarmField()
		{
			return _emptyCultivationField;
		}

		public void SetEmptyCultivationField(bool value)
		{
			_emptyCultivationField = value;
			if (value)
			{
				HighLight.SetToActiveColor();
			}
		}

		public bool IsFarm()
		{
			return gameObject.GetComponent<BuildingPrefab>() != null;
		}
		

		private void Start()
		{
			HighLight = GetComponent<HighLight>();
			GridManager.Instance.AddNode(this);
			
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
			return _listIndex;
		}

		public void RemoveCultivationTile()
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

		public void ResetNode()
		{
			_spriteRenderer.sprite = GridManager.Instance.BuildingPlacement.GetOriginalSprite();
			GetComponent<NodeState>().ResetSate();
			HighLight.ResetActiveColor();
			HighLight.ChangeColorToOld();
			_listIndex = -1;
			RemoveCultivationTile();
			
			
		}


		private void OnMouseDown()
		{
	
		}
	}
}
