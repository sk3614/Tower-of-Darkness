using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BlackMarketTable : MonoBehaviour
{
    public MarketPlace marketPlace;
    public void Start()
    {
        MP3();
        MP4();
        MP5();
        MP6();
        MP7();
        MP8();
        MP9();
        MP10();
        MP11();
        MP12();
        MP13();
        MP14();
        if (marketPlace.isBlackMarketload)
        {

        }
        else
        {
            marketPlace.SetMarketByProgress();
        }

    }

    public void MP3()
    {
        SellListByProgress sellListByProgress=new SellListByProgress();
        sellListByProgress.Progress = 3;


        int value = 0;
        //
        for (int i = 0; i < 3; i++)
        {
            value = Random.Range(0, 12);
            if (value < 1)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["돌 열쇠"];
                sellItem.price = 6;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 2)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["쇠 열쇠"];
                sellItem.price = 25;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 3)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["금 열쇠"];
                sellItem.price = 75;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 4)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["초록 물약"];
                sellItem.price = 4;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 5)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["푸른 물약"];
                sellItem.price = 8;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 6)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["붉은 물약"];
                sellItem.price = 10;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 7)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["루비"];
                sellItem.price = 25;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 8)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["사파이어"];
                sellItem.price = 25;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 9)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["에메랄드"];
                sellItem.price = 100;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 10)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["다이아몬드"];
                sellItem.price = 50;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 11)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["연막탄"];
                sellItem.price = 5;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 12)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["비밀방 두루마리"];
                sellItem.price = 10;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
        } 


       
        marketPlace.sellListByProgresses.Add(sellListByProgress);
        return;
     }
    public void MP4()
    {
        SellListByProgress sellListByProgress = new SellListByProgress();
        sellListByProgress.Progress = 4;

        int value = 0;
        //
        for (int i = 0; i < 3; i++)
        {
            value = Random.Range(0, 12);
            if (value < 1)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["돌 열쇠"];
                sellItem.price = 6;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 2)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["쇠 열쇠"];
                sellItem.price = 25;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 3)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["금 열쇠"];
                sellItem.price = 75;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 4)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["초록 물약"];
                sellItem.price = 4;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 5)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["푸른 물약"];
                sellItem.price = 8;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 6)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["붉은 물약"];
                sellItem.price = 10;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 7)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["루비"];
                sellItem.price = 25;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 8)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["사파이어"];
                sellItem.price = 25;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 9)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["에메랄드"];
                sellItem.price = 100;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 10)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["다이아몬드"];
                sellItem.price = 50;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 11)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["연막탄"];
                sellItem.price = 5;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 12)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["비밀방 두루마리"];
                sellItem.price = 10;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
        }
        marketPlace.sellListByProgresses.Add(sellListByProgress);
        return;
    }
    public void MP5()
    {
        SellListByProgress sellListByProgress = new SellListByProgress();
        sellListByProgress.Progress = 5;


        int value = 0;
        //
        for (int i = 0; i < 4; i++)
        {
            value = Random.Range(0, 17);
            if (value < 1)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["돌 열쇠"];
                sellItem.price = 8;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 2)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["쇠 열쇠"];
                sellItem.price = 40;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 3)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["금 열쇠"];
                sellItem.price = 100;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 4)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["초록 물약"];
                sellItem.price =10;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 5)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["푸른 물약"];
                sellItem.price = 15;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 6)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["붉은 물약"];
                sellItem.price = 25;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 7)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["흰 물약"];
                sellItem.price = 35;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 8)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["루비"];
                sellItem.price = 50;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 9)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["사파이어"];
                sellItem.price = 50;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 10)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["아메지스트"];
                sellItem.price = 100;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 11)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["앰버"];
                sellItem.price = 100;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 12)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["다이아몬드"];
                sellItem.price = 100;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 13)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["경험의 유물"];
                sellItem.price = 150;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 14)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["연막탄"];
                sellItem.price = 8;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 15)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["비밀방 두루마리"];
                sellItem.price = 16;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 16)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["신석"];
                sellItem.price = 100;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 17)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["신비한 가루"];
                sellItem.price = 15;
                sellItem.num = 5;
                sellListByProgress.sellItems.Add(sellItem);
            }
        }


        marketPlace.sellListByProgresses.Add(sellListByProgress);
        return;
    }
    public void MP6()
    {
        SellListByProgress sellListByProgress = new SellListByProgress();
        sellListByProgress.Progress = 6;


        int value = 0;
        //
        for (int i = 0; i < 4; i++)
        {
            value = Random.Range(0, 17);
            if (value < 1)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["돌 열쇠"];
                sellItem.price = 8;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 2)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["쇠 열쇠"];
                sellItem.price = 40;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 3)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["금 열쇠"];
                sellItem.price = 100;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 4)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["초록 물약"];
                sellItem.price = 10;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 5)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["푸른 물약"];
                sellItem.price = 15;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 6)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["붉은 물약"];
                sellItem.price = 25;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 7)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["흰 물약"];
                sellItem.price = 35;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 8)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["루비"];
                sellItem.price = 50;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 9)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["사파이어"];
                sellItem.price = 50;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 10)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["아메지스트"];
                sellItem.price = 100;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 11)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["앰버"];
                sellItem.price = 100;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 12)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["다이아몬드"];
                sellItem.price = 100;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 13)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["경험의 유물"];
                sellItem.price = 150;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 14)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["연막탄"];
                sellItem.price = 8;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 15)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["비밀방 두루마리"];
                sellItem.price = 16;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 16)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["신석"];
                sellItem.price = 100;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 17)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["신비한 가루"];
                sellItem.price = 15;
                sellItem.num = 5;
                sellListByProgress.sellItems.Add(sellItem);
            }
        }


        marketPlace.sellListByProgresses.Add(sellListByProgress);
        return;
    }
    public void MP7()
    {
        SellListByProgress sellListByProgress = new SellListByProgress();
        sellListByProgress.Progress = 7;


        int value = 0;
        //
        for (int i = 0; i < 5; i++)
        {
            value = Random.Range(0, 20);
            if (value < 1)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["돌 열쇠"];
                sellItem.price = 10;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 2)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["쇠 열쇠"];
                sellItem.price = 50;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 3)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["금 열쇠"];
                sellItem.price = 125;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 4)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["초록 물약"];
                sellItem.price = 15;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 5)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["푸른 물약"];
                sellItem.price = 25;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 6)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["붉은 물약"];
                sellItem.price = 35;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 7)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["흰 물약"];
                sellItem.price = 50;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 8)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["루비"];
                sellItem.price = 75;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 9)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["사파이어"];
                sellItem.price = 75;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 10)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["아메지스트"];
                sellItem.price = 125;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 11)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["앰버"];
                sellItem.price = 125;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 12)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["다이아몬드"];
                sellItem.price = 125;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 13)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["경험의 유물"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 14)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["연막탄"];
                sellItem.price = 8;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 15)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["비밀방 두루마리"];
                sellItem.price = 16;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 16)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["신석"];
                sellItem.price = 150;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 17)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["신비한 가루"];
                sellItem.price = 25;
                sellItem.num = 2;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 18)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["에메랄드"];
                sellItem.price = 150;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 19)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["자격의 유물"];
                sellItem.price = 150;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 20)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["보석 열쇠"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
        }


        marketPlace.sellListByProgresses.Add(sellListByProgress);
        return;
    }
    public void MP8()
    {
        SellListByProgress sellListByProgress = new SellListByProgress();
        sellListByProgress.Progress = 8;


        int value = 0;
        //
        for (int i = 0; i < 5; i++)
        {
            value = Random.Range(0, 20);
            if (value < 1)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["돌 열쇠"];
                sellItem.price = 10;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 2)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["쇠 열쇠"];
                sellItem.price = 50;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 3)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["금 열쇠"];
                sellItem.price = 125;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 4)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["초록 물약"];
                sellItem.price = 15;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 5)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["푸른 물약"];
                sellItem.price = 25;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 6)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["붉은 물약"];
                sellItem.price = 35;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 7)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["흰 물약"];
                sellItem.price = 50;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 8)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["루비"];
                sellItem.price = 75;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 9)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["사파이어"];
                sellItem.price = 75;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 10)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["아메지스트"];
                sellItem.price = 125;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 11)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["앰버"];
                sellItem.price = 125;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 12)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["다이아몬드"];
                sellItem.price = 125;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 13)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["경험의 유물"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 14)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["연막탄"];
                sellItem.price = 8;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 15)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["비밀방 두루마리"];
                sellItem.price = 16;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 16)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["신석"];
                sellItem.price = 150;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 17)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["신비한 가루"];
                sellItem.price = 25;
                sellItem.num = 2;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 18)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["에메랄드"];
                sellItem.price = 150;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 19)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["자격의 유물"];
                sellItem.price = 150;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 20)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["보석 열쇠"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
        }


        marketPlace.sellListByProgresses.Add(sellListByProgress);
        return;
    }
    public void MP9()
    {
        SellListByProgress sellListByProgress = new SellListByProgress();
        sellListByProgress.Progress = 9;


        int value = 0;
        //
        for (int i = 0; i < 5; i++)
        {
            value = Random.Range(0, 20);
            if (value < 1)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["돌 열쇠"];
                sellItem.price = 10;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 2)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["쇠 열쇠"];
                sellItem.price = 50;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 3)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["금 열쇠"];
                sellItem.price = 125;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 4)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["초록 물약"];
                sellItem.price = 15;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 5)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["푸른 물약"];
                sellItem.price = 25;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 6)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["붉은 물약"];
                sellItem.price = 35;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 7)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["흰 물약"];
                sellItem.price = 50;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 8)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["루비"];
                sellItem.price = 75;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 9)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["사파이어"];
                sellItem.price = 75;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 10)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["아메지스트"];
                sellItem.price = 125;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 11)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["앰버"];
                sellItem.price = 125;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 12)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["다이아몬드"];
                sellItem.price = 125;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 13)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["경험의 유물"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 14)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["연막탄"];
                sellItem.price = 8;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 15)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["비밀방 두루마리"];
                sellItem.price = 16;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 16)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["신석"];
                sellItem.price = 150;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 17)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["신비한 가루"];
                sellItem.price = 25;
                sellItem.num = 2;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 18)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["에메랄드"];
                sellItem.price = 150;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 19)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["자격의 유물"];
                sellItem.price = 150;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 20)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["보석 열쇠"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
        }


        marketPlace.sellListByProgresses.Add(sellListByProgress);
        return;
    }
    public void MP10()
    {
        SellListByProgress sellListByProgress = new SellListByProgress();
        sellListByProgress.Progress = 10;


        int value = 0;
        //
        for (int i = 0; i < 6; i++)
        {
            value = Random.Range(0, 20);
            if (value < 1)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["돌 열쇠"];
                sellItem.price = 20;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 2)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["쇠 열쇠"];
                sellItem.price = 75;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 3)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["금 열쇠"];
                sellItem.price = 175;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 4)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["초록 물약"];
                sellItem.price = 20;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 5)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["푸른 물약"];
                sellItem.price = 40;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 6)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["붉은 물약"];
                sellItem.price = 60;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 7)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["흰 물약"];
                sellItem.price = 80;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 8)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["루비"];
                sellItem.price = 120;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 9)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["사파이어"];
                sellItem.price = 120;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 10)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["아메지스트"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 11)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["앰버"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 12)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["다이아몬드"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 13)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["경험의 유물"];
                sellItem.price = 300;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 14)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["연막탄"];
                sellItem.price = 10;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 15)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["비밀방 두루마리"];
                sellItem.price = 20;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 16)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["신석"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 17)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["신비한 가루"];
                sellItem.price = 37;
                sellItem.num = 2;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 18)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["에메랄드"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 19)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["자격의 유물"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 20)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["보석 열쇠"];
                sellItem.price = 300;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
        }


        marketPlace.sellListByProgresses.Add(sellListByProgress);
        return;
    }
    public void MP11()
    {
        SellListByProgress sellListByProgress = new SellListByProgress();
        sellListByProgress.Progress = 11;


        int value = 0;
        //
        for (int i = 0; i < 6; i++)
        {
            value = Random.Range(0, 20);
            if (value < 1)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["돌 열쇠"];
                sellItem.price = 20;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 2)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["쇠 열쇠"];
                sellItem.price = 75;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 3)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["금 열쇠"];
                sellItem.price = 175;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 4)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["초록 물약"];
                sellItem.price = 20;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 5)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["푸른 물약"];
                sellItem.price = 40;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 6)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["붉은 물약"];
                sellItem.price = 60;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 7)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["흰 물약"];
                sellItem.price = 80;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 8)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["루비"];
                sellItem.price = 120;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 9)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["사파이어"];
                sellItem.price = 120;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 10)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["아메지스트"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 11)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["앰버"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 12)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["다이아몬드"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 13)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["경험의 유물"];
                sellItem.price = 300;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 14)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["연막탄"];
                sellItem.price = 10;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 15)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["비밀방 두루마리"];
                sellItem.price = 20;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 16)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["신석"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 17)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["신비한 가루"];
                sellItem.price = 37;
                sellItem.num = 2;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 18)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["에메랄드"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 19)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["자격의 유물"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 20)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["보석 열쇠"];
                sellItem.price = 300;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
        }


        marketPlace.sellListByProgresses.Add(sellListByProgress);
        return;
    }
    public void MP12()
    {
        SellListByProgress sellListByProgress = new SellListByProgress();
        sellListByProgress.Progress = 12;


        int value = 0;
        //
        for (int i = 0; i < 6; i++)
        {
            value = Random.Range(0, 20);
            if (value < 1)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["돌 열쇠"];
                sellItem.price = 20;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 2)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["쇠 열쇠"];
                sellItem.price = 75;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 3)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["금 열쇠"];
                sellItem.price = 175;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 4)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["초록 물약"];
                sellItem.price = 20;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 5)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["푸른 물약"];
                sellItem.price = 40;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 6)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["붉은 물약"];
                sellItem.price = 60;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 7)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["흰 물약"];
                sellItem.price = 80;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 8)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["루비"];
                sellItem.price = 120;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 9)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["사파이어"];
                sellItem.price = 120;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 10)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["아메지스트"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 11)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["앰버"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 12)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["다이아몬드"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 13)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["경험의 유물"];
                sellItem.price = 300;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 14)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["연막탄"];
                sellItem.price = 10;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 15)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["비밀방 두루마리"];
                sellItem.price = 20;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 16)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["신석"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 17)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["신비한 가루"];
                sellItem.price = 37;
                sellItem.num = 2;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 18)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["에메랄드"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 19)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["자격의 유물"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 20)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["보석 열쇠"];
                sellItem.price = 300;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
        }


        marketPlace.sellListByProgresses.Add(sellListByProgress);
        return;
    }
    public void MP13()
    {
        SellListByProgress sellListByProgress = new SellListByProgress();
        sellListByProgress.Progress = 13;


        int value = 0;
        //
        for (int i = 0; i < 6; i++)
        {
            value = Random.Range(0, 20);
            if (value < 1)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["돌 열쇠"];
                sellItem.price = 20;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 2)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["쇠 열쇠"];
                sellItem.price = 75;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 3)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["금 열쇠"];
                sellItem.price = 175;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 4)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["초록 물약"];
                sellItem.price = 20;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 5)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["푸른 물약"];
                sellItem.price = 40;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 6)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["붉은 물약"];
                sellItem.price = 60;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 7)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["흰 물약"];
                sellItem.price = 80;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 8)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["루비"];
                sellItem.price = 120;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 9)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["사파이어"];
                sellItem.price = 120;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 10)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["아메지스트"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 11)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["앰버"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 12)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["다이아몬드"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 13)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["경험의 유물"];
                sellItem.price = 300;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 14)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["연막탄"];
                sellItem.price = 10;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 15)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["비밀방 두루마리"];
                sellItem.price = 20;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 16)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["신석"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 17)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["신비한 가루"];
                sellItem.price = 37;
                sellItem.num = 2;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 18)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["에메랄드"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 19)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["자격의 유물"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 20)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["보석 열쇠"];
                sellItem.price = 300;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
        }


        marketPlace.sellListByProgresses.Add(sellListByProgress);
        return;
    }
    public void MP14()
    {
        SellListByProgress sellListByProgress = new SellListByProgress();
        sellListByProgress.Progress = 14;


        int value = 0;
        //
        for (int i = 0; i < 6; i++)
        {
            value = Random.Range(0, 20);
            if (value < 1)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["돌 열쇠"];
                sellItem.price = 20;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 2)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["쇠 열쇠"];
                sellItem.price = 75;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 3)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["금 열쇠"];
                sellItem.price = 175;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 4)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["초록 물약"];
                sellItem.price = 20;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 5)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["푸른 물약"];
                sellItem.price = 40;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 6)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["붉은 물약"];
                sellItem.price = 60;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 7)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["흰 물약"];
                sellItem.price = 80;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 8)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["루비"];
                sellItem.price = 120;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 9)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["사파이어"];
                sellItem.price = 120;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 10)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["아메지스트"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 11)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["앰버"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 12)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["다이아몬드"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 13)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["경험의 유물"];
                sellItem.price = 300;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 14)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["연막탄"];
                sellItem.price = 10;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 15)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["비밀방 두루마리"];
                sellItem.price = 20;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 16)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["신석"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 17)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["신비한 가루"];
                sellItem.price = 37;
                sellItem.num = 2;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 18)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["에메랄드"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 19)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["자격의 유물"];
                sellItem.price = 200;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
            else if (value < 20)
            {
                SellItem sellItem = new SellItem();
                sellItem.item = AddItem.S.itemDictionary["보석 열쇠"];
                sellItem.price = 300;
                sellItem.num = 1;
                sellListByProgress.sellItems.Add(sellItem);
            }
        }


        marketPlace.sellListByProgresses.Add(sellListByProgress);
        return;
    }


}
