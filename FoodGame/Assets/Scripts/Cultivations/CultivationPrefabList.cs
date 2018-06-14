using System;
using UnityEngine;

namespace Cultivations
{
    [Serializable]
    public class CultivationPrefabList
    {
        public CultivationPrefab MyCultivationPrefab;
        public bool HasRequestNotication;

        public CultivationPrefabList(CultivationPrefab myCultivationPrefab)
        {
            MyCultivationPrefab = myCultivationPrefab;
        }

    }
}
