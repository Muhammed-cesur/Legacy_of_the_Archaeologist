using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject mapUI; // 2B harita UI eleman�

    void Start()
    {
        // Ba�lang��ta 2B haritay� kapal� olarak ayarla
        mapUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMap();
        }
    }

    void ToggleMap()
    {
        // 2B harita durumunu tersine �evir
        bool mapActive = mapUI.activeSelf;
        mapUI.SetActive(!mapActive);
    }
}
