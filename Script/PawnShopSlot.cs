using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PawnShopSlot : MonoBehaviour
{

    public Item item;
    public PawnShop pawnShop;

    public Image itemImage;
    public Text ItemName;
    public Text itemPrice;

    public void SetShopSlot()
    {
        itemImage.sprite = item.itemImage;
        itemPrice.text = item.sellPrice + " Gold";

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


        if (Player.S.mainProgress < 4)
        {
            if (item.itemName == "돌 열쇠")
            {
                itemPrice.text = 5 + " Gold";
            }
            else if (item.itemName == "쇠 열쇠")
            {
                itemPrice.text = 20 + " Gold";
            }
        }
        else if (Player.S.mainProgress < 7)
        {
            if (item.itemName == "돌 열쇠")
            {
                itemPrice.text = 7 + " Gold";
            }
            else if (item.itemName == "쇠 열쇠")
            {
                itemPrice.text = 30 + " Gold";
            }
            else if (item.itemName == "금 열쇠")
            {
                itemPrice.text = 55 + " Gold";
            }
            else if (item.itemName == "연막탄")
            {
                itemPrice.text = 5 + " Gold";
            }
        }
        else if (Player.S.mainProgress < 9)
        {
            if (item.itemName == "돌 열쇠")
            {
                itemPrice.text = 10 + " Gold";
            }
            else if (item.itemName == "쇠 열쇠")
            {
                itemPrice.text = 40 + " Gold";
            }
            else if (item.itemName == "금 열쇠")
            {
                itemPrice.text = 66 + " Gold";
            }
            else if (item.itemName == "비밀방 두루마리")
            {
                itemPrice.text = 15 + " Gold";
            }
            else if (item.itemName == "연막탄")
            {
                itemPrice.text = 7 + " Gold";
            }
        }
        else if (Player.S.mainProgress < 10)
        {
            if (item.itemName == "돌 열쇠")
            {
                itemPrice.text = 10 + " Gold";
            }
            else if (item.itemName == "쇠 열쇠")
            {
                itemPrice.text = 40 + " Gold";
            }
            else if (item.itemName == "금 열쇠")
            {
                itemPrice.text = 66 + " Gold";
            }
            else if (item.itemName == "보석 열쇠")
            {
                itemPrice.text = 200 + " Gold";
            }
            else if (item.itemName == "비밀방 두루마리")
            {
                itemPrice.text = 15 + " Gold";
            }
            else if (item.itemName == "연막탄")
            {
                itemPrice.text = 7 + " Gold";
            }
        }
        gameObject.GetComponent<Button>().onClick.AddListener(() => pawnShop.SelectItem(item));
    }
}
