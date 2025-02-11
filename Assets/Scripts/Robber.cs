using UnityEngine;
using Unity.Netcode;

public class Robber : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public float hackRadius = 3f;
    public LayerMask packetLayer;

    private Vector3 moveDirection;

    void Update()
    {
        if (!IsOwner) return; // Only allow movement for the local player

        HandleMovement();
        HandleHacking();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        if (moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection;
        }

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    void HandleHacking()
    {
        Collider[] packets = Physics.OverlapSphere(transform.position, hackRadius, packetLayer);
        foreach (Collider packet in packets)
        {
            if (packet.CompareTag("PackageToCache") || packet.CompareTag("PackageToALU"))
            {
                Package package = packet.GetComponent<Package>();
                if (package != null && !package.hacked)
                {
                    package.hacked = true;
                    Debug.Log("Packet hacked by Robber!");
                }
            }
        }
    }
}