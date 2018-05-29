using Cultivations;
using Events;
using Money;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace TimeSystem
{
    public class TimeManager : Singleton<TimeManager>
    {
        private float _timeStamp;

        public int Month = 1;
        public int Year;

        public float TimeToIncrease;

        public Text DateUi;

        public Finance FinanceScript;
        // Update is called once per frame
        protected override void Awake()
        {
            Year = 2018;
        }

        
        //TODO REWORK THIS
        private void FixedUpdate ()
        {
 
            if (!EventManager.Instance.InEventMenu)
            {
                _timeStamp += TimeToIncrease * Time.deltaTime;
                if (_timeStamp >= 10)
                {
                    
                    if (Month >= 12)
                    {
                        Month = 1;
                        if (Year >= 2040)
                        {
                            Debug.Log("EndGame");
                        }
                        else
                        {
                            Year++;
                        }

                    }
                    else
                    {
                        Month++;
                    }

                    switch (Month)
                    {
                        case 1:
                            DateUi.text = "January " + Year;
                            break;
                        case 2:
                            DateUi.text = "February " + Year;
                            break;
                        case 3:
                            DateUi.text = "March " + Year;
                            break;
                        case 4:
                            DateUi.text = "April " + Year;
                            break;
                        case 5:
                            DateUi.text = "May " + Year;
                            break;
                        case 6:
                            DateUi.text = "June " + Year;
                            break;
                        case 7:
                            DateUi.text = "July " + Year;
                            break;
                        case 8:
                            DateUi.text = "August " + Year;
                            break;
                        case 9:
                            DateUi.text = "September " + Year;
                            break;
                        case 10:
                            DateUi.text = "October " + Year;
                            break;
                        case 11:
                            DateUi.text = "November " + Year;
                            break;
                        case 12:
                            DateUi.text = "December " + Year;
                            break;
                        default:
                            break;

                    }
                    FinanceScript.UpdateText();
                    EventManager.Instance.CheckDate(Month, Year);
                    SimpleMoneyManager.Instance.ChangeMonth();
                    CultivationManager.Instance.TickPerMonth();
                    _timeStamp = 0;
                }
            }
        }
    }
}
