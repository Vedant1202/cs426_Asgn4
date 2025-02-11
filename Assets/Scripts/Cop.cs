using UnityEngine;
using Unity.Netcode;

public class Cop : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;

    private Vector3 moveDirection;

    void Update()
    {
        if (!IsOwner) return; // Only allow movement for the local player

        HandleMovement();
        HandleShooting();
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

    void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.forward * 1.5f, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = transform.forward * bulletSpeed;
        }
        else
        {
            Debug.LogError("Bullet prefab is missing a Rigidbody!");
        }
    }
}