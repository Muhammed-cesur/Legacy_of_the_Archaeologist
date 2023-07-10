using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Canvas pauseCanvas;
    public Canvas inGameCanvas;
    public Canvas inventoryCanvas;
    
    private void Start()
    {
        pauseCanvas.enabled = false;
        inventoryCanvas.enabled = false;
        inGameCanvas.enabled = true; 
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
    }
    
    public void OpenInventory()
    {
        if (inventoryCanvas.enabled == false)
        {
            inventoryCanvas.enabled = true;
            inGameCanvas.enabled = false; 
            pauseCanvas.enabled = false;
            ToggleGamePause();
        }

        else if (inventoryCanvas.enabled == true)
        {
            inventoryCanvas.enabled = false;
            inGameCanvas.enabled = true;
            pauseCanvas.enabled = false;
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
            ToggleGamePause();
        }

        else if (pauseCanvas.enabled == true)
        {
            inventoryCanvas.enabled = false;
            inGameCanvas.enabled = true;
            pauseCanvas.enabled = false;
            ToggleGamePause();
        }
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
}
