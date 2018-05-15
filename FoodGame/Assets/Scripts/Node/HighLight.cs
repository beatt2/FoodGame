using UnityEngine;

namespace Node
{
	public class HighLight : MonoBehaviour
	{
		private Color _startingColor;
		private Color _blue = Color.blue;
		private SpriteRenderer _spriteRenderer;

		private void Awake()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_startingColor = _spriteRenderer.color;
		}
		

		public void ChangeColorBlue()
		{
			_spriteRenderer.color = Color.blue;
		}

		public void ChangeColorGreen()
		{
			_spriteRenderer.color = Color.green;
		}

		public void ChangeColorToOld()
		{
			_spriteRenderer.color = _startingColor;
		}


	}
}
