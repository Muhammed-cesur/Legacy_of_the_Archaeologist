using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NpcDiyalog : MonoBehaviour
{
    public List<string> diyaloglar = new List<string>(); 
    public GameObject Diyalog;
    
    private int mevcutDiyalogIndex = 0; 

    bool playerDetection = false;
   

    public TextMeshProUGUI diyalogText; 

    void Start()
    {
        Diyalog.SetActive(false);
        GuncelleDiyalogText();
    }

    void Update()
    {
        if (playerDetection && Input.GetKeyDown(KeyCode.F))
        {
            GecisYap();
        }
    }

    void GecisYap()
    {
        if (mevcutDiyalogIndex < diyaloglar.Count - 1)
        {
            mevcutDiyalogIndex++;
            GuncelleDiyalogText();
        }
        else
        {
            Debug.Log("Diyaloglar bitti.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            playerDetection = true;
            Diyalog.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerDetection = false;
        Diyalog.SetActive(false);
    }

    void GuncelleDiyalogText()
    {
        if (diyalogText != null)
        {
            diyalogText.text = diyaloglar[mevcutDiyalogIndex];
        }
    }
}
