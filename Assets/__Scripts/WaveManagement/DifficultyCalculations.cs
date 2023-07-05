using UnityEngine;


public static class DifficultyCalculations
{
    public static int MinEnemiesSpawned(int difficulty)
    {
        return Mathf.RoundToInt(Mathf.Pow(2, difficulty * 0.05f));
    }  
    public static int MaxEnemiesSpawned(int difficulty)
    {
        return Mathf.RoundToInt(Mathf.Pow(3, difficulty * 0.06f) + 2);
    }
    public static float MinTimeBetweenSpawns(int difficulty)
    {
        return 2 - 0.05f * difficulty;
    }
    public static float MaxTimeBetweenSpawns(int difficulty)
    {
        return 4 - 0.03f * difficulty;
    }
    public static int DifficultyValue(int difficulty)
    {
        return 35 + 3 * difficulty;
    }
}
