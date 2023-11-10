using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    public static EquipmentUI S;
    public GameObject UIbase;

    public EquipSlot weaponSlot;
    public EquipSlot armorSlot;


    public GameObject P_EquipSlot;
    public Transform T_EquipSlots;

    public List<GameObject> slots;

    //infoUI
    public GameObject infoUI;
    public Text equipName;
    public Text equipInfo;
    public Image equipImage;



    private void Awake()
    {
        if (S==null)
        {
            S = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        infoUI.SetActive(false);
        for (int i = 0; i < Player.S.EquipmentStrings.Count; i++)
        {
            AddEquipment(Dictionaries.S.GetEquipment(Player.S.EquipmentStrings[i]));
        }
    }
    public void UIOn()
    {

    }
    public void UIOff()
    {

    }
    
    public void InfoUIOn(Equipment _equipment)
    {
        infoUI.SetActive(true);
        switch (Options.S.language)
        {
            case Options.Language.Kor:
                equipName.text = _equipment.equipName;
                equipInfo.text = _equipment.equipInfo;
                break;
            case Options.Language.Eng:
                equipName.text = _equipment.equipEName;
                equipInfo.text = _equipment.equipEInfo;
                break;
            default:
                break;
        }

        equipImage.sprite = _equipment.equipSprite;
    }
    public void GetEquipment(string _equipment)
    {
        Equipment equip = Dictionaries.S.GetEquipment(_equipment);
        Player.S.EquipmentStrings.Add(equip.equipName);

        Player.S.hp += equip.HP;
        Player.S.ATK += equip.ATK;
        Player.S.DEF += equip.DEF;
        Player.S.SPD += equip.SPD;
        Player.S.POW += equip.POW;
        Player.S.CRD += equip.CRD;
        Player.S.CRC += equip.CRC;
        Player.S.ICD += equip.ICD;
        Player.S.DCD += equip.DCD;
        Player.S.HIT += equip.HIT;
        Player.S.PIE += equip.PIE;
        Player.S.RG_Infect += equip.InfectREG;
        Player.S.RG_Fear += equip.FearREG;
        Player.S.RG_Doom += equip.DOOMREG;
        Player.S.RG_Curse += equip.CURSEREG;
        Player.S.AVD+= equip.AVD;
        Player.S.RG_Burn += equip.AllRG;
        Player.S.RG_Curse += equip.AllRG;
        Player.S.RG_Daze += equip.AllRG;
        Player.S.RG_Dispel += equip.AllRG;
        Player.S.RG_Doom += equip.AllRG;
        Player.S.RG_Erosion += equip.AllRG;
        Player.S.RG_Fear += equip.AllRG;
        Player.S.RG_Infect += equip.AllRG;
        Player.S.RG_Misfortune += equip.AllRG;
        Player.S.RG_Paralyze += equip.AllRG;
        Player.S.RG_Poison += equip.AllRG;
        Player.S.RG_Sleep += equip.AllRG;
        Player.S.RG_stun += equip.AllRG;



        AddEquipment(equip);
    }
    public void AddEquipment(Equipment equipment)
    {
        GameObject go = Instantiate(P_EquipSlot, T_EquipSlots);
        EquipSlot slot = go.GetComponent<EquipSlot>();
        slot.equipment = equipment;
        slot.equipImage.sprite = slot.equipment.equipSprite;
        slots.Add(go);

        go.GetComponent<Button>().onClick.AddListener(() =>InfoUIOn(slot.equipment));




    }
    public int Mp7QuestCheck()
    {
        int num = 0;
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].GetComponent<EquipSlot>().equipment.isquestitem)
            {
                num += 1;
            }
        }
        return num;
    }
    public void RemoveEquipment(Equipment _equipment)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].GetComponent<EquipSlot>().equipment.equipName==_equipment.equipName)
            {
                Destroy(slots[i].gameObject);
                slots.Remove(slots[i]);
                break;
            }
        }
        for (int i = 0; i < Player.S.EquipmentStrings.Count; i++)
        {
            if (Player.S.EquipmentStrings[i]==_equipment.equipName)
            {
                Player.S.EquipmentStrings.RemoveAt(i);

                return;
            }
           
        }

    }

}
