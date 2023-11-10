using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MonsterBookSlot : MonoBehaviour
{
    public Image image;
    public Text Name;
    public Text HP;
    public Text ATK;
    public Text DEF;
    public Text AP;
    public Text DP;
    public Text expectDMG;
    public MonsterData mdata;

    public Transform T_skillslot;
    public GameObject P_Skillslot;

    public void SetSlot(MonsterData monster)
    {
        image.sprite = monster.monsterImage;
        mdata = monster;
        List<Dictionary<string, object>> data=MonsterBookUI.S.monsterDatas;
        switch (Options.S.language)
        {
            case Options.Language.Kor:
                Name.text = data[monster.MonsterID - 1]["name"].ToString();
                break;
            case Options.Language.Eng:
                Name.text = data[monster.MonsterID - 1]["Ename"].ToString();
                break;
            default:
                break;
        }

        HP.text = "HP : "+ data[monster.MonsterID-1]["HP"].ToString();
        ATK.text = "ATK : " + data[monster.MonsterID-1]["ATK"].ToString();
        DEF.text = "DEF : " + data[monster.MonsterID-1]["DEF"].ToString();
        AP.text = "AP : " + data[monster.MonsterID - 1]["A.P"].ToString();
        DP.text = "DP : " + data[monster.MonsterID - 1]["D.P"].ToString();
        int Mhp = (int)data[monster.MonsterID-1]["HP"];
        int ap = (int)data[monster.MonsterID - 1]["A.P"];
        int dp = (int)data[monster.MonsterID - 1]["D.P"];
        int Mdmg;
        if ((int)data[monster.MonsterID - 1]["Class"]==5)
        {
            Mdmg = (int)data[monster.MonsterID - 1]["ATK"] * ap;
        }
        else
        {
            Mdmg = (int)data[monster.MonsterID - 1]["ATK"] * ap - Player.S.DEF;
        }



        int Pdmg = Player.S.ATK*Player.S.AP - (int)data[monster.MonsterID-1]["DEF"]*dp;
        int expectDmg = 0;
        Pdmg = Mathf.Clamp(Pdmg, 0,10000);
        Mdmg = Mathf.Clamp(Mdmg, 0, 10000);
        if (Pdmg<1||(Pdmg<1&&Mdmg<1))
        {
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    expectDMG.text = "예상피해 : " + Player.S.hp.ToString();
                    break;
                case Options.Language.Eng:
                    expectDMG.text = "Expected damage : " + Player.S.hp.ToString();
                    break;
                default:
                    break;
            }

        }
        else
        {
            if ((int)data[monster.MonsterID - 1]["SPD"] <= Player.S.SPD)
            {
                while (Mhp > 0)
                {
                    Mhp -= Pdmg;
                    if (Mhp <= 0)
                    {
                        break;
                    }
                    expectDmg += Mdmg;
                }
            }
            else
            {
                while (Mhp > 0)
                {
                    expectDmg += Mdmg;
                    Mhp -= Pdmg;
                }
            }
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    expectDMG.text = "예상피해 : " + expectDmg.ToString();
                    break;
                case Options.Language.Eng:
                    expectDMG.text = "Expected damage : " + expectDmg.ToString();
                    break;
                default:
                    break;
            }

        }


        switch (Options.S.language)
        {
            case Options.Language.Kor:
                Name.text = data[monster.MonsterID - 1]["name"].ToString();
                break;
            case Options.Language.Eng:
                Name.text = data[monster.MonsterID - 1]["Ename"].ToString();
                break;
            default:
                break;
        }
        




    }

    public void OpenInfo()
    {
        MonsterBookUI.S.infoui.UiOn(mdata);

    }

}


