using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string itemName;
    public string eitemname;
    public Sprite itemImage;
    public ItemType itemType;
    public StatType statType;
    public SkillClass skillClass;
    public string itemShortInfo;
    [TextArea]
    public string itemLongInfo;
    public string E_itemShortInfo;
    [TextArea]
    public string E_itemLongInfo;
    public int buyPrice;
    public int sellPrice;
    public int potionHealNum;
    public int sortNum;

    public enum ItemType
    {
        UseItem,
        KeyItem,
        EventItem,
        Artifact,
        ETC,
        Potion,
        Stone,
        Skill,
        SecretWallChance,
        RunAway,
        Equip,
    }
    public enum StatType
    {
        None,
        ATK,
        DEF,
        AVD,
        HIT,
        POW,
        Level,
        SPD,
        CP,
        PP,
        TP,
        CRC,
           
    }
    public enum SkillClass
    {
        normal,
    }

        
}
