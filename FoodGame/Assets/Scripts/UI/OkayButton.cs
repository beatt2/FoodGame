using Events;
using UnityEngine;


namespace UI
{
    public class OkayButton : UIButtonAbstract
    {

        public GameObject Ui;
        
        public override void TaskOnClick()
        {
            EventManager.Instance.InEventMenu = false;
            Ui.SetActive(false);
        }
    }
}
