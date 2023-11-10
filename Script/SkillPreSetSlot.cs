using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SkillPreSetSlot : MonoBehaviour,IPointerDownHandler
{
    public Image skillImage;
    public Image UpgradeSkillImage;
    public Skill skill;
    public GameObject DelButton;
    public bool removedelbutton;
    public void Start()
    {
        if (skill==null)
        {
            return;
        }

        if (skill.isPublicSkill&&!removedelbutton)
        {
            DelButton.SetActive(true);
            DelPublicSkill();
        }
        else
        {
            DelButton.SetActive(false);
        }
        if (skill.isUpgrade)
        {
            UpgradeSkillImage.gameObject.SetActive(true);
        }
        else
        {
            UpgradeSkillImage.gameObject.SetActive(false);
        }
    }

    public void Update()
    {


    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(1) && SkillUI.S.SetUIbase.activeInHierarchy)
        {
            SkillUI.S.AddSkill(skill);
        }

    }
    public void SetInfoButton()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => SkillUI.S.OpenSkillInfoUI(skill));
    }
    public void SkillSetInfoButton()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => SkillUI.S.OpenSetSkillInfoUI(skill));
    }
    public void SkillDelButton()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => SkillUI.S.DelSkill(skill));
    }
    public void PSkillSetInfoButton()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => PublicSkillChange.S.OpenSetSkillInfoUI(skill));

    }
    public void PSkillDelButton()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => PublicSkillChange.S.DelSkill(skill));
    }

    public void DelPublicSkill()
    {
        DelButton.GetComponent<Button>().onClick.AddListener(() => SkillUI.S.DelPublicSkillUIOn(skill));
    }
    public void RemoveDelButton()
    {
        removedelbutton = true;
    }
}
