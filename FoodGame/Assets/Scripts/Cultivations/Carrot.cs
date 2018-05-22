using UnityEngine;

namespace Cultivations
{
    public class Carrot : Cultivation {
        public Carrot(string name, int sustainability, int moneyTick, int upgradeRank, int buildingPrice,
            NodeState.CurrentStateEnum cultivationType, NodeState.FieldTypeEnum fieldType)
            : base(name, sustainability, moneyTick, upgradeRank, buildingPrice, cultivationType, fieldType)
        {
        }    
    }
}
