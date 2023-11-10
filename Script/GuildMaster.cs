using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GuildMaster : MonoBehaviour
{
    private QuestManager questManager;

    public GameObject UIbase;

    public GameObject P_QuestSlot;
    public Transform T_QuestSlot;
    public List<GameObject> questSlots;

    public GameObject P_QuestObjectTexts;
    public Transform T_QuestObjectTexts;
    public List<GameObject> texts;

    public GameObject questListUI;
    public GameObject questInfoUI;
    public Sprite[] CheckBoxImage;

    //InfoUI
    public Text title;
    public Text infoText;
    public Button acceptButton;
    public Text RewardText;
    public Text ObjectText;
   


    public void Start()
    {
        questManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
    }

    public void UIOn()
    {
        UIbase.SetActive(true);
        questListUI.SetActive(true);
        for (int i = 0; i < questManager.curQuestData.Count; i++)
        {
            if (questManager.curQuestData[i].towerProgress <= Player.S.mainProgress)//타워진행도 체크
            {
                 CreateQuestSlot(questManager.curQuestData[i]);
            }
        }

    }
    public void UIOff()
    {
        for (int i = 0; i < questSlots.Count; i++)
        {
            Destroy(questSlots[i].gameObject);
        }
        questSlots.Clear();//퀘스트 슬롯 초기화
        questInfoUI.SetActive(false);

    }


    public void QuestInfoUIOn(QuestData questData)
    {
        questListUI.SetActive(false);
        questInfoUI.SetActive(true);
        switch (Options.S.language)
        {
            case Options.Language.Kor:
                title.text = questData.questName;
                infoText.text = questData.questInfo;
                RewardText.text = "보상 : " + questData.rewardName;
                break;
            case Options.Language.Eng:
                title.text = questData.questEName;
                infoText.text = questData.questEInfo;
                RewardText.text = "Reward : " + questData.rewardEName;
                break;
            default:
                break;
        }

        

        for (int i = texts.Count-1; i >= 0; i--)
        {
            Destroy(texts[i]);
        }
        texts.Clear();
        for (int i = 0; i < questData.questObjects.Length; i++)
        {
            GameObject go = Instantiate(P_QuestObjectTexts, T_QuestObjectTexts);
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

        //acceptButton.gameObject.SetActive(true);//버튼 on
        //acceptButton.onClick.RemoveAllListeners();//버튼 초기화
    }
    public void QuestInfoUIOff()
    {
        questInfoUI.SetActive(false);
        questListUI.SetActive(true);

    }
    public void CreateQuestSlot(QuestData _questData)
    {
        GameObject go = Instantiate(P_QuestSlot, T_QuestSlot);
        QuestSlot slot = go.GetComponent<QuestSlot>();
        slot.questData = _questData;
        switch (Options.S.language)
        {
            case Options.Language.Kor:
                slot.questTitle.text = _questData.questName;
                break;
            case Options.Language.Eng:
                slot.questTitle.text = _questData.questEName;
                break;
            default:
                break;
        }
        questSlots.Add(go);
        go.GetComponent<Button>().onClick.AddListener(() => QuestInfoUIOn(slot.questData));
        switch (_questData.questState)
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
    }

    public void AcceptQuest(QuestData questData)//퀘스트 수락
    {
        questManager.curQuestData.Add(questData);
        questData.questState = QuestData.QuestState.Accept;
        QuestInfoUIOff();
    }
    public void GiveUpQuest(QuestData questData)
    {
        questManager.curQuestData.Remove(questData);
        questData.questState = QuestData.QuestState.Wait;
        QuestInfoUIOff();
    }
    public void ClearQuest(QuestData questData)
    {
        questData.ClearQuest();
        questData.questState = QuestData.QuestState.Clear;
        QuestInfoUIOff();
    }
}
