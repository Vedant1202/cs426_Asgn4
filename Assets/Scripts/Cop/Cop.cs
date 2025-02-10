using UnityEngine;

public class Cop : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    public GameObject bulletPrefab; // Bullet prefab
    public float bulletSpeed = 10f; // Speed of bullets

    private Vector3 moveDirection;

    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        float moveZ = Input.GetAxisRaw("Vertical");   // W/S or Up/Down

        moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        if (moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection; // Rotate Cop in movement direction
        }

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Press Space to shoot
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
            rb.linearVelocity = transform.forward * bulletSpeed; // Apply force to move bullet
        }
        else
        {
            Debug.LogError("Bullet prefab is missing a Rigidbody!");
        }
    }
}
