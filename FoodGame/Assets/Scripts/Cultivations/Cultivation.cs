
using UnityEditor;
using UnityEngine;

namespace Cultivations
{
    public abstract class Cultivation
    {
        public string Name;
        public int Sustainability;
        public int MoneyTick;// = value
        public int UpgradeRank;
        public int BuildPrice; 
        public Sprite Image;
        
        

        
        public NodeState.CurrentStateEnum MyCultivationState;
        public NodeState.FieldTypeEnum FieldType;

        protected Cultivation(string name, int sustainability, int moneyTick,int upgradeRank,int buildPrice,NodeState.CurrentStateEnum currentState, NodeState.FieldTypeEnum currentFieldType, Sprite image)
        {
            Name = name;
            Sustainability = sustainability;
            MoneyTick = moneyTick;
            UpgradeRank = upgradeRank;
            MyCultivationState = currentState;
            BuildPrice = buildPrice;
            FieldType = currentFieldType;
            Image = image;

        }

    }
}
