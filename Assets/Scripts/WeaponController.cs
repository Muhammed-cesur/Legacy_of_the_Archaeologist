 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private bool Equip = false;
    private Animator _anim;

    public GameObject Eldekılıc;
    public GameObject Sirtakılıc;
    
    
    void Start()
    {
        _anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        _anim.SetBool("Equip", Equip);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Equip = !Equip;
        }
    }

    void Elde()
    {
        Sirtakılıc.SetActive(false);
        Eldekılıc.SetActive(true);
    }    
    void Sirta()
    {
        Sirtakılıc.SetActive(true);
        Eldekılıc.SetActive(false);        
    }
    
}
