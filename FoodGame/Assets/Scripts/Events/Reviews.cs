﻿using Node;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu]
    public class Reviews : ScriptableObject
    {
        public string Headline;
        [TextArea]
        public string PreInsert;

        public string Insert;

        [TextArea]
        public string AfterInsert;

        public bool Enviromental;
        public int EffectValue;
        
        public float InfluencePercentage;

        public NodeState.FieldTypeEnum FieldType;
        



    }
}