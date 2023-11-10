using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager S;
    public QuestData[] AllQuestData;//모든 퀘스트

    public List<QuestData> curQuestData = new List<QuestData>();// 현재 진행중인 퀘스트


    public void Update()
    {
        HaveCheck();
        if (Player.S.playerLocation == Player.PlayerLocation.Tower)
        {
            if (TowerMap.S.MoveLock)
            {

            }
            else
            {
                ClearCheck();
            }
        }
        else
        {
            ClearCheck();
        }


    }
    public void Awake()
    {
        if (S==null)
        {
            S = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {
    }
    public void MonsterKillCountUp(int _ObNum)
    {
        for (int i = 0; i < curQuestData.Count; i++)
        {
            for (int j = 0; j < curQuestData[i].questObjects.Length; j++)
            {
                if (curQuestData[i].questObjects[j].requirType == QuestData.RequirType.Count)
                {
                    if (curQuestData[i].questObjects[j].countType == QuestData.CountType.Kill)
                    {
                        for (int k = 0; k < curQuestData[i].questObjects[j].monsterObNum.Length; k++)
                        {
                            if (curQuestData[i].questObjects[j].monsterObNum[k] == _ObNum)
                            {
                                curQuestData[i].questObjects[j].CountUp(1);
                            }
                        }

                    }
                    if (curQuestData[i].questObjects[j].countType == QuestData.CountType.AnyKill)
                    {
                         curQuestData[i].questObjects[j].CountUp(1);
                    }
                }
            }

            
        }
    }
    public void CountUp(QuestData.CountType _countType, int _num)
    {
        for (int i = 0; i < curQuestData.Count; i++)
        {
            for (int j = 0; j < curQuestData[i].questObjects.Length; j++)
            {
                if (curQuestData[i].questObjects[j].requirType == QuestData.RequirType.Count)
                {
                    if (curQuestData[i].questObjects[j].countType == _countType)
                    {
                        curQuestData[i].questObjects[j].CountUp(_num);
                    }
                }
            }
        }
    }

    public void GoldSpend(int _goldNum)
    {
        for (int i = 0; i < curQuestData.Count; i++)
        {
            for (int j = 0; j < curQuestData[i].questObjects.Length; j++)
            {
                if (curQuestData[i].questObjects[j].requirType == QuestData.RequirType.Spend)
                {
                    if (curQuestData[i].questObjects[j].spendType == QuestData.SpendType.Gold)
                    {
                        curQuestData[i].questObjects[j].CountUp(_goldNum);
                    }
                }
            }


        }
    }
    public void SpendCheck(QuestData.SpendType _spendType, int _num)
    {
        for (int i = 0; i < curQuestData.Count; i++)
        {
            for (int j = 0; j < curQuestData[i].questObjects.Length; j++)
            {
                if (curQuestData[i].questObjects[j].requirType == QuestData.RequirType.Spend)
                {
                    if (curQuestData[i].questObjects[j].spendType == _spendType)
                    {
                        curQuestData[i].questObjects[j].CountUp(_num);
                    }
                }
            }


        }

    }

    public void HaveCheck()
    {
        for (int i = 0; i < curQuestData.Count; i++)
        {
            for (int j = 0; j < curQuestData[i].questObjects.Length; j++)
            {
                if (curQuestData[i].questObjects[j].requirType == QuestData.RequirType.Have)
                {
                    switch (curQuestData[i].questObjects[j].haveType)
                    {
                        case QuestData.HaveType.HP:
                            curQuestData[i].questObjects[j].HaveNum(Player.S.hp);
                            break;
                        case QuestData.HaveType.Level:
                            curQuestData[i].questObjects[j].HaveNum(Player.S.level);
                            break;
                        case QuestData.HaveType.ATK:
                            curQuestData[i].questObjects[j].HaveNum(Player.S.ATK);
                            break;
                        case QuestData.HaveType.DEF:
                            curQuestData[i].questObjects[j].HaveNum(Player.S.DEF);
                            break;
                        case QuestData.HaveType.CP:
                            curQuestData[i].questObjects[j].HaveNum(Player.S.useCP);
                            break;
                        case QuestData.HaveType.Artifact:
                            curQuestData[i].questObjects[j].HaveNum(ArtifactManager.S.ReturnHaveArtiNum());
                            break;
                        case QuestData.HaveType.tier1equips:
                            curQuestData[i].questObjects[j].HaveNum(EquipmentUI.S.Mp7QuestCheck());
                            break;
                        case QuestData.HaveType.ArtiGetNum:
                            curQuestData[i].questObjects[j].HaveNum(Player.S.artiGetNum);
                            break;
                        case QuestData.HaveType.UseSmokeBombNum:
                            curQuestData[i].questObjects[j].HaveNum(Player.S.useSmokebomb);
                            break;
                        case QuestData.HaveType.GetSetArti:
                            curQuestData[i].questObjects[j].HaveNum(Player.S.getSetArtifact);
                            break;
                        case QuestData.HaveType.UseGodstone:
                            curQuestData[i].questObjects[j].HaveNum(Player.S.useGodstone);
                            break;
                        case QuestData.HaveType.Gold:
                            curQuestData[i].questObjects[j].HaveNum(Player.S.gold);
                            break;
                        default:
                            break;
                    }
                }
            }

        }
    }
    public void ClearCheck()
    {
        for (int i = curQuestData.Count - 1; i >= 0; i--)
        {
            if (curQuestData[i].questState == QuestData.QuestState.Clear || curQuestData[i].FailProgress <= Player.S.mainProgress)
            {
                curQuestData.Remove(curQuestData[i]);
            }
        }
        for (int i = 0; i < curQuestData.Count; i++)
        {
            switch (curQuestData[i].clearType)
            {
                case QuestData.ClearCheckType.And:
                    int clearNum = 0;
                    for (int j = 0; j < curQuestData[i].questObjects.Length; j++)
                    {
                        if (curQuestData[i].questObjects[j].clear)
                        {
                            clearNum += 1;
                        }
                    }
                    if (clearNum >= curQuestData[i].questObjects.Length)
                    {
                        curQuestData[i].ClearQuest();
                        CountUp(QuestData.CountType.Quest, 1);
                        curQuestData[i].questState = QuestData.QuestState.Clear;
                    }

                    break;
                case QuestData.ClearCheckType.Or:
                    for (int j = 0; j < curQuestData[i].questObjects.Length; j++)
                    {
                        if (curQuestData[i].questObjects[j].clear)
                        {
                            curQuestData[i].ClearQuest();
                            CountUp(QuestData.CountType.Quest, 1);
                            curQuestData[i].questState = QuestData.QuestState.Clear;
                            break;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

    }
    public void AcceptQuest(int _num)
    {
        Debug.Log("");
        for (int i = 0; i < AllQuestData.Length; i++)
        {
            if (AllQuestData[i].questState==QuestData.QuestState.Wait&&AllQuestData[i].towerProgress==_num)
            {
                AllQuestData[i].questState = QuestData.QuestState.Accept;
                curQuestData.Add(AllQuestData[i]);

            }
        }
    }

    public void ClearReward(QuestData data,bool isfail=false)
    {
        List<Skill> skills = new List<Skill>();
        switch (data.questID)
        {
            case 1:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.GetExp(15);
                break;
            case 2:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.GetGold(20);
                break;
            case 3:
                QuestRewardUI.S.AddClearStack(data);
                AddItem.S.SearchItem("돌 열쇠");
                break;
            case 4:
                QuestRewardUI.S.AddClearStack(data,"",new int[3]{0,1,2},new string[3]{"CRC","AVD", "POW" });
                break;
            case 5:
                QuestRewardUI.S.AddClearStack(data,"",new int[3] { 0, 1, 2 }, new string[3] { "CRC", "AVD", "POW" });
                break;
            case 6:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.GetExp(15);

                break;
            case 7:

                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        QuestRewardUI.S.AddClearStack(data, "황동 반지");
                        break;
                    case Options.Language.Eng:
                        QuestRewardUI.S.AddClearStack(data, "Brass Ring");

                        break;
                    default:
                        break;
                }
                EquipmentUI.S.GetEquipment("황동 반지");

                break;
            case 8:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.GetExp(15);
                break;
            case 9:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.ATK += 2;
                Player.S.DEF += 2;
                break;
            case 10:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.GetGold(30);
                break;
            case 11:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.CP += 1;
                break;
            case 12:
                List<Artifact> artifacts = ArtifactManager.S.RandomArtifact("노말", 1);
                ArtifactManager.S.GetArtifact(artifacts[0].artifactName);
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        QuestRewardUI.S.AddClearStack(data, artifacts[0].artifactName);
                        break;
                    case Options.Language.Eng:
                        QuestRewardUI.S.AddClearStack(data, artifacts[0].Ename);
                        break;
                    default:
                        break;
                }

                break;
            case 13:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.hp += 1000;
                break;
            case 14:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.GetGold(200);
                break;
            case 15:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.GetGold(200);
                break;
            case 16:
                QuestRewardUI.S.AddClearStack(data);
                AddItem.S.SearchItem("신석", 1);
                break;
            case 17:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.GetGold(125);
                break;
            case 18:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.GetExp(130);
                break;
            case 19:
                QuestRewardUI.S.AddClearStack(data, "", new int[3] { 0, 1, 2 }, new string[3] { "CRC", "AVD", "POW" });
                break;
            case 20:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.GetExp(25);
                break;
            case 21:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.hp += 500;
                break;
            case 22:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.GetGold(50);
                Player.S.GetExp(25);
                break;
            case 23:
                QuestRewardUI.S.AddClearStack(data, "", new int[3] { 0, 1, 2 }, new string[3] { "CRC", "AVD", "POW" });
                break;
            case 24:
                QuestRewardUI.S.AddClearStack(data);
                AddItem.S.SearchItem("쇠 열쇠",3);
                break;
            case 25:
                QuestRewardUI.S.AddClearStack(data);
                AddItem.S.SearchItem("신석", 1);
                break;
            case 26:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.GetGold(100);
                Player.S.GetExp(50);
                break;
            case 27:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.GetGold(100);
                break;
            case 28:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.GetExp(1);
                break;
            case 29:
                QuestRewardUI.S.AddClearStack(data);
                AddItem.S.SearchItem("비밀방 두루마리", 4);
                break;
            case 30:
                if (isfail==true)
                {
                    QuestRewardUI.S.AddClearStack(data, "", null, null, true);
                    break;
                }
                QuestRewardUI.S.AddClearStack(data);
                Player.S.GetExp(50);
                break;
            case 31:
                QuestRewardUI.S.AddClearStack(data);
                AddItem.S.SearchItem("신비한 가루", 3);
                break;
            case 32:
                QuestRewardUI.S.AddClearStack(data, "", new int[3] { 0, 1, 2 }, new string[3] { "CRC", "AVD", "POW" });
                break;
            case 33:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.GetGold(100);
                Player.S.GetExp(50);
                break;
            case 34:
                QuestRewardUI.S.AddClearStack(data);
                AddItem.S.SearchItem("보석 열쇠");
                break;
            case 35:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.PP += 1;
                break;
            case 36:
                QuestRewardUI.S.AddClearStack(data, "", new int[3] { 0, 1, 2 }, new string[3] { "CRC", "AVD", "POW" });
                break;
            case 37:
                QuestRewardUI.S.AddClearStack(data);
                AddItem.S.SearchItem("신석", 1);
                break;
            case 38:
                QuestRewardUI.S.AddClearStack(data);
                AddItem.S.SearchItem("신비한 가루", 10);
                break;
            case 39:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.hp += 1500;
                break;
            case 40:
                QuestRewardUI.S.AddClearStack(data, "", new int[3] { 0, 1, 2 }, new string[3] { "CRC", "AVD", "POW" });
                break;
            case 41:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.hp += 2000;
                break;
            case 42:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.GetExp(150);
                break;
            case 43:
                QuestRewardUI.S.AddClearStack(data, "", new int[3] { 3, 4, 5 }, new string[3] { "HIT+2", "SPD+2", "CRR+2" });
                break;
            case 44:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.GetGold(500);
                break;
            case 45:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.LevelUP();
                break;
            case 46:
                QuestRewardUI.S.AddClearStack(data, "", new int[3] { 6, 7, 8 }, new string[3] { "CRC+2", "AVD+2", "POW+2" });
                break;
            case 47:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.GetExp(250);
                break;
            case 48:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.hp += 1500;
                break;
            case 49:
                QuestRewardUI.S.AddClearStack(data, "", new int[3] { 6, 7, 8 }, new string[3] { "CRC+2", "AVD+2", "POW+2" });
                break;
            case 50:
                List<Artifact> artifacts2 = ArtifactManager.S.RandomArtifact("에픽", 1);
                ArtifactManager.S.GetArtifact(artifacts2[0].artifactName);
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        QuestRewardUI.S.AddClearStack(data, artifacts2[0].artifactName);
                        break;
                    case Options.Language.Eng:
                        QuestRewardUI.S.AddClearStack(data, artifacts2[0].Ename);
                        break;
                    default:
                        break;
                }
                break;
            case 51:
                QuestRewardUI.S.AddClearStack(data);
                AddItem.S.SearchItem("신석", 5);
                break;
            case 52:
                QuestRewardUI.S.AddClearStack(data, "", new int[3] { 6, 7, 8 }, new string[3] { "CRC+2", "AVD+2", "POW+2" });
                break;
            case 53:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.GetGold(200);
                break;
            case 54:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.hp += 2000;
                break;
            case 55:
                QuestRewardUI.S.AddClearStack(data, "", new int[3] { 6, 7, 8 }, new string[3] { "CRC+2", "AVD+2", "POW+2" });
                break;
            case 56:
                QuestRewardUI.S.AddClearStack(data, "", new int[3] { 6, 7, 8 }, new string[3] { "CRC+2", "AVD+2", "POW+2" });
                break;
            case 57:
                QuestRewardUI.S.AddClearStack(data);
                Player.S.GetExp(200);
                break;
            case 58:
                List<Artifact> artifacts3 = ArtifactManager.S.RandomArtifact("레어", 1);
                ArtifactManager.S.GetArtifact(artifacts3[0].artifactName);
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        QuestRewardUI.S.AddClearStack(data, artifacts3[0].artifactName);
                        break;
                    case Options.Language.Eng:
                        QuestRewardUI.S.AddClearStack(data, artifacts3[0].Ename);
                        break;
                    default:
                        break;
                }
                break;
            case 59:
                QuestRewardUI.S.AddClearStack(data, "", new int[3] { 6, 7, 8 }, new string[3] { "CRC+2", "AVD+2", "POW+2" });
                break;
            default:
                break;
        }
    }
    public void CountUpQuestByID(int _ID,int _num=1,int _objectNum=0)
    {
        for (int i = 0; i < curQuestData.Count; i++)
        {
            if (curQuestData[i].questID == _ID)
            {
                curQuestData[i].questObjects[_objectNum].CountUp(_num);
            }
        }
    }
    public void QuestFail(int _ID)
    {

        for (int i = 0; i < curQuestData.Count; i++)
        {
            if (curQuestData[i].questID == _ID && curQuestData[i].questState==QuestData.QuestState.Accept)
            {
                curQuestData[i].questState = QuestData.QuestState.Fail;
                ClearReward(curQuestData[i], true);
                curQuestData.Remove(curQuestData[i]);
                return;
            }
        }
    }
}
