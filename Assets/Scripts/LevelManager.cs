using UnityEngine;

public class LevelManager : MonoBehaviour, DataInterface
{
    public static LevelManager instance { get; private set; }

    public string currentLevel = "";
    public string sceneName;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (sceneName != "Main Menu")
        {
            if (sceneName != currentLevel)
            {
                currentLevel = sceneName;
            }
        }
    }

    public void LoadData(GameData data)
    {
        this.currentLevel = data.currentLevel;
        if (data.currentLevel != currentLevel)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(data.currentLevel);
            player.instance.LoadData(data);
            SceneManager.instance.LoadData(data);
        }
    }

    public void SaveData(ref GameData data)
    {
        data.currentLevel = this.currentLevel;
    }
}
