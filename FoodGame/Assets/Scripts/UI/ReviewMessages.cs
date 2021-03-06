﻿using System.Collections.Generic;
using System.Linq;
using MathExt;
using Money;
using UnityEngine;
using UnityEngine.UI;


// ReSharper disable once CheckNamespace
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


        private readonly List<Reviews> _reviewsInInbox = new List<Reviews>();
        private Dictionary<int, bool> _readDict = new Dictionary<int, bool>();

        public float Gap;
        private Button _button;
        public GameObject PopupUi;
        public GameObject Exclamation;
        public Popup PopupScript;


        private string _name;

        private void Awake()
        {
            _currentpos = StartingPos;
            _startingPos = StartingPos.position;
            _rectTransform = Content.GetComponent<RectTransform>();
        }

        public bool NotInInbox(Reviews reviews)
        {
            for (int i = 0; i < _reviewsInInbox.Count; i++)
            {
                if (_reviewsInInbox[i] == reviews)
                {
                    return false;
                }
            }

            return true;
        }

        public void AddReview(Reviews reviews)
        {
            _reviewsInInbox.Add(reviews);
            Add(reviews.Headline, reviews.InfluencePercentage);
        }

        public void CheckReviewDuration()
        {
            foreach (var review in _reviewsInInbox)
            {
                if (review.Duration > 0)
                {
                    review.Duration--;
                }
                else
                {
                    SimpleMoneyManager.Instance.SetPercentage(review.FieldType, - review.InfluencePercentage);
                }
            }
        }

        private void Add(string headline, float percentage)
        {
            GameObject go = Instantiate(HeadlineUiPrefab, _startingPos, Quaternion.identity, Content.transform);
            go.GetComponent<Image>().sprite = percentage > 0
                ? EventManager.Instance.ReviewBackgroundPositive.GetRandom_Array()
                : EventManager.Instance.ReviewBackgroundNegative.GetRandom_Array();



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


            if (_readDict.ContainsKey(tempCount)) return;
            _readDict.Add(tempCount, false);
            Exclamation.SetActive(true);
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

        public Dictionary<int, bool> GetReadDict()
        {
            return _readDict;
        }

        public void SetReadDictionary(Dictionary<int, bool> readDict)
        {
            _readDict = readDict;
        }

        private void OnButtonClick(int index)
        {
            ChangeTextOnButton(index);
            _readDict[index] = true;
            if (ReadDictBool())
            {
                Exclamation.SetActive(false);
            }

            PopupUi.SetActive(true);
        }

        private void ChangeTextOnButton(int index)
        {
            Reviewprefab reviewPrefab = _go[index - 1].GetComponent<Reviewprefab>();
            Reviews review = _reviewsInInbox[index -1];
            reviewPrefab.Effect = PopupScript.EffectText;
            reviewPrefab.Content = PopupScript.ContentText;
            if (_reviewsInInbox[index - 1].Insert)
            {
                reviewPrefab.ChangeText
                (
                    review.Headline,
                    review.PreInsert,
                    review.FieldType,
                    review.AfterInsert,
                    review.InfluencePercentage.ToString()
                );
            }
            else
            {
                reviewPrefab.ChangeText
                (
                    review.Headline,
                    review.PreInsert,
                    review.InfluencePercentage.ToString()
                );
            }


            PopupScript.EffectText.text = reviewPrefab.Effect.text;
            PopupScript.ContentText.text = reviewPrefab.Content.text;
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


        public void SetInboxInt(List<int> inbox)
        {
            for (int i = 0; i < inbox.Count; i++)
            {
                if (_reviewsInInbox.Contains(EventManager.Instance.Reviews[inbox[i]])) continue;
                _reviewsInInbox.Add(EventManager.Instance.Reviews[inbox[i]]);
                Add(_reviewsInInbox[i].Headline, _reviewsInInbox[i].InfluencePercentage);

            }
        }

        public List<int> GetInboxInt()
        {
            List<int> inboxInt = new List<int>();
            for (int i = 0; i < _reviewsInInbox.Count; i++)
            {
                for (int j = 0; j < EventManager.Instance.Reviews.Length; j++)
                {
                    if (EventManager.Instance.Reviews[j] == _reviewsInInbox[i])
                    {
                        inboxInt.Add(j);
                    }
                }
            }
            return inboxInt;

        }
    }
}
