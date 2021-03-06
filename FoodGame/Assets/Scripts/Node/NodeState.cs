﻿using System;
using UnityEngine;

namespace Node
{
    [Serializable]
    public class NodeState : MonoBehaviour 
    {
        public enum CurrentStateEnum {Farm,Field, EmptyField, Empty}
        public enum FieldTypeEnum {Corn, Carrot, Nothing, Apple, Blackberries, Tomato , Grapes}

        public CurrentStateEnum CurrentState;
        public FieldTypeEnum FieldType;

        public void ChangeValues(NodeState nodeState)
        {
            CurrentState = nodeState.CurrentState;
            FieldType = nodeState.FieldType;
        }

        public void ChangeValues(CurrentStateEnum currentStateEnum, FieldTypeEnum fieldTypeEnum)
        {
            CurrentState = currentStateEnum;
            FieldType = fieldTypeEnum;
        }

        public void ResetSate()
        {
            CurrentState = CurrentStateEnum.Empty;
            FieldType = FieldTypeEnum.Nothing;
        }

    }
}
