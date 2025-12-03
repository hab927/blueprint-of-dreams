using UnityEngine;

public class SceneManager : MonoBehaviour, DataInterface
{
    public static SceneManager instance;
    public int currentWorld = 0;
    public CharacterController controller;
    public Vector3 worldOffset = new (100, 0, 0);

    public Vector3 playerSpawn = new(0, 201, -4.85f); // change this with checkpoint

    // Audio source for teleporter
    [SerializeField] private AudioSource teleporterAudioSource;
    [SerializeField] private AudioClip teleporterCollisionClip;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = player.instance.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Teleport();
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            DataManager.instance.SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.F6))
        {
            DataManager.instance.LoadGame();
        }
    }

    public void Teleport()
    {
        // For the teleporter sound effects
        if (teleporterAudioSource != null && teleporterCollisionClip != null)
        {
            teleporterAudioSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
            teleporterAudioSource.PlayOneShot(teleporterCollisionClip);
        }
        
        Vector3 p = controller.transform.position;
        controller.enabled = false;
        Debug.Log("off");
        if (currentWorld == 0)
        {
            controller.transform.position = new Vector3(p.x + worldOffset.x, p.y + worldOffset.y, p.z + worldOffset.z);
            currentWorld = 1;
            Debug.Log("translated to world 1");
        }
        else if (currentWorld == 1)
        {
            controller.transform.position = new Vector3(p.x - worldOffset.x, p.y - worldOffset.y, p.z - worldOffset.z);
            currentWorld = 0;
            Debug.Log("translated to world 0");
        }
        controller.enabled = true;
        Debug.Log("on");
    }

    public void LoadData(GameData data)
    {
        this.playerSpawn = data.playerSpawn;
        this.currentWorld = data.currentWorld;
    }

    public void SaveData(ref GameData data)
    {
        data.playerSpawn = this.playerSpawn;
        data.currentWorld = this.currentWorld;
    }
}
