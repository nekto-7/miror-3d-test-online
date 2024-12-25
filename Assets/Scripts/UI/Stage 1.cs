using System.Collections;

public class Stage1 : Console
{

    private string[] loadingTexts = new string[]
    {
        "   \r\nInitializing hardware:\r\n  - Verifying CPU, memory, and other critical components\r\n Loading BIOS:\r\n  - Reading the Basic Input/Output System (BIOS) from non-volatile memory (typically ROM)\r\n Configuring hardware:\r\n  - Setting up system memory, interrupt controllers, and peripheral devices\r\n Performing device detection:\r\n  - Identifying and configuring connected storage devices, network cards, etc...",
        "   Loading the operating system bootloader:\r\n  - Loading the small program that initializes the operating system\r\n Passing control to the bootloader:\r\n  - Transferring execution to the bootloader's code...",
        "   Reading the main operating system kernel from the boot device\r\n Initializing memory management:\r\n  - Setting up page tables and managing physical and virtual memory\r\n Creating user space:\r\n  - Allocating memory and setting up a protected environment for user applications...",
        "   Starting essential services:\r\n  - Initializing drivers, file systems, and network connectivity\r\n Mounting root filesystem:\r\n  - Accessing the main storage device and making its contents available\r\n Running init process:\r\n  - The first user-space process (usually named \"init\") that manages system startup tasks..."
    };

    private void Start()
    {
        outputText.text += "\n\nLoading... Please wait...\n";
        progressBar.gameObject.SetActive(true); 
        StartCoroutine(One());
    }

    private IEnumerator One()
    {
        yield return StartCoroutine(PrintText(outputText, loadingTexts));


        yield return StartCoroutine(ShowProgressBar(progressBar, panel_A, panel_B));

    }

    
}
