using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject mapUI; // 2B harita UI elemaný

    void Start()
    {
        // Baþlangýçta 2B haritayý kapalý olarak ayarla
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
        // 2B harita durumunu tersine çevir
        bool mapActive = mapUI.activeSelf;
        mapUI.SetActive(!mapActive);
    }
}
