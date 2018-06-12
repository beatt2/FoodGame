using System;
using System.Collections;
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

        public Sprite[] Sprites;
        public Text Content;
        public Text Effect;
        public GameObject Ui;
        public bool InEventMenu = false;

        private bool _eventOnGoing;

        private float _percentageEvent = 0;
        private string _name;
        public Messages MessageScript;
        public bool InMenu = false;
        public float GetInfluence()
        {
            return _percentageEvent;
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

                    MessageScript.AddEvent(EventsArray[i]);
                    if (!InMenu)
                    {
                        HeadlineUi.SetActive(true);
                    }

                    ExclamationMark.SetActive(true);

                    if (EventsArray[i].InfluencePercentage < -5 || EventsArray[i].InfluencePercentage > 8)
                    {
                        ExclamationMark.GetComponent<Image>().sprite = Sprites[2];
                    }
                    else if (EventsArray[i].InfluencePercentage < 0 || EventsArray[i].InfluencePercentage > 4)

                    {
                        ExclamationMark.GetComponent<Image>().sprite = Sprites[1];
                    }
                    else
                    {
                        ExclamationMark.GetComponent<Image>().sprite = Sprites[0];
                    }

                    StartCoroutine("UiTimer");
                    SetText(i);

                    switch (EventsArray[i].FieldType)
                    {
                        case NodeState.FieldTypeEnum.Corn:


                            SimpleMoneyManager.Instance.SetPercentage(NodeState.FieldTypeEnum.Corn,
                                EventsArray[i].InfluencePercentage);

                            break;
                        case NodeState.FieldTypeEnum.Carrot:
                            SimpleMoneyManager.Instance.SetPercentage(NodeState.FieldTypeEnum.Carrot,
                                EventsArray[i].InfluencePercentage);

                            break;
                        case NodeState.FieldTypeEnum.Nothing:
                            break;
                        case NodeState.FieldTypeEnum.Apple:
                            SimpleMoneyManager.Instance.SetPercentage(NodeState.FieldTypeEnum.Apple,
                                EventsArray[i].InfluencePercentage);
                            break;
                        case NodeState.FieldTypeEnum.Blackberries:
                            SimpleMoneyManager.Instance.SetPercentage(NodeState.FieldTypeEnum.Blackberries,
                                EventsArray[i].InfluencePercentage);
                            break;
                        case NodeState.FieldTypeEnum.Tomato:
                            SimpleMoneyManager.Instance.SetPercentage(NodeState.FieldTypeEnum.Tomato,
                                EventsArray[i].InfluencePercentage);
                            break;
                        case NodeState.FieldTypeEnum.Tree:
                            SimpleMoneyManager.Instance.SetPercentage(NodeState.FieldTypeEnum.Tree,
                                EventsArray[i].InfluencePercentage);
                            break;
                        case NodeState.FieldTypeEnum.Grapes:
                            SimpleMoneyManager.Instance.SetPercentage(NodeState.FieldTypeEnum.Grapes,
                                EventsArray[i].InfluencePercentage);
                            break;
                        default:
                            break;
                    }
                }

                if (EventsArray[i].Finishes != new Vector2Int(month, year)) continue;

                HeadlineUi.SetActive(false);
                ExclamationMark.SetActive(false);
                SetTextEnded(i);
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
