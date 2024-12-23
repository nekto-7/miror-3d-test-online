using System.Collections;
using UnityEngine;
using TMPro;
using Mirror;

public class Stage2 : MonoBehaviour
{
    public TMP_InputField inputField; // Поле ввода команд
    public TextMeshProUGUI outputText; // Текстовое поле для вывода

    private NetworkManager networkManager;

    void Start()
    {
        networkManager = NetworkManager.singleton; // Получаем ссылку на NetworkManager
        ShowIntro();
        inputField.onEndEdit.AddListener(OnCommandEntered); // Подписка на событие при завершении редактирования
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

    // Симуляция процесса загрузки с серым текстом
    private IEnumerator DisplayLoadingText(string[] loadingTexts)
    {
        foreach (string line in loadingTexts)
        {
            outputText.text += line + "\n";
            yield return new WaitForSeconds(1f); // Пауза между строками
        }

        // Когда загрузка завершена, появляется курсор
        outputText.text += "\n> ";
        inputField.gameObject.SetActive(true); // Включаем поле ввода
        inputField.ActivateInputField(); // Активируем курсор
    }

    // Обработка команд, когда пользователь нажимает Enter
    private void OnCommandEntered(string command)
    {
        if (string.IsNullOrEmpty(command)) return; // Игнорируем пустые команды

        command = command.ToLower(); // Приводим команду к нижнему регистру
        outputText.text += "\n>> " + command;  // Пишем введённую команду в вывод

        switch (command)
        {
            case "help":
                outputText.text += "\n>> Available commands: help, play, exit, create, join";
                break;
            case "play":
                outputText.text += "\n?? Starting the game...";
                Game(); // Играть
                break;
            case "exit":
                outputText.text += "\n>> Exiting program...";
                Application.Quit(); // Закрыть приложение
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

        inputField.text = ""; // Очистить поле ввода
        inputField.ActivateInputField();  // Снова активировать поле ввода для следующей команды
    }

    // Команда для создания комнаты
    private void CreateRoomCommand()
    {
        outputText.text += "\n>> Enter room name to create: ";
        inputField.ActivateInputField(); // Переводим курсор в поле ввода

        // Ожидаем, пока игрок введет имя комнаты
        StartCoroutine(WaitForRoomName("create"));
    }

    // Команда для подключения к комнате
    private void JoinRoomCommand()
    {
        outputText.text += "\n>> Enter room name to join: ";
        inputField.ActivateInputField(); // Переводим курсор в поле ввода

        // Ожидаем, пока игрок введет имя комнаты
        StartCoroutine(WaitForRoomName("join"));
    }

    // Ожидание ввода имени комнаты
    private IEnumerator WaitForRoomName(string commandType)
    {
        // Ожидаем, пока игрок не введет имя
        yield return new WaitUntil(() => !string.IsNullOrEmpty(inputField.text));

        string roomName = inputField.text;

        if (commandType == "create")
        {
            // Вызываем команду для создания комнаты
            var roomPlayer = NetworkClient.connection.identity.GetComponent<RoomPlayer>();
            roomPlayer.CmdCreateRoom(roomName); // Создаем комнату
        }
        else if (commandType == "join")
        {
            // Вызываем команду для присоединения к комнате
            var roomPlayer = NetworkClient.connection.identity.GetComponent<RoomPlayer>();
            roomPlayer.CmdJoinRoom(roomName); // Присоединяемся к комнате
        }
    }

    // Симуляция начала игры
    private void Game()
    {
        outputText.text += "\rconnection starts, please wait...\n";
        // Здесь можно добавить код для начала игры
    }

    // Загрузка текста при старте игры
    private void ShowIntro()
    {
        StartCoroutine(DisplayLoadingText(loadingTexts));
    }
}
