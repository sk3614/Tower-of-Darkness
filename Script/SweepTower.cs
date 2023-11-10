using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
[System.Serializable]
public class ObData
{
    public int obNum;
    public string name;
    public string ename;
    public int num;
}
public class SweepTower : MonoBehaviour
{

    public static SweepTower S;

    public int allGold;
    public int allExp;
    public int allHP;

    public int allATK;
    public int allDEF;
    public int allAVD;
    public int allSPD;
    public int allHIT;
    public int allPOW;
    public int allLevel;
    public int allCRC;
    public int allCRD;

    public List<int> stats;
    public int allStoneKey;
    public int allMetalKey;
    public int allGoldKey;
    public int allJewelKey;
    public int allGStone;
    public int allmysticPowder;

    public ObData[] obDatas;
    public string[] StatsString;
    //UI
    public GameObject UIbase;
    public Transform T_items;
    public Transform T_stats;

    public Text ClearText;

    public GameObject TextDummy;
    public List<GameObject> textdummies=new List<GameObject>();

    public int stonegate;
    public int steelgate;
    public int goldgate;
    public int jewelgate;
       
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
    public void Start()
    {
    }
    public void SetTowerAllvalue(int _towerNum,int _towerMaxFloor)
    {
        allGold = 0;
        allExp = 0;
        allHP = 0;
        allATK = 0;
        allDEF = 0;
        allAVD = 0;
        allSPD = 0;
        allHIT = 0;
        allPOW = 0;
        allLevel = 0;
        allCRC = 0;
        allCRD = 0;
        allStoneKey = 0;
        allMetalKey = 0;
        allGoldKey = 0;
        allJewelKey = 0;
        allGStone = 0;
        allmysticPowder = 0;
        int minfloor=0;
       

        for (int i = 0; i < obDatas.Length; i++)
        {
            obDatas[i].num = 0;
        }

        List<Dictionary<string, object>> data;
        switch (TowerMap.S.curTowerNum)
        {
            case 0:
                minfloor = 0;
                break;

            case 1:
                minfloor = 0;
                break;
            case 2:
                minfloor = -20;
                break;
            default:
                break;
        }
        Debug.Log(minfloor.ToString());
        for (int i = minfloor; i <= _towerMaxFloor; i++)
        {
            //data = CSVReader.Read("T" + _towerNum.ToString() + "F" + i.ToString(), "Maps/");
            //Debug.Log(File.Exists(Application.persistentDataPath + "/MapSave/" + "T" + TowerNum.ToString() + "F" + floor.ToString()+".csv"));
            if (File.Exists(Application.persistentDataPath + "/MapSave/" + "T" + _towerNum.ToString() + "F" + i.ToString() + ".csv"))
            {
                data = CSVReader.Read2(Application.persistentDataPath + "/MapSave/" + "T" + _towerNum.ToString() + "F" + i.ToString() + ".csv");
            }
            else
            {
                data = CSVReader.Read("T" + _towerNum.ToString() + "F" + i.ToString(), "Maps/");
            }
            for (int j = 0; j < data.Count; j++)
            {
                int da;
                da = (int)data[j]["ObjectNum"];

                //if (da==501)
                //{
                //    Debug.Log(i + "층");
                //}
                CheckObject(da);
            }

        }
        stats.Clear();
        stats.Add(allLevel);
        stats.Add(allHP);
        stats.Add(allATK);
        stats.Add(allDEF);
        stats.Add(allAVD);
        stats.Add(allSPD);
        stats.Add(allHIT);
        stats.Add(allPOW);
        stats.Add(allCRC);
        stats.Add(allCRD);
        stats.Add(allExp);

    }

