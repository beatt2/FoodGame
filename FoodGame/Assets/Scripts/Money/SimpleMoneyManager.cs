using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Money
{
    public class SimpleMoneyManager : Singleton<SimpleMoneyManager>
    {
        private int _currentMoney;
        private int MonthlyIncome;
        private int MonthlyExpenses;

        public Text moneyUI;

        public Text Income;

        public Text Expense;
        // Use this for initialization
        void Start ()
        {
            _currentMoney = 100;
            moneyUI.text = "€ " + _currentMoney;
            

            MonthlyIncome = 5;//Random.Range(30,50);
            MonthlyExpenses = 10; //Random.Range(15,25);

            Income.text = "€ " + MonthlyIncome;
            Expense.text = "€ " + MonthlyExpenses;
        }
	
        // Update is called once per frame
        void Update ()
        {
            
            
        }

        public void ChangeMonth()
        {
            MonthlyIncome = Random.Range(5, 50);
            MonthlyExpenses = Random.Range(15, 50);
            if (MonthlyIncome < MonthlyExpenses)
            {
                Income.color = new Color(1, 0, 0);
            }
            else
            {
                Income.color = new Color(0, 0, 0);
            }

            if (_currentMoney <= 0)
            {
                moneyUI.color = new Color(1, 0, 0);
            }
            else
            {
                moneyUI.color = new Color(0, 0, 0);
            }
            
            ChangeMoneyMonthly(MonthlyIncome,MonthlyExpenses);
        }

        public void ChangeMoneyMonthly(int income, int expenses)
        {
            _currentMoney += income - expenses;
            Income.text = "+€ " + income;
            Expense.text = "-€ " + expenses;

            moneyUI.text = "€ "+_currentMoney;
        }

        public void ChangeMoney(int amount)
        {

        }
    }
}
