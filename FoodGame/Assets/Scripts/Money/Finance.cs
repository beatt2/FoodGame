
using System.Collections.Generic;

using System.Linq;

using Events;
using Node;
using UnityEngine;
using UnityEngine.UI;


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

        private void ChangeText(GameObject go, int i , float percentage)
        {
            Row row = go.GetComponent<Row>();
            row.Name.text = _financeTexts[i].Name;
            row.Income.text = _financeTexts[i].Income.ToString();
            row.Expense.text = _financeTexts[i].Expense.ToString();

            Debug.Log("FinanceTexts " + _financeTexts.Count);
            //insert event getter
            //if event > 0 + else - or income/expense
            float eventPercentage = percentage /100;
            float eventChange = _financeTexts[i].Income * eventPercentage;


            float eventAmount = 100 /_financeTexts[i].Income *eventChange ;
            eventAmount = Mathf.Round(eventAmount * 100) / 100;
            row.Eventpercentage.text = eventAmount +"%";

            float total =  _financeTexts[i].Income - _financeTexts[i].Expense + (eventPercentage * _financeTexts[i].Income);
            
            total = Mathf.Round(total * 100) / 100;
            row.Total.text = "€ " + total;
            Debug.Log(total);

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
        public void RemoveText()
        {
            for (int i = 0; i < _financeTexts.Count; i++)
            {
                StartingPos.position = new Vector3(StartingPos.position.x, -Gap,0);
                

                Destroy(_go[i]);
                _go.RemoveAt(i);
                _financeTexts.RemoveAt(i);

                _currentpos = StartingPos;
            }
            

        }
        
        public void CheckForText()
        {
            _currentpos = StartingPos;
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
            
           
            for (int i = 0; i < _financeTexts.Count; i++)
            {

                ChangeText(go, i, eventPercentage);



            }
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


                           
                            ChangeText(_go[i], i, SimpleMoneyManager.Instance.GetPercentage(NodeState.FieldTypeEnum.Corn));
                            break;
                        case NodeState.FieldTypeEnum.Carrot:

                           
                            ChangeText(_go[i], i, SimpleMoneyManager.Instance.GetPercentage(NodeState.FieldTypeEnum.Carrot));
                            break;
                        case NodeState.FieldTypeEnum.Nothing:
                            break;
                        case NodeState.FieldTypeEnum.Apple:
                            ChangeText(_go[i], i, SimpleMoneyManager.Instance.GetPercentage(NodeState.FieldTypeEnum.Apple));
                            
                            break;
                        case NodeState.FieldTypeEnum.Blackberries:
                            ChangeText(_go[i], i, SimpleMoneyManager.Instance.GetPercentage(NodeState.FieldTypeEnum.Blackberries));
                           
                            break;
                        case NodeState.FieldTypeEnum.Tomato:
                            ChangeText(_go[i], i, SimpleMoneyManager.Instance.GetPercentage(NodeState.FieldTypeEnum.Tomato));
                           
                            break;
                        case NodeState.FieldTypeEnum.Tree:
                            ChangeText(_go[i], i, SimpleMoneyManager.Instance.GetPercentage(NodeState.FieldTypeEnum.Tree));
                           
                            break;
                        case NodeState.FieldTypeEnum.Grapes:
                            ChangeText(_go[i], i, SimpleMoneyManager.Instance.GetPercentage(NodeState.FieldTypeEnum.Grapes));
                           
                            break;
                        default:
                            break;
                    }
                }
            }
            
        }




    }
}
