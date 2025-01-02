using UnityEngine;
using Mirror;
using UnityEngine.UI;
using TMPro;

public class NetworkInitializer : MonoBehaviour
{
    public static NetworkInitializer Instance { get; private set; }

    [Header("Network Settings")]
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private string defaultAddress = "localhost";
    [SerializeField] private ushort defaultPort = 7777;

    [Header("UI Elements")]
    [SerializeField] private GameObject connectionPanel;
    [SerializeField] private TMP_InputField addressInput;
    [SerializeField] private TMP_InputField portInput;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private Button disconnectButton;
    [SerializeField] private TextMeshProUGUI statusText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Инициализация UI элементов
        if (addressInput != null) addressInput.text = defaultAddress;
        if (portInput != null) portInput.text = defaultPort.ToString();

        // Настройка кнопок
        if (hostButton != null) hostButton.onClick.AddListener(StartHost);
        if (clientButton != null) clientButton.onClick.AddListener(StartClient);
        if (disconnectButton != null)
        {
            disconnectButton.onClick.AddListener(StopConnection);
            disconnectButton.gameObject.SetActive(false);
        }

        // Проверка наличия NetworkManager
        if (networkManager == null)
        {
            networkManager = FindObjectOfType<NetworkManager>();
            if (networkManager == null)
            {
                Debug.LogError("NetworkManager не найден на сцене!");
                return;
            }
        }

        UpdateUI(false);
    }

    public void StartHost()
    {
        if (!NetworkClient.active)
        {
            // Установка порта
            if (ushort.TryParse(portInput.text, out ushort port))
            {
                networkManager.GetComponent<TelepathyTransport>().port = port;
            }

            networkManager.StartHost();
            UpdateUI(true);
            SetStatus("Хост запущен на порту: " + portInput.text);
        }
    }

    public void StartClient()
    {
        if (!NetworkClient.active)
        {
            // Установка адреса и порта
            networkManager.networkAddress = addressInput.text;
            if (ushort.TryParse(portInput.text, out ushort port))
            {
                networkManager.GetComponent<TelepathyTransport>().port = port;
            }

            networkManager.StartClient();
            UpdateUI(true);
            SetStatus("Подключение к: " + addressInput.text + ":" + portInput.text);
        }
    }

    public void StopConnection()
    {
        if (NetworkServer.active && NetworkClient.active)
        {
            networkManager.StopHost();
        }
        else if (NetworkClient.active)
        {
            networkManager.StopClient();
        }
        else if (NetworkServer.active)
        {
            networkManager.StopServer();
        }

        UpdateUI(false);
        SetStatus("Отключено");
    }

    private void UpdateUI(bool isConnected)
    {
        if (connectionPanel != null) connectionPanel.SetActive(!isConnected);
        if (disconnectButton != null) disconnectButton.gameObject.SetActive(isConnected);
        if (hostButton != null) hostButton.interactable = !isConnected;
        if (clientButton != null) clientButton.interactable = !isConnected;
        if (addressInput != null) addressInput.interactable = !isConnected;
        if (portInput != null) portInput.interactable = !isConnected;
    }

    private void SetStatus(string message)
    {
        if (statusText != null)
        {
            statusText.text = message;
            Debug.Log(message);
        }
    }

    private void OnEnable()
    {
        NetworkClient.OnConnectedEvent += OnClientConnected;
        NetworkClient.OnDisconnectedEvent += OnClientDisconnected;
    }

    private void OnDisable()
    {
        NetworkClient.OnConnectedEvent -= OnClientConnected;
        NetworkClient.OnDisconnectedEvent -= OnClientDisconnected;
    }

    private void OnClientConnected()
    {
        SetStatus("Подключено к серверу!");
    }

    private void OnClientDisconnected()
    {
        UpdateUI(false);
        SetStatus("Отключено от сервера");
    }
}
