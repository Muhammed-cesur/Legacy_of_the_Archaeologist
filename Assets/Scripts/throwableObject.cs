using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwableObject : MonoBehaviour
{
    private Transform pickUp;
    private Transform player;

    public float PickUpDistance;
    public float Force;

    public bool throwable;
    public bool itemspick;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").transform;
        pickUp = GameObject.Find("Pickup").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && itemspick == true && throwable)
        {
            Force += 300 * Time.deltaTime;
        }

        PickUpDistance = Vector3.Distance(player.position, transform.position);

        if (PickUpDistance <= 2)
        {
            if (Input.GetKeyDown(KeyCode.Space) && itemspick == false && pickUp.childCount < 1)
            {
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Collider>().enabled = false;
                this.transform.position = pickUp.position;
                this.transform.parent = GameObject.Find("Pickup").transform;

                itemspick = true;
                Force = 0;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && itemspick == true)
        {
            throwable = true;
            if (Force > 10)
            {
                rb.AddForce(player.transform.forward * Force);
                this.transform.parent = null;
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Collider>().enabled = true;

                itemspick = false;
                throwable = false;
                Force = 0;
            }
            Force = 0;
        }
    }
}
