
using System;
using Node;
using UnityEditor;
using UnityEngine;

namespace Cultivations
{
    [Serializable]
    public abstract class Cultivation
    {
        public string Name;
        public int Sustainability;
        public int MoneyTick;// = value
        public float ExpenseTick;
        public int MonthsToGrow;
        public int BuildPrice;
        //Going to be multiple values
        public int UpgradeValue;
        public int SpriteIndex;
        public int EnviromentValue;
        public int Happyness;
        public int SizeRank;
        



        public NodeState.CurrentStateEnum MyCultivationState;
        public NodeState.FieldTypeEnum FieldType;


        protected Cultivation(string name, int sustainability, int moneyTick, float expenseTick,
            int monthsToGrow,int buildPrice,NodeState.CurrentStateEnum currentState,
            NodeState.FieldTypeEnum currentFieldType, int upgradeValue, int spriteIndex, int enviromentValue, int happiness,
            int sizeRank)
        {
            Name = name;
            Sustainability = sustainability;
            MoneyTick = moneyTick;
            ExpenseTick = expenseTick;
            MyCultivationState = currentState;
            BuildPrice = buildPrice;
            UpgradeValue = upgradeValue;
            SpriteIndex = spriteIndex;
            EnviromentValue = enviromentValue;
            Happyness = happiness;
            MonthsToGrow = monthsToGrow;

        }

    }
}
