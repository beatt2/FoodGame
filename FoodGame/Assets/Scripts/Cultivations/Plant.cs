using System;
using Node;
using UnityEngine;

namespace Cultivations
{
    [Serializable]
    public class Plant : Cultivation
    {
        public Plant(string name, int sustainability, int moneyTick,float expenseTick, int upgradeRank, int buildingPrice,
            NodeState.CurrentStateEnum cultivationType, NodeState.FieldTypeEnum fieldType, int upgradeValue, int spriteIndex, int enviromentalValue, int happiness)
            : base(name, sustainability, moneyTick, expenseTick, upgradeRank, buildingPrice, cultivationType, fieldType,  upgradeValue, spriteIndex, enviromentalValue, happiness)
        {
        }

    }
}
