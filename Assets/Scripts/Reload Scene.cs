using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    private UIManager uıManager;

    private void Awake()
    {
        uıManager = FindObjectOfType<UIManager>();
        if (Time.timeScale == 0f)
        {
            ToggleGamePause();
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player") )
        {
            StartCoroutine(uıManager.GameOverCoroutine(2f));
            StartCoroutine(ReloadSceneAfterDelay(1f));
        }
    }
    
    IEnumerator ReloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
