using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalGiris : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(5);
        }
    }
}
