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
        public List<int> DictionaryIndexEntrys;
        public int TotalAmountOfMoneyValues;
        public bool TutorialHasPlayed;
        public Dictionary<NodeState.FieldTypeEnum, float> PercentageValues;
        public List<CultivationPrefabList> ActiveCultivationPrefabLists;
        public Dictionary<int, bool> ReadDictionaryMessages;
        public Dictionary<int, bool> ReadDictionaryReview;
        

        public SaveInfo(DateTime stopTime, int saveMonth, int saveYear, float saveMoney, List<int> dictionaryIndexEntrys,
            Dictionary<NodeState.FieldTypeEnum, float> perrcentageValues, int totalAmountOfMoneyValues, bool tutorialHasPlayed,
            Dictionary<int, bool> readDictionaryReview, Dictionary<int,bool> readDictionaryMessages
            )
        {
            StopTime = stopTime;
            SaveMonth = saveMonth;
            SaveYear = saveYear;
            SaveMoney = saveMoney;
            DictionaryIndexEntrys =  dictionaryIndexEntrys;
            PercentageValues = perrcentageValues;
            TotalAmountOfMoneyValues = totalAmountOfMoneyValues;
            TutorialHasPlayed = tutorialHasPlayed;
            ReadDictionaryReview = readDictionaryReview;
            ReadDictionaryMessages = readDictionaryMessages;
        }

    }
}
