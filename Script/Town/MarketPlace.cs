using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class SellItem
{
    public Item item;
    public int num;
    public int price;
    public int peddlerNum;
}
[System.Serializable]
public class SellClassSkill
{
    public Item item;
    public string grade;
    public int num;
    public int price;
}
[System.Serializable]
public class SellPublicSkill
{
    public Item item;
    public string grade;
    public int num;
    public int price;
}
[System.Serializable]
public class SellArtifact
{
    public Item item;
    public string grade;
    public int num;
    public int price;
}
[System.Serializable]
public class SellEquip
{
    public Item item;
    public string name;
    public int num;
    public int price;
}
[System.Serializable]
public class SellListByProgress
{
    public int Progress;
    public List<SellItem> sellItems = new List<SellItem>();
    public List<SellClassSkill> sellSkills = new List<SellClassSkill>();
    public List<SellPublicSkill> sellPublicSkills = new List<SellPublicSkill>();
    public List<SellArtifact> sellartifacts = new List<SellArtifact>();
    public List<SellEquip> sellEquip = new List<SellEquip>();
}
public class MarketPlace : MonoBehaviour
{
    public GameObject UIbase;
    public GameObject InfoUI;
    public Text T_nowGold;
    public GameObject P_slot;
    public Transform T_slots;

    public List<GameObject> slots=new List<GameObject>();

    public Image itemImage;
    public Text itemName;
    public Text itemNum;//남은 수
    public Text ItemRamainNum;//보유 수
    public Text itemInfo;
    public Text itemPrice;
    public bool isload;
    public List<SellListByProgress> sellListByProgresses = new List<SellListByProgress>();
    private TownShopSlot selectedSlot;
    public bool isBlackMarket;
    public bool isBlackMarketload;
    public bool isPortShop;
    public bool isPeddler;
    public bool iscollector;
    public PeddlerItems PeddlerItems;
    public GameObject PSkillCaution;
    public bool isCaution;
    
    private void Start()
    {
        if (Player.S.isLoad)
        {
            isload = false;
            isBlackMarketload = true;
        }
        else
        {
            if (isBlackMarket)
            {
            }
            else
            {
                SetMarketByProgress();
            }

        }
       
    }

