using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingRoom : MonoBehaviour
{
    public static TrainingRoom S;

    //훈련소
    public GameObject LevelUIbase;
    public Text T_needExp;
    public Text T_curExp;
    
    public Text T_HP;
    public Text T_LV;
    public Text T_ATK;
    public Text T_DEF;
    public Text T_aHP;
    public Text T_aLV;
    public Text T_aATK;
    public Text T_aDEF;

    public bool _________________________;
    //스킬훈련소
    public GameObject SkillUIbase;
    public List<Skill> skills = new List<Skill>();
    public List<Skill> skills2 = new List<Skill>();
    public Transform T_skillSlot;
    public GameObject P_skillSlot;
    public List<GameObject> slots = new List<GameObject>();
    public int buyPrice;//구매가격
    public int plusPrice;//살때마다 오르는 가격
    public int buyPrice2;//구매가격
    public int plusPrice2;//살때마다 오르는 가격
    public bool isLoad=false;
    //InfoUI
    public GameObject infoUI;
    public Image skillImage;
    public Text skillName;
    public Text skillType;
    public Text UsePP;
    public Text buyExp;
    public Text nowExp;
    public Text skillInfo;
    public GameObject BuyButton;
    public GameObject BuyCaution;
    //전수자
    public GameObject adeptui;

    private void Awake()
    {
        if (S==null)
        {
            S = this;
        }
        else
        {
            Destroy(gameObject);
        }
      
    }
    private void Start()
    {
        if (isLoad)
        {
            isLoad = false;
        }
        else
        {
            SetSkillsByProgress();
        }
    }
    //훈련소
    public void LevelUIon()
    {
        LevelUIbase.SetActive(true);
        int needExp = ReturnNeedEXP();
        SetTrainingroomUI();

    }

    public void LevelUp()
    {
        int needExp = ReturnNeedEXP();
        if (needExp>Player.S.exp)
        {
            return;
        }
        Player.S.LevelUP();
        Player.S.SpendExp(needExp);
        SoundManager.S.PlaySE("upgrade1");
        SetTrainingroomUI();
    }


    //스킬 훈련소
    public void SkillUIon()
    {
        SkillUIbase.SetActive(true);
        SetSkillTrainingCenter();
    }
    public void CloseSkillUI()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Destroy(slots[i]);
        }
        slots.Clear();

        infoUI.SetActive(false);
    }

    public void LearnSkill(Skill _skill)
    {
        if (Player.S.exp>=buyPrice)
        {

            SkillUI.S.GetSkill(_skill);
            if (skills.Contains(_skill))
            {
                Player.S.SpendExp(buyPrice);
                buyPrice += plusPrice;
            }
            else
            {
                Player.S.SpendExp(buyPrice2);
                buyPrice2 += plusPrice2;
            }

            SetInfoUI(_skill);
            SoundManager.S.PlaySE("upgrade1");
        }
    }

    public void SetSkillTrainingCenter()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Destroy(slots[i]);
        }
        slots.Clear();

        for (int i = 0; i < skills.Count; i++)
        {
            GameObject go = Instantiate(P_skillSlot, T_skillSlot);
            go.GetComponent<TrainingSkillSlot>().skill = skills[i];
            go.GetComponent<TrainingSkillSlot>().SetSlot(this);
            slots.Add(go);
        }
        for (int i = 0; i < skills2.Count; i++)
        {
            GameObject go = Instantiate(P_skillSlot, T_skillSlot);
            go.GetComponent<TrainingSkillSlot>().skill = skills2[i];
            go.GetComponent<TrainingSkillSlot>().SetSlot(this);
            slots.Add(go);
        }
    }

    public void SetInfoUI(Skill skill)
    {
        if (skill==null)
        {
            return;
        }
        infoUI.SetActive(true);
        skillImage.sprite = skill.skillImage;
        skillType.text = skill.GetSkillTypeString();

        if (skills.Contains(skill))
        {
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    nowExp.text = "현재 EXP : " + Player.S.exp.ToString();
                    buyExp.text = "필요 EXP : " + buyPrice.ToString();
                    UsePP.text = "소비 CP : " + skill.CP;
                    skillName.text = skill.skillName;
                    skillInfo.text = skill.skillLongInfo + "\n\n" + skill.flavorText;
                    break;
                case Options.Language.Eng:
                    nowExp.text = "Current EXP : " + Player.S.exp.ToString();
                    buyExp.text = "Require EXP : " + buyPrice.ToString();
                    UsePP.text = "Use CP : " + skill.CP;
                    skillName.text = skill.skillEName;
                    skillInfo.text = skill.skillE_LongInfo + "\n\n" + skill.flavorEText;
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    nowExp.text = "현재 EXP : " + Player.S.exp.ToString();
                    buyExp.text = "필요 EXP : " + buyPrice2.ToString();
                    UsePP.text = "소비 CP : " + skill.CP;
                    skillName.text = skill.skillName;
                    skillInfo.text = skill.skillLongInfo + "\n\n" + skill.flavorText;
                    break;
                case Options.Language.Eng:
                    nowExp.text = "Current EXP : " + Player.S.exp.ToString();
                    buyExp.text = "Require EXP : " + buyPrice2.ToString();
                    UsePP.text = "Use CP : " + skill.CP;
                    skillName.text = skill.skillEName;
                    skillInfo.text = skill.skillE_LongInfo + "\n\n" + skill.flavorEText;
                    break;
                default:
                    break;
            }
        }



        bool alreadyLearn=false;
        for (int i = 0; i < Player.S.publicSkillinven.Count; i++)
        {
            if (Player.S.publicSkillinven[i].skillName==skill.skillName)
            {
                alreadyLearn = true;
            }
        }
        if (alreadyLearn)
        {
            BuyButton.SetActive(false);
            BuyCaution.SetActive(true);
        }
        else
        {
            BuyCaution.SetActive(false);
            BuyButton.SetActive(true);
            BuyButton.GetComponent<Button>().onClick.RemoveAllListeners();
            BuyButton.GetComponent<Button>().onClick.AddListener(() => LearnSkill(skill));
        }
    }
    public void SetSkillsByProgress()
    {
        skills.Clear();
        switch (Player.S.mainProgress)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                skills=SkillDic.S.NormalSkillByRandom("노말", 3);
                buyPrice = 25;
                plusPrice = 5;
                break;
            case 3:
                skills = SkillDic.S.NormalSkillByRandom("레어", 3);
                buyPrice = 30;
                plusPrice = 6;
                break;
            case 4:
                skills = SkillDic.S.NormalSkillByRandom("레어", 3);
                buyPrice = 80;
                plusPrice = 20;
                break;
            case 5:
                skills = SkillDic.S.NormalSkillByRandom("레어", 3);

                buyPrice = 90;
                plusPrice = 30;
                break;
            case 6:
                skills.AddRange(SkillDic.S.NormalSkillByRandom("레어", 3));
                buyPrice = 120;
                plusPrice = 60;
                skills2.AddRange(SkillDic.S.NormalSkillByRandom("노말", 3));
                buyPrice2 = 50;
                plusPrice2 = 25;
                break;
            case 7:
                skills.AddRange(SkillDic.S.NormalSkillByRandom("레어", 3));
                buyPrice = 120;
                plusPrice = 60;
                skills2.AddRange(SkillDic.S.NormalSkillByRandom("노말", 3));
                buyPrice2 = 50;
                plusPrice2 = 25;
                break;
            case 8:
                skills = SkillDic.S.NormalSkillByRandom("유니크", 3);
                buyPrice = 200;
                plusPrice = 75;
                break;
            case 9:
                skills = SkillDic.S.NormalSkillByRandom("유니크", 3);
                buyPrice = 200;
                plusPrice = 75;
                break;
            case 10:
                skills = SkillDic.S.NormalSkillByRandom("유니크", 3);
                buyPrice = 200;
                plusPrice = 75;
                skills2.AddRange(SkillDic.S.NormalSkillByRandom("레어", 3));
                buyPrice2 = 100;
                plusPrice2 = 50;
                break;
            case 11:
                skills = SkillDic.S.NormalSkillByRandom("유니크", 3);
                buyPrice = 240;
                plusPrice = 100;
                skills2.AddRange(SkillDic.S.NormalSkillByRandom("레어", 3));
                buyPrice2 = 120;
                plusPrice2 = 60;
                break;
            case 12:
                skills = SkillDic.S.NormalSkillByRandom("유니크", 3);
                buyPrice = 280;
                plusPrice = 120;
                skills2.AddRange(SkillDic.S.NormalSkillByRandom("노말", 3));
                buyPrice2 = 80;
                plusPrice2 = 20;
                break;
            case 13:
                skills = SkillDic.S.NormalSkillByRandom("레어", 3);
                buyPrice = 120;
                plusPrice = 60;
                skills2.AddRange(SkillDic.S.NormalSkillByRandom("유니크", 3));
                buyPrice2 = 240;
                plusPrice2 = 100;
                break;
            case 14:
                skills = SkillDic.S.NormalSkillByRandom("레어", 3);
                buyPrice = 120;
                plusPrice = 60;
                skills2.AddRange(SkillDic.S.NormalSkillByRandom("유니크", 3));
                buyPrice2 = 240;
                plusPrice2 = 100;
                break;

            default:
                break;
        }
    }
    public void LoadSkillShop(List<string> _skillNames, List<string> _skillNames2)
    {
        isLoad = true;
        skills.Clear();
        for (int i = 0; i < _skillNames.Count; i++)
        {
            skills.Add(SkillDic.S.D_AllSkillDic[_skillNames[i]]);
        }
        skills2.Clear();
        for (int i = 0; i < _skillNames2.Count; i++)
        {
            skills2.Add(SkillDic.S.D_AllSkillDic[_skillNames2[i]]);
        }
    }

    //전수자
    public void ClassUp()
    {
        if (Player.S.mainProgress==13)
        {
            DialogueManager.S.TextSet(498, 503, null, 15);
        }
        else
        {
            DialogueManager.S.TextSet(281, 286, null, 11);
        }

    }
    public int ReturnNeedEXP()
    {
        int needExp = 0;
        switch (Player.S.mainProgress)
        {
            case 0:
                needExp = 40;
                break;
            case 1:
                needExp = 40;
                break;
            case 2:
                needExp = 40;
                break;
            case 3:
                needExp = 40;
                break;
            case 4:
                needExp = 74;
                break;
            case 5:
                needExp = 74;
                break;
            case 6:
                needExp = 74;
                break;
            case 7:
                needExp = 111;
                break;
            case 8:
                needExp = 111;
                break;
            case 9:
                needExp = 111;
                break;
            case 10:
                needExp = 111;
                break;
            case 11:
                needExp = 145;
                break;
            case 12:
                needExp = 145;
                break;
            case 13:
                needExp = 145;
                break;
            case 14:
                needExp = 145;
                break;
            default:
                needExp = 100;
                break;
        }
        return needExp;
    }
    public void SetTrainingroomUI()
    {
        switch (Options.S.language)
        {
            case Options.Language.Kor:
                T_needExp.text = "필요 EXP : " + ReturnNeedEXP();
                T_curExp.text = "보유 EXP : " + Player.S.exp.ToString();
                break;
            case Options.Language.Eng:
                T_needExp.text = "Require EXP : " + ReturnNeedEXP();
                T_curExp.text = "Current EXP : " + Player.S.exp.ToString();
                break;
            default:
                break;
        }
        switch (Player.S.job)
        {
            case Player.PlayerJob.Inquisitor:
                T_LV.text = "L V : " + Player.S.level;
                T_HP.text = "H P : " + Player.S.hp;
                T_ATK.text = "ATK : " + Player.S.ATK;
                T_DEF.text = "DEF : " + Player.S.DEF;
                T_aLV.text = "L V : " + (Player.S.level+1).ToString();
                T_aHP.text = "HP : "+(Player.S.hp+300).ToString();
                T_aATK.text = "ATK : "+(Player.S.ATK+2).ToString();
                T_aDEF.text = "DEF : " + (Player.S.DEF+2).ToString();
                break;
            case Player.PlayerJob.Paladin:
                T_LV.text = "L V : " + Player.S.level;
                T_HP.text = "H P : " + Player.S.hp;
                T_ATK.text = "ATK : " + Player.S.ATK;
                T_DEF.text = "DEF : " + Player.S.DEF;
                T_aLV.text = "L V : " + (Player.S.level + 1).ToString();
                T_aHP.text = "H P : " + (Player.S.hp + 400).ToString();
                T_aATK.text = "ATK : " + (Player.S.ATK + 1).ToString();
                T_aDEF.text = "DEF : " + (Player.S.DEF + 3).ToString();
                break;
            case Player.PlayerJob.WitchHunter:
                T_LV.text = "L V : " + Player.S.level;
                T_HP.text = "H P : " + Player.S.hp;
                T_ATK.text = "ATK : " + Player.S.ATK;
                T_DEF.text = "DEF : " + Player.S.DEF;
                T_aLV.text = "L V : " + (Player.S.level + 1).ToString();
                T_aHP.text = "H P : " + (Player.S.hp + 250).ToString();
                T_aATK.text = "ATK : " + (Player.S.ATK + 3).ToString();
                T_aDEF.text = "DEF : " + (Player.S.DEF + 1).ToString();
                break;
            case Player.PlayerJob.NightShadow:
                break;
            case Player.PlayerJob.Alchemist:
                break;
            case Player.PlayerJob.none:
                break;
            default:
                break;
        }
    }
}
