using System;
using Node;
using UnityEngine;

namespace Cultivations
{
    [Serializable]
    public class Building : Cultivation
    {

        public Building(string name, int sustainability, int moneyTick,float expenseTick, int upgradeRank, int buildingPrice,
            NodeState.CurrentStateEnum cultivationType, NodeState.FieldTypeEnum fieldType, Sprite image, int upgradeValue, GameObject [] upgradeOptions, int placementIndex, int enviromentValue, int happyness)
            : base(name, sustainability, moneyTick, expenseTick, upgradeRank, buildingPrice, cultivationType, fieldType, image, upgradeValue, upgradeOptions, placementIndex, enviromentValue,happyness)
        {
        }
    }
}
