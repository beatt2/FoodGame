using System;
using Node;


namespace Cultivations
{
    [Serializable]
    public class Plant : Cultivation
    {
        public Plant
        (
            string name,
            int sustainability,
            int moneyTick,
            float expenseTick,
            int upgradeRank,
            int buildingPrice,
            NodeState.CurrentStateEnum cultivationType,
            NodeState.FieldTypeEnum fieldType,
            int upgradeValue,
            int spriteIndex,
            int sidePanelSpriteIndex,
            int enviromentalValue,
            int happiness, int sizeRank,
            bool upgrade,
            int upgradeDuration,
            int monthCount
        )
            : base
            (
                name,
                sustainability,
                moneyTick,
                expenseTick,
                upgradeRank,
                buildingPrice,
                cultivationType,
                fieldType,
                upgradeValue,
                spriteIndex,
                sidePanelSpriteIndex,
                enviromentalValue,
                happiness,
                sizeRank,
                upgrade,
                upgradeDuration,
                monthCount
            )
        {
        }
    }
}
