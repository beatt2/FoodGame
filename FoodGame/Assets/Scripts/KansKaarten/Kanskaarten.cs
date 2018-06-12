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
        public float InfluencePercentage;
        public float Reward;

    }
}
