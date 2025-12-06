using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class DataManager : MonoBehaviour
{
    public static DataManager instance { get; private set; }
    public GameData gameData;

    private List<DataInterface> dataObjects;

    [Header("File Storage Config")]
    [SerializeField] private string fileName = "data.save";

    private FileDataHandler dataHandler;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            Debug.Log("serializer_destroyed");
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        this.dataObjects = FindAllDataObjects();
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();
        if (this.gameData == null)
        {
            Debug.Log("no game data found, initializing with default values");
            NewGame();
        }

        this.dataObjects = FindAllDataObjects();
        foreach (DataInterface dataObj in this.dataObjects)
        {
            dataObj.LoadData(gameData);
        }
        
    }

    public void SaveGame()
    {
        this.dataObjects = FindAllDataObjects();
        foreach (DataInterface dataObj in this.dataObjects)
        {
            dataObj.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        //SaveGame();
        //Debug.Log(Application.persistentDataPath);
    }

    private List<DataInterface> FindAllDataObjects()
    {
        IEnumerable<DataInterface> dataObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<DataInterface>();

        return new List<DataInterface>(dataObjects);
    }
}