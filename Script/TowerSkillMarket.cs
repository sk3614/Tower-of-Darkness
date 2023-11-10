using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TowerSkillMarket : MonoBehaviour
{
    public GameObject InfoUI;
    public Image SkillImage;
    public Text SkillName;
    public Text SkillType;
    public Text SkillLongInfo;
    public Text SkillTP;
    public List<Skill> Setskills;
    public Image[] images;



    public void MarketOn(List<Skill> skills)
    {
        Setskills = skills;
        for (int i = 0; i < images.Length; i++)
        {
            images[i].sprite = Setskills[i].skillImage;
        }
    }

    public void OpenInfoUI(int _num)
    {
        if (!InfoUI.activeInHierarchy)
        {
            InfoUI.SetActive(true);
        }
        switch (Options.S.language)
        {
            case Options.Language.Kor:
                SkillImage.sprite = Setskills[_num].skillImage;
                SkillName.text = Setskills[_num].skillName;
                SkillType.text = Setskills[_num].GetSkillTypeString();
                SkillLongInfo.text = Setskills[_num].skillLongInfo + "\n\n" + Setskills[_num].flavorText;

                if (!Setskills[_num].isPublicSkill)
                {
                    SkillTP.text = "소비 PP :" + Setskills[_num].pp.ToString();
                }
                else
                {
                    SkillTP.text = "소비 CP :" + Setskills[_num].CP.ToString();
                }
                break;
            case Options.Language.Eng:
                SkillImage.sprite = Setskills[_num].skillImage;
                SkillName.text = Setskills[_num].skillEName;
                SkillType.text = Setskills[_num].GetSkillTypeString();
                SkillLongInfo.text = Setskills[_num].skillE_LongInfo + "\n\n" + Setskills[_num].flavorEText;

                if (!Setskills[_num].isPublicSkill)
                {
                    SkillTP.text = "Use PP :" + Setskills[_num].pp.ToString();
                }
                else
                {
                    SkillTP.text = "Use CP :" + Setskills[_num].CP.ToString();
                }
                break;
            default:
                break;
        }

    }

    public void Select(int _num)
    {
        SkillUI.S.GetSkill(Setskills[_num]);
        OneTimeShop.S.ShopUIClose(4);
        OneTimeShop.S.ShopUIClose(16);
        OneTimeShop.S.ShopUIClose(25);
        if (TowerMap.S.curFloorNum==12)
        {
            TowerVariable.S.floor12Shop = true;
        }
        if (TowerMap.S.curFloorNum == 9)
        {
            TowerVariable.S.floor9Shop = true;
        }
        if (TowerMap.S.curFloorNum == 5)
        {
            TowerVariable.S.floor5shop = true;
        }
    }
}
