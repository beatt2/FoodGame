using System;
using Cultivations;
using Node;

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

        public readonly Cultivation MySavedCultivation;

        public readonly bool FenceLeft;
        public readonly bool FenceLeftOwner;
        public readonly int SizeRankLeft;

        public readonly bool FenceRight;
        public readonly bool FenceRightOwner;
        public readonly int SizeRankRight;

        public readonly bool FenceUp;
        public readonly bool FenceUpOwner;
        public readonly int SizeRankUp;

        public readonly bool FenceDown;
        public readonly bool FenceDownOwner;
        public readonly int SizeRankDown;





        public SaveNodes
        (
            int listIndex,
            NodeState.CurrentStateEnum currentState,
            NodeState.FieldTypeEnum fieldType,
            bool emptyCultivationField,
            Cultivation myCultivation,
            bool fenceLeft,
            bool fenceLeftOwner,
            bool fenceRight,
            bool fenceRightOwner,
            bool fenceUp,
            bool fenceUpOwner,
            bool fenceDown,
            bool fenceDownOwner,
            int sizeRankLeft,
            int sizeRankRight,
            int sizeRankUp,
            int sizeRankDown,
            Cultivation mySavedCultivation
        )
        {

            ListIndex = listIndex;
            CurrentState = currentState;
            FieldType = fieldType;
            EmptyCultivationField = emptyCultivationField;
            MyCultivation = myCultivation;
            FenceLeft = fenceLeft;
            FenceLeftOwner = fenceLeftOwner;
            FenceRight = fenceRight;
            FenceRightOwner = fenceRightOwner;
            FenceUp = fenceUp;
            FenceUpOwner = fenceUpOwner;
            FenceDown = fenceDown;
            FenceDownOwner = fenceDownOwner;
            SizeRankLeft = sizeRankLeft;
            SizeRankRight = sizeRankRight;
            SizeRankUp = sizeRankUp;
            SizeRankDown = sizeRankDown;
            MySavedCultivation = mySavedCultivation;

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
