using System;
using System.Collections.Generic;
using Cultivations;
using Money;
using Node;

namespace Save
{
    [Serializable]
    public class SaveInfo
    {
        public DateTime StopTime;
        public int SaveMonth;
        public int SaveYear;
        public float SaveMoney;
        public int HighestCultivationListIndex;
        public int TotalAmountOfMoneyValues;
        public bool TutorialHasPlayed;
        public Dictionary<NodeState.FieldTypeEnum, float> PercentageValues;
        public List<CultivationPrefabList> ActiveCultivationPrefabLists;
        

        public SaveInfo(DateTime stopTime, int saveMonth, int saveYear, float saveMoney, int highestCultivationListIndex,
            Dictionary<NodeState.FieldTypeEnum, float> perrcentageValues, int totalAmountOfMoneyValues, bool tutorialHasPlayed
            )
        {
            StopTime = stopTime;
            SaveMonth = saveMonth;
            SaveYear = saveYear;
            SaveMoney = saveMoney;
            HighestCultivationListIndex = highestCultivationListIndex;
            PercentageValues = perrcentageValues;
            TotalAmountOfMoneyValues = totalAmountOfMoneyValues;
            TutorialHasPlayed = tutorialHasPlayed;
        }

    }
}
