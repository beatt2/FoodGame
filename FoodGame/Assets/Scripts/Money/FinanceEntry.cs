using System.Collections.Generic;
using UnityEngine;

namespace Money
{
    public class FinanceEntry
    {
        public string Name;
        public string Income;
        public string Expense;

        public FinanceEntry(string name, int income, int expense)
        {
            Name = name;
            Income = income.ToString();
            Expense = expense.ToString();
        }
    }
}
