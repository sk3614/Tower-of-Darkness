using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TownShopSlot : MonoBehaviour
{

    public Item item;
    public int itemNum;
    public int itemPrice;
    public MarketPlace market;
    public Skill skill;
    public Artifact artifact;
    public Equipment equipment;
    public int peddler;
    public enum Kind
    {
        Item,
        Skill,
        Arti,
        Equip
    }
    public Kind kind;
    public Image itemImage;
    public Text ItemName;
    public Text T_itemPrice;
    public Text T_itemNum;
    public string itemShortInfo;
    public string itemLongInfo;
    public int buyPrice;
    public int sellPrice;

    public void SetShopSlot()
    {
        if (item.itemType==Item.ItemType.Skill)
        {

            itemImage.sprite = skill.skillImage;
            T_itemPrice.text = itemPrice.ToString() + " Gold";
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    ItemName.text = skill.skillName;
                    break;
                case Options.Language.Eng:
                    ItemName.text = skill.skillEName;
                    break;
                default:
                    break;
            }

            T_itemNum.text = itemNum.ToString();
            kind = Kind.Skill;
            gameObject.GetComponent<Button>().onClick.AddListener(() => market.SelectItem(this));
        }
        else if (item.itemType == Item.ItemType.Artifact)
        {
            itemImage.sprite = artifact.image;

            T_itemPrice.text = itemPrice.ToString() + " Gold";
            if (market.iscollector)
            {
                T_itemPrice.text = itemPrice.ToString() + " EXP";
            }
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    ItemName.text = artifact.artifactName;
                    break;
                case Options.Language.Eng:
                    ItemName.text = artifact.Ename;
                    break;
                default:
                    break;
            }

            T_itemNum.text = itemNum.ToString();
            kind = Kind.Arti;
            gameObject.GetComponent<Button>().onClick.AddListener(() => market.SelectItem(this));
        }
        else if (item.itemType == Item.ItemType.Equip)
        {
            itemImage.sprite = equipment.equipSprite;
            T_itemPrice.text = itemPrice.ToString() + " Gold";
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    ItemName.text = equipment.equipName;
                    break;
                case Options.Language.Eng:
                    ItemName.text = equipment.equipEName;
                    break;
                default:
                    break;
            }


            T_itemNum.text = itemNum.ToString();
            kind = Kind.Equip;
            gameObject.GetComponent<Button>().onClick.AddListener(() => market.SelectItem(this));
        }
        else
        {
            itemImage.sprite = item.itemImage;
            T_itemPrice.text = itemPrice.ToString() + " Gold";
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    ItemName.text = item.itemName;
                    break;
                case Options.Language.Eng:
                    ItemName.text = item.eitemname;
                    break;
                default:
                    break;
            }


            T_itemNum.text = itemNum.ToString();
            kind = Kind.Item;
            gameObject.GetComponent<Button>().onClick.AddListener(() => market.SelectItem(this));
        }
       
    }
}