    public void CheckObject(int _obNum)
    {
        for (int i = 0; i < obDatas.Length; i++)
        {
            if (obDatas[i].obNum == _obNum)
            {
                obDatas[i].num += 1;
            }
        }
        if (_obNum>300&&_obNum<1000)
        {
           MonsterData data= Dictionaries.S.GetTowerObjectDic(_obNum).monsterData;


            allExp += (int)Battle.S.monsterDatas[data.MonsterID - 1]["EXP"];
            allGold += (int)Battle.S.monsterDatas[data.MonsterID - 1]["GOL"];
        }
        else
        {
            switch (_obNum)
            {
                case 101://초록 물약
                    allHP+= 150 * Player.S.HEL / 100;
                    break;
                case 102://푸른 물약
                    allHP += 300 * Player.S.HEL / 100;
                    break;
                case 103://붉은 물약
                    allHP += 500 * Player.S.HEL / 100;
                    break;
                case 104://흰 물약
                    allHP += 750 * Player.S.HEL / 100;
                    break;
                case 105://레이어드 물약
                    allHP += 1200 * Player.S.HEL / 100;
                    break;


                case 41:
                    stonegate += 1;
                    break;
                case 42:
                    steelgate += 1;
                    break;
                case 43:
                    goldgate += 1;
                    break;
                case 44:
                    jewelgate += 1;
                    break;

                case 111://붉은 강화석
                    allATK+= TowerMap.S.curTowerNum;
                    break;
                case 112://푸른 강화석
                    allDEF += TowerMap.S.curTowerNum;
                    break;
                case 113://초록 강화석
                   allSPD += 1;
                    break;
                case 114://자주석 강화석
                    allCRC += 1;
                    break;
                case 115://검은 강화석
                    allAVD += 2;
                    break;
                case 116://흰 강화석
                   allPOW += 1;
                    break;
                case 121://경험의 유물
                    allLevel += 1;
                    break;
                case 122://운명의 유물
                    allCRD += 5;
                    break;
                case 123://자격의 유물
                    allHIT += 1;
                    break;
                case 124://자격의 유물
                    allGold += 200;
                    break;
                case 125://자격의 유물
                    allExp += 100;
                    break;

                case 131: //무기1

                    break;

                case 132: //방어구

                    break;
                case 141://돌열쇠
                    allStoneKey += 1;
                    break;
                case 142://쇠열쇠
                    allMetalKey += 1;
                    break;
                case 143://금열쇠
                    allGoldKey += 1;
                    break;
                case 144://보석열쇠
                    allJewelKey += 1;
                    break;
                case 145://해골 열쇠
                    allStoneKey += 1;
                    allMetalKey += 1;
                    allGoldKey += 1;
                    allJewelKey += 1;
                    break;
                case 183://신석
                    allGStone += 1;
                    break;
                case 184://보석열쇠
                    allmysticPowder += 1;
                    break;

            }

        }

    }

    public void GetAllItem()
    {
        if (ArtifactManager.S.MedalOfPolitician.able)
        {
            allGold += allGold * 15 / 100;
            allExp += allExp * 15 / 100;
        }
        
        Player.S.gold += allGold;
        Player.S.exp += allExp;
        Player.S.hp += allHP;

        Player.S.ATK += allATK;
        Player.S.DEF += allDEF;
        Player.S.AVD += allAVD;
        Player.S.HIT += allHIT;
        Player.S.SPD += allSPD;
        Player.S.POW += allPOW;
        Player.S.CRC += allCRC;
        Player.S.CRD += allCRD;
        for (int i = 0; i < allLevel; i++)
        {
            Player.S.LevelUP();
        }


        AddItem.S.SearchItem("돌 열쇠", allStoneKey);
        AddItem.S.SearchItem("쇠 열쇠", allMetalKey);
        AddItem.S.SearchItem("금 열쇠", allGoldKey);
        AddItem.S.SearchItem("보석 열쇠", allJewelKey);
    }