    public void SetMarketByTable()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Destroy(slots[i].gameObject);
        }
        slots.Clear();



    }

    public void SetMarketByProgress()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Destroy(slots[i].gameObject);
        }
        slots.Clear();

        for (int i = 0; i < sellListByProgresses.Count; i++)
        {
            if (sellListByProgresses[i].Progress==Player.S.mainProgress)
            {
                for (int j = 0; j < sellListByProgresses[i].sellItems.Count; j++)
                {
                    GameObject go = Instantiate(P_slot, T_slots);
                    TownShopSlot slot = go.GetComponent<TownShopSlot>();
                    slot.item = sellListByProgresses[i].sellItems[j].item;
                    slot.itemPrice = sellListByProgresses[i].sellItems[j].price;
                    slot.itemNum = sellListByProgresses[i].sellItems[j].num;
                    slot.market = this;
                    slot.SetShopSlot();
                    slot.peddler = sellListByProgresses[i].sellItems[j].peddlerNum;
                    slots.Add(go);


                }
                for (int j = 0; j < sellListByProgresses[i].sellEquip.Count; j++)
                {
                    
                        GameObject go = Instantiate(P_slot, T_slots);
                        TownShopSlot slot = go.GetComponent<TownShopSlot>();
                        slot.equipment = Dictionaries.S.GetEquipment(sellListByProgresses[i].sellEquip[j].name);
                        slot.item = sellListByProgresses[i].sellEquip[j].item;
                        slot.itemPrice = sellListByProgresses[i].sellEquip[j].price;
                        slot.itemNum = 1;

                        slot.market = this;
                        slot.SetShopSlot();
                        slots.Add(go);

                }
                for (int j = 0; j < sellListByProgresses[i].sellSkills.Count; j++)
                {
                    List<Skill> skills = SkillDic.S.ClassSkillByRandom(sellListByProgresses[i].sellSkills[j].grade, sellListByProgresses[i].sellSkills[j].num);
                    Debug.Log(skills.Count);
                    for (int k = 0; k < sellListByProgresses[i].sellSkills[j].num; k++)
                    {
                        GameObject go = Instantiate(P_slot, T_slots);
                        TownShopSlot slot = go.GetComponent<TownShopSlot>();
                        slot.skill = skills[k];
                        slot.item = sellListByProgresses[i].sellSkills[j].item;
                        slot.itemPrice = sellListByProgresses[i].sellSkills[j].price;
                        slot.itemNum = 1;
                        slot.market = this;
                        slot.SetShopSlot();
                        slots.Add(go);
                    }

                }
                for (int l = 0; l < sellListByProgresses[i].sellPublicSkills.Count; l++)
                {
                    List<Skill> skills = SkillDic.S.NormalSkillByRandom(sellListByProgresses[i].sellPublicSkills[l].grade, sellListByProgresses[i].sellPublicSkills[l].num);

                    for (int k = 0; k < sellListByProgresses[i].sellPublicSkills[l].num; k++)
                    {
                        GameObject go = Instantiate(P_slot, T_slots);
                        TownShopSlot slot = go.GetComponent<TownShopSlot>();
                        slot.skill = skills[k];
                        slot.item = sellListByProgresses[i].sellPublicSkills[l].item;
                        slot.itemPrice = sellListByProgresses[i].sellPublicSkills[l].price;
                        slot.itemNum = 1;
                        slot.market = this;
                        slot.SetShopSlot();
                        slots.Add(go);
                    }

                }
                for (int z = 0; z < sellListByProgresses[i].sellartifacts.Count; z++)
                {
                    List<Artifact> artifacts = ArtifactManager.S.RandomArtifact(sellListByProgresses[i].sellartifacts[z].grade, sellListByProgresses[i].sellartifacts[z].num);

                    for (int k = 0; k < sellListByProgresses[i].sellartifacts[z].num; k++)
                    {
                        GameObject go = Instantiate(P_slot, T_slots);
                        TownShopSlot slot = go.GetComponent<TownShopSlot>();
                        slot.artifact = artifacts[k];
                        slot.item = sellListByProgresses[i].sellartifacts[z].item;
                        slot.itemPrice = sellListByProgresses[i].sellartifacts[z].price;
                        switch (Player.S.mainProgress)
                        {
                            case 3:
                                if (slot.artifact.rare==1)
                                {
                                    slot.itemPrice = 110;
                                }
                                else
                                {
                                    slot.itemPrice = 60;
                                }
                                break;
                            case 4:
                                if (slot.artifact.rare == 1)
                                {
                                    slot.itemPrice = 125;
                                }
                                else
                                {
                                    slot.itemPrice = 75;
                                }
                                break;
                            case 5:
                                if (slot.artifact.rare == 1)
                                {
                                    slot.itemPrice = 200;
                                }
                                else
                                {
                                    slot.itemPrice = 100;
                                }
                                break;
                            case 6:
                                if (slot.artifact.rare == 1)
                                {
                                    slot.itemPrice = 200;
                                }
                                else
                                {
                                    slot.itemPrice = 150;
                                }
                                break;

                            default:
                                break;
                        }
                        slot.itemNum = 1;
                        slot.market = this;
                        slot.SetShopSlot();
                        slots.Add(go);
                    }

                }
                break;
            }
        }






    }

    public void UIOn()
    {
        if (isPeddler)
        {
            if (Player.S.mainProgress<3)
            {
                DialogueManager.S.TextSet(48, 49);
                return;
            }
            PeddlerItems.SetShop();
            SetMarketByProgress();
        }
        if (isBlackMarket)
        {
            if (Player.S.mainProgress < 3)
            {
                DialogueManager.S.TextSet(48, 49);
                return;
            }
        }
        if (iscollector)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                Destroy(slots[i].gameObject);
            }
            slots.Clear();
            ArtiCollector artiCollector = GetComponent<ArtiCollector>();
            artiCollector.AddMarketList();

        }
        UIbase.SetActive(true);
        T_nowGold.text = "Gold : " + Player.S.gold;
        if (iscollector)
        {
            T_nowGold.text = "EXP : " + Player.S.exp;
        }

    }
    public void UIClose()
    {
        InfoUI.SetActive(false);
        selectedSlot = null;
    }


    public void SelectItem(TownShopSlot _slot)
    {
        if (_slot==null)
        {
            return;
        }
        InfoUI.SetActive(true);
        if (_slot.item.itemType != Item.ItemType.Skill && _slot.item.itemType!=Item.ItemType.Artifact&& _slot.item.itemType != Item.ItemType.Equip)
        {
            selectedSlot = _slot;

            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    itemName.text = _slot.item.itemName;
                    itemNum.text = _slot.itemNum.ToString() + "개 남음";
                    ItemRamainNum.text = Inventory.S.SearchItemCount(_slot.item.itemName).ToString() + "개 보유";
                    itemInfo.text = _slot.item.itemLongInfo;
                    itemPrice.text = _slot.itemPrice + " Gold";
                    break;
                case Options.Language.Eng:
                    itemName.text = _slot.item.eitemname;
                    itemNum.text = _slot.itemNum.ToString() + " Left";
                    ItemRamainNum.text = Inventory.S.SearchItemCount(_slot.item.itemName).ToString() + " Hold";
                    itemInfo.text = _slot.item.E_itemLongInfo;
                    itemPrice.text = _slot.itemPrice + " Gold";
                    break;
                default:
                    break;
            }


            itemImage.sprite = _slot.item.itemImage;

            _slot.SetShopSlot();
        }
        else if (_slot.item.itemType == Item.ItemType.Artifact)
        {

            selectedSlot = _slot;

            itemName.text = _slot.item.itemName;
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    itemName.text = _slot.artifact.artifactName;
                    itemNum.text = _slot.itemNum.ToString() + "개 남음";
                    ItemRamainNum.text = "";
                    itemInfo.text = _slot.artifact.Info;
                    itemPrice.text = _slot.itemPrice+ " Gold";
                    if (iscollector)
                    {
                        itemPrice.text = _slot.itemPrice + " EXP";
                    }

                    for (int i = 1; i <= 10; i++)
                    {
                        itemInfo.text = itemInfo.text.Replace(i + "TN", (Player.S.CurTowerNum * i).ToString());
                    }
                    itemInfo.text = itemInfo.text.Replace("TN", Player.S.CurTowerNum.ToString());
                    if (_slot.artifact.artifactName == "황금 달걀")
                    {
                        itemInfo.text = itemInfo.text.Replace("횟수", (50 - Player.S.GoldenEggStack).ToString() + "회 남음");
                    }
                    if (_slot.artifact.artifactName == "신비하지 않은 사전")
                    {
                        itemInfo.text = itemInfo.text.Replace("횟수", (50 - Player.S.BookStack).ToString() + "회 남음");
                    }
                    if (_slot.artifact.artifactName == ArtifactManager.S.DirtSpoon.artifactName)
                    {
                        itemInfo.text = itemInfo.text.Replace("횟수", (50 - Player.S.SpoonStack).ToString() + "회 남음");
                    }
                    break;
                case Options.Language.Eng:
                    itemName.text = _slot.artifact.Ename;
                    ItemRamainNum.text = "";
                    itemNum.text = _slot.itemNum.ToString() + " Left";
                    itemPrice.text = _slot.itemPrice + " Gold";
                    if (iscollector)
                    {
                        itemPrice.text = _slot.itemPrice + " EXP";
                    }
                    itemInfo.text = _slot.artifact.E_Info;
                    for (int i = 1; i <= 10; i++)
                    {
                        itemInfo.text = itemInfo.text.Replace(i + "TN", (Player.S.CurTowerNum * i).ToString());
                    }
                    itemInfo.text = itemInfo.text.Replace("TN", "(" + Player.S.CurTowerNum.ToString() + ")");
                    if (_slot.artifact.artifactName == "황금 달걀")
                    {
                        itemInfo.text = itemInfo.text.Replace("횟수", "Remain " + (50 - Player.S.GoldenEggStack).ToString() + "Battle");
                    }
                    if (_slot.artifact.artifactName == "신비하지 않은 사전")
                    {
                        itemInfo.text = itemInfo.text.Replace("횟수", "Remain " + (50 - Player.S.BookStack).ToString() + "Battle");
                    }
                    if (_slot.artifact.artifactName == ArtifactManager.S.DirtSpoon.artifactName)
                    {
                        itemInfo.text = itemInfo.text.Replace("횟수", "Remain " + (50 - Player.S.SpoonStack).ToString() + "Battle");
                    }

                    break;
                default:
                    break;
            }

            ItemRamainNum.text = "";
            itemImage.sprite = _slot.artifact.image;



            _slot.SetShopSlot();
        }
        else if (_slot.item.itemType == Item.ItemType.Skill) 
        {
            selectedSlot = _slot;
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    itemName.text = _slot.skill.skillName;
                    itemNum.text = _slot.itemNum.ToString() + "개 남음";
                    ItemRamainNum.text = _slot.skill.GetSkillTypeString();
                    itemInfo.text = _slot.skill.skillLongInfo;
                    itemPrice.text = _slot.itemPrice + " Gold";
                    break;
                case Options.Language.Eng:
                    itemName.text = _slot.skill.skillEName;
                    itemNum.text = _slot.itemNum.ToString() + " Left";
                    ItemRamainNum.text = _slot.skill.GetSkillTypeString();
                    itemInfo.text = _slot.skill.skillE_LongInfo;
                    itemPrice.text = _slot.itemPrice + " Gold";
                    break;
                default:
                    break;
            }

            itemImage.sprite = _slot.skill.skillImage;

            if (_slot.skill.isPublicSkill)
            {
                ItemRamainNum.text += "   CP" + _slot.skill.CP;
            }
            _slot.SetShopSlot();
        }
        else if (_slot.item.itemType == Item.ItemType.Equip)
        {
            selectedSlot = _slot;
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    itemName.text = _slot.equipment.equipName;
                    itemNum.text = _slot.itemNum.ToString() + "개 남음";
                    ItemRamainNum.text = "";
                    itemImage.sprite = _slot.equipment.equipSprite;
                    itemInfo.text = _slot.equipment.equipInfo;
                    itemPrice.text = _slot.itemPrice + " Gold";
                    break;
                case Options.Language.Eng:
                    itemName.text = _slot.equipment.equipEName;
                    itemNum.text = _slot.itemNum.ToString() + " Left";
                    ItemRamainNum.text = "";
                    itemImage.sprite = _slot.equipment.equipSprite;
                    itemInfo.text = _slot.equipment.equipEInfo;
                    itemPrice.text = _slot.itemPrice + " Gold";
                    break;
                default:
                    break;
            }


            _slot.SetShopSlot();
        }
        T_nowGold.text = "Gold : " + Player.S.gold;
        if (iscollector)
        {
            T_nowGold.text = "EXP : " + Player.S.exp;
        }
    }
    public void BuyItem()
    {
        if (selectedSlot == null) return;
        if (iscollector&& Player.S.exp < selectedSlot.itemPrice) return;
        if (!iscollector&&Player.S.gold < selectedSlot.itemPrice) return;

        if (selectedSlot.itemNum == 0) return;

        if (iscollector)
        {
            Player.S.SpendExp(selectedSlot.itemPrice);
            Player.S.getSetArtifact += 1;
        }
        else
        {
            if (isCaution)
            {
            }
            else
            {
                Player.S.SpendGold(selectedSlot.itemPrice);
            }
        }





        switch (selectedSlot.item.itemType)
        {
            case Item.ItemType.Equip:
                EquipmentUI.S.GetEquipment(selectedSlot.equipment.equipName);
                break;
            case Item.ItemType.UseItem:
                break;
            case Item.ItemType.KeyItem:
                AddItem.S.SearchItem(selectedSlot.item.itemName, 1);
                break;
            case Item.ItemType.EventItem:
                break;
            case Item.ItemType.Artifact:
                ArtifactManager.S.GetArtifact(selectedSlot.artifact.artifactName);
                for (int i = 0; i < slots.Count; i++)
                {
                    if (slots[i].GetComponent<TownShopSlot>().item.itemType == Item.ItemType.Artifact)
                    {
                        switch (Player.S.mainProgress)
                        {
                            case 0:
                                break;
                            case 1:
                                break;
                            case 2:
                                if (slots[i].GetComponent<TownShopSlot>().artifact.rare == 0 && selectedSlot.artifact.rare == 0)
                                {
                                    slots[i].GetComponent<TownShopSlot>().itemPrice += 10;
                                    slots[i].GetComponent<TownShopSlot>().T_itemPrice.text = slots[i].GetComponent<TownShopSlot>().itemPrice.ToString() + " Gold"; ;
                                }
                                break;
                            case 3:
                                if (slots[i].GetComponent<TownShopSlot>().artifact.rare == 0 && selectedSlot.artifact.rare == 0)
                                {
                                    slots[i].GetComponent<TownShopSlot>().itemPrice += 12;
                                    slots[i].GetComponent<TownShopSlot>().T_itemPrice.text = slots[i].GetComponent<TownShopSlot>().itemPrice.ToString() + " Gold"; ;
                                }
                                break;
                            case 4:
                                if (slots[i].GetComponent<TownShopSlot>().artifact.rare == 0 && selectedSlot.artifact.rare == 0)
                                {
                                    slots[i].GetComponent<TownShopSlot>().itemPrice += 25;
                                    slots[i].GetComponent<TownShopSlot>().T_itemPrice.text = slots[i].GetComponent<TownShopSlot>().itemPrice.ToString() + " Gold"; ;
                                }
                                else if (slots[i].GetComponent<TownShopSlot>().artifact.rare == 1 && selectedSlot.artifact.rare == 1)
                                {
                                    slots[i].GetComponent<TownShopSlot>().itemPrice += 50;
                                    slots[i].GetComponent<TownShopSlot>().T_itemPrice.text = slots[i].GetComponent<TownShopSlot>().itemPrice.ToString() + " Gold"; ;
                                }
                                break;
                            case 5:
                               if (slots[i].GetComponent<TownShopSlot>().artifact.rare == 1 && selectedSlot.artifact.rare == 1)
                                {
                                    slots[i].GetComponent<TownShopSlot>().itemPrice += 75;
                                    slots[i].GetComponent<TownShopSlot>().T_itemPrice.text = slots[i].GetComponent<TownShopSlot>().itemPrice.ToString() + " Gold"; ;
                                }
                                break;
                            case 6:
                                if (slots[i].GetComponent<TownShopSlot>().artifact.rare == 0 && selectedSlot.artifact.rare == 0)
                                {
                                    slots[i].GetComponent<TownShopSlot>().itemPrice += 50;
                                    slots[i].GetComponent<TownShopSlot>().T_itemPrice.text = slots[i].GetComponent<TownShopSlot>().itemPrice.ToString() + " Gold"; ;
                                }
                                else if (slots[i].GetComponent<TownShopSlot>().artifact.rare == 1 && selectedSlot.artifact.rare == 1)
                                {
                                    slots[i].GetComponent<TownShopSlot>().itemPrice += 75;
                                    slots[i].GetComponent<TownShopSlot>().T_itemPrice.text = slots[i].GetComponent<TownShopSlot>().itemPrice.ToString() + " Gold"; ;
                                }
                                break;
                            case 7:
                                if (slots[i].GetComponent<TownShopSlot>().artifact.rare == 0 && selectedSlot.artifact.rare == 0)
                                {
                                    slots[i].GetComponent<TownShopSlot>().itemPrice += 50;
                                    slots[i].GetComponent<TownShopSlot>().T_itemPrice.text = slots[i].GetComponent<TownShopSlot>().itemPrice.ToString() + " Gold"; ;
                                }
                                else if (slots[i].GetComponent<TownShopSlot>().artifact.rare == 1 && selectedSlot.artifact.rare == 1)
                                {
                                    slots[i].GetComponent<TownShopSlot>().itemPrice += 75;
                                    slots[i].GetComponent<TownShopSlot>().T_itemPrice.text = slots[i].GetComponent<TownShopSlot>().itemPrice.ToString() + " Gold"; ;
                                }
                                break;
                            case 8:
                                if (slots[i].GetComponent<TownShopSlot>().artifact.rare == 2 && selectedSlot.artifact.rare == 2)
                                {
                                    slots[i].GetComponent<TownShopSlot>().itemPrice += 150;
                                    slots[i].GetComponent<TownShopSlot>().T_itemPrice.text = slots[i].GetComponent<TownShopSlot>().itemPrice.ToString() + " Gold"; ;
                                }
                                break;
                            case 9:
                                if (slots[i].GetComponent<TownShopSlot>().artifact.rare == 2 && selectedSlot.artifact.rare == 2)
                                {
                                    slots[i].GetComponent<TownShopSlot>().itemPrice += 150;
                                    slots[i].GetComponent<TownShopSlot>().T_itemPrice.text = slots[i].GetComponent<TownShopSlot>().itemPrice.ToString() + " Gold"; ;
                                }
                                break;
                            case 10:
                                if (slots[i].GetComponent<TownShopSlot>().artifact.rare == 1 && selectedSlot.artifact.rare == 1)
                                {
                                    slots[i].GetComponent<TownShopSlot>().itemPrice += 66;
                                    slots[i].GetComponent<TownShopSlot>().T_itemPrice.text = slots[i].GetComponent<TownShopSlot>().itemPrice.ToString() + " Gold"; ;
                                }
                                if (slots[i].GetComponent<TownShopSlot>().artifact.rare == 2 && selectedSlot.artifact.rare == 2)
                                {
                                    slots[i].GetComponent<TownShopSlot>().itemPrice += 133;
                                    slots[i].GetComponent<TownShopSlot>().T_itemPrice.text = slots[i].GetComponent<TownShopSlot>().itemPrice.ToString() + " Gold"; ;
                                }
                                break;
                            case 11:
                                if (slots[i].GetComponent<TownShopSlot>().artifact.rare == 0 && selectedSlot.artifact.rare == 0)
                                {
                                    slots[i].GetComponent<TownShopSlot>().itemPrice += 66;
                                    slots[i].GetComponent<TownShopSlot>().T_itemPrice.text = slots[i].GetComponent<TownShopSlot>().itemPrice.ToString() + " Gold"; ;
                                }
                                break;
                            case 12:
                                if (slots[i].GetComponent<TownShopSlot>().artifact.rare == 1 && selectedSlot.artifact.rare == 1)
                                {
                                    slots[i].GetComponent<TownShopSlot>().itemPrice += 90;
                                    slots[i].GetComponent<TownShopSlot>().T_itemPrice.text = slots[i].GetComponent<TownShopSlot>().itemPrice.ToString() + " Gold"; ;
                                }
                                break;
                            case 13:
                                if (slots[i].GetComponent<TownShopSlot>().artifact.rare == 2 && selectedSlot.artifact.rare == 2)
                                {
                                    slots[i].GetComponent<TownShopSlot>().itemPrice += 100;
                                    slots[i].GetComponent<TownShopSlot>().T_itemPrice.text = slots[i].GetComponent<TownShopSlot>().itemPrice.ToString() + " Gold"; ;
                                }
                                break;
                            case 14:
                                if (slots[i].GetComponent<TownShopSlot>().artifact.rare == 2 && selectedSlot.artifact.rare == 2)
                                {
                                    slots[i].GetComponent<TownShopSlot>().itemPrice += 100;
                                    slots[i].GetComponent<TownShopSlot>().T_itemPrice.text = slots[i].GetComponent<TownShopSlot>().itemPrice.ToString() + " Gold"; ;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                break;
            case Item.ItemType.ETC:
                break;
            case Item.ItemType.Potion:
                Player.S.hp += selectedSlot.item.potionHealNum;
                break;
            case Item.ItemType.Stone:
                switch (selectedSlot.item.statType)
                {
                    case Item.StatType.None:
                        break;
                    case Item.StatType.ATK:
                        Player.S.ATK += Player.S.CurTowerNum;
                        break;
                    case Item.StatType.DEF:
                        Player.S.DEF += Player.S.CurTowerNum;
                        break;
                    case Item.StatType.AVD:
                        Player.S.AVD += 1;
                        break;
                    case Item.StatType.HIT:
                        Player.S.HIT += 1;
                        break;
                    case Item.StatType.POW:
                        Player.S.POW += 1;
                        break;
                    case Item.StatType.Level:
                        Player.S.LevelUP();
                        break;
                    case Item.StatType.SPD:
                          Player.S.SPD += 1;
                        break;
                    case Item.StatType.CP:
                        Player.S.CP+=1;
                        break;
                    case Item.StatType.PP:
                        Player.S.PP += 1;
                        break;
                    case Item.StatType.TP:
                        break;
                    case Item.StatType.CRC:
                        Player.S.CRC += 1;
                        break;
                    default:
                        break;
                }


                break;
            case Item.ItemType.Skill:
                if (selectedSlot.skill.isPublicSkill)
                {
                    if (selectedSlot.skill.CP + Player.S.useCP > Player.S.CP && isCaution == false)
                    {
                        PSkillCaution.SetActive(true);
                        isCaution = true;
                        return;
                    }
                }
                isCaution = false;
                SkillUI.S.GetSkill(selectedSlot.skill);
                for (int i = 0; i < slots.Count; i++)
                {
                    if (slots[i].GetComponent<TownShopSlot>().item.itemType==Item.ItemType.Skill)
                    {
                        switch (Player.S.mainProgress)
                        {
                            case 0:
                                break;
                            case 1:
                                break;
                            case 2:
                                slots[i].GetComponent<TownShopSlot>().itemPrice += 10;
                                slots[i].GetComponent<TownShopSlot>().T_itemPrice.text = slots[i].GetComponent<TownShopSlot>().itemPrice.ToString()+" Gold";
                                break;
                            case 3:
                                slots[i].GetComponent<TownShopSlot>().itemPrice += 25;
                                slots[i].GetComponent<TownShopSlot>().T_itemPrice.text = slots[i].GetComponent<TownShopSlot>().itemPrice.ToString() + " Gold";
                                break;
                            case 4:
                                if (slots[i].GetComponent<TownShopSlot>().skill.skillClass==Skill.SkillClass.Normal)
                                {
                                    slots[i].GetComponent<TownShopSlot>().itemPrice += 25;
                                    slots[i].GetComponent<TownShopSlot>().T_itemPrice.text = slots[i].GetComponent<TownShopSlot>().itemPrice.ToString() + " Gold";
                                }

                                if (slots[i].GetComponent<TownShopSlot>().skill.skillClass == Skill.SkillClass.Rare)
                                {
                                    slots[i].GetComponent<TownShopSlot>().itemPrice += 50;
                                    slots[i].GetComponent<TownShopSlot>().T_itemPrice.text = slots[i].GetComponent<TownShopSlot>().itemPrice.ToString() + " Gold";
                                }
                                break;
                            case 5:
                                if (slots[i].GetComponent<TownShopSlot>().skill.skillClass == Skill.SkillClass.Rare)
                                {
                                    slots[i].GetComponent<TownShopSlot>().itemPrice += 75;
                                    slots[i].GetComponent<TownShopSlot>().T_itemPrice.text = slots[i].GetComponent<TownShopSlot>().itemPrice.ToString() + " Gold";
                                }
                                break;
                            case 6:
                                if (slots[i].GetComponent<TownShopSlot>().skill.skillClass == Skill.SkillClass.Rare)
                                {
                                    slots[i].GetComponent<TownShopSlot>().itemPrice += 75;
                                    slots[i].GetComponent<TownShopSlot>().T_itemPrice.text = slots[i].GetComponent<TownShopSlot>().itemPrice.ToString() + " Gold";
                                }
                                break;
                            case 7:
                                if (slots[i].GetComponent<TownShopSlot>().skill.skillClass == Skill.SkillClass.Rare)
                                {
                                    slots[i].GetComponent<TownShopSlot>().itemPrice += 75;
                                    slots[i].GetComponent<TownShopSlot>().T_itemPrice.text = slots[i].GetComponent<TownShopSlot>().itemPrice.ToString() + " Gold";
                                }

                                break;
                            default:
                                break;
                        }
                    }
                }
                break;
            case Item.ItemType.SecretWallChance:
                break;
            case Item.ItemType.RunAway:
                break;
            default:
                break;
        }

        SoundManager.S.PlaySE("income");
        if (isPeddler)
        {
            Player.S.Pedller[selectedSlot.peddler - 1] = 0;
        }
        selectedSlot.itemNum -= 1;
        SelectItem(selectedSlot);
    }
    public void TutorialClick1()
    {
        SelectItem(slots[0].GetComponent<TownShopSlot>());
    }
    public void TutorialClick2()
    {
        BuyItem();
    }

    public void PublicCautionNo()
    {
        isCaution = false;
        Player.S.gold += selectedSlot.itemPrice;
       // selectedSlot = null;
    }
}
