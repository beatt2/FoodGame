using UnityEngine;

namespace Cultivations
{
    public class PlantPrefab :CultivationPrefab
    {
        [HideInInspector] public Plant MyPlant;
        
        public NodeState.CurrentStateEnum CurrentState;
        public NodeState.FieldTypeEnum FieldType;
    
        protected void Awake()
        {
            MyPlant = new Plant(Name,Sustainability,MoneyTick,BuildingPrice,UpgradeRank,CurrentState, FieldType);
        }
        
        public void ChangeValues(Plant plant)
        {
            MyPlant = plant;
            AddCultivation();   
        }

        private void AddCultivation()
        {
            CultivationManager.Instance.AddValue(MyPlant);
        }

    }
}
