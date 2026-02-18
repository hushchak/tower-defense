public class GameData
{
    public bool[] IsLevelAvailable;

    public GameData(int levelCount, int defaultAvailableLevels)
    {
        IsLevelAvailable = new bool[levelCount];
        for (int i = 0; i < defaultAvailableLevels; i++)
        {
            IsLevelAvailable[i] = true;
        }
    }
}
