using UnityEngine;
using Unity.Netcode;

public class RobberMovement : NetworkBehaviour
{
    public float moveSpeed = 5f;
    private Vector3 moveDirection;

    void Update()
    {
        if (!IsOwner) return; // Only allow movement for the local player

        HandleMovement();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        // Correcting the movement direction
        moveDirection = new Vector3(-moveX, 0, -moveZ).normalized;

        if (moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection;
        }

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
