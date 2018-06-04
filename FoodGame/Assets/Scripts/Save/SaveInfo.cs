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

        public SaveInfo(DateTime stopTime, int saveMonth, int saveYear)
        {
            StopTime = stopTime;
            SaveMonth = saveMonth;
            SaveYear = saveYear;
        }

    }
}
