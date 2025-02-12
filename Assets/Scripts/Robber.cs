using UnityEngine;
using Unity.Netcode;

public class Robber : NetworkBehaviour
{
    public float moveSpeed = 40F;
    private Vector3 moveDirection;

    void Update()
    {
        // if (!IsOwner)
        // {
        //     Debug.Log("Not the owner of this Robber. Skipping input.");
        //     return;
        // }

        Debug.Log("Processing input for Robber.");
        HandleMovement();
    }

    void HandleMovement()
    {
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.I)) moveZ += 1f;  // Forward
        if (Input.GetKey(KeyCode.K)) moveZ -= 1f;  // Backward
        if (Input.GetKey(KeyCode.J)) moveX -= 1f;  // Left
        if (Input.GetKey(KeyCode.L)) moveX += 1f;  // Right

        // ✅ Corrected movement to match standard Unity axis behavior
        moveDirection = new Vector3(-moveX, 0, -moveZ).normalized;


        if (moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection;
        }

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
