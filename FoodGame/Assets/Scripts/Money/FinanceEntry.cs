using System.Collections.Generic;
using UnityEngine;

namespace Money
{
    public class FinanceEntry
    {
        public string Name;
        public float Income;
        public float Expense;
        

        public FinanceEntry(string name, int income, int expense)
        {
            Name = name;
            Income = income;
            Expense = expense;
        }
    }
}
