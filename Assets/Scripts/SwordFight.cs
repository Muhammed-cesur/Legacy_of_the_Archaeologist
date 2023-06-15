using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFight : MonoBehaviour
{
    private Animator _animator;
    public float cooldownTime = 2f;
    private float nextFireTime = 0f;
    public static int ofClicks = 0;
    private float lastClickTime = 0;
    private float comboDelay = 1;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime>0.7f && _animator.GetCurrentAnimatorStateInfo(0).IsName("hit") )
        {
            _animator.SetBool("hit", false);
        }
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime>0.7f && _animator.GetCurrentAnimatorStateInfo(0).IsName("hit2") )
        {
            _animator.SetBool("hit2", false);
        }


        if (Time.time-lastClickTime>comboDelay)
        {
            ofClicks = 0;
        }

        if (Time.time>nextFireTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnClick();
            }
        }
    }

    void OnClick()
    {
        lastClickTime = Time.time;
        ofClicks++;
        if (ofClicks==1)
        {
            _animator.SetBool("hit",true);
        }

        ofClicks = Mathf.Clamp(ofClicks, 0, 3);

        if (ofClicks>=2&& _animator.GetCurrentAnimatorStateInfo(0).normalizedTime>0.7f && _animator.GetCurrentAnimatorStateInfo(0).IsName("hit") )
        {
            _animator.SetBool("hit",false);
            _animator.SetBool("hit2",true);

        }
        if (ofClicks>=3&& _animator.GetCurrentAnimatorStateInfo(0).normalizedTime>0.7f && _animator.GetCurrentAnimatorStateInfo(0).IsName("hit2") )
        {
            _animator.SetBool("hit2",false);
            _animator.SetBool("hit3",true);

        }
    }
}
