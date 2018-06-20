using UnityEngine;

namespace Events
{
    public class EventButton : MonoBehaviour
    {
        public void CloseEvent()
        {

            EventManager.Instance.Ui.SetActive(false);
        }

        public void OpenEventInfo()
        {
            EventManager.Instance.Ui.SetActive(true);
        }
    }
}
