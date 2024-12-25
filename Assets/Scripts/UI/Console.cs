using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    public GameObject panel_A;
    public GameObject panel_B;
    public TextMeshProUGUI outputText;  
    public Slider progressBar;  
    public IEnumerator PrintText(TextMeshProUGUI textUI , string[] loadingTexts)
    {
        foreach (string line in loadingTexts)
        {
            textUI.text += line + "\n";   
            yield return new WaitForSeconds(1f);   
        }
    }
    public IEnumerator ShowProgressBar(Slider progressBar, GameObject panl1, GameObject panl2)
    {
        float progress = 0f;
        while (progress < 1f)
        {
            if (progress < 0.9f)
            {
                progress += Time.deltaTime * 0.4f;  // ���������� ���������
                progressBar.value = progress;
            }
            else
            {
                progress += Time.deltaTime * 0.05f;  // ���������� ���������
                progressBar.value = progress;
            }

            yield return null;  // ���� ���� ����
        }
 
        panl1.SetActive(false);
        panl2.SetActive(true);   
    }

    public IEnumerator AsyncLoadScene(string sceneName) 
    {
        AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
 
        while (!operation.isDone) 
        {    
            float progress = Mathf.Clamp01(operation.progress / 0.9f); 
            progressBar.value = progress;
 
            yield return new WaitForSeconds(1f);
        }
    }
}
