using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SceneManager : MonoBehaviour, DataInterface
{
    public static SceneManager instance;
    public int currentWorld = 0;
    public CharacterController controller;
    public Vector3 worldOffset = new (100, 0, 0);

    public Vector3 playerSpawn = new(0, 201, -4.85f); // change this with checkpoint

    private bool isTeleporting = false;
    public float transition_time = 1.2f;

    [SerializeField] private Volume vignette_volume; // Assign your Volume in Inspector
    public Vignette vignette_effect;
    public bool one_way_teleport = false;
    // Audio source for teleporter
    [SerializeField] private AudioSource teleporterAudioSource;
    [SerializeField] private AudioClip teleporterCollisionClip;

    private void Awake()
    {
        if (!vignette_volume.profile)
        {
            Debug.LogError("Vignette profile not assigned.");
        }
        if (!vignette_volume.profile.TryGet(out vignette_effect))
        {
            Debug.LogError("Vignette not found in Volume profile.");
        }

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

        if (DataManager.instance)
        {
            LoadData(DataManager.instance.gameData);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isTeleporting)
        {
            StartCoroutine(blink(transition_time));
            if (one_way_teleport)
            {
                this.enabled = false;
            }
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

    public IEnumerator blink(float blinkTime)
    {
        isTeleporting = true;
        float startTime = 0f;
        while (startTime < blinkTime)
        {
            yield return new WaitForEndOfFrame();
            float lerp_value = Mathf.Lerp(0f, 1f, startTime / blinkTime);
            startTime += Time.deltaTime;
            if (vignette_effect)
            {
                vignette_effect.intensity.value = lerp_value;
            }
        }
        if (vignette_effect)
        {
            vignette_effect.intensity.value = 1f;
        }
        Teleport();
        startTime = 0f;
        while (startTime < blinkTime)
        {
            yield return new WaitForEndOfFrame();
            float lerp_value = Mathf.Lerp(1f, 0f, startTime / blinkTime);
            startTime += Time.deltaTime;
            if (vignette_effect)
            {
                vignette_effect.intensity.value = lerp_value;
            }
        }
        if (vignette_effect)
        {
            vignette_effect.intensity.value = 0f;
        }

        yield return null;
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
        isTeleporting = false;
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
