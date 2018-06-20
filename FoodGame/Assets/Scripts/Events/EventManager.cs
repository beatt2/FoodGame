using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KansKaarten;
using MathExt;
using Money;
using Node;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Events
{
    public class EventManager : Singleton<EventManager>
    {
        public Events[] EventsArray;
        public Reviews[] Reviews;
        public Kanskaarten[] KansKaartenArray;

        public Text Headline;
        public GameObject HeadlineUi;
        public GameObject KanskaartUi;
        public GameObject KanskaartPopup;
        public GameObject ExclamationMark;
        public GameObject ExclamationMarkReview;


        public Sprite[] Sprites;
        public Text KanskaartText;
        public Text RewardKansKaart;
        public Text PercentageKansKaart;
        public GameObject Ui;
        public bool InEventMenu = false;

        private bool _eventOnGoing;

        private float _percentageEvent = 0;
        private string _name;
        public Messages MessageScript;
        public ReviewMessages ReviewScript;
        public bool InMenu = false;


        public Sprite[] ReviewBackgroundNegative;
        public Sprite[] ReviewBackgroundPositive;
        public Sprite[] MessageBackground;
        private Image _headlineUiImage;
        private bool _inKanskaartMenu = false;
        private readonly List<Kanskaarten> _actieveKanskaarten = new List<Kanskaarten>();

        private Dictionary<NodeState.FieldTypeEnum, int> _enviromentalValues =
            new Dictionary<NodeState.FieldTypeEnum, int>();

        private Dictionary<NodeState.FieldTypeEnum, int> _happinessValues =
            new Dictionary<NodeState.FieldTypeEnum, int>();


        public float GetInfluence()
        {
            return _percentageEvent;
        }

        protected override void Awake()
        {
            base.Awake();
            _headlineUiImage = HeadlineUi.GetComponent<Image>();
        }


        public void CheckDate(int month, int year)
        {
            SetMessageScreen(month, year);
            SetKansKaart(month, year);
        }

        public void AddEnviromentValue(NodeState.FieldTypeEnum fieldType, int value)
        {
            if (!_enviromentalValues.ContainsKey(fieldType))
            {
                _enviromentalValues.Add(fieldType, value);
            }
            else
            {
                _enviromentalValues[fieldType] += value;
            }

            CheckEnviromentValue();
        }


        public int GetEnviromentValue()
        {
            int value = 0;
            foreach (var enviromental in _enviromentalValues)
            {
                value += enviromental.Value;
            }

            return value;
        }

        public int GetHappinessValue()
        {
            int value = 0;
            foreach (var happiness in _happinessValues)
            {
                value += happiness.Value;

            }

            return value;
        }

        private void CheckEnviromentValue()
        {
            for (int i = 0; i < Reviews.Length; i++)
            {
                if (Reviews[i].Enviromental)
                {
                    for (int j = 0; j < _enviromentalValues.Count; j++)
                    {
                        if (Reviews[i].EffectValue != _enviromentalValues.ElementAt(j).Value) continue;
                        if (Reviews[i].OnlyOnFactory) continue;
                        if (Reviews[i].Insert)
                        {
                            SetReviews(Reviews[i], _enviromentalValues.ElementAt(j).Key);
                        }
                        else
                        {
                            SetReviews(Reviews[i]);
                        }

                    }
                }
                else
                {
                    for (int j = 0; j < _enviromentalValues.Count; j++)
                    {
                        if (Reviews[i].EffectValue != _enviromentalValues.ElementAt(j).Value) continue;
                        if (Reviews[i].OnlyOnFactory) continue;
                        if (Reviews[i].Insert)
                        {
                            SetReviews(Reviews[i], _enviromentalValues.ElementAt(j).Key);
                        }
                        else
                        {
                            SetReviews(Reviews[i]);
                        }
                    }
                }

            }
        }

        public void AddFactoryReview()
        {
            for (int i = 0; i < Reviews.Length; i++)
            {
                if (Reviews[i].OnlyOnFactory)
                {
                    SetReviews(Reviews[i]);
                    Reviews[i].OnlyOnFactory = false;
                    return;

                }
            }
        }

        public void AddHappinessValue(NodeState.FieldTypeEnum fieldType, int value)
        {
            if (!_happinessValues.ContainsKey(fieldType))
            {
                _happinessValues.Add(fieldType, value);
            }
            else
            {
                _happinessValues[fieldType] += value;
            }
        }


        private void ActivateCard()
        {
            _inKanskaartMenu = true;
            KanskaartUi.SetActive(true);
        }


        public void SetKansKaart(int month, int year)
        {
            for (int i = 0; i < KansKaartenArray.Length; i++)
            {
                if (KansKaartenArray[i].Starts == new Vector2Int(month, year))
                {
                    _actieveKanskaarten.Add(KansKaartenArray[i]);
                    if (!_inKanskaartMenu)
                    {
                        ActivateCard();
                    }
                }
            }
        }

        public void OnClickAcceptKansKaart()
        {
            SetEffect();
            SetTextKanskaart();
            KanskaartUi.SetActive(false);
            KanskaartPopup.SetActive(true);
        }

        public void OnClickCloseKansKaart()
        {
            KanskaartUi.SetActive(false);
            KanskaartPopup.SetActive(false);
            _actieveKanskaarten.RemoveAt(0);
            if (_actieveKanskaarten.Count > 0)
            {
                ActivateCard();
            }
        }

        //REFACTOR
        private void SetEffect()
        {
            KanskaartText.text = _actieveKanskaarten[0].Headline;
            string effectPercentage = "";
            string effectReward = "";

            if (_actieveKanskaarten[0].InfluencePercentage < 0)
            {
                SimpleMoneyManager.Instance.SetPercentage(_actieveKanskaarten[0].FieldType,
                    _actieveKanskaarten[0].InfluencePercentage);
                effectPercentage = Finance.GetName(_actieveKanskaarten[0].FieldType) + " - " +
                                   _actieveKanskaarten[0].InfluencePercentage + "%";
            }
            else if (_actieveKanskaarten[0].InfluencePercentage > 0)
            {
                SimpleMoneyManager.Instance.SetPercentage(_actieveKanskaarten[0].FieldType,
                    _actieveKanskaarten[0].InfluencePercentage);
                effectPercentage = Finance.GetName(_actieveKanskaarten[0].FieldType) + " + " +
                                   _actieveKanskaarten[0].InfluencePercentage + "%";
            }

            if (_actieveKanskaarten[0].Reward > 0)
            {
                SimpleMoneyManager.Instance.AddMoney(_actieveKanskaarten[0].Reward);
                effectReward = Finance.GetName(_actieveKanskaarten[0].FieldType) + " + " +
                               _actieveKanskaarten[0].Reward + " gold";
            }
            else if (_actieveKanskaarten[0].Reward < 0)
            {
                SimpleMoneyManager.Instance.AddMoney(_actieveKanskaarten[0].Reward);
                effectReward = Finance.GetName(_actieveKanskaarten[0].FieldType) + _actieveKanskaarten[0].Reward +
                               " gold";
            }

            PercentageKansKaart.text = effectPercentage;
            RewardKansKaart.text = effectReward;
        }

        private void SetTextKanskaart()
        {
            KanskaartText.text = _actieveKanskaarten[0].Headline;
        }


        private void SetMessageScreen(int month, int year)
        {
            for (int i = 0; i < EventsArray.Length; i++)
            {
                if (EventsArray[i].Starts == new Vector2Int(month, year))
                {
                    StopCoroutine("UiTimer");
                    SoundManager.Instance.PlayMessageSound();
                    if (!InMenu)
                    {
                        HeadlineUi.SetActive(true);
                        _headlineUiImage.sprite = EventsArray[i].InfluencePercentage < 0
                            ? MessageBackground[1]
                            : MessageBackground[0];
                    }

                    StartCoroutine("UiTimer");
                    SetText(i);

                    SimpleMoneyManager.Instance.SetPercentage(EventsArray[i].FieldType,
                        EventsArray[i].InfluencePercentage);


                    if (EventsArray[i].Finishes != new Vector2Int(month, year)) continue;
                    HeadlineUi.SetActive(false);
                    //TODO WHY ?
                    SetTextEnded(i);
                }
            }
        }

        public void SetReviews(Reviews review, NodeState.FieldTypeEnum fieldType = NodeState.FieldTypeEnum.Nothing)
        {
            StartCoroutine(SetReviewsIE(review,fieldType));
        }

        private IEnumerator SetReviewsIE(Reviews review, NodeState.FieldTypeEnum fieldType = NodeState.FieldTypeEnum.Nothing)
        {
            yield return new WaitForSeconds(Random.Range(4,25));
            StopCoroutine("UiTimer");
            SetText(review);
            SoundManager.Instance.PlayMessageSound();
            if (review.FieldType != NodeState.FieldTypeEnum.Nothing)
            {
                review.FieldType = fieldType;
            }
            ReviewScript.AddReview(review);
            if (!InMenu)
            {
                HeadlineUi.SetActive(true);
                _headlineUiImage.sprite = review.InfluencePercentage < 0
                    ? ReviewBackgroundPositive.GetRandom_Array()
                    : ReviewBackgroundNegative.GetRandom_Array();
            }
            SimpleMoneyManager.Instance.SetPercentage(review.FieldType, review.InfluencePercentage);
            StartCoroutine("UiTimer");


        }




        private IEnumerator UiTimer()
        {
            yield return new WaitForSeconds(5);
            HeadlineUi.SetActive(false);
        }

        public void SetText(int whichevent)
        {
            Headline.text = EventsArray[whichevent].Headline;
        }
        public void SetText(Reviews review)
        {
            Headline.text = review.Headline;
        }


        public void SetTextEnded(int whichevent)
        {
            Headline.text = "is gestopt";
        }
    }
}
