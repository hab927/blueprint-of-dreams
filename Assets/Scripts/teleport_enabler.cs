using UnityEngine;

public class teleport_enabler : MonoBehaviour
{
    public SceneManager SM;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SM = FindFirstObjectByType<SceneManager>();
        SM.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SM.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            SM.enabled = false;
        }
    }
    
}
