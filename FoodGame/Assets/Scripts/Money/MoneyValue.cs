using System;
using Cultivations;

namespace Money
{
    [Serializable]
    public class MoneyValue
    {

        public readonly float Income;
        public readonly float Expense;
        public readonly int MonthsToGrow;
        public int MonthCount;
        public readonly Cultivation MyCultivation;



        public MoneyValue(Cultivation cultivation, int monthCount)
        {
            Income = cultivation.MoneyTick;
            Expense = cultivation.ExpenseTick;
            MonthCount = monthCount;
            MonthsToGrow = cultivation.MonthsToGrow;
            MyCultivation = cultivation;
        }

    }
}
