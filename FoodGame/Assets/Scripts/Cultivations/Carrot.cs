using UnityEngine;

namespace Cultivations
{
    public class Carrot : Cultivation {
        public Carrot(string name, int sustainability, int moneyTick,float expenseTick, int upgradeRank, int buildingPrice,
            NodeState.CurrentStateEnum cultivationType, NodeState.FieldTypeEnum fieldType, Sprite image, int upgradeValue, GameObject [] upgradeOptions)
            : base(name, sustainability, moneyTick, expenseTick, upgradeRank, buildingPrice, cultivationType, fieldType, image, upgradeValue, upgradeOptions)
        {
        }    
    }
}