    public void TowerClear(int _TowerNum)
    {
        UIbase.SetActive(true);
        TowerMap.S.MoveLock = true;
        for (int i = 0; i < textdummies.Count; i++)
        {
            Destroy(textdummies[i].gameObject);

        }
        textdummies.Clear();

        switch (_TowerNum)
        {
            case 0:
                //ClearText.text=""
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        ClearText.text = "축하드립니다! 보스를 토벌하여 성공적으로 튜토리얼을 마무리 하셨습니다!";
                        break;
                    case Options.Language.Eng:
                        ClearText.text = "Congratulations! you successfully finished tutorial\nby defeating the boss!";
                        break;
                    default:
                        break;
                }

                SetTowerAllvalue(0, 6);
                break;
            case 1:

                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        ClearText.text = "축하드립니다 뼈의 탑 킹 오브더 데드를 격파하였습니다.\nPP,TP가 1 증가하였습니다";
                        break;
                    case Options.Language.Eng:
                        ClearText.text = "Congratulations, you have defeated the King of The Dead\nin the Tower of Bone.\nPP and TP +1";
                        break;
                    default:
                        break;
                }

                SetTowerAllvalue(1, 21);
                Player.S.PP += 1;
                Player.S.PlusTP();
                break;
            case 2:

                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        ClearText.text = "축하드립니다 피의 탑 드라큘라 원을 격파하였습니다.\nPP,TP가 1 증가하였습니다.";
                        break;
                    case Options.Language.Eng:
                        ClearText.text = "Congratulations, you have defeated the Dracula One\nin the Tower of Blood.\nPP and TP +1 .";
                        break;
                    default:
                        break;
                }

                SetTowerAllvalue(2, 10);
                Player.S.PP += 1;
                Player.S.PlusTP();
                break;
            case 3:

                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        ClearText.text = "축하드립니다 심연의 탑 황금 왕을 격파하였습니다.\nPP,TP가 1 증가하였습니다.";
                        break;
                    case Options.Language.Eng:
                        ClearText.text = "Congratulations, you have defeated the Golden King\nin the Tower of Abyss.\nPP and TP +1 .";
                        break;
                    default:
                        break;
                }

                SetTowerAllvalue(3, 19);
                Player.S.PP += 1;
                Player.S.PlusTP();
                break;
            default:
                break;
        }

        for (int i = 0; i < obDatas.Length; i++)
        {
            if (obDatas[i].num>0)
            {
                GameObject go = Instantiate(TextDummy,T_items);
                textdummies.Add(go);

                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        go.GetComponent<Text>().text = obDatas[i].name + " X" + obDatas[i].num.ToString();
                        break;
                    case Options.Language.Eng:
                        go.GetComponent<Text>().text = obDatas[i].ename + " X" + obDatas[i].num.ToString();
                        break;
                    default:
                        break;
                }
            }

        }
        if (allGold > 0)
        {
            GameObject go = Instantiate(TextDummy, T_items);
            textdummies.Add(go);
            go.GetComponent<Text>().text = "Gold" + " X" +allGold.ToString();
        }
        for (int i = 0; i < stats.Count; i++)
        {
            if (stats[i]>0)
            {
                GameObject go = Instantiate(TextDummy, T_stats);
                textdummies.Add(go);
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        go.GetComponent<Text>().text = StatsString[i] + " X" + stats[i];
                        break;
                    case Options.Language.Eng:
                        go.GetComponent<Text>().text = StatsString[i] + " X" + stats[i];
                        break;
                    default:
                        break;
                }

            }

        }
        GetAllItem();
    }

    public void NextProgress()
    {
        if (TowerMap.S.curTowerNum == 1&&Player.S.isDemo)
        {
            TextBox.S.DemoendUI.SetActive(true);
            TowerMap.S.MoveLock = true;
        }
        else
        {
            if (!Player.S.SecretWallOn)
            {
                Player.S.SecretWallOn = true;
                AddItem.S.SearchItem("비밀방 두루마리", 10);
            }
            TowerMap.S.MoveLock = false;
            switch (TowerMap.S.curTowerNum)
            {
                case 0:
                    TowerStory.S.StartStory(Player.S.mainProgress);
                    break;
                case 1:
                    TowerStory.S.StartStory(Player.S.mainProgress);
                    break;
                case 2:
                    //TextBox.S.BetaendUI.SetActive(true);
                    TowerStory.S.StartStory(6);
                    break;
                case 3:
                    TowerStory.S.StartStory(10);
                    break;
                default:
                    break;
            }

        }

    }
}
