using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookInfoUI : MonoBehaviour
{
    private MonsterData mdata;

    public GameObject SkillSlotPrefab;
    public Transform T_skillslot;

    public Text skillInfo;

    public GameObject uibase;
    public Image monsterImage;
    public Text m_name;
    public Text hp;
    public Text atk;
    public Text def;
    public Text kind;
    public Text job;
    public Text pow;
    public Text spd;
    public Text hit;
    public Text avd;
    public Text crc;
    public Text crd;
    public Text crr;
    public Text pie;
    public Text blk;
    public Text icd;
    public Text dcd;
    public Text vam;
    public Text reg;
    public Text hel;
    public Text arc;
    public Text AP;
    public Text DP;
    public Text BP;

    public List<GameObject> prefabs = new List<GameObject>();

    public void UiOn(MonsterData _data)
    {
        uibase.SetActive(true);
        mdata = _data;
        monsterImage.sprite = mdata.monsterImage;
        List<Dictionary<string, object>> data = MonsterBookUI.S.monsterDatas;

        switch (Options.S.language)
        {
            case Options.Language.Kor:
                m_name.text = data[mdata.MonsterID - 1]["name"].ToString();
                break;
            case Options.Language.Eng:
                m_name.text = data[mdata.MonsterID - 1]["Ename"].ToString();
                break;
            default:
                break;
        }
        kind.text = ReturnFace((int)data[mdata.MonsterID - 1]["Face"]);
        job.text = ReturnClass((int)data[mdata.MonsterID - 1]["Class"]);
        hp.text = "HP : " + data[mdata.MonsterID - 1]["HP"].ToString();
        atk.text = "ATK : " + data[mdata.MonsterID - 1]["ATK"].ToString();
        def.text = "DEF : " + data[mdata.MonsterID - 1]["DEF"].ToString();
        AP.text = "AP : " + data[mdata.MonsterID - 1]["A.P"].ToString();
        DP.text = "DP : " + data[mdata.MonsterID - 1]["D.P"].ToString();

        pow.text = "POW : " + data[mdata.MonsterID - 1]["POW"].ToString();
        spd.text = "SPD : " + data[mdata.MonsterID - 1]["SPD"].ToString();
        hit.text = "HIT : " + data[mdata.MonsterID - 1]["HIT"].ToString();
        avd.text = "AVD : " + data[mdata.MonsterID - 1]["AVD"].ToString();
        crc.text = "CRC : " + data[mdata.MonsterID - 1]["CRC"].ToString();
        crd.text = "CRD : " + data[mdata.MonsterID - 1]["CRD"].ToString();
        crr.text = "CRR : " + data[mdata.MonsterID - 1]["CRR"].ToString();
        pie.text = "PIE : " + data[mdata.MonsterID - 1]["PIE"].ToString();
        blk.text = "BLK : " + data[mdata.MonsterID - 1]["BLK"].ToString();
        icd.text = "ICD : " + data[mdata.MonsterID - 1]["ICD"].ToString();
        dcd.text = "DCD : " + data[mdata.MonsterID - 1]["DCD"].ToString();
        vam.text = "VAM : " + data[mdata.MonsterID - 1]["VAM"].ToString();
        reg.text = "REG : " + data[mdata.MonsterID - 1]["REG"].ToString();
        hel.text = "HEL : " + data[mdata.MonsterID - 1]["HEL"].ToString();
        arc.text = "ARC : " + data[mdata.MonsterID - 1]["ARC"].ToString();
        BP.text = "BP : " + data[mdata.MonsterID - 1]["BP"].ToString();

        SetSkills(mdata);
    }

    public string ReturnFace(int _data)
    {
        switch (Options.S.language)
        {
            case Options.Language.Kor:
                switch (_data)
                {
                    case 1:
                        //1.수호자
                        //2.전사
                        //3.암살자
                        //4.사수
                        //5.마법사
                        //6.무투가
                        //7.기타
                        return "불사";
                    case 2:
                        return "악마";
                    case 3:
                        return "타락자";
                    case 4:
                        return "피조물";
                    case 5:
                        return "마물";
                    case 6:
                        return "정령";
                    case 7:
                        return "아인";
                    case 8:
                        return "기타";
                    default:
                        break;
                }
                break;
            case Options.Language.Eng:
                switch (_data)
                {
                    case 1:
                        //1.불사
                        //2.악마
                        //3.타락자
                        //4.피조물
                        //5.마물
                        //6.정령
                        //7.아인
                        //8.기타
                        //1.수호자
                        //2.전사
                        //3.암살자
                        //4.사수
                        //5.마법사
                        //6.무투가
                        //7.기타
                        return "Undead";
                    case 2:
                        return "Demon";
                    case 3:
                        return "Fallen";
                    case 4:
                        return "Creature";
                    case 5:
                        return "Monster";
                    case 6:
                        return "Elemental";
                    case 7:
                        return "Anthro";
                    case 8:
                        return "ETC";
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        return "";
    }
    public string ReturnClass(int _data)
    {
        switch (Options.S.language)
        {
            case Options.Language.Kor:
                switch (_data)
                {
                    case 1:
                        return "수호자";
                    case 2:
                        return "전사";
                    case 3:
                        return "암살자";
                    case 4:
                        return "사수";
                    case 5:
                        return "마법사";
                    case 6:
                        return "무투가";
                    case 7:
                        return "기타";
                    default:
                        break;
                }
                break;
            case Options.Language.Eng:
                switch (_data)
                {
                    case 1:
                        return "Guardian";
                    case 2:
                        return "Warrior";
                    case 3:
                        return "Assassin";
                    case 4:
                        return "Archer";
                    case 5:
                        return "Wizard";
                    case 6:
                        return "Fighter";
                    case 7:
                        return "ETC";
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        return "";
    }
    public void SetSkills(MonsterData _data)
    {
        skillInfo.text = "";
        if (prefabs.Count > 0)
        {
            for (int i = prefabs.Count - 1; i >= 0; i--)
            {
                Destroy(prefabs[i]);
            }
            prefabs.Clear();
        }
        for (int i = 0; i < _data.skillList.Count; i++)
        {
            GameObject go = Instantiate(SkillSlotPrefab, T_skillslot);
            MonsterBookSkillSlot slot = go.GetComponent<MonsterBookSkillSlot>();
            slot.bookInfoUI = this;
            slot.InfoUISetSlot(_data.skillList[i]);
            prefabs.Add(go);
        }
    }
}
