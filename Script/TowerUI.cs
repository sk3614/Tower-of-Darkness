using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{

    public Text HP;
    public Text Level;
    public Text ATK;
    public Text DEF;
    public Text StoneKey;
    public Text MetalKey;
    public Text JewelKey;
    public Text GoldKey;
    public Text Gold;
    public Text EXP;

    public Text JobName;
    public Text HIT;
    public Text AVD;
    public Text CRC;
    public Text CRD;
    public Text SPD;
    public Text POW;
    public Text Kostack;

    //2ND
    public Text Progress;

    public Text fleeStack;
    public Text secretWallChance;
    public GameObject First;
    public GameObject Second;
    
    public void Update()
    {

        if (!Battle.S.battleUIBase.activeInHierarchy)
        {
            if (!TowerStory.S.storyUI.activeInHierarchy)
            {
                UpdatePlayerSimpleUI();
                if (Input.GetKeyDown(KeyCode.Tab))
        {
                    ChangeStatUI();
                }
            }

        }

    }

    public GameObject monsterBookUI;
    public GameObject FloorMoveUI;
    public GameObject TowerMarketUI;

    public void MonsterBookUIOn()
    {
        MonsterBookUI.S.UIOn();
    }
    public void FloorMoveUIOn()
    {
        FloorMoveUI.GetComponentInParent<MoveFloorUI>().MoveFloorUIOpen();
    }
    public void TowerMarketUIOn()
    {
        TowerMarketUI.SetActive(true);
    }

    public void UpdatePlayerSimpleUI()
    {
        JobName.text = Player.S.characterName;
        HP.text ="HP : "+ Player.S.hp;
        Level.text = "Level : " + Player.S.level;
        ATK.text = "ATK : " + Player.S.ATK;
        DEF.text = "DEF : " + Player.S.DEF;
        Gold.text = "Gold : " + Player.S.gold;
        EXP.text = "EXP : " + Player.S.exp;
        HIT.text = "HIT : " + Player.S.HIT;
        AVD.text = "AVD : " + Mathf.CeilToInt(Player.S.AVD);
        CRC.text = "CRC : " + Mathf.CeilToInt(Player.S.CRC);
        POW.text = "POW : " + Mathf.CeilToInt(Player.S.POW);
        SPD.text = "SPD : " + Player.S.SPD;
        Kostack.text = "KO : " + Player.S.KoStack;

        switch (Options.S.language)
        {
            case Options.Language.Kor:
                fleeStack.text = "연막탄 : " +Inventory.S.SearchItemCount("연막탄");
                Progress.text = "진행도 : " + Player.S.mainProgress;
                break;
            case Options.Language.Eng:
                fleeStack.text = "Smoke Bomb : " + Inventory.S.SearchItemCount("연막탄");
                Progress.text = "Progress : " + Player.S.mainProgress;
                break;
            default:
                break;
        }

        if (Player.S.SecretWallOn)
        {
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    secretWallChance.text = "비밀방 두루마리 : " + Inventory.S.SearchItemCount("비밀방 두루마리");
                    break;
                case Options.Language.Eng:
                    secretWallChance.text = "Reveal Scroll : " + Inventory.S.SearchItemCount("비밀방 두루마리");
                    break;
                default:
                    break;
            }

        }
        else
        {
            secretWallChance.text = "";
        }


        //PIE.text = "PIE : " + Player.S.PIE;
        //BLK.text = "BLK : " + Player.S.BLK;
        //CRR.text = "CRR : " + Player.S.CRR;
        //POW.text = "POW : " + Player.S.POW;


        StoneKey.text = Inventory.S.SearchItemCount("돌 열쇠").ToString();
        MetalKey.text = Inventory.S.SearchItemCount("쇠 열쇠").ToString();
        GoldKey.text = Inventory.S.SearchItemCount("금 열쇠").ToString();
        JewelKey.text = Inventory.S.SearchItemCount("보석 열쇠").ToString();

    }
    public void ChangeStatUI()
    {
        if (First.activeInHierarchy)
        {
            First.SetActive(false);
            Second.SetActive(true);
        }
        else
        {
            First.SetActive(true);
            Second.SetActive(false);
        }
    }
}
