using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItem : MonoBehaviour
{
    public static AddItem S;
    public Item[] items;
    public Skill[] skills;
    public Item[] artifacts;
    public Item[] Equipments;
    public Item item;
    public Skill skill;
    public TowerObjectData[] objectData;
    public Item[] blackMarketItems;


    public Dictionary<string, Item> itemDictionary = new Dictionary<string, Item>();
    public Dictionary<string, Skill> skillDictionary = new Dictionary<string, Skill>();
    public Dictionary<string, TowerObjectData> towerObjecDictionary = new Dictionary<string, TowerObjectData>();
    public Dictionary<string, Item> BlackMarketDic = new Dictionary<string, Item>();
    void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Destroy(gameObject);
        }
        for (int i = 0; i < items.Length; i++)
        {
            itemDictionary.Add(items[i].itemName, items[i]);
        }
        for (int i = 0; i < skills.Length; i++)
        {
            skillDictionary.Add(skills[i].skillName, skills[i]);
        }
        for (int i = 0; i < artifacts.Length; i++)
        {
            itemDictionary.Add(artifacts[i].itemName, artifacts[i]);
        }
        for (int i = 0; i < Equipments.Length; i++)
        {
            itemDictionary.Add(Equipments[i].itemName, Equipments[i]);
        }
        for (int i = 0; i < objectData.Length; i++)
        {
            towerObjecDictionary.Add(objectData[i].objectName, objectData[i]);
        }
    }
    void Start()
    {

    }

    public void SearchItem(string _itemname, int num = 1)
    {
        Item tmpItem = itemDictionary[_itemname];
        item = tmpItem;
        Inventory.S.GetItem(item, num);
        item = null;

    }
    public Item ReturnItemFromName(string _itemname)
    {
        if (itemDictionary.ContainsKey(_itemname))
        {
            return itemDictionary[_itemname];
        }
        else
        {
            return null;
        }
    }
    public TowerObjectData ReturnObjectFromName(string _itemname)
    {
        if (towerObjecDictionary.ContainsKey(_itemname))
        {
            return towerObjecDictionary[_itemname];
        }
        else
        {
            return null;
        }
    }
    public void SearchSkill(string _skillname)
    {
        Skill tmpSkill = skillDictionary[_skillname];
        skill = tmpSkill;
        SkillUI.S.GetSkill(skill);
        skill = null;
    }
    public void DelSkill(string _skillname)
    {
        //Skill tmpSkill = skillDictionary[_skillname];
        //skill = tmpSkill;
        //SkillUI.S.DelSkill(skill);
        //skill = null;
    }
}
