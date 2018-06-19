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


            if (!_readDict.ContainsKey(tempCount))
            {
                _readDict.Add(tempCount, false);
                Exclamation.SetActive(true);
            }
        }
    }
}