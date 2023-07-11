using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int maxHealth = 100; // Maksimum can puan�
    private int currentHealth; // Mevcut can puan�

    private void Start()
    {
        currentHealth = maxHealth; // Ba�lang��ta can puan�n� maksimum de�ere ayarla
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = Mathf.Clamp(value, 0, maxHealth); }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth; // Can puan�n� s�f�rla
    }

    // Di�er fonksiyonlar ve kodlar...
}
