using System.Collections.Generic;
using System.Threading;
using Money;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Events
{
    public class EventManager : Singleton<EventManager>
    {
        public Events[] EventsArray;
        
        public Text Headline;
        public Text Content;
        public GameObject Ui;
        public bool InEventMenu;
        private readonly bool[] _eventGoingOn = new bool[10];

        private float _percentageEvent = 0;
        private void Start()
        {
            Ui.SetActive(InEventMenu);
        }

        public float GetInfluence()
        {
            return _percentageEvent;
        }

        public void CheckDate(int month,int year)
        {
            
            for (int i = 0; i < EventsArray.Length; i++)
            {
                if (EventsArray[i].Starts == new Vector2Int(month,year))
                {
                    
                    InEventMenu = true;
                    _eventGoingOn[i] = true;
                    Ui.SetActive(InEventMenu);
                    SetText(i);
                    
                    switch (i)
                    {
                        case 1:
                            Debug.Log("Increase Overall Income");
                           
                            SimpleMoneyManager.Instance.SetPercentage(NodeState.FieldTypeEnum.Corn, EventsArray[i].InfluencePercentage);
                           // _percentageEvent = EventsArray[i].InfluencePercentage;
                            break;
                        case 2:
                            Debug.Log("Increase overall income");
                            break;
                        case 3:
                            Debug.Log("decrease overall income");
                            break;
                        case 4:
                            Debug.Log("unhappy reviews");
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7:
                            break;

                        default:
                            break;
                    }
                }

                if (EventsArray[i].Finishes == new Vector2Int(month, year))
                {                   
                    _eventGoingOn[i] = false;
                    InEventMenu = true;
                    Ui.SetActive(InEventMenu);
                    SetTextEnded(i);
                }
            }
        }
        public void SetText(int whichevent)
        {
            Headline.text = EventsArray[whichevent].Headline;
            Content.text = EventsArray[whichevent].Content;      
        }

        public void SetTextEnded(int whichevent)
        {
            Headline.text = EventsArray[whichevent].Headline + " has ended";
            Content.text = EventsArray[whichevent].Content;        
        }

    }
}
