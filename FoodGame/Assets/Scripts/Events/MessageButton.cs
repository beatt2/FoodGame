using UnityEngine;

namespace Events
{
    public class MessageButton : MonoBehaviour
    {
        public GameObject MessageUi;
        public GameObject PopupUi;
        private int _index;


        public void OpenMessages()
        {
            MessageUi.SetActive(true);
            EventManager.Instance.InMenu = true;
        }

        public void CloseMessages()
        {
            MessageUi.SetActive(false);
            EventManager.Instance.InMenu = false;
        }
        public void OpenPopup()
        {
            PopupUi.SetActive(true);

        }

        public void ClosePopup()
        {
            PopupUi.SetActive(false);
        }
    }
}
