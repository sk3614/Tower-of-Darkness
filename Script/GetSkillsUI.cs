using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetSkillsUI : MonoBehaviour
{
    public static GetSkillsUI S;

    public GameObject uiBase;
    public GameObject slotPrefab;
    public Transform T_slot;
    [SerializeField]
    private List<GameObject> slots=new List<GameObject>();

    public Text InfoText;//설명문구 강화인지 획득인지.
    public Text SkillLongInfo;
    private List<List<string>> uiStack=new List<List<string>>();
    private List<bool> isUpgrade=new List<bool>();

    public void Awake()
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

    private void LateUpdate()
    {
        if (!uiBase.gameObject.activeInHierarchy && uiStack.Count > 0)
        {
            if (Player.S.playerLocation == Player.PlayerLocation.Tower)
            {
                if (!TowerMap.S.MoveLock)
                {
                    OpenUI(uiStack[0],isUpgrade[0]);
                    uiStack.RemoveAt(0);
                    isUpgrade.RemoveAt(0);
                }
            }
            else
            {
                OpenUI(uiStack[0], isUpgrade[0]);
                uiStack.RemoveAt(0);
                isUpgrade.RemoveAt(0);
            }

        }
    }

    public void AddStack(List<string> _skillnames, bool _isUpgrade)
    {
        List<string> skills = new List<string>(_skillnames);
        uiStack.Add(skills);
        isUpgrade.Add(_isUpgrade);
    }

    private void OpenUI(List<string> _skillnames,bool _isUpgrade)
    {
        uiBase.SetActive(true);
        for (int i = 0; i < slots.Count; i++)
        {
            Destroy(slots[i]);
            
        }
        slots.Clear();
        SkillLongInfo.text = "";
        switch (Options.S.language)
        {
            case Options.Language.Kor:
                if (_isUpgrade)
                {
                    InfoText.text = "아래 스킬들이 강화되었습니다.";
                }
                else
                {
                    InfoText.text = "아래 스킬들을 획득했습니다.";
                }

                break;
            case Options.Language.Eng:
                if (_isUpgrade)
                {
                    InfoText.text = "Upgrade skills";
                }
                else
                {
                    InfoText.text = "Get skills";
                }
                break;
            default:
                break;
        }//강화 획득 대사.

        for (int i = 0; i < _skillnames.Count; i++)
        {
            GameObject go = Instantiate(slotPrefab, T_slot);
            slots.Add(go);
            GetSkillUISlot slot = go.GetComponent<GetSkillUISlot>();
            slot.skill = SkillDic.S.D_AllSkillDic[_skillnames[i]];
            slot.skillImage.sprite = slot.skill.skillImage;
            int num = i;
            slot.skillImage.GetComponent<Button>().onClick.AddListener(() => Select(num));

            if (_isUpgrade)
            {
                slot.upgradeImage.gameObject.SetActive(true);
            }
            else
            {
                slot.upgradeImage.gameObject.SetActive(false);
            }


        }//스킬프리팹 생성

    }

    public void Select(int _num)
    {
        Skill skill = slots[_num].GetComponent<GetSkillUISlot>().skill;
        switch (Options.S.language)
        {
            case Options.Language.Kor:
                SkillLongInfo.text = skill.skillName+ " [" + skill.GetSkillTypeString() + "]" + "\n\n" + skill.skillLongInfo;
                break;
            case Options.Language.Eng:
                SkillLongInfo.text = skill.skillEName + " [" + skill.GetSkillTypeString() + "]" + "\n\n" + skill.skillE_LongInfo;
                break;
            default:
                break;
        }

    }
}

