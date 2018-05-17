using Events;
using UnityEngine;


namespace UI
{
    public class OkayButton : MonoBehaviour
    {

        public GameObject Ui;

        //public override void TaskOnClick()
        //{
        //    EventManager.Instance.InEventMenu = false;
        //    Ui.SetActive(false);
        //}
        public void CloseFinance()
        {
            EventManager.Instance.InEventMenu = false;
            Ui.SetActive(false);
        }

    }
}
