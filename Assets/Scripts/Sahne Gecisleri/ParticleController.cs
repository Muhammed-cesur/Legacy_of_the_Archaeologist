using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public ParticleSystem particleSystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            particleSystem.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            particleSystem.Stop();
        }
    }
}
