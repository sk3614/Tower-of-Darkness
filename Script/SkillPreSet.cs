using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillPreSet : MonoBehaviour
{
    public GameObject preSetSlotPreFab;
    public Transform T_slots;
    public Skill[] skills;
    public GameObject SetButton;
    public GameObject EquipButton;


    public List<GameObject> slots =new List<GameObject>();
    
    public GameObject isSelectUI;
    public void SetPreSet(Skill[] _skills)
    {
        skills = _skills;

        for (int i = slots.Count-1; i >=0 ; i--)
        {
            Destroy(slots[i]);
        }
        slots.Clear();
        for (int i = 0; i < skills.Length; i++)
        {
            GameObject go = Instantiate(preSetSlotPreFab, T_slots);
            slots.Add(go);
            go.GetComponent<SkillPreSetSlot>().skillImage.sprite= skills[i].skillImage;
            go.GetComponent<SkillPreSetSlot>().skill = skills[i];
            go.GetComponent<SkillPreSetSlot>().SetInfoButton();
        }

    }
    public void SkillSetPreSet(Skill _skill)
    {
        GameObject go = Instantiate(preSetSlotPreFab, T_slots);
        go.GetComponent<SkillPreSetSlot>().skillImage.sprite = _skill.skillImage;
        go.GetComponent<SkillPreSetSlot>().skill = _skill;

    }
}
