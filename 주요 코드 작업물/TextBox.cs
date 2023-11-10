using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextBox : MonoBehaviour
{
    public static TextBox S;

    public GameObject textBox;
    public GameObject textBox2;//Battle;
    public Text text;
    public Text text2;

    public List<string> texts=new List<string>();
    public int curTextNum;
    public int endEventNum;
    public int battleStartNum;
    public List<Dictionary<string, object>> data;

    public GameObject BetaendUI;
    public GameObject DemoendUI;
    private void Awake()
    {
        if (S==null)
        {
            S = this;
            data = CSVReader.Read("TowerTextBox", "CSV_Files/");
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void LateUpdate()
    {
        if (textBox.activeInHierarchy)
        {
            TowerMap.S.MoveLock = true;

        }
        if ((Input.GetKeyUp(KeyCode.Space) && (textBox.activeInHierarchy || textBox2.activeInHierarchy))||
            (Input.GetMouseButtonDown(0) && (textBox.activeInHierarchy || textBox2.activeInHierarchy)))
        {
            SoundManager.S.PlaySE("node");
            NextText();
        }
    }

    public void TextBoxOn(List<string> _texts, bool noNPC=true,int _endEventNum=0,int _battleStartNum=0)
    {
        SoundManager.S.PlaySE("node");
        endEventNum = _endEventNum;
        battleStartNum = _battleStartNum;
        if (battleStartNum > 0)
        {
            textBox2.SetActive(true);
            textBox.SetActive(false);
            textBox2.transform.SetAsFirstSibling();
        }
        else
        {
            textBox2.SetActive(false);
            textBox.SetActive(true);
        }

        texts = _texts;
        if (noNPC)
        {
            curTextNum = 1;
        }
        else curTextNum = 0;
        text.text = texts[0];
        text2.text = texts[0];
        TowerMap.S.MoveLock = true;
    }

    public void PrintText(int _num)
    {
        text.text = texts[_num];
        text2.text = texts[_num];
    }

    public void NextText()
    {

        if (curTextNum>=texts.Count)
        {
            CloseTextBox();
        }
        else
        {
            PrintText(curTextNum);
        }
        curTextNum += 1;
    }
    public void CloseTextBox()
    {
        curTextNum = 0;
        texts.Clear();
        textBox.SetActive(false);
        textBox2.SetActive(false);
        if (battleStartNum>0)
        {

        }
        else
        {
            TowerMap.S.MoveLock = false;
        }

        if (endEventNum>0)
        {
            EndEvent(endEventNum);
        }
    }

    public List<string> GetTexts(int start, int end)
    {
        List<string> str = new List<string>();
        for (int i = start; i <= end; i++)
        {
           

            if (Options.S.language == Options.Language.Kor)
            {
                str.Add(data[i]["Kor"].ToString());
            }
            else if (Options.S.language == Options.Language.Eng)
            {
                str.Add(data[i]["Eng"].ToString());
            }

           
        }


        return str;

    }

    public void EndEvent(int _num)
    {
        switch (_num)
        {
            case 1:
                TextBoxOn(GetTexts(55, 56));
                break;
            case 2://흑기사 조우
                Battle.S.GoBattleScene(TowerMap.S.towerCellArray[4, 4].Cell.GetComponent<MapCell>().towerObjectData.monsterData,TowerMap.S.towerCellArray[4, 4]);
                break;
            case 3://슬로터 조우
                Battle.S.GoBattleScene(TowerMap.S.towerCellArray[4, 3].Cell.GetComponent<MapCell>().towerObjectData.monsterData, TowerMap.S.towerCellArray[4, 3]);
                break;
            case 4://죽음의 왕 부활 이벤트
                StartCoroutine(Battle.S.PlayBattle(Battle.S.battleEffects, battleStartNum));
                break;
            case 5://죽음의 왕 조우
                Battle.S.GoBattleScene(TowerMap.S.towerCellArray[7, 4].Cell.GetComponent<MapCell>().towerObjectData.monsterData, TowerMap.S.towerCellArray[7, 4]);
                break;
            case 6://흑기사 처치
                for (int i = 0; i < QuestManager.S.curQuestData.Count; i++)
                {
                    if (QuestManager.S.curQuestData[i].questID==4)
                    {
                        QuestManager.S.curQuestData[i].questObjects[0].CountUp(1);
                    }
                }
                break;
            case 7://슬로터 처치
                for (int i = 0; i < QuestManager.S.curQuestData.Count; i++)
                {
                    if (QuestManager.S.curQuestData[i].questID == 5)
                    {
                        QuestManager.S.curQuestData[i].questObjects[0].CountUp(1);
                    }
                }
                Player.S.GetBasicClassSkill(1);

                break;
            case 8://21층 도달
                for (int i = 0; i < QuestManager.S.curQuestData.Count; i++)
                {
                    if (QuestManager.S.curQuestData[i].questID == 13)
                    {
                        QuestManager.S.curQuestData[i].questObjects[0].CountUp(1);
                    }
                }
                break;
            case 9://29번npc
                OneTimeShop.S.ShopUIOn(3);
                break;
            case 10://30번npc
                OneTimeShop.S.ShopUIOn(4);
                break;
            case 11://리치 처치
                SweepTower.S.TowerClear(TowerMap.S.curTowerNum);
                break;
            case 12://2탑중보1 조우
                Battle.S.GoBattleScene(TowerMap.S.towerCellArray[4, 6].Cell.GetComponent<MapCell>().towerObjectData.monsterData, TowerMap.S.towerCellArray[4, 6]);
                break;
            case 13://페어리 전직

                Player.S.ClassUp();
                break;
            case 14:
                SweepTower.S.TowerClear(TowerMap.S.curTowerNum);
                break;
            case 15://전직1
                OneTimeShop.S.ShopUIOn(11);
                break;
            case 16://전직2
                OneTimeShop.S.ShopUIOn(12);
                break;
            case 17://전직3
                OneTimeShop.S.ShopUIOn(13);
                break;
            case 18://포가튼원 처리
                Battle.S.GoBattleScene(TowerMap.S.towerCellArray[4, 2].Cell.GetComponent<MapCell>().towerObjectData.monsterData, TowerMap.S.towerCellArray[4, 2]);
                break;
            case 19://에이션트 처리
                Battle.S.GoBattleScene(TowerMap.S.towerCellArray[4, 7].Cell.GetComponent<MapCell>().towerObjectData.monsterData, TowerMap.S.towerCellArray[4, 7]);
                break;
            case 20://에이션트 처리
                SweepTower.S.TowerClear(TowerMap.S.curTowerNum);
                
                break;
            case 21://오버로드 조우
                Battle.S.GoBattleScene(TowerMap.S.towerCellArray[5, 4].Cell.GetComponent<MapCell>().towerObjectData.monsterData, TowerMap.S.towerCellArray[5, 4]);
                break;
            case 22://오버로드 처리
               // BetaendUI.SetActive(true);
                break;
            case 23://스핑크스 조우
                Battle.S.GoBattleScene(TowerMap.S.towerCellArray[4, 4].Cell.GetComponent<MapCell>().towerObjectData.monsterData, TowerMap.S.towerCellArray[4, 4]);
                break;
            case 24://스핑크스 처리
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        AddItem.S.SearchItem("고대의 석판");
                        TowerMap.S.GetItemTextOn( "고대의 석판을 획득했습니다.");
                        break;
                    case Options.Language.Eng:
                        TowerMap.S.GetItemTextOn(" Get Ancient stone tablet.");
                        AddItem.S.SearchItem("고대의 석판");
                        break;
                    default:
                        break;
                }
               // BetaendUI.SetActive(true);
                break;
            case 25://타이탄 조우
                Battle.S.GoBattleScene(TowerMap.S.towerCellArray[2, 3].Cell.GetComponent<MapCell>().towerObjectData.monsterData, TowerMap.S.towerCellArray[2, 3]);
                break;
            case 26://타이탄 처리
                    //BetaendUI.SetActive(true);
                break;
            case 27://19층 npc대화
                Player.S.hp += 1500;
                break;
            case 28://황금왕 조우
                Battle.S.GoBattleScene(TowerMap.S.towerCellArray[4, 5].Cell.GetComponent<MapCell>().towerObjectData.monsterData, TowerMap.S.towerCellArray[4, 5]);
                break;
            case 29://황금왕 처리
                SweepTower.S.TowerClear(TowerMap.S.curTowerNum);
                //BetaendUI.SetActive(true);
                break;
            case 30://npc 88
                Player.S.GetGold(250);
                break;
            case 31://더 스콜피온 조우
                Battle.S.GoBattleScene(TowerMap.S.towerCellArray[4, 0].Cell.GetComponent<MapCell>().towerObjectData.monsterData, TowerMap.S.towerCellArray[4, 0]);
                break;
            case 32://마이 레이디 조우
                Battle.S.GoBattleScene(TowerMap.S.towerCellArray[5, 4].Cell.GetComponent<MapCell>().towerObjectData.monsterData, TowerMap.S.towerCellArray[5, 4]);
                break;
            case 33://켄세이 조우
                Battle.S.GoBattleScene(TowerMap.S.towerCellArray[4, 7].Cell.GetComponent<MapCell>().towerObjectData.monsterData, TowerMap.S.towerCellArray[4, 7]);
                break;
            case 34://켄세이 처리
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        AddItem.S.SearchItem("고대의 석판2");
                        TowerMap.S.GetItemTextOn("고대의 석판2를 획득했습니다.");
                        break;
                    case Options.Language.Eng:
                        TowerMap.S.GetItemTextOn(" Get Ancient stone tablet2.");
                        AddItem.S.SearchItem("고대의 석판2");
                        break;
                    default:
                        break;
                }
                break;
            case 35://로제타 조우
                Battle.S.GoBattleScene(TowerMap.S.towerCellArray[4, 4].Cell.GetComponent<MapCell>().towerObjectData.monsterData, TowerMap.S.towerCellArray[4, 4]);
                break;
            case 36://크로우 조우
                Battle.S.GoBattleScene(TowerMap.S.towerCellArray[4, 4].Cell.GetComponent<MapCell>().towerObjectData.monsterData, TowerMap.S.towerCellArray[4, 4]);
                break;
            case 37://선지자j조우
                Battle.S.GoBattleScene(TowerMap.S.towerCellArray[4, 6].Cell.GetComponent<MapCell>().towerObjectData.monsterData, TowerMap.S.towerCellArray[4, 6]);
                break;
            case 38://선지자j처치
                    BetaendUI.SetActive(true);
                break;
            case 39://눈조우
                Battle.S.GoBattleScene(TowerMap.S.towerCellArray[4, 4].Cell.GetComponent<MapCell>().towerObjectData.monsterData, TowerMap.S.towerCellArray[4, 4]);

                break;
            case 130:
                UIBar.S.OpenCharacterUI();
                UIBar.S.GoSkillSetUI();
                break;

            default:
                break;
        }
    }
}
