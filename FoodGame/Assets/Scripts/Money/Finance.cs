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

        public GameObject RowPrefab;
        
        private List<GameObject> _go = new List<GameObject>();


        private RectTransform _currentpos;
        public RectTransform StartingPos;
        private float _totalAmount;

        public float Gap;

        public Text TotalAmountText;

        public List<float> TotalAmount = new List<float>();
        //public Text[] Name;
        //public Text[] Income;
        //public Text[] Expense;
        //public Text[] Total;

        private void Start()
        {
            _currentpos = StartingPos;

            _financeTexts.Add(new FinanceEntry("Corn",50,25));
            _financeTexts.Add(new FinanceEntry("Carrot",40,10));
            _financeTexts.Add(new FinanceEntry("Apples", 50, 10));
            _financeTexts.Add(new FinanceEntry("Strawberry", 20, 25));
            _financeTexts.Add(new FinanceEntry("Wheat", 10, 2));
            _financeTexts.Add(new FinanceEntry("Corn", 50, 25));
            _financeTexts.Add(new FinanceEntry("Carrot", 40, 10));
            _financeTexts.Add(new FinanceEntry("Apples", 50, 10));
            _financeTexts.Add(new FinanceEntry("Strawberry", 20, 25));
            _financeTexts.Add(new FinanceEntry("Wheat", 10, 2));
            _financeTexts.Add(new FinanceEntry("Corn", 50, 25));
            _financeTexts.Add(new FinanceEntry("Carrot", 40, 10));
            _financeTexts.Add(new FinanceEntry("Apples", 50, 10));
            _financeTexts.Add(new FinanceEntry("Strawberry", 20, 25));
            _financeTexts.Add(new FinanceEntry("Wheat", 10, 2));
            _financeTexts.Add(new FinanceEntry("Corn", 50, 25));
            _financeTexts.Add(new FinanceEntry("Carrot", 40, 10));
            _financeTexts.Add(new FinanceEntry("Apples", 50, 10));
            _financeTexts.Add(new FinanceEntry("Strawberry", 20, 25));
            _financeTexts.Add(new FinanceEntry("Wheat", 10, 2));

            for (int i = 0; i < _financeTexts.Count; i++)
            {

                GameObject go = Instantiate(RowPrefab, _currentpos.position, Quaternion.identity, Content.transform) as GameObject;
                _go.Add(go);

                

                _currentpos.position = new Vector3(_currentpos.position.x, _currentpos.position.y +Gap, _currentpos.position.z);

                
                ChangeText(go, i);
                Debug.Log(_go.Count);
                if (_go.Count > 10)
                {
                    
                    RectTransform rectangle = Content.GetComponent<RectTransform>();
                    rectangle.sizeDelta += new Vector2(0, 45);
                }

                //_changeContent = true;
                //AddTextvertical(_financeTexts[i].Name, VerticalParents[0],i);
                //AddTextvertical(_financeTexts[i].Income.ToString(), VerticalParents[1],i+1);
                //AddTextvertical(_financeTexts[i].Expense.ToString(), VerticalParents[2],i+2);
                //AddTextvertical("EVENT", VerticalParents[3],i+3);

                //float total = _financeTexts[i].Income - _financeTexts[i].Expense;
                //AddTotal(total.ToString(), VerticalParents[4],i+4, _financeTexts[i].Income, _financeTexts[i].Expense);
                ////AddPanel(Parent, i);
            }            
        }

        public void ChangeText(GameObject go, int i)
        {
            Row row = go.GetComponent<Row>();
            row.Name.text = _financeTexts[i].Name;
            row.Income.text = _financeTexts[i].Income.ToString();
            row.Expense.text = _financeTexts[i].Expense.ToString();

            float total =  _financeTexts[i].Income - _financeTexts[i].Expense;
            row.Total.text = "€ " + total;
            if (_financeTexts[i].Income > _financeTexts[i].Expense)
            {
               row.Total.color = new Color(0, 0.5f, 0);
            }
            else
            {
               row.Total.color = new Color(1, 0, 0);
            }
            TotalAmount.Add(total);
            UpdateTotalAmount();
        }

        public void UpdateTotalAmount()
        {
            
            for (int i = 0; i < TotalAmount.Count; i++)
            {
              _totalAmount += TotalAmount[i];
            }

            TotalAmountText.text = "€ " + _totalAmount;
            if (_totalAmount > 0)
            {
                TotalAmountText.color = new Color(0, 0.5f, 0);
            }
            else
            {
                TotalAmountText.color = new Color(1, 0, 0);
            }
            for (int i = 0; i < TotalAmount.Count; i++)
            {
                TotalAmount.RemoveAt(i);
            }
        }
        //private void AddTextvertical(string textString, GameObject parent,int i)
        //{
        //    //int test = 0;
            
        //    _uiGoText.Add(new GameObject("newText" + i));

           
            
        //        _uiGoText[i].transform.SetParent(parent.transform);
        //        Text text = _uiGoText[i].AddComponent(typeof(Text)) as Text;


        //        text.text = textString;


        //        text.font = FontType;
        //        text.fontSize = 30;
        //        text.color = new Color(0.1f, .1f, .1f);
        //        text.alignment = TextAnchor.UpperCenter;
        //        if (_financeTexts.Count > 6 && _changeContent)
        //        {
        //            _changeContent = false;
        //            RectTransform rectangle = Content.GetComponent<RectTransform>();
        //            rectangle.sizeDelta += new Vector2(0, 40);
        //        }




        //    //test++;

        //}

        //private void AddTotal(string textString, GameObject parent, int i, float income, float expense)
        //{
        //    _uiGoText.Add(new GameObject("newText"));
        //    _uiGoText[i].transform.SetParent(parent.transform);

        //    Text text = _uiGoText[i].AddComponent<Text>();


        //    text.text = "€ " + textString;


        //    text.font = FontType;
        //    text.fontSize = 30;
        //    text.color = new Color(0.1f, .1f, .1f);
        //    text.alignment = TextAnchor.UpperCenter;
        //    if (income > expense)
        //    {
        //        text.color = new Color(0, 0.5f, 0);
        //    }
        //    else
        //    {
        //        text.color = new Color(1, 0, 0);
        //    }
        //}
        public void Remove(GameObject go)
        {
            _go.Remove(go);
        }
        public void UpdateText()
        {

            for (int i = 0; i < _financeTexts.Count; i++)
            {

                ////_financeTexts.RemoveAt(i);

                //GameObject go = Instantiate(RowPrefab, _currentpos.position, Quaternion.identity, Content.transform) as GameObject;
                //_go.Add(go);
                


                _currentpos.position = new Vector3(_currentpos.position.x, _currentpos.position.y + Gap, _currentpos.position.z);


                ChangeText(_go[i], i);


                //_uiGoText.RemoveAt(i);
                //_changeContent = true;
                //AddTextvertical(_financeTexts[i].Name, VerticalParents[0],i);
                //AddTextvertical(_financeTexts[i].Income.ToString(), VerticalParents[1],i+1);
                //AddTextvertical(_financeTexts[i].Expense.ToString(), VerticalParents[2],i+2);
                //AddTextvertical("EVENT", VerticalParents[3],i+3);

                //float total = _financeTexts[i].Income - _financeTexts[i].Expense;
                //AddTotal(total.ToString(), VerticalParents[4], i+4, _financeTexts[i].Income, _financeTexts[i].Expense);
                ////AddPanel(Parent, i);
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
