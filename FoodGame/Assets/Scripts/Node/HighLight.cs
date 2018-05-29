using UnityEngine;

namespace Node
{
	public class HighLight : MonoBehaviour
	{
		private Color _startingColor;
		private Color _originalColor;
		private Color _blue = Color.blue;
		private Color _activeColor = new Color(45,161,0,255);
		private SpriteRenderer _spriteRenderer;

		private void Awake()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_startingColor = _spriteRenderer.color;
			_originalColor = _startingColor;
		}

		//TODO  turn this into some selection state
		public bool IsSelected()
		{
			return _spriteRenderer.color == Color.blue || _spriteRenderer.color == Color.green || _spriteRenderer.color == Color.red;

		}

		public void SetAlpha(bool alpha)
		{
			// ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
			if (alpha)
			{
				_spriteRenderer.color = new Color(_spriteRenderer.color.r,_spriteRenderer.color.g,_spriteRenderer.color.b, 0.5f);
			}
			else
			{
				_spriteRenderer.color = new Color(_spriteRenderer.color.r,_spriteRenderer.color.g,_spriteRenderer.color.b, 1);
			}
			
		}

		public void ChangeColorBlue()
		{
			_spriteRenderer.color = Color.blue;
		}

		public void ChangeColorGreen()
		{
			_spriteRenderer.color = Color.green;
		}

		public void ChangeColorRed()
		{
			_spriteRenderer.color = Color.red;
		}

		public void SetToActiveColor()
		{
			_spriteRenderer.color = _activeColor;
			_startingColor = _activeColor;
		}

		public void ResetActiveColor()
		{
			_startingColor = _originalColor;
		}

		public bool IsBlue()
		{
			return _spriteRenderer.color == Color.blue;
		}

		public bool IsRed()
		{
			return _spriteRenderer.color == Color.red;
		}

	

		public void ChangeColorToOld()
		{
			
			_spriteRenderer.color = _startingColor;
			
		}


	}
}
