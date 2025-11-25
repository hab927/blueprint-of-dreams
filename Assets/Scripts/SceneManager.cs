using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public static TeleportManager instance;
    public int currentWorld = 0;
    public CharacterController controller;
    public Vector3 worldOffset = new (100, 0, 0);

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
}
