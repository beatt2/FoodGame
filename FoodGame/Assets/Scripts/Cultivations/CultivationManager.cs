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

        public CultivationType CultType;
        Dictionary<Enum,Cultivation > _cultivations = new Dictionary<Enum, Cultivation>();

        private void Start()
        {
            
            
            //TickPerMonth();
            AddValue();
        }

        public void AddValue(Cultivation cultivation)
        {
            _cultivations.Add(cultivation.MyCultivationType, cultivation);
            
        }

        public void RemoveValue(Cultivation cultivation)
        {
            _cultivations.Remove(cultivation.MyCultivationType);
        }

        public void TickPerMonth()
        {
            
            foreach (var cultivation in _cultivations)
            {
                //CultType = (CultivationType) cultivation.Key;
                //Debug.Log(cultivation.Key + " " + cultivation.Value);

                SimpleMoneyManager.Instance.AddFinance((CultivationType) cultivation.Key, cultivation.Value.MoneyTick);
            }
        }
    }
}
