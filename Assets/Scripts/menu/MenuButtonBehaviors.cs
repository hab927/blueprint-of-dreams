using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonBehaviors : MonoBehaviour
{
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }

    public void LoadGame()
    {
        DataManager.instance.LoadGame();
        UnityEngine.SceneManagement.SceneManager.LoadScene(LevelManager.instance.currentLevel);
    }
}
