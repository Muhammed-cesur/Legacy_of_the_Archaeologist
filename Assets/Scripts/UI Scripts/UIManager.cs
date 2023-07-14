using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public Canvas pauseCanvas;
    public Canvas inGameCanvas;
    public Canvas inventoryCanvas;
    public Canvas map;
    public Canvas gameOverCanvas;
    
    private Vector3 initialPosition;
    private void Start()
    {
        pauseCanvas.enabled = false;
        inventoryCanvas.enabled = false;
        inGameCanvas.enabled = true;
        map.enabled = false;
        gameOverCanvas.enabled = false;
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseCanvas(); // ESC tuşuna basıldığında Canvas'ı aç/kapat
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenInventory(); // I tuşuna basıldığında Envanter'i aç/kapat
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            OpenMap();
        }
    }
    void OpenMap()
    {
        if (map.enabled == false)
        {
            inventoryCanvas.enabled = false;
            inGameCanvas.enabled = false;
            pauseCanvas.enabled = false;
            map.enabled = true;
            ToggleGamePause();

        }

        else if (map.enabled == true)
        {
            inventoryCanvas.enabled = false;
            inGameCanvas.enabled = true;
            pauseCanvas.enabled = false;
            map.enabled = false;
            ToggleGamePause();

        }
    }
    public void OpenInventory()
    {
        if (inventoryCanvas.enabled == false)
        {
            inventoryCanvas.enabled = true;
            inGameCanvas.enabled = false; 
            pauseCanvas.enabled = false;
            map.enabled = false;
            ToggleGamePause();
        }

        else if (inventoryCanvas.enabled == true)
        {
            inventoryCanvas.enabled = false;
            inGameCanvas.enabled = true;
            pauseCanvas.enabled = false;
            map.enabled = false;
            ToggleGamePause();
        }
     
    }
    

    public void TogglePauseCanvas()
    {
        if (pauseCanvas.enabled == false)
        {
            inventoryCanvas.enabled = false;
            inGameCanvas.enabled = false; 
            pauseCanvas.enabled = true;
            map.enabled = false;
            ToggleGamePause();
        }

        else if (pauseCanvas.enabled == true)
        {
            inventoryCanvas.enabled = false;
            inGameCanvas.enabled = true;
            pauseCanvas.enabled = false;
            map.enabled = false;
            ToggleGamePause();
        }
    }
    
    public void GameOver()
    {
        if (gameOverCanvas.enabled == false)
        {
            pauseCanvas.enabled = false;
            inventoryCanvas.enabled = false;
            inGameCanvas.enabled = false;
            map.enabled = false;
            gameOverCanvas.enabled = true;
            ToggleGamePause();
        }
    }
    
   
    
    public  IEnumerator GameOverCoroutine(float delay)
    {
        // Game Over ekranını etkinleştir
        GameOver();
        yield return new WaitForSeconds(delay); // 3 saniye bekle
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //ResetCharacter();
        ToggleGamePause();

    }
    
    private void ToggleGamePause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f; // Eğer oyun duraklatılmışsa devam ettir
        }
        else
        {
            Time.timeScale = 0f; // Eğer oyun devam ediyorsa duraklat
        }
    }
    public void GameResume()
    {
        pauseCanvas.enabled = false;
        inventoryCanvas.enabled = false;
        inGameCanvas.enabled = true;
        map.enabled = false;
        gameOverCanvas.enabled = false;
        ToggleGamePause();
    }
    
    
}
