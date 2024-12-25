using System.Collections;


public class Stage3 : Console
{
    private void Start()
    {
        StartCoroutine(One());
    }

    private IEnumerator One()
    { 
        outputText.text += "\n\nLoading... Please wait...\n"; 
        progressBar.gameObject.SetActive(true);  
        yield return StartCoroutine(ShowProgressBar(progressBar, panel_A, panel_B));

    } 

}
