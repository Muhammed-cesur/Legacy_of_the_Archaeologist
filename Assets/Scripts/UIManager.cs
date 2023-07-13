using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Canvas pauseCanvas;
    public Canvas inGameCanvas;
    public Canvas inventoryCanvas;
    public Canvas map;
    public Canvas gameOverCanvas;
    
    private void Start()
    {
        pauseCanvas.enabled = false;
        inventoryCanvas.enabled = false;
        inGameCanvas.enabled = true;
        map.enabled = false;
        gameOverCanvas.enabled = false;
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
        pauseCanvas.enabled = false;
        inventoryCanvas.enabled = false;
        inGameCanvas.enabled = false;
        map.enabled = false;
        gameOverCanvas.enabled = true;
        ToggleGamePause();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
