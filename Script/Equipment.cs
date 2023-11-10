using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewEquipment", menuName = "NewEquipment")]
public class Equipment : ScriptableObject
{
    public string equipName;
    public string equipEName;
    [TextArea]
    public string stats;
    [TextArea]
    public string E_stats;
    public int UpgradeNum;
    public Sprite equipSprite;
    public EquipKind equipKind;
    public Skill weaponNormalAttack;
    public Sprite[] armorSkin;
    [TextArea]
    public string equipInfo;
    [TextArea]
    public string equipEInfo;
    public Equipment upgradeEquip;
    public bool isquestitem;
    public int ATK;
    public int DEF;
    public int POW;
    public int SPD;
    public int CRD;
    public int HP;
    public int ICD;
    public int DCD;
    public int HIT;
    public int CRC;
    public int PIE;
    public int FearREG;
    public int InfectREG;
    public int DOOMREG;
    public int CURSEREG;
    public int AVD;
    public int AllRG;


    public enum EquipKind
    {
        Weapon,
        Armor,
        Accecaary,
    }
}
