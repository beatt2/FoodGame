using System.Collections.Generic;
using System.Linq;
using Node;
using Save;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Money
{
    public class SimpleMoneyManager : Singleton<SimpleMoneyManager>
    {
        private float _currentMoney = 50000;
        private float _monthlyIncome;
        private float _monthlyExpenses;

        public Text MoneyUi;



        public Text Income;

        public Text Expense;

        private float _fruitValue;
        private float _farmValue;
        private float _vegetablevalue;


        private float _cornValue;    
        //private float _


        private readonly Dictionary<NodeState.FieldTypeEnum,MoneyValue> _moneyValues = new Dictionary<NodeState.FieldTypeEnum, MoneyValue>();
        //TODO THIS IS FOR PROTOTYPE
        public bool ShowExpenses;


        // Use this for initialization
        private void Start ()
        {
            _currentMoney = SaveManager.Instance.GetMoney();
            MoneyUi.text = "€ " + _currentMoney;


            _monthlyIncome = 5;
            _monthlyExpenses = 10;

            Income.text = "€ " + _monthlyIncome;
            Expense.text = "€ " + _monthlyExpenses;
        }

        public void ChangeMonth()
        {
            ChangeMoneyMonthly();
        }


        public void ChangeMoneyMonthly()
        {

   
            for (int i = 0; i < _moneyValues.Keys.Count; i++)
            {

                var percentage = _moneyValues[_moneyValues.ElementAt(i).Key].Income / 100  * _moneyValues[_moneyValues.ElementAt(i).Key].Percentage;
                _currentMoney += _moneyValues[_moneyValues.ElementAt(i).Key].Income - _moneyValues[_moneyValues.ElementAt(i).Key].Expense + percentage ;
                
            }


        }
        public bool EnoughMoney(int value)
        {
            if (_currentMoney -value >= 0)
            {
                return true;
            }
            Debug.Log("Sorry not enough money");
            return false;


        }
        
        public void AddMonthlyIncome(int value)
        {
            _monthlyIncome += value;
            Income.text = _monthlyIncome.ToString();
        }

        public void AddMonthlyExpenses(int value)
        {
            _monthlyExpenses += value;
            Expense.text = _monthlyExpenses.ToString();
        }

        public void RemoveMoney(int value)
        {
            if (EnoughMoney(value))
            {
                _currentMoney -= value;
                MoneyUi.text = _currentMoney.ToString();
            }

        }

        public Dictionary<NodeState.FieldTypeEnum, MoneyValue> GetMoneyValueDict()
        {
            return _moneyValues;
        }

        public void AddFinance(NodeState.FieldTypeEnum fieldType,float income, float expense)
        {

            if (!_moneyValues.ContainsKey(fieldType))
            {
                _moneyValues.Add(fieldType, new MoneyValue(income , expense,0));
            }
            else
            {
                //Debug.Log( _moneyValues[fieldType].Income + " income");
                _moneyValues[fieldType].Income += income;
                //Debug.Log( _moneyValues[fieldType].Expense + " expense");
                _moneyValues[fieldType].Expense += expense;
            }
       

         
        }
        
        

        public void SetPercentage(NodeState.FieldTypeEnum fieldTypeEnum, float percentage)
        {
           
            _moneyValues[fieldTypeEnum].Percentage = percentage;
        }

        public float GetPercentage(NodeState.FieldTypeEnum fieldTypeEnum)
        {
            return _moneyValues[fieldTypeEnum].Percentage;
        }

        public float GetMoneyValue(NodeState.FieldTypeEnum fieldTypeEnum)
        {
            return _moneyValues[fieldTypeEnum].Income;
        }

        public float GetExpense(NodeState.FieldTypeEnum fieldTypeEnum)
        {
            return _moneyValues[fieldTypeEnum].Expense;
        }
        

        public float GetCurrentMoney()
        {
            return _currentMoney;
        }


 
        public void Remove(NodeState.FieldTypeEnum fieldType)
        {
            _moneyValues.Remove(fieldType);
        }
    }
}
