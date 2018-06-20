using System.Collections;
using Events;
using TimeSystem;
using UI;
using UnityEngine;

namespace Money
{
    public class FinanceButton : MonoBehaviour
    {
        public GameObject FinanceUi;
        public Finance FinanceScript;
        private bool _opened;


        public void CloseFinanceMenu()
        {
            FinanceUi.SetActive(false);
            TimeManager.Instance.InFinanceMenu = false;
            FinanceScript.RemoveText();
            _opened = false;
        }

        public void OpenFinanceMenu()
        {
            FinanceUi.SetActive(true);
            TimeManager.Instance.InFinanceMenu = true;

            if (_opened) return;
            FinanceScript.CheckForText();
            _opened = true;

        }
    }
}
