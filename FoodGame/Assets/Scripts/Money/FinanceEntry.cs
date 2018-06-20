namespace Money
{
    public class FinanceEntry
    {
        private string _name;
        private float _income;
        private float _expense;


        public FinanceEntry(string name, float income, float expense)
        {
            _name = name;
            _income = income;
            _expense = expense;
        }
    }
}
