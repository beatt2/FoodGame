using System;
using System.Globalization;
using Cultivations;
using Tools;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Money
{
    public class SimpleMoneyManager : Singleton<SimpleMoneyManager>
    {
        private float _currentMoney;
        private float _monthlyIncome;
        private float _monthlyExpenses;

        public Text MoneyUi;

        
        
        public Text Income;

        public Text Expense;

        private float _fruitValue;
        private float _farmValue;
        private float _vegetablevalue;
        
        
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

        public void AddFinance(NodeState.FieldTypeEnum fieldType,float value)
        {
            //Debug.Log("tick");
            switch (fieldType)
                {
                    case NodeState.FieldTypeEnum.Carrot:
                        _vegetablevalue += value;
                        //Debug.Log("Fruit " + _fruitValue);
                    break;
                    case NodeState.FieldTypeEnum.Corn:
                        _vegetablevalue += value;
                        //Debug.Log("Vegetable " + _vegetablevalue);
                    break;
                    case NodeState.FieldTypeEnum.Nothing:
                        break;
                case NodeState.FieldTypeEnum.Apple:
                    _fruitValue += value;
                    break;
                    default:
                        break;
                }

            _monthlyIncome += value;




        }
    }
}
