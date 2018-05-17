using Events;
using UI;
using UnityEngine;

namespace Money
{
    public class FinanceButton : MonoBehaviour
    {
        public GameObject FinanceUi;
        public void CloseFinanceMenu()
        {
            EventManager.Instance.InEventMenu = false;
            FinanceUi.SetActive(false);
        }

        public void OpenFinanceMenu()
        {
            EventManager.Instance.InEventMenu = true;
            FinanceUi.SetActive(true);
        }
    }
}
