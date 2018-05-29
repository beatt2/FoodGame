using System;
using System.Collections;
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
        public GameObject[] VerticalParents;
        public GameObject Content;
        public Font FontType;
        public GameObject Test;
        private readonly List<FinanceEntry> _financeTexts = new List<FinanceEntry>();
        private bool _changeContent;

        private List<GameObject> _uiGoText = new List<GameObject>();

        //public Text[] Name;
        //public Text[] Income;
        //public Text[] Expense;
        //public Text[] Total;

        private void Start()
        {
            _financeTexts.Add(new FinanceEntry("Corn",50,25));
            _financeTexts.Add(new FinanceEntry("Carrot",40,10));
            _financeTexts.Add(new FinanceEntry("Apples", 50, 10));
            _financeTexts.Add(new FinanceEntry("Strawberry", 20, 25));
            _financeTexts.Add(new FinanceEntry("Wheat", 10, 2));

            //FinanceEntry data = new FinanceEntry("Corn", 50, 25);
            for (int i = 0; i < _financeTexts.Count; i++)
            {
                _changeContent = true;
                AddTextvertical(_financeTexts[i].Name, VerticalParents[0],i);
                AddTextvertical(_financeTexts[i].Income.ToString(), VerticalParents[1],i);
                AddTextvertical(_financeTexts[i].Expense.ToString(), VerticalParents[2],i);
                AddTextvertical("EVENT", VerticalParents[3],i);

                float total = _financeTexts[i].Income - _financeTexts[i].Expense;
                AddTotal(total.ToString(), VerticalParents[4],i);
                //AddPanel(Parent, i);
            }            
        }
        private void AddTextvertical(string textString, GameObject parent,int i)
        {
            _uiGoText.Add(new GameObject("newText"));

           
            
                _uiGoText[i].transform.SetParent(parent.transform);
                Text text = gameObject.AddComponent(typeof(Text)) as Text;


                text.text = textString;


                text.font = FontType;
                text.fontSize = 30;
                text.color = new Color(0.1f, .1f, .1f);
                text.alignment = TextAnchor.UpperCenter;
                if (_financeTexts.Count > 6 && _changeContent)
                {
                    _changeContent = false;
                    RectTransform rectangle = Content.GetComponent<RectTransform>();
                    rectangle.sizeDelta += new Vector2(0, 40);
                } 

            

            
            

        }

        private void AddTotal(string textString, GameObject parent, int i)
        {
            _uiGoText[i] = new GameObject("newText");
            _uiGoText[i].transform.SetParent(parent.transform);

            Text text = _uiGoText[i].AddComponent<Text>();


            text.text = "€ " + textString;


            text.font = FontType;
            text.fontSize = 30;
            text.color = new Color(0.1f, .1f, .1f);
            text.alignment = TextAnchor.UpperCenter;
            if (_financeTexts[i].Income > _financeTexts[i].Expense)
            {
                text.color = new Color(0, 0.5f, 0);
            }
            else
            {
                text.color = new Color(1, 0, 0);
            }
        }

        public void UpdateText()
        {

            for (int i = 0; i < _financeTexts.Count; i++)
            {

                //_financeTexts.RemoveAt(i);

                _changeContent = true;
                AddTextvertical(_financeTexts[i].Name, VerticalParents[0],i);
                AddTextvertical(_financeTexts[i].Income.ToString(), VerticalParents[1],i);
                AddTextvertical(_financeTexts[i].Expense.ToString(), VerticalParents[2],i);
                AddTextvertical("EVENT", VerticalParents[3],i);

                float total = _financeTexts[i].Income - _financeTexts[i].Expense;
                AddTotal(total.ToString(), VerticalParents[4], i);
                //AddPanel(Parent, i);
            }
        }



        //public void AddPanel(GameObject parent, int number)
        //{
        //    GameObject uiGo = new GameObject("newPanel");        
        //    uiGo.transform.SetParent(parent.transform);
        //    uiGo.AddComponent<CanvasRenderer>();
        //    HorizontalLayoutGroup horizon = uiGo.AddComponent<HorizontalLayoutGroup>();
        //    horizon.spacing = -50;

        //    AddTextToCanvas(_financeTexts[number].Name, uiGo);
        //    AddTextToCanvas(_financeTexts[number].Income.ToString(), uiGo);
        //    AddTextToCanvas(_financeTexts[number].Expense.ToString(), uiGo);
        //    AddTextToCanvas("EVENT", uiGo);

        //    float total = _financeTexts[number].Income - _financeTexts[number].Expense;
        //    AddTextToCanvas(total.ToString(), uiGo);
        //}



        //private void AddTextToCanvas(string textString, GameObject parent)
        //{
        //    GameObject uiGoText = new GameObject("newText");
        //    uiGoText.transform.SetParent(parent.transform);


        //    Text text = uiGoText.AddComponent<Text>();


        //    text.text = textString;


        //    text.font = FontType;
        //    text.fontSize = 30;
        //    text.color = new Color(0.1f,.1f,.1f);
        //    text.alignment = TextAnchor.UpperCenter;


        //}

    }
}
