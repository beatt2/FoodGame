
using UnityEditor;

namespace Cultivations
{
    public abstract class Cultivation
    {
        public string Name;
        public int Sustainability;
        public int MoneyTick;// = value
        public int UpgradeRank;
        public int BuildPrice;
        
        

        
        public NodeState.CurrentStateEnum MyCultivationState;
        public NodeState.FieldTypeEnum FieldType;

        protected Cultivation(string name, int sustainability, int moneyTick,int upgradeRank,int buildPrice,NodeState.CurrentStateEnum currentState, NodeState.FieldTypeEnum currentFieldType)
        {
            Name = name;
            Sustainability = sustainability;
            MoneyTick = moneyTick;
            UpgradeRank = upgradeRank;
            MyCultivationState = currentState;
            BuildPrice = buildPrice;
            FieldType = currentFieldType;
        }

    }
}
