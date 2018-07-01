using System;
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
    
        public string Effect;
        public Vector2Int Starts;
        public Vector2Int Finishes;
        public FieldTypes[] MyFieldTypes;

        public int Enviromental;
        public int Happiness;

    }

    [Serializable]
    public struct FieldTypes
    {
        public NodeState.FieldTypeEnum FieldType;
        public float InfluencePercentage;
    }



}
