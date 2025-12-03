using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]

public class GameData
{
    public Vector3 playerPosition;
    public int keysHeld;
    public int currentWorld;
    public bool gateOpen;

    public GameData()
    {
        this.playerPosition = new Vector3(0, 201, -4.85f);
        this.keysHeld = 0;
        this.currentWorld = 0;
        this.gateOpen = false;
    }
}
