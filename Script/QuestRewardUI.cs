using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class QuestClearStack
{
    public QuestData data;
    public int[] rewardNums;
    public string[] rewardButtonStrings;
    public string rewardString;
    public bool isFail;

}

public class QuestRewardUI : MonoBehaviour
{
    public static QuestRewardUI S;


    public GameObject uiBase;
    public GameObject P_reward;
    public Transform T_reward;
    private List<GameObject> rewards=new List<GameObject>();
    public GameObject exitButton;
    public Text questName;
    public Text questReward;

    public List<QuestClearStack> clearStacks = new List<QuestClearStack>();

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
    private void LateUpdate()
    {
        if (uiBase.gameObject.activeInHierarchy&&exitButton.activeInHierarchy&&Input.GetKeyUp(KeyCode.Space))
        {
            UIClose();
        }
        if (!uiBase.gameObject.activeInHierarchy&&clearStacks.Count>0)
        {
            if (Player.S.playerLocation==Player.PlayerLocation.Tower)
            {
                if (!TowerMap.S.MoveLock)
                {
                    UIOn(clearStacks[0]);
                    clearStacks.RemoveAt(0);
                }
            }
            else
            {
                UIOn(clearStacks[0]);
                clearStacks.RemoveAt(0);
            }

        }
    }

    public void UIOn(QuestClearStack _RewardData)
    {
        uiBase.SetActive(true);
        exitButton.SetActive(true);
        if (Player.S.playerLocation == Player.PlayerLocation.Tower)
        {
            TowerMap.S.MoveLock = true;
        }
        if (_RewardData.isFail)
        {
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    questName.text = _RewardData.data.questName + " 실패";
                    questReward.text = _RewardData.data.questInfo;

                    break;
                case Options.Language.Eng:
                    questName.text =  _RewardData.data.questEName+ "Failed";
                    questReward.text = _RewardData.data.questEInfo;
                    break;
                default:
                    break;
            }
            return;
        }


        switch (Options.S.language)
        {
            case Options.Language.Kor:
                questName.text = _RewardData.data.questName + " 클리어";

                if (_RewardData.rewardString != "")
                {
                    questReward.text = "보상 : " + _RewardData.rewardString;
                }
                else
                {
                    questReward.text = "보상 : " + _RewardData.data.rewardName;
                }
                break;
            case Options.Language.Eng:
                questName.text = "Clear "+_RewardData.data.questEName;

                if (_RewardData.rewardString != "")
                {
                    questReward.text = "Reward : " + _RewardData.rewardString;
                }
                else
                {
                    questReward.text = "Reward : " + _RewardData.data.rewardEName;
                }
                break;
            default:
                break;
        }

        for (int i = 0; i < rewards.Count; i++)
        {
            Destroy(rewards[i].gameObject);

        }
        rewards.Clear();

        if (_RewardData.rewardNums != null)
        {
            exitButton.SetActive(false);
            for (int i = 0; i < _RewardData.rewardNums.Length; i++)
            {
                int j = i;
                GameObject go = Instantiate(P_reward, T_reward);
                Button B_go = go.GetComponent<Button>();
                Text B_Text = go.GetComponentInChildren<Text>();

                B_go.onClick.AddListener(() => RewardNum(_RewardData.rewardNums[j]));
                B_Text.text = _RewardData.rewardButtonStrings[i];
                rewards.Add(go);
            }
        }
        
    }


    public void RewardNum(int _num)
    {
        switch(_num)
        {
            case 0:
                Player.S.CRC += 1;
                break;
            case 1:
                Player.S.AVD += 1;
                break;
            case 2:
                Player.S.POW += 1;
                break;
            case 3:
                Player.S.HIT += 2;
                break;
            case 4:
                Player.S.SPD += 2;
                break;
            case 5:
                Player.S.CRR += 2;
                break;
            case 6:
                Player.S.CRC += 2;
                break;
            case 7:
                Player.S.AVD += 2;
                break;
            case 8:
                Player.S.POW += 2;
                break;

        }
        UIClose();
    }
    public void UIClose()
    {
        uiBase.SetActive(false);
        if (Player.S.playerLocation == Player.PlayerLocation.Tower)
        {
            TowerMap.S.MoveLock = false;
        }
    }
    public void AddClearStack(QuestData _data,string _rewardName="", int[] rewardNum = null, string[] strings=null,bool isFail=false)
    {
        QuestClearStack questClearStack=new QuestClearStack();
        questClearStack.data = _data;
        questClearStack.rewardNums = rewardNum;
        questClearStack.rewardButtonStrings = strings;
        questClearStack.rewardString = _rewardName;
        questClearStack.isFail = isFail;
        clearStacks.Add(questClearStack);
    }
}
