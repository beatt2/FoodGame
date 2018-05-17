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
		

	
		

		private void Start()
		{
			HighLight = GetComponent<HighLight>();
			GridManager.Instance.AddNode(this);
			
		}

		public bool IsSelected()
		{
			return HighLight.IsSelected();
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
