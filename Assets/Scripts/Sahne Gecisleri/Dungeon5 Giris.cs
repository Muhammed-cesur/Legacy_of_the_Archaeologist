using UnityEngine;
using UnityEngine.SceneManagement;

public class Dungeon5Giris : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(4);
        }
    }
}
