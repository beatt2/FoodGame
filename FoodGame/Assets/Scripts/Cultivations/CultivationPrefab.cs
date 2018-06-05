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
        public int PlacementIndex;
        public NodeState.CurrentStateEnum MyCurrentState;
        public NodeState.FieldTypeEnum MyFieldType;
        public GameObject [] UpgradeOptions;


        public int EnviromentValue;
        public int Happyness;


    }
}
