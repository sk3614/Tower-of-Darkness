using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeddlerTable : MonoBehaviour
{
    public MarketPlace marketPlace;

    public Item equipDummy;
    public Equipment[] weapons;
    public Equipment[] armors;
    public Equipment[] acces;

    public int weapontier;
    public int armortier;
    public int accetier;

    public void Awake()
    {
        if (Player.S.mainProgress>=3)
        {
            SetShop();
        }

    }

    public void SetShop()
    {
        marketPlace.sellListByProgresses.Clear();
        SellListByProgress sellListByProgress = new SellListByProgress();
        sellListByProgress.Progress = Player.S.mainProgress;

        armortier = 0;
        weapontier = 0;
        accetier = 0;
        for (int i = 0; i <Player.S.EquipmentStrings.Count ; i++)
        {
            //weapon
            if (Player.S.EquipmentStrings[i]=="판금 갑옷")
            {
                armortier += 1;
            }
            if (Player.S.EquipmentStrings[i] == "비늘 갑옷")
            {
                armortier += 1;
            }
            if (Player.S.EquipmentStrings[i] == "경갑옷")
            {
                armortier += 1;
            }

            //acce
            if (Player.S.EquipmentStrings[i] == "룬 팔찌")
            {
                accetier += 1;
            }
            if (Player.S.EquipmentStrings[i] == "벌레가 든 병")
            {
                accetier += 1;
            }

            //armor
            if ((Player.S.EquipmentStrings[i] == "강력한 검"
               || Player.S.EquipmentStrings[i] == "강력한 검+1"
               || Player.S.EquipmentStrings[i] == "강력한 검+2"
               || Player.S.EquipmentStrings[i] == "강력한 검+3"))
            {
                weapontier += 1;
            }
            if ((Player.S.EquipmentStrings[i] == "대검"
                || Player.S.EquipmentStrings[i] == "대검+1"
                || Player.S.EquipmentStrings[i] == "대검+2"
                || Player.S.EquipmentStrings[i] == "대검+3"))
            {
                weapontier += 1;
            }
            if ((Player.S.EquipmentStrings[i] == "노장의 검"
                || Player.S.EquipmentStrings[i] == "노장의 검+1"
                || Player.S.EquipmentStrings[i] == "노장의 검+2"
                || Player.S.EquipmentStrings[i] == "노장의 검+3"))
            {
                weapontier += 1;
            }

        }
        if (weapons.Length>weapontier)
        {
            switch (weapontier)
            {
                case 0:
                    sellListByProgress.sellEquip.Add(CreateSellEquip(weapontier, weapons));
                    break;
                case 1:
                    if (Player.S.mainProgress>=6)
                    {
                    sellListByProgress.sellEquip.Add(CreateSellEquip(weapontier, weapons));
                    }

                    break;
                case 2:
                    if (Player.S.mainProgress >= 10)
                    {
                        sellListByProgress.sellEquip.Add(CreateSellEquip(weapontier, weapons));
                    }
                    break;
                case 3:
                    break;
                default:
                    break;
            }

        }
        if (armors.Length > armortier)
        {

            switch (armortier)
            {
                case 0:
                    sellListByProgress.sellEquip.Add(CreateSellEquip(armortier, armors));
                    break;
                case 1:
                    if (Player.S.mainProgress >= 6)
                    {
                        sellListByProgress.sellEquip.Add(CreateSellEquip(armortier, armors));
                    }

                    break;
                case 2:
                    if (Player.S.mainProgress >= 10)
                    {
                        sellListByProgress.sellEquip.Add(CreateSellEquip(armortier, armors));
                    }
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }

        if (acces.Length > accetier)
        {
            switch (accetier)
            {
                case 0:
                    sellListByProgress.sellEquip.Add(CreateSellEquip(accetier, acces));
                    break;
                case 1:
                    if (Player.S.mainProgress >= 6)
                    {
                        sellListByProgress.sellEquip.Add(CreateSellEquip(accetier, acces));
                    }

                    break;
                case 2:
                    if (Player.S.mainProgress >= 10)
                    {
                        sellListByProgress.sellEquip.Add(CreateSellEquip(accetier, acces));
                    }
                    break;
                case 3:
                    break;
                default:
                    break;
            }

        }






        marketPlace.sellListByProgresses.Add(sellListByProgress);
        return;
    }

    public SellEquip CreateSellEquip(int tier,Equipment[] equips)
    {
        SellEquip equip = new SellEquip();

        equip.item = equipDummy;
        equip.name = equips[tier].equipName;
        switch (tier)
        {
            case 0:
                equip.price = 100;
                break;
            case 1:
                equip.price = 200;
                break;
            case 2:
                equip.price = 300;
                break;
            case 3:
                equip.price = 350;
                break;
            default:
                break;
        }
        equip.num = 1;
        return equip;
    }
}
