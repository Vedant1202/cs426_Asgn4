using UnityEngine;
using System.Collections;

public class RAM : MonoBehaviour
{
    public GameObject packageToCachePrefab; // Prefab for RAM → Cache package
    public Transform cache; // Reference to Cache
    public int maxPackages = 10; // Max number of packages RAM can send
    public float operationTime = 120f; // Total allowed time in seconds

    private int packageCount = 0;
    private float timer = 0f;
    private bool canSpawn = true;
    private float spawnInterval; // Dynamically calculated interval

    void Start()
    {
        spawnInterval = operationTime / maxPackages; // Calculate interval dynamically
        StartCoroutine(SendPackages());
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= operationTime)
        {
            canSpawn = false; // Stop spawning after 120s
        }
    }

    IEnumerator SendPackages()
    {
        while (canSpawn && packageCount < maxPackages)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (!canSpawn) yield break; // Stop spawning if time exceeded

            GameObject package = Instantiate(packageToCachePrefab, transform.position, Quaternion.identity);
            package.GetComponent<Package>().target = cache;
            packageCount++;
        }
    }
}
