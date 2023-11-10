using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeddlerItems : MonoBehaviour
{
    public MarketPlace marketPlace;
    public void Awake()
    {
        //SetShop();
    }

    public void SetShop()
    {
        SellListByProgress sellListByProgress = new SellListByProgress();
        sellListByProgress.Progress = Player.S.mainProgress;
        if (Player.S.mainProgress>=3)
        {
            if (Player.S.Pedller[0]==0)
            {

            }
            else
            {
                sellListByProgress.sellItems.Add(CreateSellitem(1, "CP 강화석", 125,1));
            }

        }if (Player.S.mainProgress >= 4)
        {
            if (Player.S.Pedller[1] == 0)
            {

            }
            else
            {
                sellListByProgress.sellItems.Add(CreateSellitem(1, "PP 강화석", 100,2));
            }

        }
        if (Player.S.mainProgress >= 6)
        {
            if (Player.S.Pedller[2] == 0)
            {

            }
            else
            {
                sellListByProgress.sellItems.Add(CreateSellitem(1, "CP 강화석", 100,3));
            }
        }
        if (Player.S.mainProgress >= 7)
        {
            if (Player.S.Pedller[3] == 0)
            {

            }
            else
            {
                sellListByProgress.sellItems.Add(CreateSellitem(1, "TP 강화석", 120,4));
            }

        }
        if (Player.S.mainProgress >= 8)
        {
            if (Player.S.Pedller[4] == 0)
            {

            }
            else
            {
                sellListByProgress.sellItems.Add(CreateSellitem(1, "PP 강화석", 150,5));
            }

        }
        if (Player.S.mainProgress >= 10)
        {
            if (Player.S.Pedller[5] == 0)
            {

            }
            else if (Player.S.Pedller[5] == 1)
            {
                sellListByProgress.sellItems.Add(CreateSellitem(1, "CP 강화석", 100, 6));
            }
            else
            {
                sellListByProgress.sellItems.Add(CreateSellitem(2, "CP 강화석", 100, 6));
            }

        }
        if (Player.S.mainProgress >= 12)
        {
            if (Player.S.Pedller[5] == 0)
            {

            }
            else
            {
                sellListByProgress.sellItems.Add(CreateSellitem(1, "PP 강화석", 200, 7));
            }

        }
        if (Player.S.mainProgress >= 14)
        {
            if (Player.S.Pedller[6] == 0)
            {

            }
            else
            {
                sellListByProgress.sellItems.Add(CreateSellitem(1, "CP 강화석", 125, 8));
            }

        }
        marketPlace.sellListByProgresses.Clear();
        marketPlace.sellListByProgresses.Add(sellListByProgress);
        return;
    }

    public SellItem CreateSellitem(int num,string name,int _price,int _peddlernum)
    {
        SellItem item = new SellItem();
        item.item = AddItem.S.itemDictionary[name];
        item.num = num;
        item.price = _price;
        item.peddlerNum = _peddlernum;
        return item;
    }
}
