using Node;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu]
    public class Events : ScriptableObject
    {
        public string Headline;
        [TextArea]
        public string Content;
        public Vector2Int Starts;
        public Vector2Int Finishes;
        public NodeState.FieldTypeEnum FieldType;
        public NodeState.CurrentStateEnum Type;
        public float InfluencePercentage;
    }



}
