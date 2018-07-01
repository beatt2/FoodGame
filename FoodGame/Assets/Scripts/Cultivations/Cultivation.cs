using System;
using Node;

namespace Cultivations
{

    [Serializable]
    public abstract class Cultivation
    {
        public readonly string Name;
        public int MoneyTick;// = value
        public readonly float ExpenseTick;
        public readonly int MonthsToGrow;
        public int BuildPrice;
        public readonly int UpgradeValue;
        public readonly int SpriteIndex;
        public readonly int SidePanelSpriteIndex;
        public  int EnviromentValue;
        public  int Happiness;
        public int SizeRank;
        public int MonthCount;
        public readonly bool Upgrade;
        public int UpgradeDuration;
        public readonly int UpgradePrefabIndex;
        public NodeState.CurrentStateEnum MyCultivationState;
        public NodeState.FieldTypeEnum FieldType;

        protected Cultivation
        (
            string name,
            int upgradePrefabIndex,
            int moneyTick,
            float expenseTick,
            int monthsToGrow,
            int buildPrice,
            NodeState.CurrentStateEnum currentState,
            NodeState.FieldTypeEnum currentFieldType,
            int upgradeValue,
            int spriteIndex,
            int sidePanelSpriteIndex,
            int enviromentValue,
            int happiness,
            int sizeRank,
            bool upgrade ,
            int upgradeDuration,
            int monthCount
        )
        {
            Name = name;
            MoneyTick = moneyTick;
            ExpenseTick = expenseTick;
            MyCultivationState = currentState;
            BuildPrice = buildPrice;
            UpgradeValue = upgradeValue;
            SpriteIndex = spriteIndex;
            SidePanelSpriteIndex = sidePanelSpriteIndex;
            EnviromentValue = enviromentValue;
            Happiness = happiness;
            MonthsToGrow = monthsToGrow;
            SizeRank = sizeRank;
            Upgrade = upgrade;
            UpgradeDuration = upgradeDuration;
            UpgradePrefabIndex = upgradePrefabIndex;
            FieldType = currentFieldType;
            MonthCount = monthCount;

        }

    }
}
