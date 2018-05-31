using UnityEngine;

namespace Cultivations
{
    public abstract class CultivationPrefab : MonoBehaviour
    {
        public string Name;
        public int Sustainability;
        public int MoneyTick;
        public float ExpenseTick;
        public int UpgradeRank;
        public int BuildingPrice;
        public int UpgradeValue;
        public NodeState.CurrentStateEnum MyCurrentState;
        public NodeState.FieldTypeEnum MyFieldType;
        public GameObject [] UpgradeOptions;



    }
}
