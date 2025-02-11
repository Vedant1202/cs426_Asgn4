using UnityEngine;

public class Package : MonoBehaviour
{
    public Transform target;
    public bool hacked = false;
    public float speed = 4f; // Default movement speed
    public float detectionRadius = 5f; // Radius to detect Cop or Robber

    public Material hackedMaterial; // Material when hacked
    public Material normalMaterial; // Material when not hacked

    private Renderer packageRenderer;

    void Start()
    {
        packageRenderer = GetComponent<Renderer>(); // Get the Renderer component

        if (target == null)
        {
            Debug.LogError(gameObject.name + " has no target assigned!");
            Destroy(gameObject); // Destroy package if it has no target
        }
    }

    void Update()
    {
        if (target == null) return;

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        DetectCopOrRobber(); // Check for nearby Cop/Robber
        UpdateMaterial(); // Apply correct material

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            // Package stops but does not get destroyed
        }
    }

    void DetectCopOrRobber()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Cop"))
            {
                hacked = false;
                return; // Stop checking once a Cop is found
            }
            else if (collider.CompareTag("Enemy"))
            {
                hacked = true;
            }
        }
    }

    void UpdateMaterial()
    {
        if (packageRenderer != null)
        {
            packageRenderer.material = hacked ? hackedMaterial : normalMaterial;
        }
    }
}
