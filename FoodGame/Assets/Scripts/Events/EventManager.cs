using System;
using System.Collections;
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

        public Text Headline;
        public GameObject HeadlineUi;

        public GameObject ExclamationMark;
        public GameObject ExclamationMarkReview;

        public Sprite[] Sprites;
        public Text Content;
        public Text Effect;
        public GameObject Ui;
        public bool InEventMenu = false;

        private bool _eventOnGoing;

        private float _percentageEvent = 0;
        private string _name;
        public Messages MessageScript;
        public Messages ReviewScript;
        public bool InMenu = false;

        public Sprite[] ReviewBackground;
        public Sprite MessageBackground;

        private Image _headlineUiImage;



        public float GetInfluence()
        {
            return _percentageEvent;
        }

        protected override void Awake()
        {
            base.Awake();
            _headlineUiImage = HeadlineUi.GetComponent<Image>();
        }

        public void CheckDate(int month,int year)
        {

          SetMessageScreen(month,year);

        }

        private void SetMessageScreen(int month, int year)
        {
            for (int i = 0; i < EventsArray.Length; i++)
            {
                if (EventsArray[i].Starts == new Vector2Int(month, year))
                {

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
                            _headlineUiImage.sprite = ReviewBackground.GetRandom_Array();
                        }
                        else
                        {
                            _headlineUiImage.sprite = MessageBackground;

                        }
                    }

                    if (!EventsArray[i].Review)
                    {
                        ExclamationMark.SetActive(true);
                    }
                    else
                    {
                        ExclamationMarkReview.SetActive(true);
                    }

                    if (EventsArray[i].InfluencePercentage < -5 || EventsArray[i].InfluencePercentage > 8)
                    {
                        if (!EventsArray[i].Review)
                        {
                            ExclamationMark.GetComponent<Image>().sprite = Sprites[2];
                        }
                        else
                        {
                            ExclamationMarkReview.GetComponent<Image>().sprite = Sprites[2];
                        }

                    }
                    else if (EventsArray[i].InfluencePercentage < 0 || EventsArray[i].InfluencePercentage > 4)
                    {
                        if (!EventsArray[i].Review)
                        {
                            ExclamationMark.GetComponent<Image>().sprite = Sprites[1];
                        }
                        else
                        {
                            ExclamationMarkReview.GetComponent<Image>().sprite = Sprites[1];
                        }
                    }
                    else
                    {
                        if (!EventsArray[i].Review)
                        {
                            ExclamationMark.GetComponent<Image>().sprite = Sprites[0];
                        }
                        else
                        {
                            ExclamationMarkReview.GetComponent<Image>().sprite = Sprites[0];
                        }
                    }

                    StartCoroutine("UiTimer");
                    SetText(i);

                    SimpleMoneyManager.Instance.SetPercentage(EventsArray[i].FieldType,
                        EventsArray[i].InfluencePercentage);


                    if (EventsArray[i].Finishes != new Vector2Int(month, year)) continue;
                    HeadlineUi.SetActive(false);
                    if (EventsArray[i].Review)
                    {
                        ExclamationMarkReview.SetActive(false);
                    }
                    else
                    {
                        ExclamationMark.SetActive(false);

                    }

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
            //Content.text = EventsArray[whichevent].Content;

            //if (EventsArray[whichevent].InfluencePercentage > 0)
            //{
            //    Effect.text = GetName(EventsArray[whichevent].FieldType) + "  " + "+ " + EventsArray[whichevent].InfluencePercentage + "%";
            //}
            //else
            //{
            //    Effect.text = GetName(EventsArray[whichevent].FieldType) + "  " + EventsArray[whichevent].InfluencePercentage + " %";
            //}

            //FieldText.text = GetName(EventsArray[whichevent].FieldType);
        }
        //private string GetName(NodeState.FieldTypeEnum fieldType)
        //{
        //    _name = "";

        //    switch (fieldType)
        //    {
        //        case NodeState.FieldTypeEnum.Corn:
        //            _name = "Maïs";
        //            break;
        //        case NodeState.FieldTypeEnum.Carrot:
        //            _name = "Wortel";
        //            break;
        //        case NodeState.FieldTypeEnum.Nothing:
        //            _name = "Null";
        //            break;
        //        case NodeState.FieldTypeEnum.Apple:
        //            _name = "Appel";
        //            break;
        //        case NodeState.FieldTypeEnum.Blackberries:
        //            _name = "Bramen";
        //            break;
        //        case NodeState.FieldTypeEnum.Tomato:
        //            _name = "Tomaten";
        //            break;
        //        case NodeState.FieldTypeEnum.Tree:
        //            _name = "Bomen";
        //            break;
        //        case NodeState.FieldTypeEnum.Grapes:
        //            _name = "Druiven";
        //            break;
        //        default:
        //            break;
        //    }

        //    return _name;
        //}

        public void SetTextEnded(int whichevent)
        {
            Headline.text = "is gestopt";
        }

    }
}
