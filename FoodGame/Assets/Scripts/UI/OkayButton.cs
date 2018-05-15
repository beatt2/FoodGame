using Events;
using UnityEngine;


namespace UI
{
    public class OkayButton : UIButtonAbstract
    {

        public GameObject ui;
        
        public override void OnButtonClick()
        {
            EventManager.Instance.inEventMenu = false;
            ui.SetActive(false);
        }
    }
}
