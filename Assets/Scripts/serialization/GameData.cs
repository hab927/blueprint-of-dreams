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

    public GameData()
    {
        this.playerPosition = new Vector3(0, 201, -4.85f);
        this.playerSpawn = new Vector3(0, 201, -4.85f);
        this.hasKey = false;
        this.currentWorld = 0;
        this.gateOpen = false;
    }
}
