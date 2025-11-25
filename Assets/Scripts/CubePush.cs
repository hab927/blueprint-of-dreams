using UnityEngine;

public class CubePush : MonoBehaviour
{
    // used https://www.youtube.com/watch?v=3BOn2gs7z04 to figure out how to make the player push rigidbodies
    [SerializeField]
    private float forceMagnitude;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;
        if (rb != null && !rb.isKinematic)
        {
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection = forceDirection.normalized;
            rb.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
        }
    }
}
