using System;

namespace Cultivations
{
    [Serializable]
    public class CultivationPrefabList
    {
        public readonly CultivationPrefab MyCultivationPrefab;
        public bool HasRequestNotication;

        public CultivationPrefabList(CultivationPrefab myCultivationPrefab)
        {
            MyCultivationPrefab = myCultivationPrefab;
        }

    }
}
