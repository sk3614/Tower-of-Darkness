using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Slot : MonoBehaviour
{
    public Item item;
    public string itemName;
    public string EitemName;
    public int itemCount;
    public string itemLongInfo;
    public string itemShortInfo;
    public Image itemImage;
    public Item.ItemType itemType;
    public Text text_Count;
    public int sortnum;

    
    
    public void AddItem(Item _item, int _num=1)
    {

        switch (Options.S.language)
        {
            case Options.Language.Kor:
                itemName = _item.itemName;
                itemShortInfo = _item.itemShortInfo;
                itemLongInfo = _item.itemLongInfo;
                break;
            case Options.Language.Eng:
                itemName = _item.eitemname;
                itemShortInfo = _item.E_itemShortInfo;
                itemLongInfo = _item.E_itemLongInfo;
                break;
            default:
                break;
        }
        item = _item;
        itemType = _item.itemType;
        itemImage.sprite = _item.itemImage;
        itemCount += _num;
        text_Count.text =itemCount.ToString();
        sortnum = _item.sortNum;

       
    }
    public void AddArtifact(Artifact _artifact)
    {
        item = null;
        
        itemType = Item.ItemType.Artifact;
        itemImage.sprite = _artifact.image;
        itemCount = 1;

        itemLongInfo = _artifact.Info;


        switch (Options.S.language)
        {
            case Options.Language.Kor:
                itemName = _artifact.artifactName;
                itemLongInfo = _artifact.Info;

                for (int i = 1; i <= 10; i++)
                {
                    itemLongInfo = itemLongInfo.Replace(i + "TN", (Player.S.CurTowerNum * i).ToString());
                }
                itemLongInfo = itemLongInfo.Replace("TN", Player.S.CurTowerNum.ToString());
                if (_artifact.artifactName == "황금 달걀")
                {
                    itemLongInfo = itemLongInfo.Replace("횟수", (50 - Player.S.GoldenEggStack).ToString() + "회 남음");
                }
                if (_artifact.artifactName == "신비하지 않은 사전")
                {
                    itemLongInfo = itemLongInfo.Replace("횟수", (50 - Player.S.BookStack).ToString() + "회 남음");
                }
                if (_artifact.artifactName == ArtifactManager.S.DirtSpoon.artifactName)
                {
                    itemLongInfo = itemLongInfo.Replace("횟수", (50 - Player.S.SpoonStack).ToString() + "회 남음");
                }
                break;
            case Options.Language.Eng:
                itemName = _artifact.Ename;
                itemLongInfo = _artifact.E_Info;
                for (int i = 1; i <= 10; i++)
                {
                    itemLongInfo = itemLongInfo.Replace(i + "TN", (Player.S.CurTowerNum * i).ToString());
                }
                itemLongInfo = itemLongInfo.Replace("TN", "(" + Player.S.CurTowerNum.ToString() + ")");
                if (_artifact.artifactName == "황금 달걀")
                {
                    itemLongInfo = itemLongInfo.Replace("횟수", "Remain " + (50 - Player.S.GoldenEggStack).ToString() + "Battle");
                }
                if (_artifact.artifactName == "신비하지 않은 사전")
                {
                    itemLongInfo = itemLongInfo.Replace("횟수", "Remain " + (50 - Player.S.BookStack).ToString() + "Battle");
                }
                if (_artifact.artifactName == ArtifactManager.S.DirtSpoon.artifactName)
                {
                    itemLongInfo = itemLongInfo.Replace("횟수", "Remain " + (50 - Player.S.SpoonStack).ToString() + "Battle");
                }

                break;
            default:
                break;
        }
        text_Count.text = "";
        
    }
    
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }
    public void ClearSlot()
    {
        Destroy(gameObject);
    }
    public void OpenItemInfo()
    {
        Inventory.S.OpenItemInfoUI(this);
    }
}
