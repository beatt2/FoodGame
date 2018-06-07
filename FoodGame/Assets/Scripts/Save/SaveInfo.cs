using System;

namespace Save
{
    [Serializable]
    public class SaveInfo
    {
        public DateTime StopTime;
        public int SaveMonth;
        public int SaveYear;
        public int SaveMoney;
        public int HighestCultivationListIndex;

        public SaveInfo(DateTime stopTime, int saveMonth, int saveYear, int highestCultivationListIndex)
        {
            StopTime = stopTime;
            SaveMonth = saveMonth;
            SaveYear = saveYear;
            HighestCultivationListIndex = highestCultivationListIndex;
        }

    }
}
