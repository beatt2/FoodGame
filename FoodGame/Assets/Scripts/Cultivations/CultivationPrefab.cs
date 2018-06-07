using System;
using Node;
using UnityEngine;

namespace Cultivations
{
    [Serializable]
    public abstract class CultivationPrefab : MonoBehaviour
    {
        public string Name;
        public int Sustainability;
        public int MoneyTick;
        public float ExpenseTick;
        public int UpgradeRank;
        public int BuildingPrice;
        public int UpgradeValue;
        public int SpriteIndex;
        public NodeState.CurrentStateEnum MyCurrentState;
        public NodeState.FieldTypeEnum MyFieldType;
        


        public int EnviromentValue;
        public int Happiness;


    }
}
