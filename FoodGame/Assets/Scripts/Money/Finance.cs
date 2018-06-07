
using System.Collections.Generic;

using System.Linq;

using Events;
using Node;
using UnityEngine;
using UnityEngine.UI;


namespace Money
{
    public class Finance : MonoBehaviour {

        public GameObject Content;
        private readonly List<FinanceEntry> _financeTexts = new List<FinanceEntry>();



        public GameObject RowPrefab;        
        private readonly List<GameObject> _go = new List<GameObject>();


        private RectTransform _currentpos;
        public RectTransform StartingPos;
        private float _totalAmount;

        public float Gap;

        public Text TotalAmountText;

        private string _name;
        private float _eventPercentage;
        
        private string GetName(NodeState.FieldTypeEnum fieldType)
        {
            _name = "Null";

            switch (fieldType)
            {
                case NodeState.FieldTypeEnum.Corn:
                    _name = "Maïs";
                    break;
                case NodeState.FieldTypeEnum.Carrot:
                    _name = "Wortel";
                    break;
                case NodeState.FieldTypeEnum.Nothing:
                    _name = "Null";
                    break;
                case NodeState.FieldTypeEnum.Apple:
                    _name = "Appel";
                    break;
                case NodeState.FieldTypeEnum.Blackberries:
                    _name = "Bramen";
                    break;
                case NodeState.FieldTypeEnum.Tomato:
                    _name = "Tomaten";
                    break;
                case NodeState.FieldTypeEnum.Tree:
                    _name = "Bomen";
                    break;
                case NodeState.FieldTypeEnum.Grapes:
                    _name = "Druiven";
                    break;
            }

            return _name;
        }

        private void Start()
        {
            _currentpos = StartingPos;
        }

        
        
        public void CheckForText()
        {
            _totalAmount = 0;
            
            //_currentpos = StartingPos;
            for (int i = 0; i < SimpleMoneyManager.Instance.GetMoneyValueDict().Keys.Count; i++)
            {
                

                    switch (SimpleMoneyManager.Instance.GetMoneyValueDict().ElementAt(i).Key)
                {
                    case NodeState.FieldTypeEnum.Corn:
                        
                        
                            AddText(GetName(NodeState.FieldTypeEnum.Corn), SimpleMoneyManager.Instance.GetMoneyValue(NodeState.FieldTypeEnum.Corn), SimpleMoneyManager.Instance.GetExpense(NodeState.FieldTypeEnum.Corn), SimpleMoneyManager.Instance.GetPercentage(NodeState.FieldTypeEnum.Corn));
                        
                        break;
                    case NodeState.FieldTypeEnum.Carrot:
                        
                            AddText(GetName(NodeState.FieldTypeEnum.Carrot), SimpleMoneyManager.Instance.GetMoneyValue(NodeState.FieldTypeEnum.Carrot), SimpleMoneyManager.Instance.GetExpense(NodeState.FieldTypeEnum.Carrot), SimpleMoneyManager.Instance.GetPercentage(NodeState.FieldTypeEnum.Carrot));
                        
                        break;
                    case NodeState.FieldTypeEnum.Nothing:
                        break;
                    case NodeState.FieldTypeEnum.Apple:
                        AddText(GetName(NodeState.FieldTypeEnum.Apple), SimpleMoneyManager.Instance.GetMoneyValue(NodeState.FieldTypeEnum.Apple), SimpleMoneyManager.Instance.GetExpense(NodeState.FieldTypeEnum.Apple), SimpleMoneyManager.Instance.GetPercentage(NodeState.FieldTypeEnum.Apple));
                        break;
                    case NodeState.FieldTypeEnum.Blackberries:
                        AddText(GetName(NodeState.FieldTypeEnum.Blackberries), SimpleMoneyManager.Instance.GetMoneyValue(NodeState.FieldTypeEnum.Blackberries), SimpleMoneyManager.Instance.GetExpense(NodeState.FieldTypeEnum.Blackberries), SimpleMoneyManager.Instance.GetPercentage(NodeState.FieldTypeEnum.Blackberries));
                        break;
                    case NodeState.FieldTypeEnum.Tomato:
                        AddText(GetName(NodeState.FieldTypeEnum.Tomato), SimpleMoneyManager.Instance.GetMoneyValue(NodeState.FieldTypeEnum.Tomato), SimpleMoneyManager.Instance.GetExpense(NodeState.FieldTypeEnum.Tomato), SimpleMoneyManager.Instance.GetPercentage(NodeState.FieldTypeEnum.Tomato));
                        break;
                    case NodeState.FieldTypeEnum.Tree:
                        AddText(GetName(NodeState.FieldTypeEnum.Tree), SimpleMoneyManager.Instance.GetMoneyValue(NodeState.FieldTypeEnum.Tree), SimpleMoneyManager.Instance.GetExpense(NodeState.FieldTypeEnum.Tree), SimpleMoneyManager.Instance.GetPercentage(NodeState.FieldTypeEnum.Tree));
                        break;
                    case NodeState.FieldTypeEnum.Grapes:
                        AddText(GetName(NodeState.FieldTypeEnum.Grapes), SimpleMoneyManager.Instance.GetMoneyValue(NodeState.FieldTypeEnum.Grapes), SimpleMoneyManager.Instance.GetExpense(NodeState.FieldTypeEnum.Grapes), SimpleMoneyManager.Instance.GetPercentage(NodeState.FieldTypeEnum.Grapes));
                        break;
                    default:
                        break;
                }
            }
        }

