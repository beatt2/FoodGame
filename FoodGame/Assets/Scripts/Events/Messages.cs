using System.Collections.Generic;
using System.Linq;
using Grid;
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


        private readonly List<Events> _eventsInInbox = new List<Events>();
        private Dictionary<int, bool> _readDict = new Dictionary<int, bool>();

        public float Gap;
        private Button _button;
        public GameObject PopupUi;
        public GameObject Exclamation;
        public Popup PopupScript;


        private string _name;

        // Use this for initialization
        private void Awake()
        {
            _currentpos = StartingPos;
            _startingPos = StartingPos.position;
            _rectTransform = Content.GetComponent<RectTransform>();

        }
        public bool NotInInbox(Events myEvent)
        {
            for (int i = 0; i < _eventsInInbox.Count; i++)
            {
                if (_eventsInInbox[i] == myEvent)
                {
                    return false;
                }
            }

            return true;
        }



        public void AddEvent(Events events)
        {
            _eventsInInbox.Add(events);
            string effect = events.Effect;
            Add(events.Headline, events.MyFieldTypes);
        }

        public void CheckDuration(int month, int year)
        {
            foreach (var events in _eventsInInbox)
            {
                if (events.Finishes == new Vector2Int(month, year))
                {
                    for (int i = 0; i < events.MyFieldTypes.Length; i++)
                    {
                        SimpleMoneyManager.Instance.SetPercentage(events.MyFieldTypes[i].FieldType, -events.MyFieldTypes[i].InfluencePercentage);

                    }
                }
            }
        }


        public void Add(string headline, FieldTypes [] fieldTypes)
        {
            GameObject go = Instantiate(HeadlineUiPrefab, _startingPos, Quaternion.identity, Content.transform) as GameObject;

            if (fieldTypes[0].InfluencePercentage < 0)
            {
               
                    go.GetComponent<Image>().sprite = EventManager.Instance.MessageBackground[1];
                
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


            if (!_readDict.ContainsKey(tempCount))
            {
                _readDict.Add(tempCount, false);
                Exclamation.SetActive(true);
            }

        }

        private void UpdateTextPos()
        {
            for (int i = 1; i < _go.Count; i++)
            {
                float tempGap = Gap * i;

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

        public Dictionary<int, bool> GetReadDict()
        {
            return _readDict;
        }

        public void SetReadDictionary(Dictionary<int, bool> readDict)
        {
            _readDict = readDict;
        }


        public void SetInboxInt(List<int> inbox)
        {
            for (var index = 0; index < inbox.Count; index++)
            {
                if (!_eventsInInbox.Contains(EventManager.Instance.EventsArray[inbox[index]]))
                {
                    _eventsInInbox.Add(EventManager.Instance.EventsArray[inbox[index]]);
                    Add(_eventsInInbox[index].Headline, _eventsInInbox[index].MyFieldTypes);
                }
            }

            int oldMonth = SaveManager.Instance.GetSaveMonth();
            int oldYear = SaveManager.Instance.GetSaveYear();
            int month = TimeManager.Instance.GetMonth();
            int year = TimeManager.Instance.GetYear();
            for (int i = 0; i < TimeManager.Instance.GetTotalAddedMonths(); i++)
            {
                for (int j = 0; j < EventManager.Instance.EventsArray.Length; j++)
                {
                    if(EventManager.Instance.EventsArray[j].Starts == new Vector2Int(oldMonth,oldYear))
                    {
                        _eventsInInbox.Add(EventManager.Instance.EventsArray[j]);
                        Add(_eventsInInbox[_eventsInInbox.Count - 1].Headline, _eventsInInbox[_eventsInInbox.Count -1].MyFieldTypes);
                        for (int k = 0; k < _eventsInInbox[_eventsInInbox.Count -1].MyFieldTypes.Length; k++)
                        {
                            int temp =GridManager.Instance.GetCultivationByFieldType(_eventsInInbox[_eventsInInbox.Count - 1].MyFieldTypes[k].FieldType);
                            EventManager.Instance.AddEnviromentValue(_eventsInInbox[_eventsInInbox.Count - 1].MyFieldTypes[k].FieldType,temp);
                            EventManager.Instance.AddHappinessValue(_eventsInInbox[_eventsInInbox.Count - 1].MyFieldTypes[k].FieldType,temp);
                        }
        
                        
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
            string effect = _eventsInInbox[index - 1].Effect;
            messagePrefab.Effect.text = effect;
        }

        public void OnButtonClick(int index)
        {
            ChangeTextOnButton(index);
            _readDict[index] = true;
            if (ReadDictBool())
            {
                Exclamation.SetActive(false);
            }
            PopupUi.SetActive(true);
        }

        private bool ReadDictBool()
        {
            for (int i = 0; i < _readDict.Keys.Count; i++)
            {
                if (_readDict[_readDict.ElementAt(i).Key] == false)
                {
                    return false;
                }
            }

            return true;
        }
    }

}
