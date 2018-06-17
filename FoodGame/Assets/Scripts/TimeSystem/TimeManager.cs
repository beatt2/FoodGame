using System;
using System.Collections;
using System.Linq;
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

        public Finance FinanceScript;

        public int WaitForSeconds;
        public bool InFinanceMenu = false;

        private int _totalAddedMonths;

         
        

        public void Start()
        {
            CalculateNewTime();
            StartCoroutine("Timer");
    
        }

        private void CalculateNewTime()
        {
            DateTime currentTime = DateTime.Now;
            DateTime oldTime = SaveManager.Instance.GetStopTime();
            TimeSpan temp = currentTime.Subtract(oldTime);
            int totalAddedMonths =(int)temp.TotalSeconds / WaitForSeconds;
            _totalAddedMonths = totalAddedMonths;
            Month = SaveManager.Instance.GetSaveMonth() == 0 ? 1 : SaveManager.Instance.GetSaveMonth();
            Year = SaveManager.Instance.GetSaveMonth() == 0 ? 2018 : SaveManager.Instance.GetSaveYear();
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

        public void CalculateMoney()
        {
            var moneyValues = SimpleMoneyManager.Instance.GetMoneyValueDict();
            float tempTotal =0;
            
            for (int i = 0; i < _totalAddedMonths; i++)
            {
                CultivationManager.Instance.MonthlyTick();
                for (int j = 0; j < moneyValues.Keys.Count; j++)
                {
                    foreach (var t in moneyValues.ElementAt(j).Value)
                    {
                        if (t.MonthsToGrow == t.MonthCount)
                        {
                            tempTotal += t.Income;
                            t.MonthCount = 0;
                            t.MyCultivation.MonthCount = t.MonthCount;                           
                        }
                        else
                        {
                            t.MonthCount++;                 
                        }

                    }
                    float percentage = 0;
                    if(SimpleMoneyManager.Instance.GetPercentageValues().ContainsKey(moneyValues.ElementAt(j).Key))
                    {
                        percentage = tempTotal / 100 *
                                     SimpleMoneyManager.Instance.GetPercentageValues().ElementAt(j).Value;
                    }
                    SimpleMoneyManager.Instance.AddMoney(tempTotal + percentage);
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

                if (InFinanceMenu)
                {
                    FinanceScript.UpdateText();
                }

                EventManager.Instance.CheckDate(Month, Year);
                SimpleMoneyManager.Instance.ChangeMonth();
                CultivationManager.Instance.MonthlyTick();
            }
        }
    }
}
