using Cultivations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityScript.Macros;

namespace Money
{
    public class MoneyValue
    {

        public float Income;
        public float Expense;
        public int MonthsToGrow;
        public int MonthCount = 0;
        public float Percentage;
        public Cultivation MyCultivation;



        public MoneyValue(Cultivation cultivation)
        {
            Income = cultivation.MoneyTick;
            Expense = cultivation.ExpenseTick;
            Percentage = 0;
            MonthsToGrow = cultivation.MonthsToGrow;
            MyCultivation = cultivation;
        }

    }
}
