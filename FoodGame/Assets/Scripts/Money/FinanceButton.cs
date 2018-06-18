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
        private bool _opened = false;
        public void CloseFinanceMenu()
        {
            //EventManager.Instance.InEventMenu = false;
            FinanceUi.SetActive(false);
            TimeManager.Instance.InFinanceMenu = false;
            FinanceScript.RemoveText();
            _opened = false;
        }

        public void OpenFinanceMenu()
        {
            
            FinanceUi.SetActive(true);
           // EventManager.Instance.InEventMenu = true;
            TimeManager.Instance.InFinanceMenu = true;
            
            if (!_opened)
            {
                FinanceScript.CheckForText();
                _opened = true;
            }
            
            

        }
    }
}
