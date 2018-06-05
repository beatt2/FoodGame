using UnityEngine;
using UnityScript.Macros;

namespace Money
{
    public class MoneyValue
    {

        public float Income;
        public float Percentage;


        public MoneyValue(float value, float percentage)
        {
            Income = value;
            Percentage = percentage;
        }

    }
}
