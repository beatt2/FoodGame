using System;
using Node;
using UnityEngine;
using Random = System.Random;

namespace KansKaarten
{
    [CreateAssetMenu]


    public class Kanskaarten : ScriptableObject
    {

        [TextArea] public string PreInsert;
        public bool Insert;
        public string AfterInsert;
        
        
        public Vector2Int Starts;

        public bool PrefabUpgrade;

        public NodeState.FieldTypeEnum FieldType
        {
            get
            {
                if (!Insert) return FieldType;
                var values = Enum.GetValues(typeof(NodeState.FieldTypeEnum));
                Random random = new Random();
                NodeState.FieldTypeEnum randomFieldType = (NodeState.FieldTypeEnum) values.GetValue(random.Next(values.Length));
                if (randomFieldType == NodeState.FieldTypeEnum.Nothing)
                {
                    randomFieldType = (NodeState.FieldTypeEnum) values.GetValue(random.Next(values.Length));
                }
                return randomFieldType;
            }
        }
        public NodeState.CurrentStateEnum Type;
        public enum BuildingSize
        {
            All = 0,
            Small = 2,
            Medium = 1,
            Big = 3
        }

        public BuildingSize Size;

        public string Effect;

        public float InfluencePercentage;
        public float Reward;
        public int EnviromentInfluence;
        public int HappinessInfluence;
        public int IncomePercentageIncrease;


    }
}
