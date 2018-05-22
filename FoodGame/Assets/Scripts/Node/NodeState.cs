using UnityEngine;

public class NodeState : MonoBehaviour 
{
    public enum CurrentStateEnum {Farm,Field, EmptyField, Empty}
    public enum FieldTypeEnum {Corn, Carrot, Nothing}

    public CurrentStateEnum CurrentState;
    public FieldTypeEnum FieldType;

}
