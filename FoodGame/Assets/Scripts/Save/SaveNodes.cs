using System;
using Cultivations;
using Node;
using UnityEngine;

namespace Save
{
    [Serializable]
    public class SaveNodes
    {
        public readonly Cultivation MyCultivation;
        public readonly int ListIndex;
        public readonly bool EmptyCultivationField;
        public readonly NodeState.CurrentStateEnum CurrentState;
        public readonly NodeState.FieldTypeEnum FieldType;

        public bool FenceLeft;
        public bool FenceLeftOwner;
        
        public bool FenceRight;
        public bool FenceRightOwner;
        
        public bool FenceUp;
        public bool FenceUpOwner;
        
        public bool FenceDown;
        public bool FenceDownOwner;




        public SaveNodes(int listIndex, NodeState.CurrentStateEnum currentState,
            NodeState.FieldTypeEnum fieldType,
            bool emptyCultivationField, Cultivation myCultivation, bool fenceLeft, bool fenceLeftOwner,
            bool fenceRight, bool fenceRightOwner, bool fenceUp, bool fenceUpOwner, bool fenceDown, bool fenceDownOwner)
        {
            
            ListIndex = listIndex;
            CurrentState = currentState;
            FieldType = fieldType;
            EmptyCultivationField = emptyCultivationField;
            MyCultivation = myCultivation;
            FenceLeft = fenceLeft;
            FenceLeftOwner = fenceLeftOwner;
            FenceRight = fenceRight;
            FenceRightOwner = fenceRight;
            FenceUp = fenceUp;
            FenceUpOwner = fenceUpOwner;
            FenceDown = fenceDown;
            FenceDownOwner = fenceDownOwner;

        }
        public SaveNodes(int listIndex,NodeState.CurrentStateEnum currentState ,
            NodeState.FieldTypeEnum fieldType , bool emptyCultivationField)
        {
            
            ListIndex = listIndex;
            FieldType = fieldType;
            EmptyCultivationField = emptyCultivationField;
            CurrentState = currentState;

        }

  
    }
}