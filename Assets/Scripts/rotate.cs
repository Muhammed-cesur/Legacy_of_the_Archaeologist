using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public float YrotationSpeed = 0.01f; 
    public float XrotationSpeed = 0.0f; 


    private float y = 0.0f;
    private float x = 0.0f;


    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        transform.localRotation = Quaternion.Euler(x, y, 0);
        y += YrotationSpeed; 
		x += XrotationSpeed;
    }
}
