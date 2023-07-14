using UnityEngine;
using UnityEngine.SceneManagement;



public class UIManagement : MonoBehaviour 
{
    

   
    
    
    public void RestartGame()
    {
        Debug.Log("Are you sure you want to Restart?");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Credits()
    {
        SceneManager.LoadScene(6);
    }
    public void QuitGame()
    {
        Debug.Log("Are you sure you want to Exit?");
        Application.Quit();
    }
}

