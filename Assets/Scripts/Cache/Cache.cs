using UnityEngine;

public class Cache : MonoBehaviour
{
    public GameObject packageToALUPrefab; // Prefab for Cache → ALU package
    public Transform alu; // ALU reference
    public float speedMultiplier = 2f; // Speed multiplier for Cache → ALU transfer

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PackageToCache")) // Detect package from RAM
        {
            Destroy(other.gameObject); // Destroy the received package
            SendPackageToALU();
        }
    }

    void SendPackageToALU()
    {
        if (alu == null)
        {
            Debug.LogError("ALU target is not assigned in Cache!");
            return;
        }

        GameObject package = Instantiate(packageToALUPrefab, transform.position, Quaternion.identity);
        Package packageScript = package.GetComponent<Package>();

        if (packageScript != null)
        {
            packageScript.target = alu;
            packageScript.speed *= speedMultiplier; // Multiply speed by defined factor
        }
    }
}
