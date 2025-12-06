using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]

public class GameData
{
    public Vector3 playerPosition;
    public Vector3 playerSpawn;
    public bool hasKey;
    public int currentWorld;
    public bool gateOpen;
    public string currentLevel;
    public int saved_minutes;
    public float saved_seconds;

    public GameData()
    {
        this.playerPosition = new Vector3(0, 201, -4.85f);
        this.playerSpawn = new Vector3(0, 201, -4.85f);
        this.hasKey = false;
        this.currentWorld = 0;
        this.gateOpen = false;
        this.currentLevel = "Level1";
        this.saved_minutes = 0;
        this.saved_seconds = 0f;
    }
}
