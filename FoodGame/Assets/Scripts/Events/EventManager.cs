using System.Collections;
using System.Collections.Generic;
using KansKaarten;
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
        public Kanskaarten[] KansKaartenArray;
        public Text Headline;
        public GameObject HeadlineUi;
        public GameObject KanskaartUi;
        public GameObject KanskaartPopup;
        public GameObject ExclamationMark;
        public Button KanskaartButton;

        public Sprite[] Sprites;
        public GameObject Ui;
        public bool InEventMenu = false;

        private bool _eventOnGoing;

        private float _percentageEvent = 0;
        private string _name;
        public Messages MessageScript;
        public bool InMenu = false;
        public Text KanskaartText;

        private bool _inKanskaartMenu = false;
        private readonly List<Kanskaarten> _actieveKanskaarten = new List<Kanskaarten>();

        public float GetInfluence()
        {
            return _percentageEvent;
        }

        public void CheckDate(int month,int year)
        {
            
          SetMessageScreen(month,year);
            SetKansKaart(month,year);
            
        }

        public void ApplyCard()
        {

        }
        private void SetKansKaart(int month, int year)
        {
            for (int i = 0; i < KansKaartenArray.Length; i++)
            {
                if (KansKaartenArray[i].Starts == new Vector2Int(month, year))
                {
                    Debug.Log(i);
                    _actieveKanskaarten.Add(KansKaartenArray[i]);
                    
                   
                        KanskaartUi.SetActive(true);
                        _inKanskaartMenu = true;


                        Button tempButton = KanskaartButton.GetComponent<Button>();
                        int tempCount = KansKaartenArray.Length;
                        tempButton.onClick.AddListener(() => this.OnClickAccept(tempCount));
                    

                    

                    //SetTextKanskaart(i);


                    //    switch (EventsArray[i].FieldType)
                    //    {
                    //        case NodeState.FieldTypeEnum.Corn:


                    //            SimpleMoneyManager.Instance.SetPercentage(NodeState.FieldTypeEnum.Corn,
                    //                EventsArray[i].InfluencePercentage);

                    //            break;
                    //        case NodeState.FieldTypeEnum.Carrot:
                    //            SimpleMoneyManager.Instance.SetPercentage(NodeState.FieldTypeEnum.Carrot,
                    //                EventsArray[i].InfluencePercentage);

                    //            break;
                    //        case NodeState.FieldTypeEnum.Nothing:
                    //            break;
                    //        case NodeState.FieldTypeEnum.Apple:
                    //            SimpleMoneyManager.Instance.SetPercentage(NodeState.FieldTypeEnum.Apple,
                    //                EventsArray[i].InfluencePercentage);
                    //            break;
                    //        case NodeState.FieldTypeEnum.Blackberries:
                    //            SimpleMoneyManager.Instance.SetPercentage(NodeState.FieldTypeEnum.Blackberries,
                    //                EventsArray[i].InfluencePercentage);
                    //            break;
                    //        case NodeState.FieldTypeEnum.Tomato:
                    //            SimpleMoneyManager.Instance.SetPercentage(NodeState.FieldTypeEnum.Tomato,
                    //                EventsArray[i].InfluencePercentage);
                    //            break;
                    //        case NodeState.FieldTypeEnum.Tree:
                    //            SimpleMoneyManager.Instance.SetPercentage(NodeState.FieldTypeEnum.Tree,
                    //                EventsArray[i].InfluencePercentage);
                    //            break;
                    //        case NodeState.FieldTypeEnum.Grapes:
                    //            SimpleMoneyManager.Instance.SetPercentage(NodeState.FieldTypeEnum.Grapes,
                    //                EventsArray[i].InfluencePercentage);
                    //            break;
                    //        default:
                    //            break;
                    //    }
                }

                //if (EventsArray[i].Finishes != new Vector2Int(month, year)) continue;

                //HeadlineUi.SetActive(false);
                //ExclamationMark.SetActive(false);
                //SetTextEnded(i);
            }
        }

        private void OnClickAccept(int index)
        {
            ChangeTextButton(index);
            KanskaartUi.SetActive(false);
            KanskaartPopup.SetActive(true);
            _actieveKanskaarten.RemoveAt(index - 1);
            if (_actieveKanskaarten.Count >= 1)
            {
                KanskaartUi.SetActive(true);
            }
            else
            {
                _inKanskaartMenu = false;
            }
        }

        private void ChangeTextButton(int index)
        {
            Debug.Log(index);
            KanskaartText.text = KansKaartenArray[index -1].Headline;
            if (KansKaartenArray[index -1].InfluencePercentage > 0 || KansKaartenArray[index - 1].InfluencePercentage < 0)
            {
                switch (KansKaartenArray[index - 1].FieldType)
                {
                    case NodeState.FieldTypeEnum.Corn:
                        break;
                    case NodeState.FieldTypeEnum.Carrot:
                        break;
                    case NodeState.FieldTypeEnum.Nothing:
                        break;
                    case NodeState.FieldTypeEnum.Apple:
                        break;
                    case NodeState.FieldTypeEnum.Blackberries:
                        break;
                    case NodeState.FieldTypeEnum.Tomato:
                        break;
                    case NodeState.FieldTypeEnum.Tree:
                        break;
                    case NodeState.FieldTypeEnum.Grapes:
                        break;
                    default:
                        break;
                }
                
            }
            else if (KansKaartenArray[index - 1].Reward > 0 || KansKaartenArray[index - 1].Reward < 0)
            {
                switch (KansKaartenArray[index - 1].FieldType)
                {
                    case NodeState.FieldTypeEnum.Corn:
                        SimpleMoneyManager.Instance.AddMoney(KansKaartenArray[index - 1].Reward);
                        break;
                    case NodeState.FieldTypeEnum.Carrot:
                        break;
                    case NodeState.FieldTypeEnum.Nothing:
                        break;
                    case NodeState.FieldTypeEnum.Apple:
                        break;
                    case NodeState.FieldTypeEnum.Blackberries:
                        break;
                    case NodeState.FieldTypeEnum.Tomato:
                        break;
                    case NodeState.FieldTypeEnum.Tree:
                        break;
                    case NodeState.FieldTypeEnum.Grapes:
                        break;
                    default:
                        break;
                }
            }
            //string effect = GetName(_eventsInInbox[index - 1].FieldType) + " " + _eventsInInbox[index - 1].InfluencePercentage + "%";

        }

        private void SetTextKanskaart(int whichcard)
        {
            KanskaartText.text = KansKaartenArray[whichcard].Headline;
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
