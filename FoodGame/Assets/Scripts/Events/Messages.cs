using System.Collections.Generic;
using Node;
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
        private readonly List<Events> _eventsInInbox = new List<Events>();

        public float Gap;

        private Button _button;

        public GameObject PopupUi;

        public Popup PopupScript;

        private string _name;
        // Use this for initialization
        void Start ()
        {
            _currentpos = StartingPos;

            //Add("testheadline","Test content","Test effect", 1);

        }
        private string GetName(NodeState.FieldTypeEnum fieldType)
        {
            _name = "";

            switch (fieldType)
            {
                case NodeState.FieldTypeEnum.Corn:
                    _name = "Maïs";
                    break;
                case NodeState.FieldTypeEnum.Carrot:
                    _name = "Wortel";
                    break;
                case NodeState.FieldTypeEnum.Nothing:
                    _name = "Null";
                    break;
                case NodeState.FieldTypeEnum.Apple:
                    _name = "Appel";
                    break;
                case NodeState.FieldTypeEnum.Blackberries:
                    _name = "Bramen";
                    break;
                case NodeState.FieldTypeEnum.Tomato:
                    _name = "Tomaten";
                    break;
                case NodeState.FieldTypeEnum.Tree:
                    _name = "Bomen";
                    break;
                case NodeState.FieldTypeEnum.Grapes:
                    _name = "Druiven";
                    break;
                default:
                    break;
            }

            return _name;
        }

        public void AddEvent(Events events)
        {
            _eventsInInbox.Add(events);
             
            
            string effect = GetName(events.FieldType) + " " + events.InfluencePercentage + "%";
            Add(events.Headline,events.Content, effect);
        }

        public void Add(string headline, string content, string effect)
        {
          

            
            
            _messageText.Add(new MessageEntry(headline, content, effect));

            GameObject go = Instantiate(HeadlineUiPrefab, StartingPos.position, Quaternion.identity, Content.transform) as GameObject;
            _go.Add(go);

            if (_go.Count > 1)
            {
                UpdateTextPos();
            }





            MessagePrefab messagePrefab = go.GetComponent<MessagePrefab>();
            messagePrefab.Headline.text = headline;

            
            
            Button tempButton = go.GetComponent<Button>();
            int tempCount = _eventsInInbox.Count;
            tempButton.onClick.AddListener(() => this.OnButtonClick(tempCount));
            //ChangeText(go, headline,content,effect);
        }

        public void UpdateTextPos()
        {
            foreach (var t in _go)
            {
                _currentpos.position = new Vector3(_currentpos.position.x, _currentpos.position.y + Gap,
                    _currentpos.position.z);
                t.transform.position = _currentpos.position;
            }
        }

        //public void ChangeText(GameObject go,string headline, string content, string effect)
        //{
        //    MessagePrefab messagePrefab = go.GetComponent<MessagePrefab>();
        //    messagePrefab.Content = PopupScript.ContentText;
        //    messagePrefab.Effect = PopupScript.EffectText;
        //    messagePrefab.Headline.text = headline;
        //    messagePrefab.Content.text = content;
        //    messagePrefab.Effect.text = effect;
            
        //    //Content.headline = messagePrefab.headline
        //    //PopupScript.ContentText = messagePrefab.Content;
        //    //PopupScript.EffectText = messagePrefab.Effect;
        
            
            
            
        //}

        public void ChangeTextOnButton(int index)
        {
            Debug.Log(index);
            MessagePrefab messagePrefab = _go[index - 1].GetComponent<MessagePrefab>();
            messagePrefab.Content = PopupScript.ContentText;
            messagePrefab.Effect = PopupScript.EffectText;
            //messagePrefab.Headline.text = _eventsInInbox[index].Headline;
            messagePrefab.Content.text = _eventsInInbox[index -1].Content;
            string effect = GetName(_eventsInInbox[index - 1].FieldType) + " " + _eventsInInbox[index - 1].InfluencePercentage + "%";
            messagePrefab.Effect.text = effect ;

            //Content.headline = messagePrefab.headline
            //PopupScript.ContentText = messagePrefab.Content;
            //PopupScript.EffectText = messagePrefab.Effect;

            //_button = _go[index].GetComponent<Button>();
            //_button.onClick.AddListener(() => OnButtonClick(_eventsInInbox.Count));


        }

        public void OnButtonClick(int index)
        {
            ChangeTextOnButton(index);
            PopupUi.SetActive(true);
        }

    }
}
