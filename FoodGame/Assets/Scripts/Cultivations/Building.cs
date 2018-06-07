﻿using System;
using Node;
using UnityEngine;

namespace Cultivations
{
    [Serializable]
    public class Building : Cultivation
    {

        public Building(string name, int sustainability, int moneyTick,float expenseTick, int upgradeRank, int buildingPrice,
            NodeState.CurrentStateEnum cultivationType, NodeState.FieldTypeEnum fieldType,  int upgradeValue, int spriteIndex, int enviromentalValue, int happiness)
            : base(name, sustainability, moneyTick, expenseTick, upgradeRank, buildingPrice, cultivationType, fieldType, upgradeValue,  spriteIndex, enviromentalValue, happiness)
        {
        }
    }
}
