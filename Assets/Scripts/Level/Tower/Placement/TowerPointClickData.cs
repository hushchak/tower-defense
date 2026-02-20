public class TowerPointClickData
{
    public TowerPlacementPoint Point { get; private set; }
    public Tower Tower { get; private set; }

    public TowerPointClickData(TowerPlacementPoint point, Tower tower)
    {
        Point = point;
        Tower = tower;
    }
}
