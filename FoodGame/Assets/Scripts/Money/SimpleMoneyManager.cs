using Tools;
using UnityEngine;
using UnityEngine.UI;

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
        // Use this for initialization
        private void Start ()
        {
            _currentMoney = 100;
            MoneyUi.text = "€ " + _currentMoney;
            

            _monthlyIncome = 5;//Random.Range(30,50);
            _monthlyExpenses = 10; //Random.Range(15,25);

            Income.text = "€ " + _monthlyIncome;
            Expense.text = "€ " + _monthlyExpenses;
        }

        public void ChangeMonth()
        {
            _monthlyIncome = Random.Range(5, 50);
            _monthlyExpenses = Random.Range(15, 50);
            if (_monthlyIncome < _monthlyExpenses)
            {
                Income.color = new Color(1, 0, 0);
            }
            else
            {
                Income.color = new Color(0, 0, 0);
            }

            if (_currentMoney <= 0)
            {
                MoneyUi.color = new Color(1, 0, 0);
            }
            else
            {
                MoneyUi.color = new Color(0, 0, 0);
            }
            
            ChangeMoneyMonthly(_monthlyIncome,_monthlyExpenses);
        }

        public void ChangeMoneyMonthly(float income, float expenses)
        {
            _currentMoney += income - expenses;
            Income.text = "+€ " + income;
            Expense.text = "-€ " + expenses;

            MoneyUi.text = "€ "+_currentMoney;
        }

        public void ChangeMoney(int amount)
        {

        }
    }
}
