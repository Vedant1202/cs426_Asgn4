using UnityEngine;

public class Robber : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5.0f;           // Movement speed (units/second)
    public float rotationSpeed = 90.0f;  // Rotation speed in degrees/second (increased for more responsive turning)

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
        // HandleShooting();
    }

    // Uses transform.Rotate for smoother, more responsive turning.
    void HandleRotation()
    {
        float rotationInput = 0f;
        if (Input.GetKey(KeyCode.L))
            rotationInput = 1f;
        else if (Input.GetKey(KeyCode.J))
            rotationInput = -1f;

        // Rotate around the Y-axis based on rotationSpeed and deltaTime.
        t.Rotate(Vector3.up, rotationInput * rotationSpeed * Time.deltaTime);
    }

    // Reads forward/backward input.
    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.I))
        {
            moveInput = 1f;
        }
        else if (Input.GetKey(KeyCode.K))
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
}
