using TMPro;
using UnityEngine;
using Unity.Netcode;

public class GameManager : NetworkBehaviour
{
    public GameObject copPrefab;  // Assign Cop prefab in Inspector
    public GameObject robberPrefab; // Assign Robber prefab in Inspector
    public Transform copSpawnPoint; // Cop spawn location
    public Transform[] robberSpawnPoints; // Array of spawn points for robbers
    private int robberSpawnIndex = 0; // To cycle through spawn points

    

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += AssignRole;
        }
    }

    private void AssignRole(ulong clientId)
    {
        GameObject playerPrefab;
        Transform spawnPoint;

        if (clientId == NetworkManager.Singleton.LocalClientId)
        {
            // First player (host) is Cop
            playerPrefab = copPrefab;
            spawnPoint = copSpawnPoint;
        }
        else
        {
            // All other players are Robbers
            playerPrefab = robberPrefab;
            spawnPoint = robberSpawnPoints[robberSpawnIndex];

            // Cycle through spawn points
            robberSpawnIndex = (robberSpawnIndex + 1) % robberSpawnPoints.Length;
        }

        GameObject player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        player.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
    }
}
