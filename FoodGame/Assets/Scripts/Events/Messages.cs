using System.Collections.Generic;
using MathExt;
using Money;
using Node;
using Save;
using TimeSystem;
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
        private Vector3 _startingPos;
        private RectTransform _rectTransform;

        private readonly List<GameObject> _go = new List<GameObject>();
        private List<Events> _eventsInInbox = new List<Events>();

        public float Gap;

        private Button _button;

        public GameObject PopupUi;

        public Popup PopupScript;

        public bool Review;

        private string _name;

        // Use this for initialization
        private void Awake()
        {
            _currentpos = StartingPos;
            _startingPos = StartingPos.position;
            _rectTransform = Content.GetComponent<RectTransform>();
        }



        public void AddEvent(Events events)
        {
            _eventsInInbox.Add(events);
            string effect = Finance.GetName(events.FieldType) + " " + events.InfluencePercentage + "%";
            Add(events.Headline, events.InfluencePercentage);
        }

        public void Add(string headline, float percentage)
        {
            GameObject go =
                Instantiate(HeadlineUiPrefab, _startingPos, Quaternion.identity, Content.transform) as GameObject;
            if (Review)
            {
                go.GetComponent<Image>().sprite = percentage < 0 ? EventManager.Instance.ReviewBackgroundNegative.GetRandom_Array() : EventManager.Instance.ReviewBackgroundPositive.GetRandom_Array();
            }
            else if (percentage < 0)
            {
                if (percentage < 0)
                {
                    go.GetComponent<Image>().sprite = EventManager.Instance.MessageBackground[1];
                }
            }

            _go.Insert(0, go);

            if (_go.Count > 1)
            {
                UpdateTextPos();
            }

            if (_go.Count > 3)
            {
                _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, _rectTransform.sizeDelta.y - Gap);
            }

            MessagePrefab messagePrefab = go.GetComponent<MessagePrefab>();
            messagePrefab.Headline.text = headline;
            Button tempButton = go.GetComponent<Button>();
            int tempCount = _eventsInInbox.Count;
            tempButton.onClick.AddListener(() => this.OnButtonClick(tempCount));
            //ChangeText(go, headline,content,effect);
        }

        private void UpdateTextPos()
        {
            for (int i = 1; i < _go.Count; i++)
            {
                float tempGap = Gap * i;
                Debug.Log(_startingPos);

                _currentpos.position = new Vector3(_currentpos.position.x, _currentpos.position.y + tempGap,
                    _currentpos.position.z);
                _go[i].transform.position = _currentpos.position;
                _currentpos.position = _startingPos;
            }
        }

        public List<int> GetInboxInt()
        {
            List<int> inboxInt = new List<int>();

            for (int i = 0; i < _eventsInInbox.Count; i++)
            {
                for (int j = 0; j < EventManager.Instance.EventsArray.Length; j++)
                {
                    if (EventManager.Instance.EventsArray[j] == _eventsInInbox[i])
                    {
                        inboxInt.Add(j);
                    }
                }
            }
            return inboxInt;

        }

        public void SetInboxInt(List<int> inbox)
        {
            for (var index = 0; index < inbox.Count; index++)
            {
                _eventsInInbox.Add(EventManager.Instance.EventsArray[inbox[index]]);
                Add(_eventsInInbox[index].Headline, _eventsInInbox[index].InfluencePercentage);
                
            }

            int oldMonth = SaveManager.Instance.GetSaveMonth();
            int oldYear = SaveManager.Instance.GetSaveYear();
            int month = TimeManager.Instance.GetMonth();
            int year = TimeManager.Instance.GetYear();
            for (int i = 0; i < TimeManager.Instance.GetTotalAddedMonths(); i++)
            {
                for (int j = 0; j < EventManager.Instance.EventsArray.Length; j++)
                {
                    if(EventManager.Instance.EventsArray[j].Starts == new Vector2Int(oldMonth,oldYear) && EventManager.Instance.EventsArray[j].Review == Review)
                    {
                        
                        _eventsInInbox.Add(EventManager.Instance.EventsArray[j]);
                        Add(_eventsInInbox[_eventsInInbox.Count - 1].Headline, _eventsInInbox[_eventsInInbox.Count -1].InfluencePercentage);
                    }
                }
   
                if (oldMonth >= 12)
                {
                    oldMonth = 1;
                    oldYear++;
                }
                else
                {
                    oldMonth++;
                }

                if (oldMonth == month && oldYear == year)
                {
                    break;
                }
           
            }
        }

        public void ChangeTextOnButton(int index)
        {
            Debug.Log(index);
            MessagePrefab messagePrefab = _go[index - 1].GetComponent<MessagePrefab>();
            messagePrefab.Content = PopupScript.ContentText;
            messagePrefab.Effect = PopupScript.EffectText;
            messagePrefab.Content.text = _eventsInInbox[index - 1].Content;
            string effect = Finance.GetName(_eventsInInbox[index - 1].FieldType) + " " +
                            _eventsInInbox[index - 1].InfluencePercentage + "%";
            messagePrefab.Effect.text = effect;
        }

        public void OnButtonClick(int index)
        {
            ChangeTextOnButton(index);
            PopupUi.SetActive(true);
        }
    }
}
