using System;
using Cultivations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityScript.Macros;

namespace Money
{
    [Serializable]
    public class MoneyValue
    {

        public float Income;
        public float Expense;
        public int MonthsToGrow;
        public int MonthCount = 0;
        public float Percentage;
        public Cultivation MyCultivation;



        public MoneyValue(Cultivation cultivation, int monthCount)
        {
            Income = cultivation.MoneyTick;
            Expense = cultivation.ExpenseTick;
            Percentage = 0;
            MonthCount = monthCount;
            MonthsToGrow = cultivation.MonthsToGrow;
            MyCultivation = cultivation;
        }

    }
}
