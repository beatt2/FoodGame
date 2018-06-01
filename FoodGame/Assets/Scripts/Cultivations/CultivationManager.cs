﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Money;
using Tools;
using UnityEngine;

namespace Cultivations
{
    public class CultivationManager : Singleton<CultivationManager>
    {
      
        
  


        private readonly Dictionary<Enum,List<Cultivation>> _cultivations = new Dictionary<Enum,List<Cultivation>>();


        public Dictionary<Enum, List<Cultivation>> GetCultivations()
        {
            return _cultivations;
        }

        private void Start()
        {
            //
            
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
            CheckForNull(cultivation.MyCultivationState);
            _cultivations[cultivation.MyCultivationState].Add(cultivation);
        }

        private void CheckForNull(NodeState.CurrentStateEnum currentState)
        {
            if (_cultivations.ContainsKey(currentState)) return ;
            _cultivations.Add(currentState, new List<Cultivation>());

        }

        public void RemoveEntry(Cultivation cultivation)
        {
            //TODO STILL HAS TO LINK TO MONEYMANAGER
            _cultivations[cultivation.MyCultivationState].Remove(cultivation);
        }

        public void TickPerMonth()
        {
            for (int i = 0; i < _cultivations.Keys.Count; i++)
            {
                foreach (var tick in _cultivations.ElementAt(i).Value)
                {
                    SimpleMoneyManager.Instance.AddFinance
                    (
                        (NodeState.FieldTypeEnum)_cultivations.ElementAt(i).Key,tick.MoneyTick
                    );
                }
            }
        }
    }
}    
