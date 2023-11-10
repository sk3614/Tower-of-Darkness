using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EquipSlot : MonoBehaviour
{
    public Image equipImage;
    public Equipment equipment;

    public void Start()
    {
        SetEquipSlot();
    }

    public void SetEquipSlot()
    {
        if (equipment != null)
        {
            equipImage.sprite = equipment.equipSprite;
        }
    }
}
