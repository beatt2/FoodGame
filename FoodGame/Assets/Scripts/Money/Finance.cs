using System;
using Events;
using UnityEngine;
using UnityEngine.UI;

namespace Money
{
    public class Finance : MonoBehaviour {
	
        // Update is called once per frame
        public Text[] FinanceTexts;

        public Text[] Name;
        public Text[] Income;
        public Text[] Expense;
        public Text[] Total;
        private void Update ()
        {
	    
        }

        public void AddFinance()
        {

        }



        public void GetName()
        {

        }

        public void GetIncome()
        {
            
        }

        public void GetExpense()
        {

        }

        public void GetTotal()
        {

        }

        public void UpdateText(Text[] texts)
        {
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].text = "testValue";
            }
        }

        
    }
}
