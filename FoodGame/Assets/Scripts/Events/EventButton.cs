using UnityEngine;

namespace Events
{
    public class EventButton : MonoBehaviour
    {
        public GameObject EventsUi;

        public void CloseEvent()
        {
            EventManager.Instance.InEventMenu = false;
            EventsUi.SetActive(false);
        }
    }
}
