using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cultivations;
using Events;
using Grid;
using Node;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Money
{
    public class Finance : MonoBehaviour {

        public GameObject Parent;
        public GameObject Content;
        private readonly List<FinanceEntry> _financeTexts = new List<FinanceEntry>();



        public GameObject RowPrefab;        
        private List<GameObject> _go = new List<GameObject>();


        private RectTransform _currentpos;
        public RectTransform StartingPos;
        private float _totalAmount;

        public float Gap;

        public Text TotalAmountText;

        public List<float> TotalAmount = new List<float>();

        private void Start()
        {
            
            _currentpos = StartingPos;
           

            for (int i = 0; i < CultivationManager.Instance.GetCultivations().Keys.Count; i++)
            {
                foreach (var tick in CultivationManager.Instance.GetCultivations().ElementAt(i).Value)
                {

                    switch (tick.FieldType)
                    {
                        case NodeState.FieldTypeEnum.Corn:
                         
                            
                            AddText("Corn",tick.MoneyTick,tick.ExpenseTick,EventManager.Instance.GetInfluence());
                            break;
                        case NodeState.FieldTypeEnum.Apple:
                            AddText("Apple", tick.MoneyTick, tick.ExpenseTick, EventManager.Instance.GetInfluence());
                            break;
                        case NodeState.FieldTypeEnum.Carrot:
                            AddText("Carrot", tick.MoneyTick, tick.ExpenseTick, EventManager.Instance.GetInfluence());
                            break;
                        case NodeState.FieldTypeEnum.Nothing:
                            break;
                       default:
                            break;


                    }
                }
            }
        }

        private void ChangeText(GameObject go, int i)
        {
            Row row = go.GetComponent<Row>();
            row.Name.text = _financeTexts[i].Name;
            row.Income.text = _financeTexts[i].Income.ToString();
            row.Expense.text = _financeTexts[i].Expense.ToString();

            //insert event getter
            //if event > 0 + else - or income/expense
            float eventPercentage = Random.Range(-10.0f, 10.0f) /100;
            float eventChange = _financeTexts[i].Income * eventPercentage;


            float eventAmount = 100 /_financeTexts[i].Income *eventChange ;
            eventAmount = Mathf.Round(eventAmount * 100) / 100;
            row.Eventpercentage.text = eventAmount +"%";

            float total =  _financeTexts[i].Income - _financeTexts[i].Expense + (eventPercentage * _financeTexts[i].Income);
            total = Mathf.Round(total * 100) / 100;
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

            
            //Debug.Log(_totalAmount);
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
            TotalAmountText.text = "€ " + _totalAmount;
        }
        public void RemoveText(GameObject go)
        {
            _go.Remove(go);
            Destroy(go);
            if (_go.Count <= 10)
            {

                RectTransform rectangle = Content.GetComponent<RectTransform>();
                rectangle.sizeDelta += new Vector2(0, -45);
            }

        }

        public void AddText(string name, float income, float expense, float eventPercentage)
        {
            
            _financeTexts.Add(new FinanceEntry(name,income,expense));


            GameObject go = Instantiate(RowPrefab, _currentpos.position, Quaternion.identity, Content.transform) as GameObject;
            _go.Add(go);
            _currentpos.position = new Vector3(_currentpos.position.x, _currentpos.position.y + Gap, _currentpos.position.z);
            
            if (_go.Count > 10)
            {

                RectTransform rectangle = Content.GetComponent<RectTransform>();
                rectangle.sizeDelta += new Vector2(0, 45);
            }
            for (int i = 0; i < _financeTexts.Count; i++)
            {

                ChangeText(go, i);



            }
        }
        public void UpdateText()
        {
            if (!EventManager.Instance.InEventMenu)
            {
                _totalAmount = 0;
                for (int i = 0; i < _financeTexts.Count; i++)
                {


                    ChangeText(_go[i], i);


                }
            }
            
        }

       
    }
}
