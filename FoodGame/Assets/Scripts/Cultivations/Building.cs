namespace Cultivations
{
    public class Building : Cultivation
    {

        public Building(string name, int sustainability, int moneyTick, int upgradeRank, int buildingPrice,
            NodeState.CurrentStateEnum cultivationType, NodeState.FieldTypeEnum fieldType)
            : base(name, sustainability, moneyTick, upgradeRank, buildingPrice, cultivationType, fieldType)
        {
        }        
    }
}
