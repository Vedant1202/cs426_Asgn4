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
    [SerializeField] private Button start_btn;
    [SerializeField] private TMP_Text joinCodeText;
    [SerializeField] private TMP_InputField joinCodeInputField;
    [SerializeField] private int maxPlayers = 4;

    public GameManager gameManager;

    private string joinCode;

    private void Awake()
    {
        host_btn.onClick.AddListener(() => StartHostRelay());
        client_btn.onClick.AddListener(() => StartClientRelay(joinCodeInputField.text));
        start_btn.onClick.AddListener(() => {gameManager.StartGame(); start_btn.gameObject.SetActive(false);});
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

            Debug.Log("Host started. Netcode will automatically spawn the Cop.");
            start_btn.gameObject.SetActive(true);
            host_btn.gameObject.SetActive(false);
            client_btn.gameObject.SetActive(false);
            joinCodeInputField.gameObject.SetActive(false);
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
            host_btn.gameObject.SetActive(false);
            client_btn.gameObject.SetActive(false);
            joinCodeInputField.gameObject.SetActive(false);
        }
        catch (RelayServiceException e)
        {
            Debug.LogError(e);
        }
    }

    private void AssignPlayerRole(bool isHost)
    {
        if (!isHost) // Only spawn manually for clients
        {
            Debug.Log("You are the Robber (Client).");
            SpawnPlayer("Robber");
        }
    }

    private void SpawnPlayer(string role)
    {
        GameObject playerPrefab = role == "Robber" ? Resources.Load<GameObject>("Robber") : null;
        if (playerPrefab != null)
        {
            GameObject player = Instantiate(playerPrefab);
            NetworkObject networkObject = player.GetComponent<NetworkObject>();
            if (networkObject != null)
            {
                networkObject.SpawnWithOwnership(NetworkManager.Singleton.LocalClientId, true);
                Debug.Log($"{role} spawned with ownership for client {NetworkManager.Singleton.LocalClientId}.");
            }
            else
            {
                Debug.LogError($"Failed to spawn {role}: NetworkObject component missing.");
            }
        }
        else
        {
            Debug.LogError($"Failed to load {role} prefab.");
        }
    }
}
