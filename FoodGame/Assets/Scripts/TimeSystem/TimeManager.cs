using System;
using System.Collections;
using System.Xml.Schema;
using Cultivations;
using Events;
using Money;
using Save;
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

       


  

        private void Start()
        {
            CalculateNewTime();
            StartCoroutine("Timer");
        }

        private void CalculateNewTime()
        {
            DateTime currentTime = DateTime.Now;
            DateTime oldTime = SaveManager.Instance.GetStopTime();
            Debug.Log(oldTime);
            TimeSpan temp = currentTime.Subtract(oldTime);
            Debug.Log(temp.TotalSeconds);    
            int totalAddedMonths =(int)temp.TotalSeconds / WaitForSeconds;

            Month = SaveManager.Instance.GetSaveMonth() == 0 ? 1 : SaveManager.Instance.GetSaveMonth();
            Year = SaveManager.Instance.GetSaveMonth() == 0 ? 2018 : SaveManager.Instance.GetSaveYear();

            Year = 2018;
            Month = 1;
            for (int i = 0; i < totalAddedMonths; i++)
            {
                if (Month >= 12)
                {
                    Month = 1;
                    Year++;
                }
                else
                {
                    Month++;
                }
            }
         



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