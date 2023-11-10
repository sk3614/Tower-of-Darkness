using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MonsterBookSkillSlot : MonoBehaviour
{
    public Skill skill;


    public Text SkillName;
    public GameObject InfoBg;
    public Text InfoText;
    public string S_Name;
    public string S_info;
    public BookInfoUI bookInfoUI;


    private void Update()
    {
        //if (InfoBg.activeInHierarchy)
        //{
        //    InfoBg.GetComponent<RectTransform>().sizeDelta = new Vector2(InfoText.gameObject.GetComponent<RectTransform>().sizeDelta.x + 10, InfoText.gameObject.GetComponent<RectTransform>().sizeDelta.y + 5);
        //}
    }

    // Start is called before the first frame update
   
    //public void SetSlot(Skill _skill)
    //{
    //    skill = _skill;
    //    switch (Options.S.language)
    //    {
    //        case Options.Language.Kor:
    //            SkillName.text = skill.skillName;
    //            InfoText.text = "<size=45>" + skill.skillName+ "</size>" + "\n" + skill.skillLongInfo;
    //            break;
    //        case Options.Language.Eng:
    //            SkillName.text = skill.skillEName;
    //            InfoText.text = "<size=45>" + skill.skillEName + "</size>" + "\n" + skill.skillE_LongInfo;
    //            break;
    //        default:
    //            break;
    //    }



    //}
    public void InfoUISetSlot(Skill _skill)
    {
        skill = _skill;
        switch (Options.S.language)
        {
            case Options.Language.Kor:
                SkillName.text = skill.skillName + " [" + _skill.GetSkillTypeString() + "]";
                S_info =skill.skillLongInfo;
                break;
            case Options.Language.Eng:
                SkillName.text = skill.skillEName+" ["+ _skill.GetSkillTypeString() + "]" ;
                S_Name = skill.skillEName;
                S_info = skill.skillE_LongInfo;
                break;
            default:
                break;
        }



    }
    public void InfoOn()
    {
        bookInfoUI.skillInfo.text = S_info;
    }

    //public void InfoOn()
    //{
    //    SetSlot(skill);
    //    InfoBg.GetComponent<RectTransform>().sizeDelta = new Vector2(InfoText.gameObject.GetComponent<RectTransform>().sizeDelta.x + 10, InfoText.gameObject.GetComponent<RectTransform>().sizeDelta.y + 5);



    //    InfoBg.SetActive(true);
    //    InfoBg.transform.SetParent(MonsterBookUI.S.T_Skillslots);
    //    MonsterBookUI.S.skillslots.Add(InfoBg.gameObject);
    //}
}
