using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PublicSkillChange : MonoBehaviour
{
    public static PublicSkillChange S;
    public GameObject SetUIbase;
    public int nowCP;
    public Skill isSelectSkill;
    public List<Skill> SelectedSkills;
    public Text RemainPP;
    public GameObject skillSlotPrefab;
    public Transform T_slots;
    public List<GameObject> invenSlots = new List<GameObject>();

    //InfoUI
    public GameObject SetInfoUI;
    public Image SetSkillImage;
    public Text SetSkillName;
    public Text SetSkillType;
    public Text SetSkillLongInfo;
    public Text SetSkillTP;
    public Text UiName;
    public Transform T_PresetSlots;
    public List<GameObject> PresetSlots = new List<GameObject>();
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
    }
    public void SetPresetUIOn(Skill _skill)
    {
        nowCP = 0;
        SetUIbase.SetActive(true);

        List<Skill> skills = Player.S.publicSkillinven;
        skills.Add(_skill);
        for (int i = 0; i < Player.S.publicSkillinven.Count; i++)
        {
            AddSkill(Player.S.publicSkillinven[i]);
        }

        for (int i = invenSlots.Count - 1; i >= 0; i--)
        {
            Destroy(invenSlots[i]);
        }
        invenSlots.Clear();
        for (int i = 0; i < skills.Count; i++)
        {
            GameObject go = Instantiate(skillSlotPrefab, T_slots);
            invenSlots.Add(go);
            go.GetComponent<SkillPreSetSlot>().skillImage.sprite = skills[i].skillImage;
            go.GetComponent<SkillPreSetSlot>().skillImage.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
            go.GetComponent<SkillPreSetSlot>().skill = skills[i];
            go.GetComponent<SkillPreSetSlot>().PSkillSetInfoButton();
            go.GetComponent<SkillPreSetSlot>().RemoveDelButton();
        }
        switch (Options.S.language)
        {
            case Options.Language.Kor:
                RemainPP.text = "남은 CP: " + (Player.S.CP - nowCP).ToString();
                break;
            case Options.Language.Eng:
                RemainPP.text = "Remain CP: " + (Player.S.CP - nowCP).ToString();
                break;
            default:
                break;
        }


    }
    public void SetPublicSkill()
    {
        Player.S.publicSkillinven = new List<Skill>(SelectedSkills);
        Player.S.useCP = nowCP;
        SelectedSkills.Clear();
        for (int i = PresetSlots.Count - 1; i >= 0; i--)
        {
            Destroy(PresetSlots[i]);
        }
        PresetSlots.Clear();
        SetUIbase.SetActive(false);
    }

    public void DelSkill(Skill _skill)
    {
        for (int i = SelectedSkills.Count-1; i >= 0; i--)
        {
            if (SelectedSkills[i].skillName == _skill.skillName)
            {
                nowCP -= _skill.CP;
                Destroy(PresetSlots[i]);
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        RemainPP.text = "남은 CP: " + (Player.S.CP - nowCP).ToString();
                        break;
                    case Options.Language.Eng:
                        RemainPP.text = "Remain CP: " + (Player.S.CP - nowCP).ToString();
                        break;
                    default:
                        break;
                }

                SelectedSkills.RemoveAt(i);
                PresetSlots.RemoveAt(i);
                return;
            }
        }
    }
    public void OpenSetSkillInfoUI(Skill _skill)
    {
        if (!SetInfoUI.activeInHierarchy)
        {
            SetInfoUI.SetActive(true);
        }
        isSelectSkill = _skill;
        SetSkillImage.sprite = _skill.skillImage;
        SetSkillType.text = _skill.GetSkillTypeString();


        switch (Options.S.language)
        {
            case Options.Language.Kor:
                SetSkillName.text = _skill.skillName;
                SetSkillLongInfo.text = _skill.skillLongInfo;
                if (!_skill.isPublicSkill)
                {
                    SetSkillTP.text = "소비 PP :" + _skill.pp.ToString();
                }
                else
                {
                    SetSkillTP.text = "소비 CP :" + _skill.CP.ToString();
                }
                break;
            case Options.Language.Eng:
                SetSkillName.text = _skill.skillEName;
                SetSkillLongInfo.text = _skill.skillE_LongInfo;
                if (!_skill.isPublicSkill)
                {
                    SetSkillTP.text = "Use PP :" + _skill.pp.ToString();
                }
                else
                {
                    SetSkillTP.text = "Use CP :" + _skill.CP.ToString();
                }
                break;
            default:
                break;
        }

    }

    public void AddSkill()
    {
        for (int i = 0; i < SelectedSkills.Count; i++)
        {
            if (SelectedSkills[i].skillName == isSelectSkill.skillName)
            {
                return;
            }
        }
        if (nowCP + isSelectSkill.CP <= Player.S.CP)
        {
            SelectedSkills.Add(isSelectSkill);
            GameObject go = Instantiate(skillSlotPrefab, T_PresetSlots);
            PresetSlots.Add(go);
            go.GetComponent<SkillPreSetSlot>().skillImage.sprite = isSelectSkill.skillImage;
            go.GetComponent<SkillPreSetSlot>().skillImage.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
            go.GetComponent<SkillPreSetSlot>().skill = isSelectSkill;
            go.GetComponent<SkillPreSetSlot>().PSkillDelButton();
            go.GetComponent<SkillPreSetSlot>().RemoveDelButton();
            nowCP += isSelectSkill.CP;
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    RemainPP.text = "남은 CP: " + (Player.S.CP - nowCP).ToString();
                    break;
                case Options.Language.Eng:
                    RemainPP.text = "Remain CP: " + (Player.S.CP - nowCP).ToString();
                    break;
                default:
                    break;
            }
        }



    }
    public void AddSkill(Skill _skill)
    {
        for (int i = 0; i < SelectedSkills.Count; i++)
        {
            if (SelectedSkills[i].skillName == _skill.skillName)
            {
                return;
            }
        }
        if (nowCP + _skill.CP <= Player.S.CP)
        {
            SelectedSkills.Add(_skill);
            GameObject go = Instantiate(skillSlotPrefab, T_PresetSlots);
            PresetSlots.Add(go);
            go.GetComponent<SkillPreSetSlot>().skillImage.sprite = _skill.skillImage;
            go.GetComponent<SkillPreSetSlot>().skillImage.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
            go.GetComponent<SkillPreSetSlot>().skill = _skill;
            go.GetComponent<SkillPreSetSlot>().PSkillDelButton();
            go.GetComponent<SkillPreSetSlot>().RemoveDelButton();
            nowCP += _skill.CP;
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    RemainPP.text = "남은 CP: " + (Player.S.CP - nowCP).ToString();
                    break;
                case Options.Language.Eng:
                    RemainPP.text = "Remain CP: " + (Player.S.CP - nowCP).ToString();
                    break;
                default:
                    break;
            }
        }



    }

}
