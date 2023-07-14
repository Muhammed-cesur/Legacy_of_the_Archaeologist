using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainManager : MonoBehaviour
{
    public Canvas mainCanvas;
    public Canvas CreditsCanvas;
    public Canvas OptionsCanvas;
    // Start is called before the first frame update
    void Start()
    {
        mainCanvas.enabled = true;
        CreditsCanvas.enabled = false;
        OptionsCanvas.enabled = false;
    }
    public void MainMenu()
    {
        mainCanvas.enabled = true;
        CreditsCanvas.enabled = false;
        OptionsCanvas.enabled = false;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    

    public void OpenCredits()
    {
        mainCanvas.enabled = false;
        CreditsCanvas.enabled = true;
        OptionsCanvas.enabled = false;
    }
    
    public void OpenOptions()
    {
        mainCanvas.enabled = false;
        CreditsCanvas.enabled = false;
        OptionsCanvas.enabled = true;
    }

    public void QuitGame()
    {
        Debug.Log("Are you sure you want to Exit?");
        Application.Quit();
    }
}
