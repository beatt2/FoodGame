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
		

		public void ChangeColor()
		{
			_spriteRenderer.color = _spriteRenderer.color == Color.blue ? _startingColor : Color.blue;
		}

	}
}
