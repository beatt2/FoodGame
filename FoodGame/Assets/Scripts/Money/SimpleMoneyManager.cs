using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Money
{
    public class SimpleMoneyManager : Singleton<SimpleMoneyManager>
    {
        private float _currentMoney = 50000000;
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
            _currentMoney = 5000;
            MoneyUi.text = "€ " + _currentMoney;


            _monthlyIncome = 5;
            _monthlyExpenses = 10;

            Income.text = "€ " + _monthlyIncome;
            Expense.text = "€ " + _monthlyExpenses;
        }

        public void ChangeMonth()
        {

            Income.color = _monthlyIncome < _monthlyExpenses ? Color.red : Color.black;
            MoneyUi.color = _currentMoney < 0 ? Color.red : Color.black;

            ChangeMoneyMonthly(_monthlyIncome,_monthlyExpenses);
        }


        public void ChangeMoneyMonthly(float income, float expenses)
        {
            _currentMoney += income - expenses;

            if (ShowExpenses)
            {
                Income.text = "+€ " + income;
                Expense.text = "-€ " + expenses;
            }


            MoneyUi.text = "€ "+ _currentMoney;
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

        public void AddFinance(NodeState.FieldTypeEnum fieldType,float value)
        {
            _moneyValues.Add(fieldType, new MoneyValue(value , 0));

            _moneyValues[NodeState.FieldTypeEnum.Corn].Percentage = .5f;

            _monthlyIncome += value;
        }

        public float GetCurrentMoney()
        {
            return _currentMoney;
        }


        public void Add(NodeState.FieldTypeEnum fieldType, float value,float percentage)
        {
            
            _moneyValues.Add(fieldType,new MoneyValue(value,percentage));
        }

        public void Remove(NodeState.FieldTypeEnum fieldType)
        {
            _moneyValues.Remove(fieldType);
        }
    }
}
