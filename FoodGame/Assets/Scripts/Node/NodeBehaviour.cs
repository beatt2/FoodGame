using Grid;
using UnityEngine;

namespace Node
{
	public class NodeBehaviour : MonoBehaviour
	{
		[HideInInspector]
		public HighLight HighLight;

		private bool _selected;


		
		
		private void Awake()
		{

		}

		private void Start()
		{
			HighLight = GetComponent<HighLight>();
			GridManager.Instance.AddNode(this);
		}

		public bool IsSelected()
		{
			return _selected;
		}

		private void OnMouseDown()
		{
			Debug.Log(GetComponent<SpriteRenderer>().sortingOrder);
			GridManager.Instance.SetSelectedNode(this);
	
		}




		private void Update () 
		{
		
		}
	}
}
