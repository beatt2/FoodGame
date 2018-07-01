using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cultivations;
using Grid;
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


        public Text KanskaartText;
        public Text RewardKansKaart;
        public Text PercentageKansKaart;
        public GameObject Ui;

        private bool _eventOnGoing;

        private float _percentageEvent = 0;
        private string _name;

        public Messages MessageScript;
        public ReviewMessages ReviewScript;

        public bool InMenu;


        public Sprite[] ReviewBackgroundNegative;
        public Sprite[] ReviewBackgroundPositive;
        public Sprite[] MessageBackground;

        private Image _headlineUiImage;
        private bool _inKanskaartMenu;
        private readonly List<Kanskaarten> _actieveKanskaarten = new List<Kanskaarten>();

        private readonly Dictionary<NodeState.FieldTypeEnum, int> _enviromentalValues = new Dictionary<NodeState.FieldTypeEnum, int>();

        private readonly Dictionary<NodeState.FieldTypeEnum, int> _happinessValues = new Dictionary<NodeState.FieldTypeEnum, int>();

        private bool _reviewCurrentlyActive;

        public MessageButton MyMessageButton;
        public MessageButton MyReviewButton;

        protected override void Awake()
        {
            base.Awake();
            _headlineUiImage = HeadlineUi.GetComponent<Image>();
        }

        public void CheckDate(int month, int year)
        {
            SetMessageScreen(month, year);
            SetKansKaart(month, year);
            MessageScript.CheckDuration(month, year);
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
            foreach (var review in Reviews)
            {
                if (review.Enviromental)
                {
                    for (int j = 0; j < _enviromentalValues.Count; j++)
                    {
                        if (review.EffectValue != _enviromentalValues.ElementAt(j).Value) continue;
                        if (review.OnlyOnFactory) continue;
                        if (review.Insert)
                        {
                            SetReviews(review, _enviromentalValues.ElementAt(j).Key);
                        }
                        else
                        {
                            SetReviews(review);
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < _enviromentalValues.Count; j++)
                    {
                        if (review.EffectValue != _enviromentalValues.ElementAt(j).Value) continue;
                        if (review.OnlyOnFactory) continue;
                        if (review.Insert)
                        {
                            SetReviews(review, _enviromentalValues.ElementAt(j).Key);
                        }
                        else
                        {
                            SetReviews(review);
                        }
                    }
                }
            }
        }

        public void AddFactoryReview()
        {
            foreach (var review in Reviews)
            {
                if (!review.OnlyOnFactory) continue;
                SetReviews(review);
                review.OnlyOnFactory = false;
                return;
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


        private void SetKansKaart(int month, int year)
        {
            foreach (var kansKaart in KansKaartenArray)
            {
                if (kansKaart.Starts != new Vector2Int(month, year)) continue;
                _actieveKanskaarten.Add(kansKaart);
                if (!_inKanskaartMenu)
                {
                    ActivateCard();
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


        private void SetEffect()
        {
            if (_actieveKanskaarten[0].Insert)
            {
                KanskaartText.text =
                    _actieveKanskaarten[0].PreInsert + " " +
                    Reviewprefab.GetInsert(_actieveKanskaarten[0].FieldType) + " " +
                    _actieveKanskaarten[0].AfterInsert;
            }
            else
            {
                KanskaartText.text = _actieveKanskaarten[0].PreInsert;
            }
          
            string effectPercentage = "";
            string effectReward = "";
            if (_actieveKanskaarten[0].PrefabUpgrade)
            {
                List<Cultivation> tempList = new List<Cultivation>();
                if (_actieveKanskaarten[0].Type == NodeState.CurrentStateEnum.Farm)
                {
                    tempList = GridManager.Instance.GetCertainSizeCultivation((int) _actieveKanskaarten[0].Size, true);
                }
                else if(_actieveKanskaarten[0].Type == NodeState.CurrentStateEnum.Field)
                {
                    tempList = GridManager.Instance.GetCertainSizeCultivation((int) _actieveKanskaarten[0].Size, false);
                }
           
                for (int i = 0; i < tempList.Count; i++)
                {
                    if (_actieveKanskaarten[0].FieldType == NodeState.FieldTypeEnum.Nothing || _actieveKanskaarten[0].FieldType == tempList[i].FieldType)
                    {
                        tempList[i].EnviromentValue += _actieveKanskaarten[0].EnviromentInfluence;
                        tempList[i].Happiness += _actieveKanskaarten[0].HappinessInfluence;
                        int monneyIncrease = tempList[i].MoneyTick / _actieveKanskaarten[0].IncomePercentageIncrease;
                        tempList[i].MoneyTick += monneyIncrease;
                        SimpleMoneyManager.Instance.AddMoney(_actieveKanskaarten[0].Reward);
                    }


                }
                
            }
            else
            {
                SimpleMoneyManager.Instance.AddMoney(_actieveKanskaarten[0].Reward);
                SimpleMoneyManager.Instance.SetPercentage(_actieveKanskaarten[0].FieldType, _actieveKanskaarten[0].InfluencePercentage);


            }
            
            
            
        
            else if (_actieveKanskaarten[0].InfluencePercentage > 0)
            {
                SimpleMoneyManager.Instance.SetPercentage(_actieveKanskaarten[0].FieldType, _actieveKanskaarten[0].InfluencePercentage);
            }

            if (_actieveKanskaarten[0].Reward > 0)
            {
                SimpleMoneyManager.Instance.AddMoney(_actieveKanskaarten[0].Reward);
            }
            else if (_actieveKanskaarten[0].Reward < 0)
            {
                SimpleMoneyManager.Instance.AddMoney(_actieveKanskaarten[0].Reward);
            }

            effectReward = _actieveKanskaarten[0].Effect;
    
            PercentageKansKaart.text = effectPercentage;
            RewardKansKaart.text = effectReward;
        }

        private void SetTextKanskaart()
        {
            Debug.Log("YOU ARE USING IT");
            // KanskaartText.text = _actieveKanskaarten[0].Headline;
        }


        private void SetMessageScreen(int month, int year)
        {
            for (int i = 0; i < EventsArray.Length; i++)
            {
                if (EventsArray[i].Starts != new Vector2Int(month, year)) continue;
                if (!MessageScript.NotInInbox(EventsArray[i])) continue;
                StopCoroutine("UiTimer");

                MessageScript.AddEvent(EventsArray[i]);
                SoundManager.Instance.PlayMessageSound();
                if (!InMenu)
                {
                    HeadlineUi.SetActive(true);
                    _headlineUiImage.sprite = EventsArray[i].MyFieldTypes[0].InfluencePercentage < 0
                        ? MessageBackground[1]
                        : MessageBackground[0];
                }

                _reviewCurrentlyActive = false;
                StartCoroutine("UiTimer");
                SetText(i);
                for (int j = 0; j < EventsArray[i].MyFieldTypes.Length; j++)
                {
                    SimpleMoneyManager.Instance.SetPercentage
                    (
                        EventsArray[i].MyFieldTypes[j].FieldType,
                        EventsArray[i].MyFieldTypes[j].InfluencePercentage
                    );

                    int amountofCults = GridManager.Instance.GetCultivationByFieldType(EventsArray[i].MyFieldTypes[j].FieldType);
                    AddEnviromentValue(EventsArray[i].MyFieldTypes[j].FieldType,amountofCults);
                    AddHappinessValue(EventsArray[i].MyFieldTypes[j].FieldType, amountofCults);
                }
            }
        }

        private void SetReviews(Reviews review, NodeState.FieldTypeEnum fieldType = NodeState.FieldTypeEnum.Nothing)
        {
            StartCoroutine(SetReviewsIE(review, fieldType));
        }

        private IEnumerator SetReviewsIE(Reviews review, NodeState.FieldTypeEnum fieldType = NodeState.FieldTypeEnum.Nothing)
        {
            if (!ReviewScript.NotInInbox(review)) yield break;
            yield return new WaitForSeconds(Random.Range(4, 25));
            StopCoroutine("UiTimer");
            _reviewCurrentlyActive = true;
            SetText(review);
            SoundManager.Instance.PlayMessageSound();
            if (fieldType != NodeState.FieldTypeEnum.Nothing)
            {
                review.FieldType = fieldType;
            }

            ReviewScript.AddReview(review);
            if (!InMenu)
            {
                HeadlineUi.SetActive(true);
                _headlineUiImage.sprite = review.InfluencePercentage > 0
                    ? ReviewBackgroundPositive.GetRandom_Array()
                    : ReviewBackgroundNegative.GetRandom_Array();
            }

            SimpleMoneyManager.Instance.SetPercentage(review.FieldType, review.InfluencePercentage);
            StartCoroutine("UiTimer");
        }

        public void OnMessageClick()
        {
            if (_reviewCurrentlyActive)
            {
                MyReviewButton.OpenMessages();
            }
            else
            {
                MyMessageButton.OpenMessages();
            }

            HeadlineUi.SetActive(false);
        }


        private IEnumerator UiTimer()
        {
            yield return new WaitForSeconds(5);
            HeadlineUi.SetActive(false);
        }

        private void SetText(int whichevent)
        {
            Headline.text = EventsArray[whichevent].Headline;
        }

        private void SetText(Reviews review)
        {
            Headline.text = review.Headline;
        }
    }
}