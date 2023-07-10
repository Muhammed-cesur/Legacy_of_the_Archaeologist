using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int maxHealth = 100; // Maksimum can puaný
    private int currentHealth; // Mevcut can puaný

    private void Start()
    {
        currentHealth = maxHealth; // Baþlangýçta can puanýný maksimum deðere ayarla
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = Mathf.Clamp(value, 0, maxHealth); }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth; // Can puanýný sýfýrla
    }

    // Diðer fonksiyonlar ve kodlar...
}
