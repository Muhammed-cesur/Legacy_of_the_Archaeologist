using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public int Helath;
    public int Exp;

    public Text HelathText;
    public Text ExpText;

    private void Awake()
    {
        Instance = this;
    }

    public void IncreaseHealth(int value)
    {
        Helath += value;
        HelathText.text = $"HP:{Health}";
    }

    public void IncreaseExp(int value)
    {
        Exp += value;
        ExpText.text = $"HP:{EXp}";
    }
}