using System;
using Money;
using Save;
using TimeSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
	public class TopBar : MonoBehaviour
	{

		public Text[] MyMoney;

		public Text[] MyMonth;
		public Text[] MyYear;

		private int _currentMoneyLength = 0;



		// Update is called once per frame
		private void Update ()
		{
			CalculateUiMoney();
			CalculateUITime();

		}

		private void CalculateUITime()
		{

			string currentYear = TimeManager.Instance.GetYear().ToString();
			MyYear[3].text = currentYear[0].ToString();
			MyYear[2].text = currentYear[1].ToString();
			MyYear[1].text = currentYear[2].ToString();
			MyYear[0].text = currentYear[3].ToString();

			string currentMonth = TimeManager.Instance.GetMonth().ToString();
			if (currentMonth.Length < 2)
			{
				MyMonth[0].text = currentMonth;
				MyMonth[1].text = "0";
			}
			else
			{
				MyMonth[0].text = currentMonth[1].ToString();
				MyMonth[1].text = currentMonth[0].ToString();
			}
		}

		private void CalculateUiMoney()
		{
			int current = (int) SimpleMoneyManager.Instance.GetCurrentMoney();
			string currentMoney = current.ToString();
			int j = 0;

			if (current < 0)
			{
				SaveManager.Instance.Reset();
				SceneManager.LoadScene("MainMenu");
			}

			if (current > _currentMoneyLength)
			{
				current -= 100;
			}

			if (currentMoney.Length < _currentMoneyLength)
			{
				foreach (var t in MyMoney)
				{
					t.text = "0";
				}
			}

			for (int i = currentMoney.Length - 1;  i > -1 ; i -- , j++)
			{
				MyMoney[i].text = currentMoney[j].ToString();
			}
			_currentMoneyLength = currentMoney.Length;


		}
	}
}
