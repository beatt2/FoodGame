using Events;
using Money;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace TimeSystem
{
    public class SimpleTime : Singleton<SimpleTime>
    {
        private float TimeStamp;

        public int Month = 1;
        public int Year;

        public float TimeToIncrease;

        public Text DateUI;
        // Update is called once per frame
        protected override void Awake()
        {
            Year = 2018;
        }

        private void Update ()
        {

            
            if (!EventManager.Instance.inEventMenu)
            {
                TimeStamp += TimeToIncrease * Time.deltaTime;
                //Debug.Log("Time " + TimeStamp + " Month " + Month + " Year " + Year);
                

                if (TimeStamp >= 10)
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
                            DateUI.text = "January " + Year;
                            break;
                        case 2:
                            DateUI.text = "February " + Year;
                            break;
                        case 3:
                            DateUI.text = "March " + Year;
                            break;
                        case 4:
                            DateUI.text = "April " + Year;
                            break;
                        case 5:
                            DateUI.text = "May " + Year;
                            break;
                        case 6:
                            DateUI.text = "June " + Year;
                            break;
                        case 7:
                            DateUI.text = "July " + Year;
                            break;
                        case 8:
                            DateUI.text = "August " + Year;
                            break;
                        case 9:
                            DateUI.text = "September " + Year;
                            break;
                        case 10:
                            DateUI.text = "October " + Year;
                            break;
                        case 11:
                            DateUI.text = "November " + Year;
                            break;
                        case 12:
                            DateUI.text = "December " + Year;
                            break;
                        default:
                            break;

                    }

                    EventManager.Instance.CheckDate(Month, Year);
                    SimpleMoneyManager.Instance.ChangeMonth();
                    TimeStamp = 0;
                }
            }
        }
    }
}
