using UnityEngine;

public class Cop : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5.0f;           // Movement speed (units/second)
    public float rotationSpeed = 90.0f;  // Rotation speed in degrees/second (increased for more responsive turning)

    [Header("Shooting Settings")]
    public float bulletSpeed = 10f;       // Bullet travel speed
    public GameObject bulletPrefab;       // Prefab for the bullet

    private Rigidbody rb;
    private Transform t;
    private float moveInput = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        t = transform;
    }

    void Update()
    {
        HandleRotation();
        HandleMovementInput();
        HandleShooting();
    }

    // Uses transform.Rotate for smoother, more responsive turning.
    void HandleRotation()
    {
        float rotationInput = 0f;
        if (Input.GetKey(KeyCode.D))
            rotationInput = 1f;
        else if (Input.GetKey(KeyCode.A))
            rotationInput = -1f;

        // Rotate around the Y-axis based on rotationSpeed and deltaTime.
        t.Rotate(Vector3.up, rotationInput * rotationSpeed * Time.deltaTime);
    }

    // Reads forward/backward input.
    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            moveInput = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveInput = -1f;
        }
        else
        {
            moveInput = 0f;
        }
    }

    // Applies movement in FixedUpdate for physics consistency.
    void FixedUpdate()
    {
        // Calculate the horizontal (XZ) velocity.
        Vector3 horizontalVelocity = t.forward * moveInput * speed;
        // Set velocity directly for crisp start/stop (preserving the vertical velocity).
        rb.linearVelocity = new Vector3(horizontalVelocity.x, rb.linearVelocity.y, horizontalVelocity.z);
    }

    // Handles shooting when Space is pressed.
    void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBullet();
        }
    }

    // Instantiates a bullet and gives it a forward velocity.
    void ShootBullet()
    {
        // Spawn the bullet a little ahead of the Cop.
        GameObject bullet = Instantiate(bulletPrefab, t.position + t.forward * 1.5f, Quaternion.identity);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.linearVelocity = t.forward * bulletSpeed;
        }
        else
        {
            Debug.LogError("Bullet prefab is missing a Rigidbody!");
        }
    }
}
