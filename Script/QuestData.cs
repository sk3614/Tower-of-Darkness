using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class QuestObject
{
    public int[] monsterObNum;//몬스터 킬일때 필요한 몬스터 번호
    public string requireItemName;//필요한 아이템 이름
    public string requireEItemName;//필요한 아이템 이름
    public string requireName;//필요한 목표 이름
    public string requireEName;//필요한 목표 이름
    public int requireNum;//필요한 수
    public int curNum;//현재 진행 수

    public QuestData.RequirType requirType;
    public QuestData.CountType countType;
    public QuestData.HaveType haveType;
    public QuestData.SpendType spendType;


    public bool clear;

    public void CountUp(int _num)
    {
        curNum += _num;
        CheckClear();
    }
    public void HaveNum(int _num)
    {
        curNum = _num;
        CheckClear();
    }
    public void CheckClear()
    {
        if (curNum >= requireNum)
        {
            clear = true;
        }
    }
}
[System.Serializable]
public class QuestData
{
    public int towerProgress;//타워 진행도
    public int FailProgress;
    public bool isClear;//클리어 했는지

    public int questID; //퀘스트 ID

    public string questName;//퀘스트 이름
    public string questInfo;//퀘스트 설명
    public string questEName;
    public string questEInfo;


    //목표

    public QuestState questState;

    public QuestObject[] questObjects;

    public string rewardName;
    public string rewardEName;
    public ClearCheckType clearType;
    public enum QuestState
    {
        Wait,//미수락
        Accept,//진행중
        Success,//성공
        Clear,//완료
        Fail//실패
        
    }
    public enum RequirType
    {
       Count,//행동할때마다 상승
       Have,//가지고만 있어도 클리어
       Spend,//소모
       etc,//하드코딩류
    }
    public enum CountType
    {
        no,
        Kill,//몬스터 킬
        LevelUp,//레벨
        AnyKill,//몬스터 아무거나 킬
        Quest
    }
    public enum HaveType
    {
        no,
        HP,//체력
        Level,//현재레벨
        ATK,
        DEF,
        CP,
        Artifact,
        tier1equips,
        ArtiGetNum,
        UseSmokeBombNum,
        GetSetArti,
        UseGodstone,
        Gold

    }
    public enum SpendType
    {
        no,
        Gold,//골드를 소모해야함
        Key,
        RevealScroll//비밀방 두루마리 소모
    }
    public enum ClearCheckType //목표 하나만, 목표전부
    {
        And,//전부
        Or//하나만
    }
    public void CheckClear()
    {
        switch (clearType)
        {
            case ClearCheckType.And:
                for (int i = 0; i < questObjects.Length; i++)
                {
                    if (!questObjects[i].clear)
                    {
                        return;
                    }
                }
                questState = QuestState.Success;
                break;
            case ClearCheckType.Or:
                for (int i = 0; i < questObjects.Length; i++)
                {
                    if (questObjects[i].clear)
                    {
                        questState = QuestState.Success;
                        break;
                    }
                }
                break;
            default:
                break;
        }


    }
    public void ClearQuest()
    {
        isClear = true;
        
        QuestManager.S.ClearReward(this);
    }
    public void FailQuest()
    {
        isClear = true;

        QuestManager.S.ClearReward(this);
    }


}