        public void AddText(string name, float income, float expense, float eventPercentage)
        {
            _financeTexts.Add(new FinanceEntry(name,income,expense));


            GameObject go = Instantiate(RowPrefab, _currentpos.position, Quaternion.identity, Content.transform) as GameObject;
            _go.Add(go);
            _currentpos.position = new Vector3(_currentpos.position.x, _currentpos.position.y + Gap, _currentpos.position.z);
           
            ChangeText(go, name,income,expense,eventPercentage);
        }

        private float CalculatePercentage(float percentageAmount, float income)
        {
            

            float eventPercentage = percentageAmount / 100;
            float eventChange = income * eventPercentage;


            float eventAmount = 100 / income * eventChange;
            eventAmount = Mathf.Round(eventAmount * 100) / 100;



            return eventAmount;
        }

        private void ChangeText(GameObject go, string name, float income, float expense, float percentage)
        {
            Row row = go.GetComponent<Row>();
            row.Name.text = name;
            row.Income.text = income.ToString();
            row.Expense.text = expense.ToString();


            
            row.Eventpercentage.text = percentage + "%";
            
            float total = income - expense + CalculatePercentage(percentage, income);

            total = Mathf.Round(total * 100) / 100;
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
            Debug.Log(_go.Count);
            for (int i = 0; i < _financeTexts.Count; i++)
            {


                
                
                
                

                _currentpos.position = new Vector3(_currentpos.position.x, _currentpos.position.y - Gap, _currentpos.position.z);
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
            if (!EventManager.Instance.InEventMenu)
            {
                _totalAmount = 0;
                for (int i = 0; i < _financeTexts.Count; i++)
                {


                    

                    switch (SimpleMoneyManager.Instance.GetMoneyValueDict().ElementAt(i).Key)
                    {
                        case NodeState.FieldTypeEnum.Corn:


                           
                            ChangeText(_go[i],GetName(NodeState.FieldTypeEnum.Corn), SimpleMoneyManager.Instance.GetMoneyValue(NodeState.FieldTypeEnum.Corn), SimpleMoneyManager.Instance.GetExpense(NodeState.FieldTypeEnum.Corn), SimpleMoneyManager.Instance.GetPercentage(NodeState.FieldTypeEnum.Corn));
                            break;
                        case NodeState.FieldTypeEnum.Carrot:


                            ChangeText(_go[i], GetName(NodeState.FieldTypeEnum.Carrot),
                                SimpleMoneyManager.Instance.GetMoneyValue(NodeState.FieldTypeEnum.Carrot),
                                SimpleMoneyManager.Instance.GetExpense(NodeState.FieldTypeEnum.Carrot),
                                SimpleMoneyManager.Instance.GetPercentage(NodeState.FieldTypeEnum.Carrot));
                            break;
                        case NodeState.FieldTypeEnum.Nothing:
                            break;
                        case NodeState.FieldTypeEnum.Apple:
                            ChangeText(_go[i], GetName(NodeState.FieldTypeEnum.Apple),
                                SimpleMoneyManager.Instance.GetMoneyValue(NodeState.FieldTypeEnum.Apple),
                                SimpleMoneyManager.Instance.GetExpense(NodeState.FieldTypeEnum.Apple),
                                SimpleMoneyManager.Instance.GetPercentage(NodeState.FieldTypeEnum.Apple));

                            break;
                        case NodeState.FieldTypeEnum.Blackberries:
                            ChangeText(_go[i], GetName(NodeState.FieldTypeEnum.Blackberries),
                                SimpleMoneyManager.Instance.GetMoneyValue(NodeState.FieldTypeEnum.Blackberries),
                                SimpleMoneyManager.Instance.GetExpense(NodeState.FieldTypeEnum.Blackberries),
                                SimpleMoneyManager.Instance.GetPercentage(NodeState.FieldTypeEnum.Blackberries));

                            break;
                        case NodeState.FieldTypeEnum.Tomato:
                            ChangeText(_go[i], GetName(NodeState.FieldTypeEnum.Tomato),
                                SimpleMoneyManager.Instance.GetMoneyValue(NodeState.FieldTypeEnum.Tomato),
                                SimpleMoneyManager.Instance.GetExpense(NodeState.FieldTypeEnum.Tomato),
                                SimpleMoneyManager.Instance.GetPercentage(NodeState.FieldTypeEnum.Tomato));

                            break;
                        case NodeState.FieldTypeEnum.Tree:
                            ChangeText(_go[i], GetName(NodeState.FieldTypeEnum.Tree),
                                SimpleMoneyManager.Instance.GetMoneyValue(NodeState.FieldTypeEnum.Tree),
                                SimpleMoneyManager.Instance.GetExpense(NodeState.FieldTypeEnum.Tree),
                                SimpleMoneyManager.Instance.GetPercentage(NodeState.FieldTypeEnum.Tree));

                            break;
                        case NodeState.FieldTypeEnum.Grapes:
                            ChangeText(_go[i], GetName(NodeState.FieldTypeEnum.Grapes),
                                SimpleMoneyManager.Instance.GetMoneyValue(NodeState.FieldTypeEnum.Grapes),
                                SimpleMoneyManager.Instance.GetExpense(NodeState.FieldTypeEnum.Grapes),
                                SimpleMoneyManager.Instance.GetPercentage(NodeState.FieldTypeEnum.Grapes));

                            break;
                        default:
                            break;
                    }
                }
            }
            
        }




    }
}
