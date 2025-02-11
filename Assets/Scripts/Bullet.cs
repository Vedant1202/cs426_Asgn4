using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float destroyTime = 3f; // Bullet disappears after 3 seconds

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // Example: Can be extended to damage enemies
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
