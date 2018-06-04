using System;
using Cultivations;
using UnityEngine;

namespace Save
{
    [Serializable]
    public class SaveNodes
    {
        public PlantPrefab Plant;
        public BuildingPrefab Build;
        public int ListIndex;
        public NodeState NodeState;
        public bool EmptyCultivationField;
    
    }
}
