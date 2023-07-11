using UnityEngine;

public enum GameStae
{
    Starting,
    Playing,
    Gameover
}

public enum GameTag
{
    Platform,
    Player,
    LeftCorner,
    RightCorner,
    CollectTable
}
public enum PrefKey
{
    BestScore
}

[System.Serializable]
public class CollecTableItem
{
    public CollecTable collecTablePrefab;
    [Range(0f, 1f)]
    public float spawnRate;
}