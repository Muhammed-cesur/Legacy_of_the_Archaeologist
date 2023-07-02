using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class UIManagement : MonoBehaviour 
{
    public string nextSceneName;
    public KeyCode key = KeyCode.I;
    public KeyCode key2 = KeyCode.Escape;
    
    

    public void GoNextScene1()
    {
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            SceneManager.LoadScene(3);
            
        }

        if (Input.GetKeyDown(key2))
        {
            SceneManager.LoadScene(4);
            
        }

        
    }

    public void GoNextOptions()
    {
        SceneManager.LoadScene(2);
    }

    public void GoNextMain()
    {
        SceneManager.LoadScene(0);
    }

    public void GoNextInventory()
    {
        SceneManager.LoadScene(3);
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

