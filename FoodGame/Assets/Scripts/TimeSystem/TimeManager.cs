using System;
using System.Collections;
using System.Linq;
using Cultivations;
using Events;
using Money;
using Save;
using Tools;
using UnityEngine;


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

        private int _savedWaitForSeconds;

         
        

        public void Start()
        {
            
            StopAllCoroutines();
            CalculateNewTime();
            SaveManager.Instance.LoadMessagesAndReviews();
            if (SaveManager.Instance.GetWaitForSeconds() > 1)
            {
                WaitForSeconds = SaveManager.Instance.GetWaitForSeconds();
                _savedWaitForSeconds = WaitForSeconds;

            }
    
            StartCoroutine("Timer");
    
        }

        public void OnPlayButton()
        {
            StopAllCoroutines();
            SetWaitForSeconds(WaitForSeconds == _savedWaitForSeconds? int.MaxValue : _savedWaitForSeconds);
            StartCoroutine("Timer");
        }

        public bool GamePaused()
        {
            return WaitForSeconds != _savedWaitForSeconds;
        }
        

        public void SetWaitForSeconds(int value)
        {
            WaitForSeconds = value;
        }

        public int GetWaitForSeconds()
        {
            return WaitForSeconds;
        }

        private void CalculateNewTime()
        {
            DateTime currentTime = DateTime.Now;
            DateTime oldTime = SaveManager.Instance.GetStopTime();
            TimeSpan temp = currentTime.Subtract(oldTime);
            int totalAddedMonths =(int)temp.TotalSeconds / WaitForSeconds;
            _totalAddedMonths = totalAddedMonths;
            Month = SaveManager.Instance.GetSaveMonth() == 0 ? 1 : SaveManager.Instance.GetSaveMonth();
            Year = SaveManager.Instance.GetSaveYear() == 0 ? 2018 : SaveManager.Instance.GetSaveYear();
            Debug.Log(Year);
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

        public int GetTotalAddedMonths()
        {
            return _totalAddedMonths;
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
                        percentage = tempTotal / 100  * SimpleMoneyManager.Instance.GetPercentageValues().ElementAt(j).Value;
                        
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
