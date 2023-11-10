using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TrainingSkillSlot : MonoBehaviour
{
    public Skill skill;
    public Image skillImage;
    public int Price;
    public int plusPrice;

    public void SetSlot(TrainingRoom trainingRoom)
    {
        skillImage.sprite = skill.skillImage;
        gameObject.GetComponent<Button>().onClick.AddListener(() => trainingRoom.SetInfoUI(skill));
    }
}
