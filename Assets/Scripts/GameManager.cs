using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject copPrefab; // Assign Cop prefab
    public Transform spawnPoint; // Set Cop spawn location

    void Start()
    {
        Instantiate(copPrefab, spawnPoint.position, Quaternion.identity);
    }
}
