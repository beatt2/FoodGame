using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class FarmButtons : MonoBehaviour
	{

		public int IndexNumber;


		[HideInInspector]
		public Button MyButton;

		private void Awake()
		{
			MyButton = GetComponent<Button>();

		}

		public void SetInteractable(bool value)
		{
			MyButton.interactable = value;
		}
	}
}
