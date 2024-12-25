using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class Stage4 : Console
{
    public TMP_InputField inputField;
    
    string[] loadingTexts = new string[]
    {
        "ГАЙД",
    };

    private void Start()
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
                SceneManager.LoadScene(0);
                break;


            case "create":
                outputText.text += "\n>> create room...";
                SceneManager.LoadScene(0);
                break;

            case "join":
                outputText.text += "\n>> join room...";
                SceneManager.LoadScene(0);
                break;

            case "check":
                outputText.text += "\n>> join room...";
                SceneManager.LoadScene(0);
                break;

            default:
                outputText.text += "\n>> Unknown command! Try again.";
                break;
        }

        inputField.text = ""; 
        inputField.ActivateInputField();  
    }
 
    private void Create()
    {

    } 

    private void Join()
    {

    }

}
