using System.Collections;
using UnityEngine;
using TMPro;
using Mirror;

public class Stage2 : Console
{
    public TMP_InputField inputField;
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
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣾⣟⣷⣦⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀  ⣴⡞⠋⠉⠀⠀⠀⠉⠳⣿⣆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀   ⣿⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⣟⢦⣀⣀⣀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⣠⣤⣞⣗⣒⣶⣤⣤⣤⣄⡀⠀⠀⠀⠀⢹⣎⡎⠉⠘⢿⡄⠀⠀⠀⠀",
        "⠀⠀⢠⣿⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⢻⣷⠀⠀⠀⠀⠹⣿⠀⠀⠀⣯⠀⠀⠀",
        "⠀⠀⣿⡏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⢿⠃⠀⠀⠀⠀⢸⡇⠀⠀⣿⡇⠀⠀",
        "⠀⠀⢻⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡼⣿⠀⠀⠀⠀⠀⠀⢹⡄⠀⠀⣿⠀⠀",
        "⠀⠀⠘⣷⣤⣀⡀⠀⠀⠀⠀⠀⢀⣞⡽⠃⠀⠀⠀⠀⠀⠀⣼⡇⠀⠀⢸⠀⠀⠀",
        "⠀⠀⠀⠈⠙⠛⣿⠛⠛⠛⠛⠛⠛⠁⠀⠀⠀⠀⠀⠀⠀⠀⣿⡇⠀⠀⢿⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⢿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⣿⠀⠀⢈⡇⠀",
        "⠀⠀⠀⠀⠀⠀⢸⡇⠀⠀⠀⠀⠀⢀⣀⣀⣀⣀⠀⠀⠀⠀⠀⣧⣧⡤⠾⠃⠀⠀",
        "⠀⠀⠀⠀⠀⠀⢸⣧⠀⠀⠀⠀⢹⡟⠛⠛⠉⣿⡆⠀⠀⠀⢸⣇⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⢸⣟⡀⠀⠀⠀⣿⠁⠀⠀⠀⢸⠇⠀⠀⠀⢸⡽⠀⠀⠀⠀⠀⠀",
        "⠀⠀⠀⠀⠀⠀⠀⠻⢿⣦⣴⠟⠀⠀⠀⠀⠀⠘⣷⡤⣤⣤⡏⠀⠀⠀⠀⠀⠀⠀",
    };

    void Start()
    {
        DisplayLoadingText();
        inputField.onEndEdit.AddListener(OnCommandEntered);  

    }
    private void DisplayLoadingText()
    {
        StartCoroutine(PrintText(outputText, loadingTexts));  
 
        inputField.gameObject.SetActive(true); // Âêëþ÷àåì ïîëå ââîäà
        inputField.ActivateInputField(); // Àêòèâèðóåì êóðñîð
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
                outputText.text += "\n>> Available commands: help, play, exit, create, join";
                break;

            case "start":
                StartGame();
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
 
    private void StartGame()
    {

    } 
 
}
