using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
