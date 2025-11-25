using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsSound : MonoBehaviour
{
    public AudioSource footstepsSound;

    void Update()
    {
        bool isMoving = false;
        if (UnityEngine.InputSystem.Keyboard.current != null)
        {
            var keyboard = UnityEngine.InputSystem.Keyboard.current;
            isMoving = keyboard.wKey.isPressed || keyboard.aKey.isPressed || keyboard.sKey.isPressed || keyboard.dKey.isPressed;
        }

        if (isMoving)
        {
            if (!footstepsSound.isPlaying)
                footstepsSound.Play();
        }
        else
        {
            if (footstepsSound.isPlaying)
                footstepsSound.Stop();
        }
    }
}
