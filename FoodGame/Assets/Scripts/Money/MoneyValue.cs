using UnityEngine;
using UnityScript.Macros;

namespace Money
{
    public class MoneyValue
    {

        public float Value;
        public float Percentage;


        public MoneyValue(float value, float percentage)
        {
            Value = value;
            Percentage = percentage;
        }

    }
}
