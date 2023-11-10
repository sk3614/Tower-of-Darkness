using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryData : MonoBehaviour
{
    public static InventoryData S;
    public int StoneKey;
    public int GoldKey;
    public int MetalKey;
    public int JewelKey;
    public int smokeBomb;
    public int SecretWallScroll;
    public int Godstone;
    public int MysticPowder;

    public void Awake()
    {
        if (S==null)
        {
            S = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void update()
    {
    }
    public void SaveKeys()
    {
        StoneKey = Inventory.S.SearchItemCount("돌 열쇠");
        MetalKey = Inventory.S.SearchItemCount("쇠 열쇠");
        GoldKey = Inventory.S.SearchItemCount("금 열쇠");
        JewelKey = Inventory.S.SearchItemCount("보석 열쇠");
        smokeBomb = Inventory.S.SearchItemCount("연막탄");
        SecretWallScroll = Inventory.S.SearchItemCount("비밀방 두루마리");
        Godstone = Inventory.S.SearchItemCount("신석");
        MysticPowder = Inventory.S.SearchItemCount("신비한 가루");
    }
    public void LoadKey()
    {
        AddItem.S.SearchItem("돌 열쇠", StoneKey);
        AddItem.S.SearchItem("쇠 열쇠", MetalKey);
        AddItem.S.SearchItem("금 열쇠", GoldKey);
        AddItem.S.SearchItem("보석 열쇠", JewelKey);
        AddItem.S.SearchItem("연막탄", smokeBomb);
        AddItem.S.SearchItem("비밀방 두루마리", SecretWallScroll);
        AddItem.S.SearchItem("신석", Godstone);
        AddItem.S.SearchItem("신비한 가루", MysticPowder);

    }
    public void ResetKey()
    {
        StoneKey = 0;
        MetalKey = 0;
        GoldKey = 0;
        JewelKey = 0;
    }

}
