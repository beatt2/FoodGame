using Events;
using UnityEngine;


namespace UI
{
    public class OkayButton : MonoBehaviour
    {

        public GameObject Ui;


        public void CloseFinance()
        {
           // EventManager.Instance.InEventMenu = false;
            Ui.SetActive(false);
        }

    }
}
