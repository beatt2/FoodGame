using System.Collections;
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

        public int WaitForSeconds;


        protected override void Awake()
        {
            base.Awake();
            Year = 2018;
            StartCoroutine("Timer");
        }

        public int GetMonth()
        {
            return Month;
        }

        public int GetYear()
        {
            return Year;
        }

        private IEnumerator Timer()
        {
            bool myLock = true;
            while (myLock)
            {
                yield return new WaitForSeconds(WaitForSeconds);
                if (Month >= 12)
                {
                    Month = 1;
                    Year++;
                }
                else
                {
                    Month++;
                }
                FinanceScript.UpdateText();
                EventManager.Instance.CheckDate(Month, Year);
                SimpleMoneyManager.Instance.ChangeMonth();

            }
        }
    }
}