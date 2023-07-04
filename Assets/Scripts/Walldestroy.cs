using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walldestroy : MonoBehaviour
{

    public GameObject wall;
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Destroy(wall);
        }
    }
}
