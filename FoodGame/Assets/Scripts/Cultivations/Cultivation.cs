
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
        public int UpgradeRank;
        public int BuildPrice;
        //public Sprite Image;
        public int UpgradeValue;
        public int SpriteIndex;
        public GameObject[] UpgradeOptions;

        public int EnviromentValue;
        public int Happyness;
        



        public NodeState.CurrentStateEnum MyCultivationState;
        public NodeState.FieldTypeEnum FieldType;


        protected Cultivation(string name, int sustainability, int moneyTick, float expenseTick,
            int upgradeRank,int buildPrice,NodeState.CurrentStateEnum currentState,
            NodeState.FieldTypeEnum currentFieldType, Sprite image, int upgradeValue, GameObject [] upgradeOptions, int spriteIndex)
        {
            Name = name;
            Sustainability = sustainability;
            MoneyTick = moneyTick;
            ExpenseTick = expenseTick;
            UpgradeRank = upgradeRank;
            MyCultivationState = currentState;
            BuildPrice = buildPrice;
            FieldType = currentFieldType;
            //Image = image;
            UpgradeValue = upgradeValue;
            UpgradeOptions = upgradeOptions;
            SpriteIndex = spriteIndex;

        }

    }
}
