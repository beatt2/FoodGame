using System;
using System.Collections.Generic;
using Events;
using UnityEngine;
using UnityEngine.UI;

namespace Money
{
    public class Finance : MonoBehaviour {
	
        // Update is called once per frame
        //public Text[] FinanceTexts;

        public GameObject Parent;
        public Font FontType;
        public GameObject Test;
        private readonly List<FinanceEntry> _financeTexts = new List<FinanceEntry>();
        //public Text[] Name;
        //public Text[] Income;
        //public Text[] Expense;
        //public Text[] Total;
        
        private void Start()
        {
            _financeTexts.Add(new FinanceEntry("Corn",50,25));
            _financeTexts.Add(new FinanceEntry("Carrot",40,10));
            //FinanceEntry data = new FinanceEntry("Corn", 50, 25);
            for (int i = 0; i < _financeTexts.Count; i++)
            {
                Debug.Log(_financeTexts[i].Name + " " + _financeTexts[i].Income + " " + _financeTexts[i].Expense);
                
            }
            AddTextToCanvas(_financeTexts[0].Name, Parent);


        }

        public void AddTextToCanvas(string textString, GameObject parent)
        {
            GameObject uiGo = new GameObject("newText"); //Instantiate(Test, Parent.transform) as GameObject; //
            uiGo.transform.SetParent(Parent.transform);

            //Text text = Test.GetComponent<Text>();
            Text text = uiGo.AddComponent<Text>();
            
            //Debug.Log(text.text);
            text.text = textString;

            //Font arialFont = (Font) Resources.GetBuiltinResource(typeof(Font), "Ariel.ttf");
            text.font = FontType;
            text.fontSize = 30;
            text.color = new Color(0.1f,.1f,.1f);
            text.alignment = TextAnchor.UpperCenter;
            //text.rectTransform
            //text.material = arialFont.material;

        }

        public void AddFinance()
        {

        }



        public void GetName()
        {
            //Name.Length + 1;
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
            
            //for (int i = 0; i < FinanceTexts.Length; i++)
            //{
            //    FinanceTexts[i].text = "Finance";
            //}
            //for (int i = 0; i < Name.Length; i++)
            //{
            //    Name[i].text = "NameHere";
            //}
            //for (int i = 0; i < Income.Length; i++)
            //{
            //    Income[i].text = "IncomeHere";
            //}
            //for (int i = 0; i < Expense.Length; i++)
            //{
            //    Expense[i].text = "ExpenseHere";
            //}
            //for (int i = 0; i < Total.Length; i++)
            //{
            //    Total[i].text = "TotalAmount";
            //}
        }

        
    }
}
