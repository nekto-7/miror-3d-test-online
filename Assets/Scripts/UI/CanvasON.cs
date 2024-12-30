using UnityEngine;

public class CanvasON : MonoBehaviour
{
    public GameObject canvas;
    void Start()
    {
        canvas.SetActive(true);
        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.None;
        
    }

 
}
