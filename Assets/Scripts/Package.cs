using UnityEngine;

public class Package : MonoBehaviour
{
    public Transform target;
    public float speed = 5f; // Default movement speed

    void Start()
    {
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

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            Destroy(gameObject); // Destroy the package at its destination
        }
    }
}
