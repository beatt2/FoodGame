using System;
using Node;
using UnityEngine;

namespace Cultivations
{
    [Serializable]
    public abstract class CultivationPrefab : MonoBehaviour
    {
        public string Name;
        public int MoneyTick;
        public float ExpenseTick;
        public int MonthsToGrow;
        public int BuildingPrice;
        public int UpgradeValue;
        public int SpriteIndex;
        public int SidePanelSpriteIndex;
        public NodeState.CurrentStateEnum MyCurrentState;
        public NodeState.FieldTypeEnum MyFieldType;
        public int EnviromentValue;
        public int Happiness;
        public int SizeRank;
        public bool Upgrade;
        public int UpgradeDuration;
        public int UpgradePrefabIndex;
        public int MonthCount;






    }
}
