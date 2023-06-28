using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFight : MonoBehaviour
{
    private Animator animator;
    private bool isAttacking;
    private float lastClickTime;
    private float maxComboDelay = 0.5f; 

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            
            if (Time.time - lastClickTime <= maxComboDelay)
            {
             
                ComboAttack();
            }
            else
            {
              
                Attack();
            }

          
            lastClickTime = Time.time;
        }
        else if (!isAttacking)
        {
            
            Idle();
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack1");
        isAttacking = true;
    }

    private void ComboAttack()
    {
        animator.SetTrigger("Attack2");
        isAttacking = true;
    }

    private void Idle()
    {
        animator.SetTrigger("Idle");
        isAttacking = false;
    }
}