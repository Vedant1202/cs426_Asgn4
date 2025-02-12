using UnityEngine;
using Unity.Netcode;

public class RobberMovement : NetworkBehaviour
{
    public float moveSpeed = 5f;
    private Vector3 moveDirection;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Robber requires a Rigidbody!");
        }
    }

    void Update()
    {
        if (!IsOwner) return; // Only allow movement for the local player

        HandleMovement();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        // ✅ Correct movement direction (No Inversion)
        moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        if (moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection;
        }
    }

    void FixedUpdate()
    {
        if (!IsOwner) return;

        // ✅ Use Rigidbody for movement (prevents passing through walls)
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Prevent movement through walls
        if (collision.gameObject.CompareTag("Wall"))
        {
            moveDirection = Vector3.zero; // Stop movement if hitting a wall
        }
    }
}
