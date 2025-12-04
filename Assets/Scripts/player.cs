using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour, DataInterface
{
    public static player instance;
    public CharacterController cc;

    private Vector3 moveVector;
    private Transform cameraTransform;
    private Vector3 horizontalVec;
    private Vector3 forwardVec;
    private float yAcceleration = 0.0f;
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float gravity = -10.0f;
    [SerializeField] private float jumpStrength = 10.0f;
    [SerializeField] private float sprintMult = 1.5f;

    // Audio source for item pickup
    [SerializeField] private AudioSource pickupAudioSource;
    [SerializeField] private AudioClip pickupAudioClip;

    // Audio source for complete level
    [SerializeField] private AudioSource yayAudioSource;
    [SerializeField] private AudioClip yayAudioClip;

    public bool hasKey = false;
    public bool gateOpen = false;

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
        cc = GetComponent<CharacterController>();
    }
    private void Update()
    {
        Raycast();

        // death
        if (transform.position.y < 100)
        {
            cc.enabled = false;
            cc.transform.position = SceneManager.instance.playerSpawn;
            cc.enabled = true;
            SceneManager.instance.currentWorld = 0;
        }
    }

    void FixedUpdate()
    {
        Movement();
    }

    public void Movement()
    {
        // gravity stuff (jumping and falling)
        if (!cc.isGrounded)
        {
            yAcceleration += gravity * Time.deltaTime;
        }
        else
        {
            yAcceleration = 0;
        }

        if (cc.isGrounded)
        {
            if (Input.GetAxis("Jump") > 0)
            {
                yAcceleration = jumpStrength;
            }
        }

        cameraTransform = MouseCamera.instance.transform;

        horizontalVec = Input.GetAxis("Horizontal") * new Vector3(cameraTransform.right.x, 0, cameraTransform.right.z).normalized;
        forwardVec = Input.GetAxis("Vertical") * new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z).normalized;

        moveVector = horizontalVec + forwardVec;

        if (moveVector.magnitude > 1)
        {
            moveVector = moveVector.normalized;
        }
        moveVector.y = yAcceleration;

        if (Input.GetAxis("Sprint") > 0)
        {
            Debug.Log("sprinting");
            moveVector.x *= sprintMult;
            moveVector.z *= sprintMult;
        }

        cc.Move(moveSpeed * Time.deltaTime * moveVector);
    }
    public void Raycast() // opening gate
    {
        if (cameraTransform)
        {
            RaycastHit hit;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), out hit, 10))
            {
                if (hit.collider.gameObject.CompareTag("Gate") && hasKey)
                {
                    hasKey = false;
                    Destroy(hit.collider.gameObject);
                    Debug.Log("gate opened");
                    gateOpen = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        if (other.gameObject.CompareTag("Key"))
        {
            hasKey = true;
            Destroy(other.gameObject);
            Debug.Log("key collected");
            // For the item pickup sound effects
            if (pickupAudioSource != null && pickupAudioClip != null)
            {
                pickupAudioSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
                pickupAudioSource.PlayOneShot(pickupAudioClip);
                Debug.Log("play audio");
            }
        }
        // Placeholder to move the player back to the starting position, this could get removed in the future
        else if (other.gameObject.CompareTag("Complete"))
        {
            Debug.Log("level complete");
            if (yayAudioSource != null && yayAudioClip != null)
            {
                other.transform.position = new Vector3(9, 2, -16);
                yayAudioSource.PlayOneShot(yayAudioClip);
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Debug.Log("hit");
        if (hit.gameObject.CompareTag("Checkpoint"))
        {
            Collider col = hit.gameObject.GetComponent<Collider>();
            SceneManager.instance.playerSpawn = new Vector3(transform.position.x, col.bounds.max.y + 3, transform.position.z);
            Debug.Log("checkpoint");
        }
    }

    // save data stuff
    public void LoadData(GameData data)
    {
        cc.enabled = false;
        cc.transform.position = data.playerPosition;
        cc.enabled = true;

        hasKey = data.hasKey;
        gateOpen = data.gateOpen;
        if (data.gateOpen)
        {
            GameObject gate = GameObject.FindWithTag("Gate");
            Debug.Log(gate);
            Destroy(gate);
        }
    }

    public void SaveData(ref GameData data)
    {
        data.playerPosition = transform.position;
        data.hasKey = hasKey;
        data.gateOpen = gateOpen;
    }
}