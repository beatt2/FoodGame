using System.Collections.Generic;
using System.Linq;
using Node;
using UnityEngine;
using UnityEngine.UI;


namespace Money
{
    public class Finance : MonoBehaviour
    {
        public GameObject Content;
        private readonly List<FinanceEntry> _financeTexts = new List<FinanceEntry>();


        public GameObject RowPrefab;
        private readonly List<GameObject> _go = new List<GameObject>();


        private RectTransform _currentpos;
        public RectTransform StartingPos;
        private float _totalAmount;

        public float Gap;

        public Text TotalAmountText;


        private float _eventPercentage;

        public static string GetName(NodeState.FieldTypeEnum fieldType)
        {
            string name = "";

            switch (fieldType)
            {
                case NodeState.FieldTypeEnum.Corn:
                    name = "Maïs";
                    break;
                case NodeState.FieldTypeEnum.Carrot:
                    name = "Wortel";
                    break;
                case NodeState.FieldTypeEnum.Nothing:
                    name = "Null";
                    break;
                case NodeState.FieldTypeEnum.Apple:
                    name = "Appel";
                    break;
                case NodeState.FieldTypeEnum.Blackberries:
                    name = "Bramen";
                    break;
                case NodeState.FieldTypeEnum.Tomato:
                    name = "Tomaten";
                    break;
                case NodeState.FieldTypeEnum.Tree:
                    name = "Bomen";
                    break;
                case NodeState.FieldTypeEnum.Grapes:
                    name = "Druiven";
                    break;
            }

            return name;
        }

        private void Start()
        {
            _currentpos = StartingPos;
        }


        public void CheckForText()
        {
            _totalAmount = 0;


            for (int i = 0; i < SimpleMoneyManager.Instance.GetMoneyValueDict().Keys.Count; i++)
            {
                var tempMoneyDict = SimpleMoneyManager.Instance.GetMoneyValueDict().ElementAt(i);
                var  tempMoneyValues = SimpleMoneyManager.Instance.GetIncomeExpenseAndPercentage(tempMoneyDict.Key);
                AddText(GetName(tempMoneyDict.Key),  tempMoneyValues[0],  tempMoneyValues[1],  tempMoneyValues[2]);
             }
        }

        public void AddText(string name, float income, float expense, float eventPercentage)
        {
            _financeTexts.Add(new FinanceEntry(name, income, expense));
            GameObject go = Instantiate(RowPrefab, _currentpos.position, Quaternion.identity, Content.transform) as GameObject;
            _go.Add(go);
            _currentpos.position =  new Vector3(_currentpos.position.x, _currentpos.position.y + Gap, _currentpos.position.z);
            ChangeText(go, name, income, expense, eventPercentage);
        }

        private float CalculatePercentage(float percentageAmount, float income)
        {
            float eventPercentage = percentageAmount / 100;
            float eventChange = income * eventPercentage;


            //float eventAmount = 100 / income * eventChange;
            //eventAmount = Mathf.Round(eventAmount * 100) / 100;

Debug.Log(eventChange);
            return eventChange;
        }

        private void ChangeText(GameObject go, string name, float income, float expense, float percentage)
        {
            Row row = go.GetComponent<Row>();
            row.Name.text = name;
            row.Income.text = income.ToString();
            row.Expense.text = expense.ToString();


            row.Eventpercentage.text = percentage + "%";

            Debug.Log(income + "income");
            Debug.Log(expense + "expesne");
            float total = income - expense + CalculatePercentage(percentage, income);

            total = Mathf.Round(total);
            row.Total.text = "€ " + total;

            row.Total.color = income > expense ? new Color(0, 0.5f, 0) : new Color(1, 0, 0);
            TotalAmountText.text = "€ " + UpdateTotalAmount(total);
        }

        private float UpdateTotalAmount(float amount)
        {
            _totalAmount += amount;


            if (_totalAmount > 0)
            {
                TotalAmountText.color = new Color(0, 0.5f, 0);
            }
            else
            {
                TotalAmountText.color = new Color(1, 0, 0);
            }


            return _totalAmount;
        }

        public void RemoveText()
        {
            for (int i = 0; i < _financeTexts.Count; i++)
            {
                _currentpos.position = new Vector3(_currentpos.position.x, _currentpos.position.y - Gap,
                    _currentpos.position.z);
                //_currentpos = StartingPos;
            }

            _financeTexts.Clear();

            foreach (var t in _go)
            {
                Destroy(t);
            }

            _go.Clear();
        }


        public void UpdateText()
        {
            _totalAmount = 0;
            for (int i = 0; i < _financeTexts.Count; i++)
            {
                var tempMoneyDict = SimpleMoneyManager.Instance.GetMoneyValueDict().ElementAt(i);
                var  tempMoneyValues = SimpleMoneyManager.Instance.GetIncomeExpenseAndPercentage(tempMoneyDict.Key);
                ChangeText(_go[i], GetName(tempMoneyDict.Key),tempMoneyValues[0], tempMoneyValues[1],  tempMoneyValues[2]);
            }

        }
    }
}
