using System;
using System.Collections;
using System.Collections.Generic;
using KansKaarten;
using MathExt;
using KansKaarten;
using Money;
using Node;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Events
{
    public class EventManager : Singleton<EventManager>
    {
        public Events[] EventsArray;
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
        public Messages ReviewScript;
        public bool InMenu = false;

        public Sprite[] ReviewBackgroundNegative;
        public Sprite[] ReviewBackgroundPositive;
        public Sprite[] MessageBackground;
        private Image _headlineUiImage;
        private bool _inKanskaartMenu = false;
        private readonly List<Kanskaarten> _actieveKanskaarten = new List<Kanskaarten>();

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


        private void SetEffect()
        {
            KanskaartText.text = _actieveKanskaarten[0].Headline;
            string effectPercentage = "";
            string effectReward = "";

            if (_actieveKanskaarten[0].InfluencePercentage < 0)
            {
                SimpleMoneyManager.Instance.SetPercentage(_actieveKanskaarten[0].FieldType,_actieveKanskaarten[0].InfluencePercentage);
                effectPercentage = Finance.GetName(_actieveKanskaarten[0].FieldType) + " - " + _actieveKanskaarten[0].InfluencePercentage + "%";
            }
            else if (_actieveKanskaarten[0].InfluencePercentage > 0)
            {
                SimpleMoneyManager.Instance.SetPercentage(_actieveKanskaarten[0].FieldType,_actieveKanskaarten[0].InfluencePercentage);
                effectPercentage = Finance.GetName(_actieveKanskaarten[0].FieldType) + " + " + _actieveKanskaarten[0].InfluencePercentage + "%";
            }

            if (_actieveKanskaarten[0].Reward > 0)
            {
                SimpleMoneyManager.Instance.AddMoney(_actieveKanskaarten[0].Reward);
                effectReward = Finance.GetName(_actieveKanskaarten[0].FieldType) + " + " +_actieveKanskaarten[0].Reward + " gold";
            }
            else if (_actieveKanskaarten[0].Reward < 0)
            {
                SimpleMoneyManager.Instance.AddMoney(_actieveKanskaarten[0].Reward);
                effectReward = Finance.GetName(_actieveKanskaarten[0].FieldType) + _actieveKanskaarten[0].Reward +" gold";
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
                    if (!EventsArray[i].Review)
                    {
                        MessageScript.AddEvent(EventsArray[i]);
                    }
                    else
                    {
                        ReviewScript.AddEvent(EventsArray[i]);
                    }

                    if (!InMenu)
                    {
                        HeadlineUi.SetActive(true);
                        if (EventsArray[i].Review)
                        {
                            //_headlineUiImage.sprite = ReviewBackgroundNegative.GetRandom_Array();
                            _headlineUiImage.sprite = EventsArray[i].InfluencePercentage < 0 ? ReviewBackgroundNegative.GetRandom_Array() : ReviewBackgroundPositive.GetRandom_Array();
                        }
                        else
                        {
                            _headlineUiImage.sprite = EventsArray[i].InfluencePercentage <0 ? MessageBackground[1] : MessageBackground[0];
                        }
                    }

                    StartCoroutine("UiTimer");
                    SetText(i);

                    SimpleMoneyManager.Instance.SetPercentage(EventsArray[i].FieldType,
                        EventsArray[i].InfluencePercentage);


                    if (EventsArray[i].Finishes != new Vector2Int(month, year)) continue;
                    HeadlineUi.SetActive(false);


                    SetTextEnded(i);
                }
            }
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


        public void SetTextEnded(int whichevent)
        {
            Headline.text = "is gestopt";
        }
    }
}
