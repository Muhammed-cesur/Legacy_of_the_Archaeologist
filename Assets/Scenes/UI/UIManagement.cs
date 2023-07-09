using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class UIManagement : MonoBehaviour 
{
    

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    
    public void GoOptions()
    {
        SceneManager.LoadScene(2);
    }

    public void GoNextMain()
    {
        SceneManager.LoadScene(0);
    }
    
    public void RestartGame()
    {
        Debug.Log("Are you sure you want to Restart?");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Are you sure you want to Exit?");
        Application.Quit();
    }
}

