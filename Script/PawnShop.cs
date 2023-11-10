using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PawnShop : MonoBehaviour
{
    public GameObject UIbase;
    public GameObject InfoUI;

    public GameObject P_slot;
    public Transform T_slots;

    public List<GameObject> slots = new List<GameObject>();

    public Image itemImage;
    public Text itemName;
    public Text ItemRamainNum;
    public Text itemInfo;
    public Text itemPrice;
    public int SelectedPrice;
    public List<Item> sellItems = new List<Item>();

    public Item selectedItem;

    public void SetPawnShop()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Destroy(slots[i].gameObject);
        }
        slots.Clear();

        GetSellItem();



        for (int i = 0; i < sellItems.Count; i++)
        {
            GameObject go = Instantiate(P_slot, T_slots);
            PawnShopSlot slot = go.GetComponent<PawnShopSlot>();
            slot.item = sellItems[i];
            slot.pawnShop = this;
            slot.SetShopSlot();
            slots.Add(go);
        }
    }
    public void UIOn()
    {
        UIbase.SetActive(true);
        SetPawnShop();
    }
    public void UIClose()
    {
        InfoUI.SetActive(false);
        sellItems.Clear();
        selectedItem = null;
    }

    public void SelectItem(Item _item)
    {
        if (_item == null)
        {
            InfoUI.SetActive(false);
            return;
        }
        InfoUI.SetActive(true);
        switch (Options.S.language)
        {
            case Options.Language.Kor:
                selectedItem = _item;
                itemName.text = _item.itemName;
                ItemRamainNum.text = Inventory.S.SearchItemCount(_item.itemName).ToString() + "개 보유";
                itemImage.sprite = _item.itemImage;
                itemInfo.text = _item.itemLongInfo;
                itemPrice.text = _item.sellPrice + " Gold";



                break;
            case Options.Language.Eng:
                selectedItem = _item;
                itemName.text = _item.eitemname;
                ItemRamainNum.text =Inventory.S.SearchItemCount(_item.itemName).ToString()+" Left";
                itemImage.sprite = _item.itemImage;
                itemInfo.text = _item.E_itemLongInfo;
                itemPrice.text = _item.sellPrice + " Gold";
                break;
            default:
                break;
        }

        if (Player.S.mainProgress < 4)
        {
            if (selectedItem.itemName == "돌 열쇠")
            {
                itemPrice.text = 5 + " Gold";
                SelectedPrice = 5;
            }
            else if (selectedItem.itemName == "쇠 열쇠")
            {
                itemPrice.text = 20 + " Gold";
                SelectedPrice = 20;
            }
        }
        else if (Player.S.mainProgress < 7)
        {
            if (selectedItem.itemName == "돌 열쇠")
            {
                itemPrice.text = 7 + " Gold";
                SelectedPrice = 7;
            }
            else if (selectedItem.itemName == "쇠 열쇠")
            {
                itemPrice.text = 30 + " Gold";
                SelectedPrice = 30;
            }
            else if (selectedItem.itemName == "금 열쇠")
            {
                itemPrice.text = 55 + " Gold";
                SelectedPrice = 55;
            }
            else if (selectedItem.itemName == "연막탄")
            {
                itemPrice.text = 5 + " Gold";
                SelectedPrice = 5;
            }
        }
        else if (Player.S.mainProgress < 9)
        {
            if (selectedItem.itemName == "돌 열쇠")
            {
                itemPrice.text = 10 + " Gold";
                SelectedPrice = 10;
            }
            else if (selectedItem.itemName == "쇠 열쇠")
            {
                itemPrice.text = 40 + " Gold";
                SelectedPrice = 40;
            }
            else if (selectedItem.itemName == "금 열쇠")
            {
                itemPrice.text = 66 + " Gold";
                SelectedPrice = 66;
            }
            else if (selectedItem.itemName == "비밀방 두루마리")
            {
                itemPrice.text = 15 + " Gold";
                SelectedPrice = 15;
            }
            else if (selectedItem.itemName == "연막탄")
            {
                itemPrice.text = 7 + " Gold";
                SelectedPrice = 7;
            }
        }
        else if (Player.S.mainProgress < 10)
        {
            if (selectedItem.itemName == "돌 열쇠")
            {
                itemPrice.text = 10 + " Gold";
                SelectedPrice = 10;
            }
            else if (selectedItem.itemName == "쇠 열쇠")
            {
                itemPrice.text = 40 + " Gold";
                SelectedPrice = 40;
            }
            else if (selectedItem.itemName == "금 열쇠")
            {
                itemPrice.text = 66 + " Gold";
                SelectedPrice = 66;
            }
            else if (selectedItem.itemName == "보석 열쇠")
            {
                itemPrice.text = 200 + " Gold";
                SelectedPrice = 200;
            }
            else if (selectedItem.itemName == "비밀방 두루마리")
            {
                itemPrice.text = 15 + " Gold";
                SelectedPrice = 15;
            }
            else if (selectedItem.itemName == "연막탄")
            {
                itemPrice.text = 7 + " Gold";
                SelectedPrice = 7;
            }
        }
        else if (Player.S.mainProgress < 11)
        {
            if (selectedItem.itemName == "돌 열쇠")
            {
                itemPrice.text = 12 + " Gold";
                SelectedPrice = 12;
            }
            else if (selectedItem.itemName == "쇠 열쇠")
            {
                itemPrice.text = 40 + " Gold";
                SelectedPrice = 40;
            }
            else if (selectedItem.itemName == "금 열쇠")
            {
                itemPrice.text = 75 + " Gold";
                SelectedPrice = 75;
            }
            else if (selectedItem.itemName == "비밀방 두루마리")
            {
                itemPrice.text = 15 + " Gold";
                SelectedPrice = 15;
            }
            else if (selectedItem.itemName == "연막탄")
            {
                itemPrice.text = 7 + " Gold";
                SelectedPrice = 7;
            }
        }
        else if (Player.S.mainProgress < 12)
        {
            if (selectedItem.itemName == "돌 열쇠")
            {
                itemPrice.text = 12 + " Gold";
                SelectedPrice = 12;
            }
            else if (selectedItem.itemName == "쇠 열쇠")
            {
                itemPrice.text = 40 + " Gold";
                SelectedPrice = 40;
            }
            else if (selectedItem.itemName == "금 열쇠")
            {
                itemPrice.text = 75 + " Gold";
                SelectedPrice = 75;
            }
            else if (selectedItem.itemName == "비밀방 두루마리")
            {
                itemPrice.text = 22 + " Gold";
                SelectedPrice = 22;
            }
            else if (selectedItem.itemName == "연막탄")
            {
                itemPrice.text = 10 + " Gold";
                SelectedPrice = 10;
            }
        }
        else if (Player.S.mainProgress < 13)
        {
            if (selectedItem.itemName == "돌 열쇠")
            {
                itemPrice.text = 17 + " Gold";
                SelectedPrice = 17;
            }
            else if (selectedItem.itemName == "쇠 열쇠")
            {
                itemPrice.text = 50 + " Gold";
                SelectedPrice = 50;
            }
            else if (selectedItem.itemName == "금 열쇠")
            {
                itemPrice.text = 110 + " Gold";
                SelectedPrice = 110;
            }
            else if (selectedItem.itemName == "보석 열쇠")
            {
                itemPrice.text = 300 + " Gold";
                SelectedPrice = 300;
            }
            else if (selectedItem.itemName == "비밀방 두루마리")
            {
                itemPrice.text = 22 + " Gold";
                SelectedPrice = 22;
            }
            else if (selectedItem.itemName == "연막탄")
            {
                itemPrice.text = 10 + " Gold";
                SelectedPrice = 10;
            }
        }
        else if (Player.S.mainProgress < 14)
        {
            if (selectedItem.itemName == "돌 열쇠")
            {
                itemPrice.text = 17 + " Gold";
                SelectedPrice = 17;
            }
            else if (selectedItem.itemName == "쇠 열쇠")
            {
                itemPrice.text = 50 + " Gold";
                SelectedPrice = 50;
            }
            else if (selectedItem.itemName == "금 열쇠")
            {
                itemPrice.text = 110 + " Gold";
                SelectedPrice = 110;
            }
            else if (selectedItem.itemName == "보석 열쇠")
            {
                itemPrice.text = 300 + " Gold";
                SelectedPrice = 300;
            }
            else if (selectedItem.itemName == "비밀방 두루마리")
            {
                itemPrice.text = 22 + " Gold";
                SelectedPrice = 22;
            }
            else if (selectedItem.itemName == "연막탄")
            {
                itemPrice.text = 10 + " Gold";
                SelectedPrice = 10;
            }
        }
        else if (Player.S.mainProgress < 15)
        {
            if (selectedItem.itemName == "돌 열쇠")
            {
                itemPrice.text = 17 + " Gold";
                SelectedPrice = 17;
            }
            else if (selectedItem.itemName == "쇠 열쇠")
            {
                itemPrice.text = 50 + " Gold";
                SelectedPrice = 50;
            }
            else if (selectedItem.itemName == "금 열쇠")
            {
                itemPrice.text = 110 + " Gold";
                SelectedPrice = 110;
            }
            else if (selectedItem.itemName == "보석 열쇠")
            {
                itemPrice.text = 300 + " Gold";
                SelectedPrice = 300;
            }
            else if (selectedItem.itemName == "비밀방 두루마리")
            {
                itemPrice.text = 22 + " Gold";
                SelectedPrice = 22;
            }
            else if (selectedItem.itemName == "연막탄")
            {
                itemPrice.text = 10 + " Gold";
                SelectedPrice = 10;
            }
        }

    }

    public void SellItem()
    {
        if (Inventory.S.SearchItemCount(selectedItem.itemName)<=0)
        {
            return;
        }
        AddItem.S.SearchItem(selectedItem.itemName, -1);
        SelectItem(selectedItem);
        if (Inventory.S.SearchItemCount(selectedItem.itemName) <= 0)
        {
            SetPawnShop();
            InfoUI.SetActive(false);
        }
        Player.S.GetGold(SelectedPrice);
        SoundManager.S.PlaySE("income");
    }

    public void GetSellItem()
    {

        if (Player.S.mainProgress<4)
        {
            if (Inventory.S.ReturnItem("돌 열쇠")!=null)
            {
                sellItems.Add(Inventory.S.ReturnItem("돌 열쇠"));
            }
            if (Inventory.S.ReturnItem("쇠 열쇠") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("쇠 열쇠"));
            }
        }
        else if (Player.S.mainProgress < 7)
        {
            if (Inventory.S.ReturnItem("돌 열쇠") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("돌 열쇠"));
            }
            if (Inventory.S.ReturnItem("쇠 열쇠") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("쇠 열쇠"));
            }
            if (Inventory.S.ReturnItem("금 열쇠") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("금 열쇠"));
            }
            if (Inventory.S.ReturnItem("연막탄") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("연막탄"));
            }
        }
        else if (Player.S.mainProgress < 9)
        {
            if (Inventory.S.ReturnItem("돌 열쇠") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("돌 열쇠"));
            }
            if (Inventory.S.ReturnItem("쇠 열쇠") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("쇠 열쇠"));
            }
            if (Inventory.S.ReturnItem("금 열쇠") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("금 열쇠"));
            }
            if (Inventory.S.ReturnItem("비밀방 두루마리") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("비밀방 두루마리"));
            }
            if (Inventory.S.ReturnItem("연막탄") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("연막탄"));
            }
        }
        else if (Player.S.mainProgress < 10)
        {
            if (Inventory.S.ReturnItem("돌 열쇠") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("돌 열쇠"));
            }
            if (Inventory.S.ReturnItem("쇠 열쇠") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("쇠 열쇠"));
            }
            if (Inventory.S.ReturnItem("금 열쇠") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("금 열쇠"));
            }
            if (Inventory.S.ReturnItem("보석 열쇠") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("보석 열쇠"));
            }
            if (Inventory.S.ReturnItem("비밀방 두루마리") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("비밀방 두루마리"));
            }
            if (Inventory.S.ReturnItem("연막탄") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("연막탄"));
            }
        }
        else if (Player.S.mainProgress < 12)
        {
            if (Inventory.S.ReturnItem("돌 열쇠") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("돌 열쇠"));
            }
            if (Inventory.S.ReturnItem("쇠 열쇠") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("쇠 열쇠"));
            }
            if (Inventory.S.ReturnItem("금 열쇠") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("금 열쇠"));
            }
            if (Inventory.S.ReturnItem("비밀방 두루마리") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("비밀방 두루마리"));
            }
            if (Inventory.S.ReturnItem("연막탄") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("연막탄"));
            }
        }
        else if (Player.S.mainProgress < 13)
        {
            if (Inventory.S.ReturnItem("돌 열쇠") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("돌 열쇠"));
            }
            if (Inventory.S.ReturnItem("쇠 열쇠") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("쇠 열쇠"));
            }
            if (Inventory.S.ReturnItem("금 열쇠") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("금 열쇠"));
            }
            if (Inventory.S.ReturnItem("보석 열쇠") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("보석 열쇠"));
            }
            if (Inventory.S.ReturnItem("비밀방 두루마리") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("비밀방 두루마리"));
            }
            if (Inventory.S.ReturnItem("연막탄") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("연막탄"));
            }
        }
        else if (Player.S.mainProgress < 14)
        {
            if (Inventory.S.ReturnItem("돌 열쇠") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("돌 열쇠"));
            }
            if (Inventory.S.ReturnItem("쇠 열쇠") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("쇠 열쇠"));
            }
            if (Inventory.S.ReturnItem("금 열쇠") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("금 열쇠"));
            }
            if (Inventory.S.ReturnItem("보석 열쇠") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("보석 열쇠"));
            }
            if (Inventory.S.ReturnItem("비밀방 두루마리") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("비밀방 두루마리"));
            }
            if (Inventory.S.ReturnItem("연막탄") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("연막탄"));
            }
        }
        else if (Player.S.mainProgress < 15)
        {
            if (Inventory.S.ReturnItem("돌 열쇠") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("돌 열쇠"));
            }
            if (Inventory.S.ReturnItem("쇠 열쇠") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("쇠 열쇠"));
            }
            if (Inventory.S.ReturnItem("금 열쇠") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("금 열쇠"));
            }
            if (Inventory.S.ReturnItem("보석 열쇠") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("보석 열쇠"));
            }
            if (Inventory.S.ReturnItem("비밀방 두루마리") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("비밀방 두루마리"));
            }
            if (Inventory.S.ReturnItem("연막탄") != null)
            {
                sellItems.Add(Inventory.S.ReturnItem("연막탄"));
            }
        }
    }
}
