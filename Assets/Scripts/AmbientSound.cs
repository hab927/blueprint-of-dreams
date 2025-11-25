using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Used https://www.youtube.com/watch?v=8-SnHgtXV3k to create this script

public class AmbientSound : MonoBehaviour
{
    [Tooltip("Area of the sound to be in")]
    public Collider Area;
    [Tooltip("Character to track")]
    public GameObject Player;

    void Update()
    {
        // Locate closest point on the collider to the player
        Vector3 closestPoint = Area.ClosestPoint(Player.transform.position);
        // Set position to closest point to the player
        transform.position = closestPoint;
    }
}