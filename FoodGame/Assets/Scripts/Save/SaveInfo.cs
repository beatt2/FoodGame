using System;

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

        public SaveInfo(DateTime stopTime, int saveMonth, int saveYear, float saveMoney, int highestCultivationListIndex)
        {
            StopTime = stopTime;
            SaveMonth = saveMonth;
            SaveYear = saveYear;
            SaveMoney = saveMoney;
            HighestCultivationListIndex = highestCultivationListIndex;
        }

    }
}
