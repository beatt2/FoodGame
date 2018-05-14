﻿using Grid;
using UnityEngine;

namespace Node
{
	public class NodeBehaviour : MonoBehaviour
	{
		[HideInInspector]
		public HighLight HighLight;

		private const float YBuildingOffset = 0.83f;

		public Vector2Int GridLocation;

		public Vector3 BuildLocation
		{
			get
			{
				return new Vector3(transform.position.x, transform.position.y + YBuildingOffset, 0);
			}
		}


		private void Start()
		{
			HighLight = GetComponent<HighLight>();
			GridManager.Instance.AddNode(this);
		}


		private void OnMouseDown()
		{
	
			GridManager.Instance.SetSelectedNode(this);
	
		}
	}
}
