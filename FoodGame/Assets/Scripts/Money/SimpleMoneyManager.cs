using System.Collections.Generic;
using System.Linq;
using Cultivations;
using Events;
using Node;
using Save;
using TimeSystem;
using Tools;
using UI;
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


        private readonly Dictionary<NodeState.FieldTypeEnum,List<MoneyValue>> _moneyValues = new Dictionary<NodeState.FieldTypeEnum, List<MoneyValue>>();
        private Dictionary<NodeState.FieldTypeEnum, float> _percentageValues = new Dictionary<NodeState.FieldTypeEnum, float>();


        private int _enviromentalValue;
        private int _happinessValue;

        public WaarschuwingScript WaarschuwingScript;

        // Use this for initialization
        private void Start ()
        {
            _currentMoney = SaveManager.Instance.GetMoney();
            MoneyUi.text = "€ " + _currentMoney;
            _currentMoney = 500000;

            _monthlyIncome = 0;
            _monthlyExpenses = 0;

            Income.text = "€ " + _monthlyIncome;
            Expense.text = "€ " + _monthlyExpenses;
            _percentageValues = SaveManager.Instance.GetPercentageValues();

        }

        public void ChangeMonth()
        {
            ChangeMoneyMonthly();
            CalculateEnviromentalAndHappiness();
        }

        private void CalculateEnviromentalAndHappiness()
        {
            int yearOffset = TimeManager.Instance.Year - 2018;
            _enviromentalValue = -(EventManager.Instance.GetEnviromentValue() * yearOffset);
            _happinessValue =  EventManager.Instance.GetHappinessValue() * yearOffset;
            AddMoney(_enviromentalValue);
            AddMoney(_happinessValue);
        }


        private void ChangeMoneyMonthly()
        {
            for (int i = 0; i < _moneyValues.Keys.Count; i++)
            {
                float tempTotal = 0;
                foreach (var t in _moneyValues.ElementAt(i).Value)
                {
                    if (t.MonthsToGrow == t.MonthCount)
                    {
                        tempTotal += t.Income;
                        t.MonthCount = 0;
                        t.MyCultivation.MonthCount = t.MonthCount;


                    }
                    else
                    {
                        t.MonthCount++;
                        t.MyCultivation.MonthCount = t.MonthCount;


                    }
                    t.MyCultivation.MonthCount = t.MonthCount;
                }

                float percentage = 0;
                if(_percentageValues.ContainsKey(_moneyValues.ElementAt(i).Key))
                {
                    percentage = tempTotal / 100 * _percentageValues.ElementAt(i).Value;
                }
                _currentMoney += tempTotal + percentage;
            }


        }
        public bool EnoughMoney(int value)
        {
            if (_currentMoney -value >= 0)
            {
                return true;
            }
            Waarschuwing("Sorry, niet genoeg geld");
            //Debug.Log("Sorry not enough money");
            return false;


        }

        public void Waarschuwing(string waarschuwing)
        {
            WaarschuwingScript.ChangeText(waarschuwing);
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

        public Dictionary<NodeState.FieldTypeEnum, List<MoneyValue>> GetMoneyValueDict()
        {
            return _moneyValues;
        }



        public void AddFinance(Cultivation cultivation, int oldMonthCount)
        {
            if (!_moneyValues.ContainsKey(cultivation.FieldType))
            {
                _moneyValues.Add(cultivation.FieldType, new List<MoneyValue>());
                _moneyValues[cultivation.FieldType].Add(new MoneyValue(cultivation, oldMonthCount));

            }
            else
            {
                _moneyValues[cultivation.FieldType].Add(new MoneyValue(cultivation, oldMonthCount));
            }

            for (int i = 0; i < _moneyValues.Keys.Count; i++)
            {

                foreach (var t in _moneyValues.ElementAt(i).Value)
                {
                    t.MyCultivation.MonthCount = t.MonthCount;

                }
            }
        }



        //TODO make it so that it will add and remove
        public void SetPercentage(NodeState.FieldTypeEnum fieldTypeEnum, float percentage)
        {
            if (!_percentageValues.ContainsKey(fieldTypeEnum))
            {
                _percentageValues.Add(fieldTypeEnum, percentage);
            }
            else
            {
                _percentageValues[fieldTypeEnum]  += percentage;
            }
        }

        public float GetPercentage(NodeState.FieldTypeEnum fieldTypeEnum)
        {
            if (_percentageValues.ContainsKey(fieldTypeEnum))
            {
                return _percentageValues[fieldTypeEnum];
            }
            return 0;
        }

        public Dictionary<NodeState.FieldTypeEnum, float> GetPercentageValues()
        {

            return _percentageValues;
        }

        public void SetPercentageValues(Dictionary<NodeState.FieldTypeEnum, float> percentagevalues)
        {
            _percentageValues = percentagevalues;
        }

        public float GetMoneyValue(NodeState.FieldTypeEnum fieldTypeEnum)
        {
            float tempIncome = 0;
            for (int i = 0; i < _moneyValues[fieldTypeEnum].Count; i++)
            {
                tempIncome += _moneyValues[fieldTypeEnum][i].Income;
            }
            return tempIncome;
        }


        public float GetExpense(NodeState.FieldTypeEnum fieldTypeEnum)
        {
            float tempExpense = 0;
            for (int i = 0; i < _moneyValues[fieldTypeEnum].Count; i++)
            {
                tempExpense += _moneyValues[fieldTypeEnum][i].Expense;
            }

            return tempExpense;
        }

        public float [] GetIncomeExpenseAndPercentage(NodeState.FieldTypeEnum fieldType)
        {
            float [] tempFloat = new float[3];
            tempFloat[0] = GetMoneyValue(fieldType);
            tempFloat[1] = GetExpense(fieldType);
            tempFloat[2] = GetPercentage(fieldType);
            return tempFloat;
        }

        public float GetCurrentMoney()
        {
            return _currentMoney;
        }

        public void RemoveValue(Cultivation cultivation)
        {
            if (_moneyValues.ContainsKey(cultivation.FieldType))
            {
                for (int i = 0; i < _moneyValues[cultivation.FieldType].Count; i++)
                {
                    if (_moneyValues[cultivation.FieldType][i].MyCultivation == cultivation)
                    {

                        _moneyValues[cultivation.FieldType].RemoveAt(i);
                    }
                }
            }



        }

        public void AddMoney(float value)
        {
            _currentMoney += value;
        }



        public void RemoveKey(NodeState.FieldTypeEnum fieldType)
        {
            _moneyValues.Remove(fieldType);
        }
    }
}
