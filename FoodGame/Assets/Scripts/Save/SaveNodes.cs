using System;
using Cultivations;
using Node;
using UnityEngine;

namespace Save
{
    [Serializable]
    public class SaveNodes
    {
        public Cultivation MyCultivation;
        public int ListIndex;
        public NodeState NodeState;
        public bool EmptyCultivationField;
        public NodeState.CurrentStateEnum CurrentState;
        public NodeState.FieldTypeEnum FieldType;




        public SaveNodes( int listIndex, NodeState.CurrentStateEnum currentState ,
            NodeState.FieldTypeEnum fieldType,
            bool emptyCultivationField, Cultivation myCultivation)
        {
            
            ListIndex = listIndex;
            CurrentState = currentState;
            FieldType = fieldType;
            EmptyCultivationField = emptyCultivationField;
            MyCultivation = myCultivation;
        }
        public SaveNodes(int listIndex,NodeState.CurrentStateEnum currentState ,
            NodeState.FieldTypeEnum fieldType , bool emptyCultivationField)
        {
            
            ListIndex = listIndex;
            FieldType = fieldType;
            EmptyCultivationField = emptyCultivationField;
            EmptyCultivationField = emptyCultivationField;
            CurrentState = currentState;

        }

  
    }
}