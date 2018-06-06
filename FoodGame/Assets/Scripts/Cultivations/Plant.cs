using System;
using Node;
using UnityEngine;

namespace Cultivations
{
    [Serializable]
    public class Plant : Cultivation
    {
        public Plant(string name, int sustainability, int moneyTick,float expenseTick, int upgradeRank, int buildingPrice,
            NodeState.CurrentStateEnum cultivationType, NodeState.FieldTypeEnum fieldType, Sprite image, int upgradeValue, GameObject[] upgradeOptions, int spriteIndex)
            : base(name, sustainability, moneyTick, expenseTick, upgradeRank, buildingPrice, cultivationType, fieldType, image, upgradeValue, upgradeOptions, spriteIndex)
        {
        }

    }
}
