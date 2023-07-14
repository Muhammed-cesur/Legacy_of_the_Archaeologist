using UnityEngine;
using UnityEngine.SceneManagement;

public class Dungeon1Giris : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SceneManager.LoadScene(2);
            }
        }
    
}
