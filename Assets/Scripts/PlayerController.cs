using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    private float InputX;
    private float InputY;
    
    public Transform Model;
    private Animator _anim;
    private Vector3 StickDirection;
    private Camera mainCam;
    private Rigidbody rb;
    private float maxSpeed;  
    
    [Range(1, 20)]
    public float RotationSpeed;
    
    public float damp;
    public KeyCode runButton=KeyCode.LeftShift;
    public KeyCode walkButton=KeyCode.C;
    public MovementType hareketTipi ;
    public enum MovementType
    {
        Direct,
        strafe
    }

    
    private void Start()
    {
        _anim = GetComponent<Animator>();
        mainCam = Camera.main;
    }

    private void LateUpdate()
    {
        InputMove();
        InputRotation();
        Movement();
    }

    #region Movement and Cam settings
    void Movement()
    {


        {
            StickDirection = new Vector3(InputX, 0, InputY);
            if (Input.GetKey(runButton))
            {
                maxSpeed = 2;
                InputX = 2 * Input.GetAxis("Horizontal");
                InputY = 2 * Input.GetAxis("Vertical");
            }        
            else if (Input.GetKey(walkButton))
            {
                maxSpeed = 0.2f;
                InputX = Input.GetAxis("Horizontal");
                InputY = Input.GetAxis("Vertical");
            } 
            else
            {
                maxSpeed = 1;
                InputX = Input.GetAxis("Horizontal");
                InputY = Input.GetAxis("Vertical");
            }            
        }

    }
    void InputMove()
    {
        _anim.SetFloat("Speed", Vector3.ClampMagnitude(StickDirection, maxSpeed).magnitude, damp, Time.deltaTime * 10);
    }
    void InputRotation()
    {
        Vector3 rotOfSet = mainCam.transform.TransformDirection(StickDirection);
        rotOfSet.y = 0;
        Model.forward = Vector3.Slerp(Model.forward, rotOfSet, Time.deltaTime * RotationSpeed);
    }
    #endregion
}