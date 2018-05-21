using Cultivations;
using Grid;
using UnityEngine;

namespace Node
{
	public class NodeBehaviour : MonoBehaviour
	{
		[HideInInspector]
		public HighLight HighLight;

		private const float YBuildingOffset = 0.83f;

		private bool _isSelected;

		public Vector2Int GridLocation;

		private SpriteRenderer _spriteRenderer;


		private int _listIndex = -1;
		private bool _emptyCultivationField;
		

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

		public int GetListIndex()
		{
			return _listIndex;
		}

	

		public void SetSprite(Sprite sprite)
		{
			_spriteRenderer.sprite = sprite;
		}


		private void OnMouseDown()
		{
			Debug.Log("piininggg"	);
			GridManager.Instance.SetSelectedNode(this);
	
		}
	}
}
