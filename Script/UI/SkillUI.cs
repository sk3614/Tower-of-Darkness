using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillUI : MonoBehaviour
{
    public static SkillUI S;

    public List<SkillPreSet> PreSets=new List<SkillPreSet>();

    public GameObject P_preset;
    public Transform T_preset;

    //SkillinfoUI
    public GameObject InfoUI;
    public Image SkillImage;
    public Image skillUpgradeImage;
    public Text SkillName;
    public Text SkillType;
    public Text SkillLongInfo;
    public Text SkillTP;
    public SkillPreSetSlot classPassiveSlot;

    public List<GameObject> skillSetButton=new List<GameObject>();
    public List<GameObject> skillEquipButton = new List<GameObject>();

    //SetPresetUI
    public GameObject SetUIbase;
    public Skill isSelectSkill;
    public List<Skill> SelectedSkills;
    public int nowPresetNum;
    public int nowPP;
    public Text RemainPP;
    public GameObject skillSlotPrefab;
    public Transform T_slots;
    public List<GameObject> invenSlots = new List<GameObject>();

    //프리셋설정 인포ui
    public GameObject SetInfoUI;
    public Image SetSkillImage;
    public Text SetSkillName;
    public Text SetSkillType;
    public Text SetSkillLongInfo;
    public Text SetSkillTP;
    public Text T_skillUI;
    public Text SeminaryName;
    public Text PylonName;
    public GameObject SetUIExitButton;
    public GameObject backUI;
    public Transform T_PresetSlots;
    public List<GameObject> PresetSlots=new List<GameObject>();

    //공용스킬 ui
    public Transform T_publicskills;
    private List<GameObject> publicSkillSlots = new List<GameObject>();
    public Text cpText;
    public GameObject DelPublicSkillUI;
    public Skill delSkill;

    //직업스킬 ui
    public Transform T_Classskills;
    private List<GameObject> classSkillSlots = new List<GameObject>();

    public bool isSet;

    private void Awake()
    {
        if (S == null)
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
    public void SlotListRenewal()
    {
    }
    public void UseTPSum()
    {

    }
    public bool IsSkillUnique(Skill _skill)
    {
        //for (int i = 0; i < skillSlots.Count; i++)
        //{
        //    if (_skill.skillName == skillSlots[i].skillName.text)
        //    {
        //        return true;
        //    }
        //}
        return false;
    }
    public void GetSkill(Skill _skill)
    {
        //for (int i = 0; i < skillSlots.Count; i++)
        //{
        //    if (_skill.skillName == skillSlots[i].skillName.text)
        //    {
        //        Debug.Log("같은 이름의 스킬이 이미 있습니다");
        //        return;
        //    }
        //}
        //GameObject go;
        //go = Instantiate(go);
        //go.transform.SetParent(skillInven.transform);
        //go.GetComponent<SkillSlot>().AddSkill(_skill);
        //SlotListRenewal();
        if (_skill == null)
        {
            return;
        }
        if (_skill.isPublicSkill)
        {
            if (_skill.CP+Player.S.useCP>Player.S.CP)
            {
                PublicSkillChange.S.SetPresetUIOn(_skill);
            }
            Player.S.publicSkillinven.Add(_skill);
            Player.S.useCP += _skill.CP;
        }
        else
        {
            Player.S.classSkillinven.Add(_skill);

        }

    }
    public void OpenSkillUI()
    {
        if (Player.S.ClassPasiveSkills.Count!=0)
        {
            classPassiveSlot.skill = Player.S.ClassPasiveSkills[0];
            classPassiveSlot.skillImage.sprite = Player.S.ClassPasiveSkills[0].skillImage;
            classPassiveSlot.SetInfoButton();

        }

        for (int i = 0; i < PreSets.Count; i++)
        {
            Destroy(PreSets[i].gameObject);
        }
        PreSets.Clear();
        skillSetButton.Clear();
        skillEquipButton.Clear();


        for (int i = 0; i < Player.S.TP; i++)
        {
            int presetNum=i;
            GameObject go = Instantiate(P_preset, T_preset);
            SkillPreSet skillPreSet = go.GetComponent<SkillPreSet>();
            PreSets.Add(skillPreSet);
            skillPreSet.isSelectUI.GetComponent<Text>().text = (i+1).ToString();
            skillSetButton.Add(skillPreSet.SetButton);

            skillPreSet.SetButton.GetComponent<Button>().onClick.AddListener(() => SetPresetUIOn(presetNum));
            skillEquipButton.Add(skillPreSet.EquipButton);
            skillPreSet.EquipButton.GetComponent<Button>().onClick.AddListener(() => SelecPreset(presetNum));

            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    skillPreSet.SetButton.GetComponentInChildren<Text>().text = "세팅";
                    skillPreSet.EquipButton.GetComponentInChildren<Text>().text = "장착";
                    break;
                case Options.Language.Eng:
                    skillPreSet.SetButton.GetComponentInChildren<Text>().text = "Set";
                    skillPreSet.EquipButton.GetComponentInChildren<Text>().text = "Equip";
                    break;
                default:
                    break;
            }

        }

        for (int i = 0; i < PreSets.Count; i++)
        {
            PreSets[i].isSelectUI.GetComponent<Text>().color = Color.white;
        }
        if (Player.S.NowPresetNum>=0)
        {
            int num = Player.S.NowPresetNum;
            PreSets[num].isSelectUI.GetComponent<Text>().color = Color.yellow;
        }
        
        for (int i = 0; i < Player.S.PreSetList.Count; i++)
        {
            PreSets[i].SetPreSet(Player.S.PreSetList[i].ToArray());
        }

        if (!isSet)
        {
            T_skillUI.gameObject.SetActive(true);
            PylonName.gameObject.SetActive(false);
            SeminaryName.gameObject.SetActive(false);

            for (int i = 0; i < PreSets.Count; i++)
            {
                skillSetButton[i].SetActive(false);
                skillEquipButton[i].SetActive(true);
            }
            SetUIExitButton.SetActive(false);
            backUI.SetActive(true);
        }
        else
        {


            if (Player.S.playerLocation==Player.PlayerLocation.Tower)
            {
                PylonName.gameObject.SetActive(true);
                SeminaryName.gameObject.SetActive(false);
            }
            else
            {
                SeminaryName.gameObject.SetActive(true);
                PylonName.gameObject.SetActive(false);
            }
            T_skillUI.gameObject.SetActive(false);
            for (int i = 0; i < PreSets.Count; i++)
            {
                skillSetButton[i].SetActive(true);
                skillEquipButton[i].SetActive(false);
            }
            SetUIExitButton.SetActive(true);
            backUI.SetActive(false);
        }

        //공용스킬
        for (int i = publicSkillSlots.Count - 1; i >= 0; i--)
        {
            Destroy(publicSkillSlots[i]);
        }
        publicSkillSlots.Clear();
        for (int i = 0; i < Player.S.publicSkillinven.Count; i++)
        {
            GameObject go = Instantiate(skillSlotPrefab, T_publicskills);
            publicSkillSlots.Add(go);
            go.GetComponent<SkillPreSetSlot>().skillImage.sprite = Player.S.publicSkillinven[i].skillImage;
            go.GetComponent<SkillPreSetSlot>().skillImage.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
            go.GetComponent<SkillPreSetSlot>().skill = Player.S.publicSkillinven[i];
            go.GetComponent<SkillPreSetSlot>().SetInfoButton();
        }
        cpText.text = "CP : " + Player.S.useCP + "/" + Player.S.CP;

        //클래스 스킬
        for (int i = classSkillSlots.Count - 1; i >= 0; i--)
        {
            Destroy(classSkillSlots[i]);
        }
        classSkillSlots.Clear();

        for (int i = 0; i < Player.S.classSkillinven.Count; i++)
        {
            GameObject go = Instantiate(skillSlotPrefab, T_Classskills);
            classSkillSlots.Add(go);
            go.GetComponent<SkillPreSetSlot>().skillImage.sprite = Player.S.classSkillinven[i].skillImage;
            go.GetComponent<SkillPreSetSlot>().skillImage.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
            go.GetComponent<SkillPreSetSlot>().skill = Player.S.classSkillinven[i];
            go.GetComponent<SkillPreSetSlot>().SetInfoButton();
        }



    }

    public void OpenSkillInfoUI(Skill _skill)
    {



        if (!InfoUI.activeInHierarchy)
        {
            InfoUI.SetActive(true);
        }
        SkillImage.sprite = _skill.skillImage;
        if (_skill.isUpgrade)
        {
            skillUpgradeImage.gameObject.SetActive(true);
        }
        else
        {
            skillUpgradeImage.gameObject.SetActive(false);
        }

        switch (Options.S.language)
        {
            case Options.Language.Kor:
                SkillName.text = _skill.skillName;
                SkillType.text = _skill.GetSkillTypeString();
                SkillLongInfo.text = _skill.skillLongInfo +"\n\n"+_skill.flavorText;

                if (!_skill.isPublicSkill)
                {
                    SkillTP.text = "소비 PP :" + _skill.pp.ToString();
                }
                else
                {
                    SkillTP.text = "소비 CP :" + _skill.CP.ToString();
                }

                break;
            case Options.Language.Eng:
                SkillName.text = _skill.skillEName;
                SkillType.text = _skill.GetSkillTypeString();
                SkillLongInfo.text = _skill.skillE_LongInfo + "\n\n" + _skill.flavorEText;

                if (!_skill.isPublicSkill)
                {
                    SkillTP.text = "Requir PP :" + _skill.pp.ToString();
                }
                else
                {
                    SkillTP.text = "Requir CP :" + _skill.CP.ToString();
                }

                break;
            default:
                break;
        }


    }
    public void CloseSkillUI()
    {
        InfoUI.SetActive(false);
    }
    public void SelecPreset(int _num)
    {
        Player.S.skillList.Clear();
        for (int i = 0; i < PreSets[_num].skills.Length; i++)
        {
            Player.S.skillList.Add(PreSets[_num].skills[i]);
        }
        Player.S.NowPresetNum = _num;
        OpenSkillUI();
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
                SetSkillType.text = _skill.GetSkillTypeString();
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
                SetSkillType.text = _skill.GetSkillTypeString();
                SetSkillLongInfo.text = _skill.skillE_LongInfo;

                if (!_skill.isPublicSkill)
                {
                    SetSkillTP.text = "Requir PP :" + _skill.pp.ToString();
                }
                else
                {
                    SetSkillTP.text = "Requir CP :" + _skill.CP.ToString();
                }

                break;
            default:
                break;
        }
    }
    public void CloseSetSkillUI()
    {
        SetInfoUI.SetActive(false);
        SetUIbase.SetActive(false);
        SelectedSkills.Clear();
        RefreshSelectedSkill();
        if (Player.S.playerLocation==Player.PlayerLocation.Tower)
        {
            TowerMap.S.MoveLock = false;
        }
        

    }
    public void SetPresetUIOn(int _num)
    {

        SetUIbase.SetActive(true);
        nowPresetNum = _num;
        nowPP = 0;
        List<Skill> skills = Player.S.classSkillinven;
        for (int i = invenSlots.Count - 1; i >= 0; i--)
        {
            Destroy(invenSlots[i]);
        }
        invenSlots.Clear();
        for (int i = 0; i < PreSets[_num].skills.Length; i++)
        {
            AddSkill(PreSets[_num].skills[i]);
        }
        for (int i = 0; i < skills.Count; i++)
        {
            GameObject go = Instantiate(skillSlotPrefab, T_slots);
            invenSlots.Add(go);
            go.GetComponent<SkillPreSetSlot>().skillImage.sprite = skills[i].skillImage;
            go.GetComponent<SkillPreSetSlot>().skillImage.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
            go.GetComponent<SkillPreSetSlot>().skill = skills[i];
            go.GetComponent<SkillPreSetSlot>().SkillSetInfoButton();
        }
        switch (Options.S.language)
        {
            case Options.Language.Kor:
                RemainPP.text = "남은 PP: " + (Player.S.PP - nowPP).ToString();
                break;
            case Options.Language.Eng:
                RemainPP.text = "Remain PP: " + (Player.S.PP - nowPP).ToString();
                break;
            default:
                break;
        }



    }
    public void AddSkill()
    {
        for (int i = 0; i < SelectedSkills.Count; i++)
        {
            if (SelectedSkills[i].skillName==isSelectSkill.skillName)
            {
                return;
            }
        }
        if (nowPP+isSelectSkill.pp<=Player.S.PP)
        {
            SelectedSkills.Add(isSelectSkill);
            GameObject go = Instantiate(skillSlotPrefab, T_PresetSlots);
            PresetSlots.Add(go);
            go.GetComponent<SkillPreSetSlot>().skillImage.sprite = isSelectSkill.skillImage;
            go.GetComponent<SkillPreSetSlot>().skillImage.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
            go.GetComponent<SkillPreSetSlot>().skill = isSelectSkill;
            go.GetComponent<SkillPreSetSlot>().SkillDelButton();
            nowPP += isSelectSkill.pp;
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    RemainPP.text = "남은 PP: " + (Player.S.PP - nowPP).ToString();
                    break;
                case Options.Language.Eng:
                    RemainPP.text = "Remain PP: " + (Player.S.PP - nowPP).ToString();
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
        if (nowPP + _skill.pp <= Player.S.PP)
        {
            SelectedSkills.Add(_skill);
            GameObject go = Instantiate(skillSlotPrefab, T_PresetSlots);
            PresetSlots.Add(go);
            go.GetComponent<SkillPreSetSlot>().skillImage.sprite = _skill.skillImage;
            go.GetComponent<SkillPreSetSlot>().skillImage.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
            go.GetComponent<SkillPreSetSlot>().skill = _skill;
            go.GetComponent<SkillPreSetSlot>().SkillDelButton();
            nowPP += _skill.pp;
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    RemainPP.text = "남은 PP: " + (Player.S.PP - nowPP).ToString();
                    break;
                case Options.Language.Eng:
                    RemainPP.text = "Remain PP: " + (Player.S.PP - nowPP).ToString();
                    break;
                default:
                    break;
            }
        }



    }
    public void DelSkill(Skill _skill)
    {
        for (int i = 0; i < SelectedSkills.Count; i++)
        {
            if (SelectedSkills[i].skillName == _skill.skillName)
            {
                nowPP -= _skill.pp;
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        RemainPP.text = "남은 PP: " + (Player.S.PP - nowPP).ToString();
                        break;
                    case Options.Language.Eng:
                        RemainPP.text = "Remain PP: " + (Player.S.PP - nowPP).ToString();
                        break;
                    default:
                        break;
                }
                Destroy(PresetSlots[i]);
                PresetSlots.RemoveAt(i);
                SelectedSkills.RemoveAt(i);
                return;
            }
        }
    }
    public void RefreshSelectedSkill()
    {
        for (int i = PresetSlots.Count-1; i >=0; i--)
        {
            Destroy(PresetSlots[i]);
        }
        PresetSlots.Clear();
        for (int i = 0; i < SelectedSkills.Count; i++)
        {
            GameObject go = Instantiate(skillSlotPrefab, T_PresetSlots);
            PresetSlots.Add(go);
            go.GetComponent<SkillPreSetSlot>().skillImage.sprite = SelectedSkills[i].skillImage;
            go.GetComponent<SkillPreSetSlot>().skillImage.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
            go.GetComponent<SkillPreSetSlot>().skill = SelectedSkills[i];
            go.GetComponent<SkillPreSetSlot>().SkillSetInfoButton();
        }
    }

    public void SetPreset()
    {
        Debug.Log(Player.S.PreSetList.Count);
        Player.S.PreSetList[nowPresetNum] = new List<Skill>(SelectedSkills) ;
        OpenSkillUI();
        CloseSetSkillUI();
        SelecPreset(nowPresetNum);
    }

    public void DelPublicSkillUIOn(Skill _skill)
    {
        DelPublicSkillUI.SetActive(true);
        delSkill = _skill;
    }
    public void DelPublicSkill()
    {
        if (delSkill==null)
        {
            return;
        }
        
        for (int i = 0; i < Player.S.publicSkillinven.Count; i++)
        {
            if (delSkill == Player.S.publicSkillinven[i])
            {
                Player.S.publicSkillinven.RemoveAt(i);
            }
        }
        Player.S.useCP -= delSkill.CP;
        delSkill = null;
        DelPublicSkillUI.SetActive(false);
        OpenSkillUI();

    }
    public void DelPSkillUIOff()
    {
        DelPublicSkillUI.SetActive(false);
        delSkill = null;
    }
    
}
