using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Networking.Transport.Relay;
using Unity.Netcode.Transports.UTP;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button host_btn;
    [SerializeField] private Button client_btn;
    [SerializeField] private TMP_Text joinCodeText;
    [SerializeField] private TMP_InputField joinCodeInputField;
    [SerializeField] private int maxPlayers = 4;

    private string joinCode;

    private void Awake()
    {
        host_btn.onClick.AddListener(() => StartHostRelay());
        client_btn.onClick.AddListener(() => StartClientRelay(joinCodeInputField.text));
    }

    private async void Start()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public async void StartHostRelay()
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxPlayers);
            joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            var serverData = new RelayServerData(allocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(serverData);

            NetworkManager.Singleton.StartHost();
            joinCodeText.text = joinCode;

            // Assign Cop role to the host
            AssignPlayerRole(true);
        }
        catch (RelayServiceException e)
        {
            Debug.LogError(e);
        }
    }

    public async void StartClientRelay(string joinCode)
    {
        try
        {
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
            var serverData = new RelayServerData(joinAllocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(serverData);

            NetworkManager.Singleton.StartClient();

            // Assign Robber role to the client
            AssignPlayerRole(false);
        }
        catch (RelayServiceException e)
        {
            Debug.LogError(e);
        }
    }

    private void AssignPlayerRole(bool isHost)
    {
        if (isHost)
        {
            Debug.Log("You are the Cop (Host).");
            // Spawn Cop prefab for the host
            SpawnPlayer("Cop");
        }
        else
        {
            Debug.Log("You are the Robber (Client).");
            // Spawn Robber prefab for the client
            SpawnPlayer("Robber");
        }
    }

    private void SpawnPlayer(string role)
    {
        GameObject playerPrefab = role == "Cop" ? Resources.Load<GameObject>("Cop") : Resources.Load<GameObject>("Robber");
        if (playerPrefab != null)
        {
            NetworkObject player = Instantiate(playerPrefab).GetComponent<NetworkObject>();
            player.SpawnWithOwnership(NetworkManager.Singleton.LocalClientId, true);
        }
        else
        {
            Debug.LogError($"Failed to load {role} prefab.");
        }
    }
}