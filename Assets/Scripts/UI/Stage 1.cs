using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stage1 : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;
    public TextMeshProUGUI outputText;  // Текстовый компонент для отображения строк
    public Slider progressBar; // Полоса прогресса
    private string[] loadingTexts = new string[]
    {
        "   \r\nInitializing hardware:\r\n  - Verifying CPU, memory, and other critical components\r\n Loading BIOS:\r\n  - Reading the Basic Input/Output System (BIOS) from non-volatile memory (typically ROM)\r\n Configuring hardware:\r\n  - Setting up system memory, interrupt controllers, and peripheral devices\r\n Performing device detection:\r\n  - Identifying and configuring connected storage devices, network cards, etc...",
        "   Loading the operating system bootloader:\r\n  - Loading the small program that initializes the operating system\r\n Passing control to the bootloader:\r\n  - Transferring execution to the bootloader's code...",
        "   Reading the main operating system kernel from the boot device\r\n Initializing memory management:\r\n  - Setting up page tables and managing physical and virtual memory\r\n Creating user space:\r\n  - Allocating memory and setting up a protected environment for user applications...",
        "   Starting essential services:\r\n  - Initializing drivers, file systems, and network connectivity\r\n Mounting root filesystem:\r\n  - Accessing the main storage device and making its contents available\r\n Running init process:\r\n  - The first user-space process (usually named \"init\") that manages system startup tasks..."
    };

    private void Start()
    {
        StartCoroutine(One());
    }

    private IEnumerator One()
    {
        // Этап 1: Выводим строки загрузки
        foreach (string line in loadingTexts)
        {
            outputText.text += line + "\n";  // Добавляем строку в текст
            yield return new WaitForSeconds(1f);  // Задержка между строками
        }

        // Этап 2: Полоса прогресса
        outputText.text += "\n\nLoading... Please wait...\n"; // Пишем статус
        progressBar.gameObject.SetActive(true); // Показываем полоску прогресса
        yield return StartCoroutine(ShowProgressBar());

    }

    // Этап заполнения полосы прогресса
    private IEnumerator ShowProgressBar()
    {
        float progress = 0f;
        while (progress < 1f)
        {
            if (progress < 0.9f)
            {
                progress += Time.deltaTime * 0.4f;  // Заполнение прогресса
                progressBar.value = progress;
            }
            else
            {
                progress += Time.deltaTime * 0.05f;  // Заполнение прогресса
                progressBar.value = progress;
            }

            yield return null;  // Ждем один кадр
        }
 
        panel1.SetActive(false);
        panel2.SetActive(true);
        
    }
}
