using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Events
{
    public class Messages : MonoBehaviour
    {

        public GameObject HeadlineUiPrefab;
        public GameObject Content;
        private RectTransform _currentpos;
        public RectTransform StartingPos;

        private readonly List<GameObject> _go = new List<GameObject>();
        private readonly List<MessageEntry> _messageText = new List<MessageEntry>();


        public float Gap;

        private Button _button;

        public GameObject PopupUi;
        // Use this for initialization
        void Start ()
        {
            _currentpos = StartingPos;

            Add("testheadline","Test content","Test effect", 1);

        }

        public void Add(string headline, string content, string effect, int index)
        {
            Debug.Log(headline);
            _messageText.Add(new MessageEntry(headline,content, effect));

            


            GameObject go = Instantiate(HeadlineUiPrefab, _currentpos.position, Quaternion.identity, Content.transform) as GameObject;
            _go.Add(go);
            _currentpos.position = new Vector3(_currentpos.position.x, _currentpos.position.y + Gap, _currentpos.position.z);

            ChangeText(go, headline,content,effect);
        }

        public void ChangeText(GameObject go,string headline, string content, string effect)
        {
            Message message = go.GetComponent<Message>();
            message.Headline.text = headline;
            message.Content.text = content;
            message.Effect.text = effect;
            _button = go.GetComponent<Button>();
            _button.onClick.AddListener(OnButtonClick);
        }

        void OnButtonClick()
        {
            PopupUi.SetActive(true);
        }
	
    }
}
