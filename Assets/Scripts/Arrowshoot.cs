using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrowshoot : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;
    public float arrowForce = 20f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootArrow();
        }
        
    }

    void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.transform.position, Quaternion.identity);
        //arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * arrowForce, ForceMode.Impulse);
    }
    

}
