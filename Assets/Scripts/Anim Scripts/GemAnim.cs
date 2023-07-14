using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemAnim : MonoBehaviour
{
    private Animator _animator;
    public GameObject prison; 


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("c");
            _animator.SetTrigger("Gem");
            prison.transform.Rotate(new Vector3(0, -90, 0));
            
        }
    }
}
