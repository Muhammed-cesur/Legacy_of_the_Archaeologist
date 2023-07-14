using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabirentGiris : MonoBehaviour
{
    public GameObject labirentGiris;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(labirentGiris);
    }
}
