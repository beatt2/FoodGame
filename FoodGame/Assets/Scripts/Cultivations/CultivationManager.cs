using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Money;
using Tools;

namespace Cultivations
{
    public class CultivationManager : Singleton<CultivationManager>
    {
        public enum CultivationType {Fruit, Vegetable , Farm };


        private readonly Dictionary<Enum,List<Cultivation>> _cultivations = new Dictionary<Enum,List<Cultivation>>();

        private void Start()
        {
            
            
            //TickPerMonth();
            //AddValue();
        }

        public void AddValue(Cultivation cultivation)
        {
            if (!SimpleMoneyManager.Instance.EnoughMoney(cultivation.BuildPrice)) return;
            SimpleMoneyManager.Instance.RemoveMoney(cultivation.BuildPrice);
            SimpleMoneyManager.Instance.AddMonthlyIncome(cultivation.MoneyTick);
            //TODO CHANGE THE WAY TO DO THIS
            SimpleMoneyManager.Instance.AddMonthlyExpenses(10);
            CheckForNull(cultivation.MyCultivationType);
            _cultivations[cultivation.MyCultivationType].Add(cultivation);
        }

        private void CheckForNull(CultivationType cultivationType)
        {
            if (_cultivations.ContainsKey(cultivationType)) return ;
            _cultivations.Add(cultivationType, new List<Cultivation>());

        }

        public void RemoveEntry(Cultivation cultivation)
        {
            _cultivations.Remove(cultivation.MyCultivationType);
        }

        public void TickPerMonth()
        {
            for (int i = 0; i < _cultivations.Keys.Count; i++)
            {
                for (int j = 0; j < _cultivations[(CultivationType) i].Count; j++)
                {
                    SimpleMoneyManager.Instance.AddFinance((CultivationType) i,_cultivations[(CultivationType)i][j].MoneyTick);
                }
            }
        }
    }
}
