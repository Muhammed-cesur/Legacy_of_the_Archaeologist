using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAnim : MonoBehaviour
{
    
    private Animator _animator;

    public GameObject dungeonExit;
    public GameObject dungeonExitDoor; 
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("b");
            _animator.SetTrigger("Key");
            dungeonExitDoor.transform.Rotate(new Vector3(0, -90, 0));
            dungeonExit.GetComponent<Collider>().isTrigger = true;

        }
    }
}
