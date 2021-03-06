﻿using System;
using System.Collections.Generic;
using Cultivations;
using Node;

namespace Save
{
    [Serializable]
    public class SaveInfo
    {
        public DateTime StopTime;
        public readonly int SaveMonth;
        public readonly int SaveYear;
        public readonly float SaveMoney;
        public List<int> DictionaryIndexEntrys;
        public int TotalAmountOfMoneyValues;
        public bool TutorialHasPlayed;
        public Dictionary<NodeState.FieldTypeEnum, float> PercentageValues;
        public List<CultivationPrefabList> ActiveCultivationPrefabLists;
        public Dictionary<int, bool> ReadDictionaryMessages;
        public Dictionary<int, bool> ReadDictionaryReview;
        public int WaitForSeconds;


        public SaveInfo
        (
            DateTime stopTime,
            int saveMonth,
            int saveYear,
            float saveMoney,
            List<int> dictionaryIndexEntrys,
            Dictionary<NodeState.FieldTypeEnum, float> perrcentageValues,
            int totalAmountOfMoneyValues,
            bool tutorialHasPlayed,
            Dictionary<int, bool> readDictionaryReview,
            Dictionary<int,bool> readDictionaryMessages,
            int waitForSeconds

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
            WaitForSeconds = waitForSeconds;
        }

    }
}
