using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestUI : MonoBehaviour
{
    public GameObject UIbase;

    public GameObject P_QuestSlot;
    public GameObject P_QuestObjectTexts;
    public Transform T_QuestSlot;
    public Transform T_QuestObjectTexts;
    public List<GameObject> slots = new List<GameObject>();
    public List<GameObject> texts = new List<GameObject>();
    public Sprite[] CheckBoxImage;
    //Info UI
    public GameObject InfoUI;
    public Text questName;
    public Text questInfo;
    public Text RewardName;
    public void QuestInfoUIOn(QuestData questData)
    {
        InfoUI.SetActive(true);
        switch (Options.S.language)
        {
            case Options.Language.Kor:
                questName.text = questData.questName;
                questInfo.text = questData.questInfo;
                break;
            case Options.Language.Eng:
                questName.text = questData.questEName;
                questInfo.text = questData.questEInfo;
                break;
            default:
                break;
        }

        for (int i = 0; i < texts.Count; i++)
        {
            Destroy(texts[i].gameObject);
        }
        texts.Clear();
        for (int i = 0; i < questData.questObjects.Length; i++)
        {
            GameObject go= Instantiate(P_QuestObjectTexts, T_QuestObjectTexts);
            QuestTextSlot textslot = go.GetComponent<QuestTextSlot>();
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    textslot.curNum.text = questData.questObjects[i].curNum.ToString();
                    textslot.requireNum.text = questData.questObjects[i].requireNum.ToString();
                    textslot.objectText.text = questData.questObjects[i].requireName.ToString();
                    break;
                case Options.Language.Eng:
                    textslot.curNum.text = questData.questObjects[i].curNum.ToString();
                    textslot.requireNum.text = questData.questObjects[i].requireNum.ToString();
                    textslot.objectText.text = questData.questObjects[i].requireEName.ToString();
                    break;
                default:
                    break;
            }

            texts.Add(go);
        }
        switch (Options.S.language)
        {
            case Options.Language.Kor:
                RewardName.text = "보상 : " + questData.rewardName;
                break;
            case Options.Language.Eng:
                RewardName.text = "Reward : " + questData.rewardEName;
                break;
            default:
                break;
        }

    }
    public void QuestInfoUIOff()
    {
        InfoUI.SetActive(false);
    }
    public void UIOn()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Destroy(slots[i].gameObject);
        }
        slots.Clear();

        for (int i = 0; i < QuestManager.S.curQuestData.Count; i++)
        {
            GameObject go = Instantiate(P_QuestSlot, T_QuestSlot);
            QuestSlot slot = go.GetComponent<QuestSlot>();
            slot.questData = QuestManager.S.curQuestData[i];
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    slot.questTitle.text = slot.questData.questName;
                    break;
                case Options.Language.Eng:
                    slot.questTitle.text = slot.questData.questEName;
                    break;
                default:
                    break;
            }

            slots.Add(go);
            go.GetComponent<Button>().onClick.AddListener(() => QuestInfoUIOn(slot.questData));
            switch (slot.questData.questState)
            {
                case QuestData.QuestState.Accept:
                    slot.CheckImage.sprite = CheckBoxImage[0];
                    break;
                case QuestData.QuestState.Clear:
                    slot.CheckImage.sprite = CheckBoxImage[1];
                    break;
                case QuestData.QuestState.Fail:
                    break;
                default:
                    slot.CheckImage.sprite = null;
                    break;
            }
            slot.SetCheckImage();
        }
    }
    public void UIOff()
    {
        QuestInfoUIOff();
    }
}
