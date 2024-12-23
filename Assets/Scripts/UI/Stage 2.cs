using System.Collections;
using UnityEngine;
using TMPro;
using Mirror;

public class Stage2 : MonoBehaviour
{
    public TMP_InputField inputField; // ���� ����� ������
    public TextMeshProUGUI outputText; // ��������� ���� ��� ������

    private NetworkManager networkManager;

    void Start()
    {
        networkManager = NetworkManager.singleton; // �������� ������ �� NetworkManager
        ShowIntro();
        inputField.onEndEdit.AddListener(OnCommandEntered); // �������� �� ������� ��� ���������� ��������������
    }

    string[] loadingTexts = new string[]
    {
        "\rInitializing user interface:\r\n  - Displaying a login prompt on the console or graphical user interface (GUI)\r\n Waiting for user input:\n",
        ">> ******\n",
        "\r- Prompting the user to enter a username and password...\n",
        ">> *************\n",
        "'\\'      /'\\'      /                   ",
        " '\\'    /  '\\'    /                    ",
        "  '\\'  /    '\\'  /                      ",
        "   '\\'/      '\\'/      ELCOME EMPLOER   ",
    };

    // ��������� �������� �������� � ����� �������
    private IEnumerator DisplayLoadingText(string[] loadingTexts)
    {
        foreach (string line in loadingTexts)
        {
            outputText.text += line + "\n";
            yield return new WaitForSeconds(1f); // ����� ����� ��������
        }

        // ����� �������� ���������, ���������� ������
        outputText.text += "\n> ";
        inputField.gameObject.SetActive(true); // �������� ���� �����
        inputField.ActivateInputField(); // ���������� ������
    }

    // ��������� ������, ����� ������������ �������� Enter
    private void OnCommandEntered(string command)
    {
        if (string.IsNullOrEmpty(command)) return; // ���������� ������ �������

        command = command.ToLower(); // �������� ������� � ������� ��������
        outputText.text += "\n>> " + command;  // ����� �������� ������� � �����

        switch (command)
        {
            case "help":
                outputText.text += "\n>> Available commands: help, play, exit, create, join";
                break;
            case "play":
                outputText.text += "\n?? Starting the game...";
                Game(); // ������
                break;
            case "exit":
                outputText.text += "\n>> Exiting program...";
                Application.Quit(); // ������� ����������
                break;
            case "create":
                CreateRoomCommand();
                break;
            case "join":
                JoinRoomCommand();
                break;
            default:
                outputText.text += "\n>> Unknown command! Try again.";
                break;
        }

        inputField.text = ""; // �������� ���� �����
        inputField.ActivateInputField();  // ����� ������������ ���� ����� ��� ��������� �������
    }

    // ������� ��� �������� �������
    private void CreateRoomCommand()
    {
        outputText.text += "\n>> Enter room name to create: ";
        inputField.ActivateInputField(); // ��������� ������ � ���� �����

        // �������, ���� ����� ������ ��� �������
        StartCoroutine(WaitForRoomName("create"));
    }

    // ������� ��� ����������� � �������
    private void JoinRoomCommand()
    {
        outputText.text += "\n>> Enter room name to join: ";
        inputField.ActivateInputField(); // ��������� ������ � ���� �����

        // �������, ���� ����� ������ ��� �������
        StartCoroutine(WaitForRoomName("join"));
    }

    // �������� ����� ����� �������
    private IEnumerator WaitForRoomName(string commandType)
    {
        // �������, ���� ����� �� ������ ���
        yield return new WaitUntil(() => !string.IsNullOrEmpty(inputField.text));

        string roomName = inputField.text;

        if (commandType == "create")
        {
            // �������� ������� ��� �������� �������
            var roomPlayer = NetworkClient.connection.identity.GetComponent<RoomPlayer>();
            roomPlayer.CmdCreateRoom(roomName); // ������� �������
        }
        else if (commandType == "join")
        {
            // �������� ������� ��� ������������� � �������
            var roomPlayer = NetworkClient.connection.identity.GetComponent<RoomPlayer>();
            roomPlayer.CmdJoinRoom(roomName); // �������������� � �������
        }
    }

    // ��������� ������ ����
    private void Game()
    {
        outputText.text += "\rconnection starts, please wait...\n";
        // ����� ����� �������� ��� ��� ������ ����
    }

    // �������� ������ ��� ������ ����
    private void ShowIntro()
    {
        StartCoroutine(DisplayLoadingText(loadingTexts));
    }
}
