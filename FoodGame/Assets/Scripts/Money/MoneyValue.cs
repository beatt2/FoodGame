using UnityEngine;
using UnityScript.Macros;

namespace Money
{
    public class MoneyValue
    {

        public float Income;
        public float Expense;
        public float Percentage;



        public MoneyValue(float income,float expense, float percentage)
        {
            Income = income;
            Expense = expense;
            Percentage = percentage;
        }

    }
}
