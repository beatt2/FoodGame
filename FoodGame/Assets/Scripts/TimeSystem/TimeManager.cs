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


        public Finance FinanceScript;
        // Update is called once per frame
        protected override void Awake()
        {
            base.Awake();
            Year = 2018;
        }

        public int GetMonth()
        {
            return Month;
        }



        public int GetYear()
        {
            return Year;
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
