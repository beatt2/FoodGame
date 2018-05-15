using Events;
using UnityEngine;


namespace UI
{
    public class OkayButton : UIButtonAbstract
    {

        public GameObject Ui;
        
        public override void OnButtonClick()
        {
            EventManager.Instance.InEventMenu = false;
            Ui.SetActive(false);
        }
    }
}
