using UnityEngine;
using TMPro;
using Mirror;

public class Stage2 : Console
{
    public TMP_InputField inputField;
    public Connection connection;
    public NetworkManager networkManager;
    string[] loadingTexts = new string[]
    {
        "\rInitializing user interface:\r\n  - Displaying a login prompt on the console or graphical user interface (GUI)\r\n Waiting for user input:\n",
        ">> ******\n",
        "\r- Prompting the user to enter a username and password...\n",
        ">> *************\n",
        "\\      /\\      /                   ",
        " \\    /  \\    /                    ",
        "  \\  /    \\  /                      ",
        "   \\/      \\/      ELCOME EMPLOER   ",
        "\n> ",
    };
    string[] amogus = new string[]
    {
        "\nAMOGUS"
    };

    void Start()
    {
        DisplayLoadingText();
        inputField.onEndEdit.AddListener(OnCommandEntered);  

    }

    private void DisplayLoadingText()
    {
        StartCoroutine(PrintText(outputText, loadingTexts));  
 
        inputField.gameObject.SetActive(true); 
        inputField.ActivateInputField();  
    }

    private void OnCommandEntered(string command)
    {
        if (string.IsNullOrEmpty(command)) return; 

        command = command.ToLower(); 
        outputText.text += "\n>> " + command;   

        switch (command)
        {
            case "exit":
                outputText.text += "\n>> Exiting program...";
                Application.Quit();  
                break;

            case "help":
                outputText.text += "\n>> List of commands:\n server - create a server;\n client - log in as a Client;\n exit - turn off the OS";
                break;

            case "server":
                Host();
                break;


            case "client":
                Client();
                break;

            case "amogus":
                StartCoroutine(PrintText(outputText, amogus));
                break;

            default:
                outputText.text += "\n>> Unknown command! Try again.";
                break;
        }

        inputField.text = ""; 
        inputField.ActivateInputField();  
    }
     private void Host()
    {
        networkManager.StartHost();
        outputText.text += "\n>> load HOST";
    }
    private void Client()
    {
        outputText.text += "\n>> load CLIENT";
    } 

 
}
