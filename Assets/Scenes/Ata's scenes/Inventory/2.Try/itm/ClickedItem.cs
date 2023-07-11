using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ClickedItem : MonoBehaviour
{
    public Slot clickedSlot;

    public Color[] rarityColors;

    [SerializeField] RawImage image;

    [SerializeField] TextMeshProUGUI txt_Name;
    [SerializeField] TextMeshProUGUI txt_Rarity;
    [SerializeField] TextMeshProUGUI txt_Weight;
    [SerializeField] TextMeshProUGUI txt_Value;
    [SerializeField] TextMeshProUGUI txt_Type;
    [SerializeField] TextMeshProUGUI txt_Stack;

    [SerializeField] TextMeshProUGUI txt_Description;

    private void OnEnable()
    {
        SetUp();
    }

    void SetUp()
    {

        txt_Name.text = clickedSlot.ItemInSlot.name;
        txt_Weight.text = $"{clickedSlot.ItemInSlot.weight * clickedSlot.AmountInSlot}kg";
        txt_Stack.text = $"{clickedSlot.AmountInSlot}/{clickedSlot.ItemInSlot.maxStack}";
        txt_Value.text = $"{clickedSlot.AmountInSlot * clickedSlot.ItemInSlot.baseValue}";
        txt_Description.text = clickedSlot.ItemInSlot.description;

        switch (clickedSlot.ItemInSlot.rarity)
        {
            case Items.Rarity.common:
                image.color = rarityColors[0];
                txt_Rarity.text = "Common";
                break;
            case Items.Rarity.uncommon:
                image.color = rarityColors[1];
                txt_Rarity.text = "Uncommon";
                break;
            case Items.Rarity.rare:
                image.color = rarityColors[2];
                txt_Rarity.text = "Rare";
                break;
            case Items.Rarity.epic:
                image.color = rarityColors[3];
                txt_Rarity.text = "Epic";
                break;
            case Items.Rarity.legendary:
                image.color = rarityColors[4];
                txt_Rarity.text = "Legendary";
                break;
        }

        switch (clickedSlot.ItemInSlot.type)
        {
            case Items.Types.misecellaneous:
                txt_Type.text = "Misecellaneous";
                break;
            case Items.Types.craftingMaterial:
                txt_Type.text = "Crafting material";
                break;
            case Items.Types.equipment:
                txt_Type.text = "Equipment";
                break;
        }

    }
    
}
