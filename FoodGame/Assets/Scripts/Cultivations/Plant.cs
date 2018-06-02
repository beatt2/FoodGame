using UnityEngine;

namespace Cultivations
{
    public class Plant : Cultivation
    {
        public Plant(string name, int sustainability, int moneyTick,float expenseTick, int upgradeRank, int buildingPrice,
            NodeState.CurrentStateEnum cultivationType, NodeState.FieldTypeEnum fieldType, Sprite image, int upgradeValue, GameObject[] upgradeOptions, int placementIndex)
            : base(name, sustainability, moneyTick, expenseTick, upgradeRank, buildingPrice, cultivationType, fieldType, image, upgradeValue, upgradeOptions, placementIndex)
        {
        }

    }
}
