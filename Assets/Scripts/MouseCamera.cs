using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    public Camera cam;
    public static MouseCamera instance;
    [SerializeField] private float sens = 200.0f;
    [SerializeField] private float x;
    [SerializeField] private float y;

    // Rendering Volume Effects for visual changes
    [SerializeField] public UnityEngine.Rendering.Volume dreamyEffect;
    [SerializeField] public UnityEngine.Rendering.Volume liminalEffect;

    private void Awake()
    {
        if(dreamyEffect && liminalEffect)
        {
            dreamyEffect.enabled = false;
            liminalEffect.enabled = false;
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

    void Start()
    {
        cam = GetComponent<Camera>();

        Vector3 euler = transform.rotation.eulerAngles;
        x = euler.x;
        y = euler.y;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        const float yMin = -89.9f;
        const float yMax = 89.9f;

        x += Input.GetAxis("Mouse X") * (sens * Time.deltaTime);
        y -= Input.GetAxis("Mouse Y") * (sens * Time.deltaTime);
        y = Mathf.Clamp(y, yMin, yMax);

        transform.rotation = Quaternion.Euler(y, x, 0.0f);
    }

    // Apply Liminal Effects when hit by a liminal or dreamy bullet for a few seconds
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("LiminalBullet"))
        {
            Debug.Log("Hit by Liminal Bullet @_@");
            liminalEffect.enabled = true;
            EnableVolumeEffectForSeconds(liminalEffect, 5f);
            
        }
        else if(collision.gameObject.CompareTag("DreamyBullet"))
        {
            Debug.Log("Hit by Dreamy Bullet V_V");
            dreamyEffect.enabled = true;
            EnableVolumeEffectForSeconds(dreamyEffect, 5f);
        }
    }

    public void EnableVolumeEffectForSeconds(UnityEngine.Rendering.Volume effect, float duration)
    {
        StartCoroutine(VolumeEffectCoroutine(effect, duration));
    }

    private System.Collections.IEnumerator VolumeEffectCoroutine(UnityEngine.Rendering.Volume effect, float duration)
    {
        if (effect != null)
        {
            effect.enabled = true;
            yield return new WaitForSeconds(duration);
            effect.enabled = false;
        }
    }
}
