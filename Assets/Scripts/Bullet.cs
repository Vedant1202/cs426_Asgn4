using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float destroyTime = 3f; // Bullet disappears after 3 seconds
    public float shrinkFactor = 0.6f; // Bullet reduces to 80% of its size on each bounce
    public int maxBounces = 3; // Max bounces before the bullet is destroyed
    private int bounceCount = 0;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get Rigidbody reference
        Destroy(gameObject, destroyTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall")) // Bullet bounces and shrinks
        {
            transform.localScale *= shrinkFactor; // Reduce bullet size
            bounceCount++;

            if (bounceCount >= maxBounces)
            {
                Destroy(gameObject); // Destroy bullet after max bounces
            }
        }
        else if (collision.gameObject.CompareTag("Enemy")) // Destroy enemy on impact
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Absorber")) // Bullet loses momentum and falls
        {
            rb.linearVelocity = Vector3.zero; // Stop movement
            rb.useGravity = true; // Enable gravity
        }
    }
}
