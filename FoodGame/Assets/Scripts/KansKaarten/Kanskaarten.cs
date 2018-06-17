using Cultivations;
using Node;
using UnityEngine;

namespace KansKaarten
{
    [CreateAssetMenu]


    public class Kanskaarten : ScriptableObject
    {

        public string Headline;
        public Vector2Int Starts;

        public NodeState.FieldTypeEnum FieldType;
        public NodeState.CurrentStateEnum Type;
        public enum BuildingSize
        {
            Small,Medium,Big
        }

        public BuildingSize Size;

        public float InfluencePercentage;
        public float Reward;
        public float EnviromentInfluence;
        public float HappynessInfluence;

    }
}
