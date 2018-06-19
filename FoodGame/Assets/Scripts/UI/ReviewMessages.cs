using System.Collections.Generic;
using Events;
using MathExt;
using Money;
using UnityEngine;
using UnityEngine.UI;


//TODO WOULD BE NICE TO REFACTOR
namespace Events
{
    public class ReviewMessages : MonoBehaviour
    {
        public GameObject HeadlineUiPrefab;
        public GameObject Content;
        private RectTransform _currentpos;
        public RectTransform StartingPos;
        private Vector3 _startingPos;
        private RectTransform _rectTransform;

        private readonly List<GameObject> _go = new List<GameObject>();


        private List<Reviews> _reviewsInInbox = new List<Reviews>();
        private Dictionary<int, bool> _readDict = new Dictionary<int, bool>();

        public float Gap;
        private Button _button;
        public GameObject PopupUi;
        public GameObject Exclamation;
        public Popup PopupScript;

        public bool Review;
        private string _name;

        private void Awake()
        {
            _currentpos = StartingPos;
            _startingPos = StartingPos.position;
            _rectTransform = Content.GetComponent<RectTransform>();
        }

        public void AddReview(Reviews reviews)
        {
            _reviewsInInbox.Add(reviews);
            string effect = Finance.GetName(reviews.FieldType) + " " + reviews.InfluencePercentage + "%";
            Add(reviews.Headline, reviews.InfluencePercentage);
        }

        public void Add(string headline, float percentage)
        {
            GameObject go = Instantiate(HeadlineUiPrefab, _startingPos, Quaternion.identity, Content.transform);
            go.GetComponent<Image>().sprite = percentage < 0 ? EventManager.Instance.ReviewBackgroundNegative.GetRandom_Array() : EventManager.Instance.ReviewBackgroundPositive.GetRandom_Array();

            if (percentage < 0)
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
            int tempCount = _reviewsInInbox.Count;
            tempButton.onClick.AddListener(() => OnButtonClick(tempCount));


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
        public void ChangeTextOnButton(int index)
        {
            Debug.Log(index);
            MessagePrefab messagePrefab = _go[index - 1].GetComponent<MessagePrefab>();
            messagePrefab.Content = PopupScript.ContentText;
            messagePrefab.Effect = PopupScript.EffectText;
            messagePrefab.Content.text = _reviewsInInbox[index - 1].Content;
            string effect = Finance.GetName(_eventsInInbox[index - 1].FieldType) + " " +
                            _eventsInInbox[index - 1].InfluencePercentage + "%";
            messagePrefab.Effect.text = effect;
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