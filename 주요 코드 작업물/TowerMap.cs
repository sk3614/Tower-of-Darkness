using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class TowerCell
{
    public TowerCell( int _x, int _y) {  x = _x; y = _y; }
    public bool isWall;

    public int x, y;
    public GameObject Cell;
}
[System.Serializable]
public class TowerMoveImage
{
    public Sprite[] up;
    public Sprite[] left;
    public Sprite[] right;
    public Sprite[] down;
}

public class TowerMap : MonoBehaviour
{
    public static TowerMap S;
    public GameObject BattleUI;

    public GameObject P_MiniMapCell;

    public GameObject TowerPlayer;
    public int playerImageNum;
    public Sprite[] UpImage;
    public Sprite[] LeftImage;
    public Sprite[] RightImage;
    public Sprite[] DownImage;

    public TowerMoveImage MoveImage_witch;
    public TowerMoveImage MoveImage_crusader;
    public TowerMoveImage MoveImage_Inquisitor;
    public float rollBackTime = 0f;
    public PlayerDirection playerDirection;
    public enum PlayerDirection
    {
        Up,
        Down,
        Right,
        Left
    }

    public Transform T_MiniMapCells;

    public RectTransform T_miniMap;
    public TowerCell[,] towerCellArray;
    public List<GameObject> mapCells;
    public int curTowerNum;
    public int curFloorNum;
    public TowerCell spawnCell;
    public TowerCell currentCell;//현재 내위치
    public TowerCell targetCell;//가고자하는 위치
    public TowerObjectData voidTile;
    public TowerObjectData[] fieldDummies;
    float movetime = 0;
    float moveCoolTime = 0f;

    public GameObject GetItemUI;
    public Text getItemName;
    public Text nowFloor;
    public Text nowPreset;

    public bool MoveLock;

    //전직환영 몬스터데이터
    public MonsterData witch;
    public MonsterData cru;
    //비밀방
    public GameObject secretWallUI;
    public Vector2Int secretWallLocation;
    public TowerObjectData secretWallDumy;
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

    //이벤트 변수
    bool Tower0NPC1;
    bool Tower0NPC2;
    bool Tower0NPC4;



    //포션 HealNum
    public int Potion_healNum;
    public void Start()
    {

        //LoadMap(7, 4, 0, 0);
        LoadMapByPogress();
        SetTowerMoveImage(Player.S.job);
        nowPreset.text ="Skill Preset : " +(Player.S.NowPresetNum+1).ToString();
        PlayTowerBGM();
        if (curTowerNum==0&&Player.S.endTuto)
        {
            TowerMap.S.MoveLock = false;
            TowerStory.S.StartStory(Player.S.mainProgress);
        }


    }
    public void Update()
    {
        switch (curTowerNum)
        {
            case 0:
                Tower0Event();
                break;
            case 1:
                Tower1Event();
                break;
            case 2:
                Tower2Event();
                break;
            case 3:
                Tower3Event();
                break;
            case 4:
                Tower4Event();
                break;
            case 7:
                Tower7Event();
                break;
            default:
                break;
        }

        if (Input.GetKeyDown(KeyCode.T) && Inventory.S.SearchItemCount("비밀방 두루마리")> 0 && Player.S.SecretWallOn)
        {
            Debug.Log("asdf");
            SecretWallUIOpen();
        }


        PlayerImageRollBack();
        if (!Battle.S.battleUIBase.activeInHierarchy && !MoveLock)
        {
            KeyDown();
        }
        if (!Battle.S.battleUIBase.activeInHierarchy && !TextBox.S.textBox.activeInHierarchy && !OneTimeShop.S.UI.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (curTowerNum!=7)
                {
                    MoveFloorUI.S.MoveFloorUIOpen();
                }

            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                MonsterBookUI.S.UIOn();
            }
        }

    }
    public void LoadMap(int TowerNum, int floor, int _x, int _y)
    {
        List<Dictionary<string, object>> data;

        //Debug.Log(File.Exists(Application.persistentDataPath + "/MapSave/" + "T" + TowerNum.ToString() + "F" + floor.ToString()+".csv"));
        if (File.Exists(Application.persistentDataPath + "/MapSave/" + "T" + TowerNum.ToString() + "F" + floor.ToString() + ".csv"))
        {
            data = CSVReader.Read2(Application.persistentDataPath + "/MapSave/" + "T" + TowerNum.ToString() + "F" + floor.ToString() + ".csv");
        }
        else
        {
            data = CSVReader.Read("T" + TowerNum.ToString() + "F" + floor.ToString(), "Maps/");

        }


        curTowerNum = TowerNum;
        Player.S.CurTowerNum = TowerNum;
        curFloorNum = floor;
        switch (Options.S.language)
        {
            case Options.Language.Kor:
                nowFloor.text = floor.ToString() + "층";
                break;
            case Options.Language.Eng:
                nowFloor.text = floor.ToString() + "F";
                break;
            default:
                break;
        }

        nowFloor.text = nowFloor.text.Replace('-', 'B');
        List<int[]> mapData = new List<int[]>();
        int mapX = 0;
        int mapY = 0;
        // Debug.Log(data.Count);
        for (int i = 0; i < data.Count; i++)
        {
            int[] da = new int[3];
            da[0] = (int)data[i]["X"];
            if (mapX < da[0]) mapX = da[0];
            da[1] = (int)data[i]["Y"];
            if (mapY < da[1]) mapY = da[1];
            da[2] = (int)data[i]["ObjectNum"];


            mapData.Add(da);
        }
        //Debug.Log(mapData.Count);
        CreateMap(mapData, mapX + 1, mapY + 1);

        SetPlayerLocation(towerCellArray[_x, _y]);

        data = null; ;

        PlayerImageChange(3);
        TowerPlayer.GetComponent<Image>().sprite = Player.S.PlayerTowerImage;

    }
    public void CreateMap(List<int[]> _mapData, int _x, int _y)
    {
        for (int i = mapCells.Count - 1; i >= 0; i--)
        {
            Destroy(mapCells[i].gameObject);

        }
        mapCells.Clear();
        towerCellArray = new TowerCell[_x, _y];

        for (int i = 0; i < _mapData.Count; i++)
        {
            towerCellArray[_mapData[i][0], _mapData[i][1]] = new TowerCell(_mapData[i][0], _mapData[i][1]);

            GameObject go = Instantiate(P_MiniMapCell, T_MiniMapCells);
            towerCellArray[_mapData[i][0], _mapData[i][1]].Cell = go;
            go.GetComponent<Image>().sprite = fieldDummies[curTowerNum].ObjectImage;
            go.GetComponent<MapCell>().towerObjectData = Dictionaries.S.GetTowerObjectDic(_mapData[i][2], _mapData[i][0], _mapData[i][1]);
            go.GetComponent<MapCell>().dummy = fieldDummies[curTowerNum];
            if (go.GetComponent<MapCell>().towerObjectData.objectType == TowerObjectData.ObjectType.Monster)
            {

                go.GetComponent<MapCell>().ObjectImage.GetComponent<Image>().sprite = go.GetComponent<MapCell>().towerObjectData.ObjectImage;
                go.GetComponent<MapCell>().aniSprite = go.GetComponent<MapCell>().towerObjectData.aniImages;
            }
            else
            {
                if (go.GetComponent<MapCell>().towerObjectData.ObjectNum==69)
                {
                    Vector2 vector2 = new Vector2(60, 60);
                    go.GetComponent<MapCell>().ObjectImage.GetComponent<RectTransform>().sizeDelta = vector2;
                }

                go.GetComponent<MapCell>().ObjectImage.GetComponent<Image>().sprite = go.GetComponent<MapCell>().towerObjectData.ObjectImage;
                go.GetComponent<MapCell>().aniSprite = go.GetComponent<MapCell>().towerObjectData.aniImages;
                go.GetComponent<MapCell>().DelSprites = go.GetComponent<MapCell>().towerObjectData.delAniImages;
            }
            go.GetComponent<MapCell>().X = _mapData[i][0];
            go.GetComponent<MapCell>().Y = _mapData[i][1];

            mapCells.Add(go);
            RectTransform T_go = go.GetComponent<RectTransform>();
            T_go.anchoredPosition = new Vector2(T_go.sizeDelta.x * 1f * go.GetComponent<MapCell>().X, T_go.sizeDelta.y * 1f * go.GetComponent<MapCell>().Y);
        }
    }
    public void ChangeFloor(TowerCell towerCell, bool Up)
    {
        CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
        //Vector4 floorData = Dictionaries.S.GetFloorData(curTowerNum, curFloorNum, towerCell.x, towerCell.y);
        if (Player.S.MaxFloorNum < curFloorNum + 1 && Up)
        {
            Player.S.MaxFloorNum = (int)curFloorNum + 1;
        }
        if (Player.S.MinFloorNum > curFloorNum - 1 && !Up)
        {
            Player.S.MinFloorNum = (int)curFloorNum - 1;
        }

        if (Up)
        {
            LoadMap(curTowerNum, curFloorNum + 1, towerCell.x, towerCell.y);

            if (curTowerNum==7&&curFloorNum==2 && GetTowerCellObData(4, 4).ObjectNum == 516)
            {
                switch (Player.S.job)
                {
                    case Player.PlayerJob.Inquisitor:
                        ChangeMapCell(towerCellArray[4, 4].Cell.GetComponent<MapCell>(), Dictionaries.S.GetTowerObjectDic(518));
                        break;
                    case Player.PlayerJob.Paladin:
                        ChangeMapCell(towerCellArray[4, 4].Cell.GetComponent<MapCell>(), Dictionaries.S.GetTowerObjectDic(516));
                        break;
                    case Player.PlayerJob.WitchHunter:
                        ChangeMapCell(towerCellArray[4, 4].Cell.GetComponent<MapCell>(), Dictionaries.S.GetTowerObjectDic(517));
                        break;
                    case Player.PlayerJob.NightShadow:
                        break;
                    case Player.PlayerJob.Alchemist:
                        break;
                    case Player.PlayerJob.none:
                        break;
                    default:
                        break;
                }

            }
        }
        else
        {
            LoadMap(curTowerNum, curFloorNum + -1, towerCell.x, towerCell.y);
        }

    }
    public void ChangeFloor(int floorNum, int _x, int _y)
    {
        CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);


        LoadMap(curTowerNum, floorNum, _x, _y);
    }
    public void GoSecretWall(TowerCell towerCell)
    {
        CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
        Vector4 floorData = Dictionaries.S.GetSecretWallData(curTowerNum, curFloorNum, towerCell.x, towerCell.y);
        LoadMap((int)floorData.x, (int)floorData.y, (int)floorData.z, (int)floorData.w);

    }
    public void MoveMap(int _num)//0=우,1=좌,2=위,3=아래
    {
        switch (_num)
        {
            case 0:
                PlayerImageChange(0);
                if (currentCell.x+1>8)
                {
                    break;
                }
                targetCell = towerCellArray[currentCell.x + 1, currentCell.y + 0];
                if (cellCheck(targetCell))
                {
                    MoveMiniMap(0);

                }

                break;
            case 1:
                PlayerImageChange(1);
                if (currentCell.x - 1 < 0)
                {
                    break;
                }
                targetCell = towerCellArray[currentCell.x - 1, currentCell.y + 0];
                if (cellCheck(targetCell))
                {
                    MoveMiniMap(1);

                }
                break;
            case 2:
                PlayerImageChange(2);
                if (currentCell.y + 1 > 8)
                {
                    break;
                }
                targetCell = towerCellArray[currentCell.x, currentCell.y + 1];
                if (cellCheck(targetCell))
                {
                    MoveMiniMap(2);

                }
                break;
            case 3:
                PlayerImageChange(3);
                if (currentCell.y -1 <0)
                {
                    break;
                }
                targetCell = towerCellArray[currentCell.x + 0, currentCell.y - 1];
                if (cellCheck(targetCell))
                {
                    MoveMiniMap(3);

                }
                break;
            default:
                break;
        }
    }

    public void MoveMiniMap(int _num)//0=우,1=좌,2=위,3=아래
    {
        switch (_num)
        {
            case 0:
                currentCell = towerCellArray[currentCell.x + 1, currentCell.y + 0];
                SetPlayerLocation(currentCell);

                break;
            case 1:
                currentCell = towerCellArray[currentCell.x - 1, currentCell.y + 0];
                SetPlayerLocation(currentCell);

                break;
            case 2:
                currentCell = towerCellArray[currentCell.x, currentCell.y + 1];
                SetPlayerLocation(currentCell);

                break;
            case 3:
                currentCell = towerCellArray[currentCell.x + 0, currentCell.y - 1];
                SetPlayerLocation(currentCell);

                break;
            default:
                break;
        }
        int value = Random.Range(0, 4);
        switch (value)
        {
            case 0:
                SoundManager.S.PlaySE("walk1");
                break;
            case 1:
                SoundManager.S.PlaySE("walk2");
                break;
            case 2:
                SoundManager.S.PlaySE("walk3");
                break;
            case 3:
                SoundManager.S.PlaySE("walk4");
                break;
            default:
                break;
        }

    }
    public void ButtonCheck()
    {
        Debug.Log("ok");
    }
    public void SetPlayerLocation(TowerCell towerCell)
    {
        RectTransform T_towerPlayer = TowerPlayer.GetComponent<RectTransform>();
        T_towerPlayer.anchoredPosition = towerCell.Cell.GetComponent<RectTransform>().anchoredPosition;
        TowerPlayer.transform.SetAsLastSibling();
        currentCell = towerCell;
    }

    public void KeyDown()
    {

        if (Input.GetKeyDown(KeyCode.Space) && !MoveLock)
        {
            cellCheck(currentCell);

        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            movetime += 1 * Time.deltaTime;
        }
        else
        {
            movetime = 0f;
        }
        if (movetime > 0.6f)
        {
            moveCoolTime += 1 * Time.deltaTime;
        }
        else
        {
            moveCoolTime = 0;
        }

        if (movetime < 0.6f)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                MoveMap(0);

            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                MoveMap(1);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                MoveMap(2);

            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                MoveMap(3);
            }

        }
        else
        {
            if (moveCoolTime > 0.07f)
            {
                if (Input.GetKey(KeyCode.D))
                {
                    MoveMap(0);
                    moveCoolTime = 0f;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    MoveMap(1);
                    moveCoolTime = 0f;
                }
                if (Input.GetKey(KeyCode.W))
                {
                    MoveMap(2);
                    moveCoolTime = 0f;

                }
                if (Input.GetKey(KeyCode.S))
                {
                    MoveMap(3);
                    moveCoolTime = 0f;
                }
            }

        }
    }
    public void GoTown()
    {

        if (curFloorNum == 14)
        {
            ChangeMapCell(4, 5, fieldDummies[curTowerNum]);
        }
        CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
        SceneChanger.S.GoTown();
    }

    public void ChangeMapCell(MapCell _mapCell, TowerObjectData _obData)
    {
        _mapCell.towerObjectData = _obData;
        _mapCell.ObjectImage.GetComponent<Image>().sprite = _obData.ObjectImage;
        _mapCell.aniSprite = new Sprite[0];
    }
    public void ChangeMapCell(int _x, int _y, TowerObjectData _obData)
    {
        towerCellArray[_x, _y].Cell.GetComponent<MapCell>().towerObjectData = _obData;
        towerCellArray[_x, _y].Cell.GetComponent<MapCell>().ObjectImage.GetComponent<Image>().sprite = _obData.ObjectImage;
    }
    public void WinBattle(TowerCell _towerCell)
    {
        MapCell mapCell = _towerCell.Cell.GetComponent<MapCell>();

        if (mapCell.towerObjectData.monsterData.MonsterID == 386 || mapCell.towerObjectData.monsterData.MonsterID == 395 || mapCell.towerObjectData.monsterData.MonsterID == 404)
        {
            ChangeMapCell(towerCellArray[mapCell.X, mapCell.Y+1].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
        }
        ChangeMapCell(mapCell, fieldDummies[curTowerNum]);

    }
    public void GetItemTextOn(TowerObjectData towerObjectData, string _PlusText="",bool noName=false)
    {
        if (GetItemUI.activeInHierarchy)
        {
            GetItemUI.GetComponent<UIAutoClose>().OpenUI();
        }
        else
        {
            GetItemUI.SetActive(true);
        }

        switch (Options.S.language)
        {
            case Options.Language.Kor:
                if (noName)
                {
                    getItemName.text =_PlusText;
                }
                else
                {
                    getItemName.text = towerObjectData.objectName + "를 획득하였습니다" + _PlusText;
                }

                break;
            case Options.Language.Eng:
                if (noName)
                {
                    getItemName.text = _PlusText;
                }
                else
                {
                    getItemName.text = "You found " + towerObjectData.e_objectName + _PlusText;
                }

                break;
            default:
                break;
        }


    }
    public void GetItemTextOn(string _text)
    {
        if (GetItemUI.activeInHierarchy)
        {
            GetItemUI.GetComponent<UIAutoClose>().OpenUI();
        }
        else
        {
            GetItemUI.SetActive(true);
        }

        getItemName.text = _text;

    }

    public bool cellCheck(TowerCell _towerCell)
    {
        MapCell mapCell = _towerCell.Cell.GetComponent<MapCell>();
        switch (_towerCell.Cell.GetComponent<MapCell>().towerObjectData.ObjectNum)
        {
            case 322:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(55, 56), true, 2);
                return false;
            case 323:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(73, 75), true, 3);
                return false;
            case 324:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(99, 100), true, 5);
                return false;
            case 349:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(131, 132), true, 12);
                return false;
            case 350:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(170, 171), true, 18);
                return false;
            case 351:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(176, 178), true, 19);
                return false;
            case 376:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(199, 200), true, 21);
                return false;
            case 377:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(213, 216), true, 23);
                return false;
            case 378:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(240, 242), true, 25);
                return false;
            case 379:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(261, 264), true, 28);
                return false;
            case 408:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(309, 310), true, 31);
                return false;
            case 409:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(313, 314), true, 32);
                return false;
            case 410:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(317, 320), true, 33);
                return false;
            case 411:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(325, 326), true, 35);
                return false;
            case 412:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(328, 331), true, 36);
                return false;
            case 413:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(336, 342), true, 37);
                return false;
            case 414:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(353, 355), true, 39);
                return false;
            default:
                break;
        }
        if (_towerCell.Cell.GetComponent<MapCell>().towerObjectData.ObjectNum > 300 && _towerCell.Cell.GetComponent<MapCell>().towerObjectData.ObjectNum < 1000)
        {
            Battle.S.GoBattleScene(mapCell.towerObjectData.monsterData, _towerCell);
            return false;
        }
        if (_towerCell.Cell.GetComponent<MapCell>().towerObjectData.ObjectNum > 2000)
        {
            return false;//더미들
        }
        else
        {
            switch (_towerCell.Cell.GetComponent<MapCell>().towerObjectData.ObjectNum)
            {
                case 0:
                    return false;

                case 1://필드
                    return true;
                case 2://필드
                    return true;
                case 3://필드
                    return true;
                case 4://필드
                    return true;
                case 5://필드
                    return true;
                case 6://필드
                    return true;
                case 7://필드
                    return true;
                case 8://필드
                    return true;

                case 11://벽
                    return false;
                case 12://벽
                    return false;
                case 13://벽
                    return false;
                case 14://벽
                    return false;
                case 15://벽
                    return false;
                case 16://벽
                    return false;
                case 17://벽
                    return false;
                case 18://벽
                    return false;

                case 21://액체
                    return false;
                case 24://공허
                    return false;

                case 31://상층계단
                    ChangeFloor(_towerCell, true);
                    SoundManager.S.PlaySE("stair");
                    return false;
                case 32://상층계단2
                    ChangeFloor(_towerCell, true);
                    SoundManager.S.PlaySE("stair");
                    return false;
                case 33://하층계단1
                    ChangeFloor(_towerCell, false);
                    SoundManager.S.PlaySE("stair");
                    return false;
                case 34://하층계단2
                    ChangeFloor(_towerCell, false);
                    SoundManager.S.PlaySE("stair");
                    return false;
                case 35://비밀계단
                    SoundManager.S.PlaySE("warp");
                    SecretStair();
                    return false;
                case 38://타운 워프
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("warp");
                    if (ArtifactManager.S.RingingBloom.able)
                    {
                        Player.S.reviveStack = 1;
                    }

                    TownWarp(Player.S.mainProgress,_towerCell.x,_towerCell.y);
                    return true;

                case 41://화강암문
                    if (Inventory.S.SearchItemCount("돌 열쇠") >= 1)
                    {
                        AddItem.S.SearchItem("돌 열쇠", -1);
                        mapCell.GetComponent<MapCell>().StartCoroutine("DelAni");
                        //ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                        QuestManager.S.SpendCheck(QuestData.SpendType.Key, 1);
                        SoundManager.S.PlaySE("stonedoor");
                        return false;
                    }
                    else if (Player.S.MasterKeyNum > 0)
                    {
                        Player.S.MasterKeyNum -= 1;
                        mapCell.GetComponent<MapCell>().StartCoroutine("DelAni");
                        //ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                        SoundManager.S.PlaySE("stonedoor");
                        QuestManager.S.SpendCheck(QuestData.SpendType.Key, 1);
                    }
                    return false;
                case 42://강철 문
                    if (Inventory.S.SearchItemCount("쇠 열쇠") >= 1)
                    {
                        AddItem.S.SearchItem("쇠 열쇠", -1);
                        mapCell.GetComponent<MapCell>().StartCoroutine("DelAni");
                        SoundManager.S.PlaySE("irondoor");
                        //ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                        QuestManager.S.SpendCheck(QuestData.SpendType.Key, 1);
                        QuestManager.S.CountUpQuestByID(24);
                        return false;
                    }
                    else if (Player.S.MasterKeyNum > 0)
                    {
                        Player.S.MasterKeyNum -= 1;
                        mapCell.GetComponent<MapCell>().StartCoroutine("DelAni");
                        SoundManager.S.PlaySE("irondoor");
                        //ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                        QuestManager.S.SpendCheck(QuestData.SpendType.Key, 1);
                    }
                    if (curTowerNum==2)
                    {
                        for (int i = 0; i < QuestManager.S.AllQuestData.Length; i++)
                        {
                            if (QuestManager.S.AllQuestData[i].questID == 24)
                            {
                                QuestManager.S.AllQuestData[i].questObjects[0].CountUp(1);
                            }
                        }
                    }

                    return false;
                case 43://황금 문
                    if (Inventory.S.SearchItemCount("금 열쇠") >= 1)
                    {
                        AddItem.S.SearchItem("금 열쇠", -1);
                        mapCell.GetComponent<MapCell>().StartCoroutine("DelAni");
                        SoundManager.S.PlaySE("golddoor");
                        //ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                        QuestManager.S.SpendCheck(QuestData.SpendType.Key, 1);
                        return false;
                    }
                    else if (Player.S.MasterKeyNum > 0)
                    {
                        Player.S.MasterKeyNum -= 1;
                        mapCell.GetComponent<MapCell>().StartCoroutine("DelAni");
                        SoundManager.S.PlaySE("golddoor");
                        //ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                        QuestManager.S.SpendCheck(QuestData.SpendType.Key, 1);
                    }
                    return false;
                case 44://수정 문
                    if (Inventory.S.SearchItemCount("보석 열쇠") >= 1)
                    {
                        AddItem.S.SearchItem("보석 열쇠", -1);
                        mapCell.GetComponent<MapCell>().StartCoroutine("DelAni");
                        SoundManager.S.PlaySE("crydoor");
                        //ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                        QuestManager.S.SpendCheck(QuestData.SpendType.Key, 1);
                        QuestManager.S.CountUpQuestByID(34);
                        return false;
                    }
                    else if (Player.S.MasterKeyNum > 0)
                    {
                        Player.S.MasterKeyNum -= 1;
                        mapCell.GetComponent<MapCell>().StartCoroutine("DelAni");
                        SoundManager.S.PlaySE("crydoor");
                        //ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                        QuestManager.S.SpendCheck(QuestData.SpendType.Key, 1);
                    }
                    return false;
                case 45://쇠창살
                    //if (Player.S.STR >= 10)
                    //{
                    //    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    //    return false;
                    //}
                    return false;
                case 46://원판 장치
                    //if (Player.S.DEX >= 10)
                    //{
                    //    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    //    return false;
                    //}
                    return false;
                case 47://요술 장벽
                    //if (Player.S.INT >= 10)
                    //{
                    //    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    //    return false;
                    //}
                    return false;
                case 48://중력장
                    //if (Player.S.END >= 10)
                    //{
                    //    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    //    return false;
                    //}
                    return false;
                case 49://장애물
                    //if (Player.S.END >= 10)
                    //{
                    //    ChangeMapCell(mapCell, fieldDummy);
                    //    return false;
                    //}
                    return false;

                case 51://필드 상점 1
                    TowerMarket.S.TowerMarketOn(0);
                    return false;
                case 52://필드 상점 2
                    TowerMarket.S.TowerMarketOn(1);
                    return false;
                case 53://필드 상점 3
                    TowerMarket.S.TowerMarketOn(3);
                    return false;
                case 54://필드 상점 4
                    if (!TowerVariable.S.floor12Shop)
                    {
                        OneTimeShop.S.ShopUIOn(5);
                    }
                    else
                    {
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                GetItemTextOn("이제 더 안팔아");
                                break;
                            case Options.Language.Eng:
                                GetItemTextOn("no longer sell");
                                break;
                            default:
                                break;
                        }


                    }

                    return false;
                case 55://필드 상점 5
                    TowerMarket.S.TowerMarketOn(5);
                    return false;
                case 56://필드 상점 6
                    TowerMarket.S.TowerMarketOn(6);
                    return false;
                case 57://필드 상점 7
                    TowerMarket.S.TowerMarketOn(7);
                    return false;
                case 58://필드 상점 8
                    TowerMarket.S.TowerMarketOn(8);
                    return false;
                case 59://필드 상점 9
                    TowerMarket.S.TowerMarketOn(10);
                    return false;
                case 1060://필드 상점 10

                    if (TowerVariable.S.market11)
                    {
                        TowerMarket.S.artiMarket.AfterMarketOn();
                    }
                    else
                    {
                        TowerMarket.S.artiMarket.FirstMarketOn();
                    }
                   TowerMarket.S.TowerMarketOn(11);
                    return false;
                case 1061://필드 상점 11
                    TowerMarket.S.TowerMarketOn(12);
                    return false;
                case 1062://필드 상점 12
                    TowerMarket.S.TowerMarketOn(13);
                    return false;
                case 1063://필드 상점 13
                    TowerMarket.S.TowerMarketOn(14);
                    return false;
                case 1064://필드 상점 14
                    if (!TowerVariable.S.floor9Shop2)
                    {
                        TowerMarket.S.artiMarket2.Floor9MarketOn();
                        TowerMarket.S.TowerMarketOn(15);
                    }
                    else
                    {
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                GetItemTextOn("기념품은 한번만 드립니다.");
                                break;
                            case Options.Language.Eng:
                                GetItemTextOn("Souvenirs are given only once.");
                                break;
                            default:
                                break;
                        }


                    }
                    return false;
                case 1065://필드 상점 15
                    if (!TowerVariable.S.floor9Shop)
                    {
                        OneTimeShop.S.ShopUIOn(16);
                    }
                    else
                    {
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                GetItemTextOn("이제 더 없어");
                                break;
                            case Options.Language.Eng:
                                GetItemTextOn("no more");
                                break;
                            default:
                                break;
                        }


                    }
                    return false;
                case 1066://필드 상점 16
                    OneTimeShop.S.ShopUIOn(21);
                    return false;
                case 1067://필드 상점 17
                    OneTimeShop.S.ShopUIOn(22);
                    return false;
                case 1068://필드 상점 18
                    TowerMarket.S.TowerMarketOn(17);
                    return false;
                case 1069://필드 상점 19
                    TowerMarket.S.TowerMarketOn(18);
                    return false;
                case 1070://필드 상점 20

                    if (TowerVariable.S.market20)
                    {
                        TowerMarket.S.artiMarket3.T4AfterMarketOn();
                    }
                    else
                    {
                        TowerMarket.S.artiMarket3.T4FirstMarketOn();
                    }
                    TowerMarket.S.TowerMarketOn(19);
                    return false;
                case 1071://필드 상점 21
                    if (!TowerVariable.S.floor5shop)
                    {
                        OneTimeShop.S.ShopUIOn(25);
                    }
                    else
                    {
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                GetItemTextOn("이제 더 없어");
                                break;
                            case Options.Language.Eng:
                                GetItemTextOn("no more");
                                break;
                            default:
                                break;
                        }


                    }
                    return false;
                case 1072://필드 상점 22
                    TowerMarket.S.TowerMarketOn(20);
                    return false;
                case 1073://필드 상점 23
                    TowerMarket.S.TowerMarketOn(21);
                    return false;
                case 60://튜토 npc
                    NPCEventOn(curTowerNum, curFloorNum, _towerCell.x, _towerCell.y);
                    return false;
                case 61://해골NPC
                    NPCEventOn(curTowerNum, curFloorNum, _towerCell.x, _towerCell.y);
                    return false;
                case 62://낭인NPC
                    NPCEventOn(curTowerNum, curFloorNum, _towerCell.x, _towerCell.y);
                    return false;
                case 63://오크NPC
                    NPCEventOn(curTowerNum, curFloorNum, _towerCell.x, _towerCell.y);
                    return false;
                case 64://마녀NPC
                    NPCEventOn(curTowerNum, curFloorNum, _towerCell.x, _towerCell.y);
                    return false;
                case 65://악마NPC
                    NPCEventOn(curTowerNum, curFloorNum, _towerCell.x, _towerCell.y);
                    return false;
                case 66://로봇NPC
                    NPCEventOn(curTowerNum, curFloorNum, _towerCell.x, _towerCell.y);
                    return false;
                case 67://인간1NPC
                    NPCEventOn(curTowerNum, curFloorNum, _towerCell.x, _towerCell.y);
                    return false;
                case 68://인간2NPC
                    NPCEventOn(curTowerNum, curFloorNum, _towerCell.x, _towerCell.y);
                    return false;
                case 69://2탑 페어리NPC
                    NPCEventOn(curTowerNum, curFloorNum, _towerCell.x, _towerCell.y);
                    return false;
                case 70://스킬세팅
                    NPCEventOn(curTowerNum, curFloorNum, _towerCell.x, _towerCell.y);
                    return false;

                case 71://쇠못 함정
                    if (ArtifactManager.S.SpikeBoots.able)
                    {
                        return true;
                    }
                    SoundManager.S.PlaySE("trap1");
                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            GetItemTextOn("쇠못 함정! HP가 25 감소되었습니다");
                            break;
                        case Options.Language.Eng:
                            GetItemTextOn("Spike trap! HP -25");
                            break;
                        default:
                            break;
                    }
                    Player.S.hp -= 25;
                    return true;
                case 72://창살 함정
                    if (ArtifactManager.S.SpikeBoots.able)
                    {
                        return true;
                    }
                    Player.S.hp -= 50;
                    SoundManager.S.PlaySE("trap1");
                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            GetItemTextOn("창살 함정! HP가 50 감소되었습니다");
                            break;
                        case Options.Language.Eng:
                            GetItemTextOn("Spear trap! HP -50");
                            break;
                        default:
                            break;
                    }
                    return true;
                case 73://가스 함정
                    if (ArtifactManager.S.SpikeBoots.able)
                    {
                        return true;
                    }
                    return true;
                case 74://용암 함정
                    if (ArtifactManager.S.SpikeBoots.able)
                    {
                        return true;
                    }
                    Player.S.hp -= 50;
                    return true;
                case 75://기계 함정
                    if (ArtifactManager.S.SpikeBoots.able)
                    {
                        return true;
                    }
                    Player.S.hp -= 100;
                    return true;
                case 76://저주 함정
                    if (ArtifactManager.S.SpikeBoots.able)
                    {
                        return true;
                    }
                    Player.S.hp -= Player.S.hp/20;
                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            GetItemTextOn("저주 함정! HP가 5% 감소되었습니다");
                            break;
                        case Options.Language.Eng:
                            GetItemTextOn("Curse trap! HP decrease 5%");
                            break;
                        default:
                            break;
                    }

                    return true;


                case 2000://비밀문더미
                    GoSecretWall(_towerCell);
                    return false;




                case 101://초록 물약
                    Potion_healNum = 150;
                    if (ArtifactManager.S.PirateFlag.able)
                    {
                        Player.S.GetGold(1);
                    }
                    if (ArtifactManager.S.UrnOfLife.able)
                    {
                        Potion_healNum += Potion_healNum/4;
                    }
                    Player.S.hp += Potion_healNum;

                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            GetItemTextOn("HP가 " + Potion_healNum + " 회복되었습니다");
                            break;
                        case Options.Language.Eng:
                            GetItemTextOn("Restore HP " + Potion_healNum);
                            break;
                        default:
                            break;
                    }
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getpotion");
                    return false;
                case 102://푸른 물약
                    if (ArtifactManager.S.PirateFlag.able)
                    {
                        Player.S.GetGold(1);
                    }
                    Potion_healNum = 300;
                    if (ArtifactManager.S.UrnOfLife.able)
                    {
                        Potion_healNum += Potion_healNum / 4;
                    }
                    Player.S.hp += Potion_healNum;

                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            GetItemTextOn("HP가 " + Potion_healNum + " 회복되었습니다");
                            break;
                        case Options.Language.Eng:
                            GetItemTextOn("Restore HP " + Potion_healNum);
                            break;
                        default:
                            break;
                    }
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getpotion");
                    return false;
                case 103://붉은 물약
                    if (ArtifactManager.S.PirateFlag.able)
                    {
                        Player.S.GetGold(1);
                    }
                    Potion_healNum = 500;
                    if (ArtifactManager.S.UrnOfLife.able)
                    {
                        Potion_healNum += Potion_healNum / 4;
                    }
                    Player.S.hp += Potion_healNum;

                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            GetItemTextOn("HP가 " + Potion_healNum + " 회복되었습니다");
                            break;
                        case Options.Language.Eng:
                            GetItemTextOn("Restore HP " + Potion_healNum);
                            break;
                        default:
                            break;
                    }
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getpotion");
                    return false;
                case 104://흰 물약
                    if (ArtifactManager.S.PirateFlag.able)
                    {
                        Player.S.GetGold(1);
                    }
                    Potion_healNum = 750;
                    if (ArtifactManager.S.UrnOfLife.able)
                    {
                        Potion_healNum += Potion_healNum / 4;
                    }
                    Player.S.hp += Potion_healNum;

                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            GetItemTextOn("HP를 " + Potion_healNum + " 회복했습니다");
                            break;
                        case Options.Language.Eng:
                            GetItemTextOn("Restored " + Potion_healNum+" HP");
                            break;
                        default:
                            break;
                    }
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getpotion");
                    return false;
                case 105://레이어드 물약
                    if (ArtifactManager.S.PirateFlag.able)
                    {
                        Player.S.GetGold(1);
                    }
                    Potion_healNum = 1200;
                    if (ArtifactManager.S.UrnOfLife.able)
                    {
                        Potion_healNum += Potion_healNum / 4;
                    }
                    Player.S.hp += Potion_healNum;

                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            GetItemTextOn("HP를 " + Potion_healNum + " 회복했습니다.");
                            break;
                        case Options.Language.Eng:
                            GetItemTextOn("Restored " + Potion_healNum + " HP");
                            break;
                        default:
                            break;
                    }
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getpotion");
                    return false;

                case 111://붉은 강화석
                    Player.S.ATK += curTowerNum;
                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            GetItemTextOn(mapCell.towerObjectData, " ATK를 " + curTowerNum + " 획득했습니다.", true);
                            break;
                        case Options.Language.Eng:
                            GetItemTextOn(mapCell.towerObjectData, " You got " + curTowerNum + " ATK", true);
                            break;
                        default:
                            break;
                    }

                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getgem");
                    return false;
                case 112://푸른 강화석
                    Player.S.DEF += curTowerNum;
                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            GetItemTextOn(mapCell.towerObjectData, " DEF를 " + curTowerNum + " 획득했습니다.", true);
                            break;
                        case Options.Language.Eng:
                            GetItemTextOn(mapCell.towerObjectData, " You got " + curTowerNum + " DEF", true);
                            break;
                        default:
                            break;
                    }

                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getgem");
                    return false;
                case 113://초록 강화석
                    Player.S.SPD += 1;
                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            GetItemTextOn(mapCell.towerObjectData, " SPD를 " + 1 + " 획득했습니다.", true);
                            break;
                        case Options.Language.Eng:
                            GetItemTextOn(mapCell.towerObjectData, " You got " + 1 + " SPD", true);
                            break;
                        default:
                            break;
                    }

                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getgem");
                    return false;
                case 114://자주석 강화석
                    Player.S.CRC += 1;
                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            GetItemTextOn(mapCell.towerObjectData, " CRC를 " + 1 + " 획득했습니다.", true);
                            break;
                        case Options.Language.Eng:
                            GetItemTextOn(mapCell.towerObjectData, " You got " + 1 + " CRC", true);
                            break;
                        default:
                            break;
                    }
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getgem");
                    return false;
                case 115://검은 강화석
                    Player.S.AVD += 1;
                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            GetItemTextOn(mapCell.towerObjectData, " AVD를 " + 1 + " 획득했습니다.", true);
                            break;
                        case Options.Language.Eng:
                            GetItemTextOn(mapCell.towerObjectData, " You got " + 1 + " AVD", true);
                            break;
                        default:
                            break;
                    }
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getgem");
                    return false;
                case 116://흰 강화석
                    Player.S.POW += 1;
                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            GetItemTextOn(mapCell.towerObjectData, " POW를 " + 1 + " 획득했습니다.", true);
                            break;
                        case Options.Language.Eng:
                            GetItemTextOn(mapCell.towerObjectData, " You got " + 1 + " POW", true);
                            break;
                        default:
                            break;
                    }
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getgem");
                    return false;

                case 121://경험의 유물
                    Player.S.LevelUP();
                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            GetItemTextOn(mapCell.towerObjectData, " 레벨이 " + 1 + " 증가했습니다.", true);
                            break;
                        case Options.Language.Eng:
                            GetItemTextOn(mapCell.towerObjectData, " Level increased by 1.", true);
                            break;
                        default:
                            break;
                    }
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getrelic");
                    return false;
                case 122://운명의 유물
                    Player.S.CRD += 5;
                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            GetItemTextOn(mapCell.towerObjectData, " CRD를 " + 5 + " 획득했습니다.", true);
                            break;
                        case Options.Language.Eng:
                            GetItemTextOn(mapCell.towerObjectData, " You got " + 5 + " CRD", true);
                            break;
                        default:
                            break;
                    }
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getrelic");
                    return false;
                case 123://자격의 유물
                    Player.S.HIT += 1;
                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            GetItemTextOn(mapCell.towerObjectData, " HIT를 " + 1 + " 획득했습니다.", true);
                            break;
                        case Options.Language.Eng:
                            GetItemTextOn(mapCell.towerObjectData, " You got " + 1 + " HIT", true);
                            break;
                        default:
                            break;
                    }
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getrelic");
                    return false;
                case 124://자격의 유물
                    Player.S.gold += 200;
                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            GetItemTextOn(mapCell.towerObjectData, " GOLD를 " + 200 + " 획득했습니다.", true);
                            break;
                        case Options.Language.Eng:
                            GetItemTextOn(mapCell.towerObjectData, " You got " + 200 + " GOLD", true);
                            break;
                        default:
                            break;
                    }
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getrelic");
                    return false;
                case 125://자격의 유물
                    Player.S.exp += 100;
                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            GetItemTextOn(mapCell.towerObjectData, " EXP를 " + 100 + " 획득했습니다.", true);
                            break;
                        case Options.Language.Eng:
                            GetItemTextOn(mapCell.towerObjectData, " You got " + 100 + " EXP", true);
                            break;
                        default:
                            break;
                    }
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getrelic");
                    return false;

                case 151: //무기1
                    GetItemTextOn(mapCell.towerObjectData, " ATK+4");
                    EquipmentUI.S.GetEquipment("날카로운 검");
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getequip");
                    return false;

                case 152: //무기2
                    GetItemTextOn(mapCell.towerObjectData, " ATK+6");
                    EquipmentUI.S.GetEquipment("균형 잡힌 검");
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getequip");
                    return false;
                case 153: //무기3
                    GetItemTextOn(mapCell.towerObjectData, " ATK+5, SPD+1");
                    EquipmentUI.S.GetEquipment("최신예 검");
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getequip");
                    return false;
                case 154: //무기4
                    GetItemTextOn(mapCell.towerObjectData, " ATK+5, POW+5");
                    EquipmentUI.S.GetEquipment("축복받은 검");
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getequip");
                    return false;
                case 155: //무기5
                    GetItemTextOn(mapCell.towerObjectData, " ATK +12, HIT +1");
                    EquipmentUI.S.GetEquipment("불패의 검");
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getequip");
                    return false;

                case 161: //방어구
                    GetItemTextOn(mapCell.towerObjectData, " DEF+3");
                    EquipmentUI.S.GetEquipment("가죽 갑옷");
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getequip");
                    return false;


                case 162: //방어구2
                    GetItemTextOn(mapCell.towerObjectData, " DEF+6, SPD-1");
                    EquipmentUI.S.GetEquipment("사슬 갑옷");
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getequip");
                    return false;
                case 163: //방어구2
                    GetItemTextOn(mapCell.towerObjectData, " DEF+5");
                    EquipmentUI.S.GetEquipment("호버크 아머");
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getequip");
                    return false;
                case 164: //방어구2
                    GetItemTextOn(mapCell.towerObjectData, " DEF+12,SPD-1,POW-1");
                    EquipmentUI.S.GetEquipment("밴디드 메일");
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getequip");
                    return false;
                case 165: //방어구5
                    GetItemTextOn(mapCell.towerObjectData, " DEF +10, SPD +2, POW -2");
                    EquipmentUI.S.GetEquipment("전투용 브리간딘");
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getequip");
                    return false;

                case 172: //악세2
                    GetItemTextOn(mapCell.towerObjectData, " POW+2");
                    EquipmentUI.S.GetEquipment("은 귀걸이");
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getequip");
                    return false;
                case 173: //악세3
                    GetItemTextOn(mapCell.towerObjectData, " DCD+5");
                    EquipmentUI.S.GetEquipment("은반지");
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getequip");
                    return false;

                case 174: //악세4
                    GetItemTextOn(mapCell.towerObjectData, " HP +1250, HIT+5");
                    EquipmentUI.S.GetEquipment("태양 목걸이");
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getequip");
                    return false;
                case 175: //악세5
                    GetItemTextOn(mapCell.towerObjectData, " CRC +5, AVD + 5");
                    EquipmentUI.S.GetEquipment("달의 망토");
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getequip");
                    return false;

                case 136: //스킬 물체
                    int returnExp = 0;
                    List<Skill> skills = new List<Skill>();
                    if (curTowerNum == 1)
                    {
                        switch (curFloorNum)
                        {
                            case 0:
                                skills = SkillDic.S.NormalSkillByRandom("노말");
                                returnExp = 10;
                                if (skills.Count == 0)
                                {
                                    break;
                                }
                                SkillUI.S.GetSkill(skills[0]);
                                break;
                            case 15:
                                skills = SkillDic.S.NormalSkillByRandom("노말");
                                returnExp = 10;
                                if (skills.Count == 0)
                                {
                                    break;
                                }
                                SkillUI.S.GetSkill(skills[0]);
                                break;
                            case 18:
                                switch (Options.S.language)
                                {
                                    case Options.Language.Kor:
                                        TowerMap.S.GetItemTextOn("경험치 15를 획득했습니다");
                                        break;
                                    case Options.Language.Eng:
                                        TowerMap.S.GetItemTextOn("Get 15EXP");
                                        break;
                                    default:
                                        break;
                                }
                                SoundManager.S.PlaySE("getbook");
                                Player.S.GetExp(15);
                                ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                                return false;
                                
                            case 21:
                                switch (Options.S.language)
                                {
                                    case Options.Language.Kor:
                                        TowerMap.S.GetItemTextOn("경험치 15를 획득했습니다");
                                        break;
                                    case Options.Language.Eng:
                                        TowerMap.S.GetItemTextOn("Get 15EXP");
                                        break;
                                    default:
                                        break;
                                }
                                SoundManager.S.PlaySE("getbook");
                                Player.S.GetExp(15);
                                ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                                return false;
                                

                            default:
                                break;
                        }
                    }

                    if (curTowerNum == 2)
                    {
                        switch (curFloorNum)
                        {
                            case -20:
                                if (mapCell.X==4)
                                {
                                    skills = SkillDic.S.NormalSkillByRandom("유니크");
                                    returnExp = 50;
                                    if (skills.Count == 0)
                                    {
                                        break;
                                    }
                                    SkillUI.S.GetSkill(skills[0]);
                                }
                                else
                                {
                                    skills = SkillDic.S.NormalSkillByRandom("레어");
                                    returnExp = 25;
                                    if (skills.Count == 0)
                                    {
                                        break;
                                    }
                                    SkillUI.S.GetSkill(skills[0]);
                                }

                                break;
                            case -18:
                                skills = SkillDic.S.NormalSkillByRandom("레어");
                                returnExp = 50;
                                if (skills.Count == 0)
                                {
                                    break;
                                }
                                SkillUI.S.GetSkill(skills[0]);
                                break;
                            case -17:
                                skills = SkillDic.S.NormalSkillByRandom("레어");
                                returnExp = 25;
                                if (skills.Count == 0)
                                {
                                    break;
                                }
                                SkillUI.S.GetSkill(skills[0]);
                                break;
                            case -7:
                                skills = SkillDic.S.NormalSkillByRandom("레어");
                                returnExp = 50;
                                if (skills.Count == 0)
                                {
                                    break;
                                }
                                SkillUI.S.GetSkill(skills[0]);
                                break;
                            case 0:
                                skills = SkillDic.S.NormalSkillByRandom("노말");
                                returnExp = 15;
                                if (skills.Count == 0)
                                {
                                    break;
                                }
                                SkillUI.S.GetSkill(skills[0]);
                                break;
                            case 9:
                                skills = SkillDic.S.NormalSkillByRandom("노말");
                                returnExp = 15;
                                if (skills.Count == 0)
                                {
                                    break;
                                }
                                SkillUI.S.GetSkill(skills[0]);
                                break;
                            default:
                                break;
                        }
                    }
                    if (curTowerNum == 3)
                    {
                        switch (curFloorNum)
                        {
                           
                            case 8:
                                skills = SkillDic.S.NormalSkillByRandom("레어");
                                returnExp = 30;
                                if (skills.Count == 0)
                                {
                                    break;
                                }
                                SkillUI.S.GetSkill(skills[0]);
                                break;
                            case 10:
                                skills = SkillDic.S.NormalSkillByRandom("유니크");
                                returnExp = 30;
                                if (skills.Count == 0)
                                {
                                    break;
                                }
                                SkillUI.S.GetSkill(skills[0]);
                                break;
                            case 15:
                                skills = SkillDic.S.NormalSkillByRandom("유니크");
                                returnExp = 30;
                                if (skills.Count == 0)
                                {
                                    break;
                                }
                                SkillUI.S.GetSkill(skills[0]);
                                break;
                            default:
                                break;
                        }
                    }
                    if (curTowerNum ==4)
                    {
                        switch (curFloorNum)
                        {
                            case 13:
                                skills = SkillDic.S.NormalSkillByRandom("유니크");
                                returnExp = 30;
                                if (skills.Count == 0)
                                {
                                    break;
                                }
                                SkillUI.S.GetSkill(skills[0]);
                                break;
                            case 18:
                                skills = SkillDic.S.NormalSkillByRandom("유니크");
                                returnExp = 30;
                                if (skills.Count == 0)
                                {
                                    break;
                                }
                                SkillUI.S.GetSkill(skills[0]);
                                break;
                            default:
                                break;
                        }
                    }
                    if (skills.Count == 0)
                    {
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                GetItemTextOn("스킬을 획득하지 못했습니다 EXP+" + returnExp);
                                Player.S.GetExp(returnExp);
                                returnExp = 0;
                                break;
                            case Options.Language.Eng:
                                GetItemTextOn("Failed to acquire skill EXP+"+returnExp);
                                Player.S.GetExp(returnExp);
                                returnExp = 0;
                                break;
                            default:
                                break;
                        }

                        ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    }
                    else
                    {
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                GetItemTextOn("스킬을 획득했습니다 " + skills[0].skillName.ToString());
                                break;
                            case Options.Language.Eng:
                                GetItemTextOn("You Get Skill " + skills[0].skillEName.ToString());
                                break;
                            default:
                                break;
                        }

                        ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    }
                    SoundManager.S.PlaySE("getbook");
                    return false;


                case 137://아티팩트 물체
                    List<Artifact> artifacts = new List<Artifact>();
                    if (curTowerNum == 1)
                    {
                        switch (curFloorNum)
                        {
                            case 2:
                                artifacts = ArtifactManager.S.RandomArtifact("노말", 1);
                                if (artifacts.Count == 0)
                                {
                                    break;
                                }
                                Debug.Log(artifacts[0].artifactName);
                                ArtifactManager.S.GetArtifact(artifacts[0].artifactName);
                                break;
                            case 17:
                                artifacts = ArtifactManager.S.RandomArtifact("레어", 1);
                                if (artifacts.Count == 0)
                                {
                                    break;
                                }
                                ArtifactManager.S.GetArtifact(artifacts[0].artifactName);
                                break;
                            default:
                                break;
                        }
                    }
                    if (curTowerNum == 2)
                    {
                        switch (curFloorNum)
                        {
                            case -20:
                                artifacts = ArtifactManager.S.RandomArtifact("레어", 1);
                                if (artifacts.Count == 0)
                                {
                                    break;
                                }
                                ArtifactManager.S.GetArtifact(artifacts[0].artifactName);
                                break;
                            case -18:
                                artifacts = ArtifactManager.S.RandomArtifact("노말", 1);
                                if (artifacts.Count == 0)
                                {
                                    break;
                                }
                                ArtifactManager.S.GetArtifact(artifacts[0].artifactName);
                                break;
                            case -17:
                                artifacts = ArtifactManager.S.RandomArtifact("레어", 1);
                                if (artifacts.Count == 0)
                                {
                                    break;
                                }
                                ArtifactManager.S.GetArtifact(artifacts[0].artifactName);
                                break;
                            case -7:
                                artifacts = ArtifactManager.S.RandomArtifact("노말", 1);
                                if (artifacts.Count == 0)
                                {
                                    break;
                                }
                                ArtifactManager.S.GetArtifact(artifacts[0].artifactName);
                                break;
                            case 3:
                                artifacts = ArtifactManager.S.RandomArtifact("레어", 1);
                                if (artifacts.Count == 0)
                                {
                                    break;
                                }
                                ArtifactManager.S.GetArtifact(artifacts[0].artifactName);
                                break;
                            case 5:
                                artifacts = ArtifactManager.S.RandomArtifact("노말", 1);
                                if (artifacts.Count == 0)
                                {
                                    break;
                                }
                                Debug.Log(artifacts[0].artifactName);
                                ArtifactManager.S.GetArtifact(artifacts[0].artifactName);
                                break;
                            case 10:
                                artifacts = ArtifactManager.S.RandomArtifact("노말", 1);
                                if (artifacts.Count == 0)
                                {
                                    break;
                                }
                                Debug.Log(artifacts[0].artifactName);
                                ArtifactManager.S.GetArtifact(artifacts[0].artifactName);
                                break;
                            default:
                                break;
                        }
                    }
                    if (curTowerNum == 3)
                    {
                        switch (curFloorNum)
                        {
                            case -5:
                                artifacts = ArtifactManager.S.RandomArtifact("레어", 1);
                                if (artifacts.Count == 0)
                                {
                                    break;
                                }
                                ArtifactManager.S.GetArtifact(artifacts[0].artifactName);
                                break;
                            case -11:
                                artifacts = ArtifactManager.S.RandomArtifact("노말", 1);
                                if (artifacts.Count == 0)
                                {
                                    break;
                                }
                                ArtifactManager.S.GetArtifact(artifacts[0].artifactName);
                                break;
                            case 2:
                                artifacts = ArtifactManager.S.RandomArtifact("유니크", 1);
                                if (artifacts.Count == 0)
                                {
                                    break;
                                }
                                ArtifactManager.S.GetArtifact(artifacts[0].artifactName);
                                break;
                            case 18:
                                artifacts = ArtifactManager.S.RandomArtifact("유니크", 1);
                                if (artifacts.Count == 0)
                                {
                                    break;
                                }
                                ArtifactManager.S.GetArtifact(artifacts[0].artifactName);
                                break;
                            default:
                                break;
                        }
                    }
                    if (curTowerNum == 4)
                    {
                        switch (curFloorNum)
                        {
                            case 13:
                                artifacts = ArtifactManager.S.RandomArtifact("유니크", 1);
                                if (artifacts.Count == 0)
                                {
                                    break;
                                }
                                ArtifactManager.S.GetArtifact(artifacts[0].artifactName);
                                break;
                            case 19:
                                artifacts = ArtifactManager.S.RandomArtifact("유니크", 1);
                                if (artifacts.Count == 0)
                                {
                                    break;
                                }
                                ArtifactManager.S.GetArtifact(artifacts[0].artifactName);
                                break;
                            case 24:
                                artifacts = ArtifactManager.S.RandomArtifact("유니크", 1);
                                if (artifacts.Count == 0)
                                {
                                    break;
                                }
                                ArtifactManager.S.GetArtifact(artifacts[0].artifactName);
                                break;
                            default:
                                break;
                        }
                    }
                    if (curTowerNum == 7)
                    {
                        switch (curFloorNum)
                        {
                            case 3:
                                artifacts = ArtifactManager.S.RandomArtifact("유니크", 1);
                                if (artifacts.Count == 0)
                                {
                                    break;
                                }
                                ArtifactManager.S.GetArtifact(artifacts[0].artifactName);
                                break;
                            case 4:
                                artifacts = ArtifactManager.S.RandomArtifact("레어", 1);
                                if (artifacts.Count == 0)
                                {
                                    break;
                                }
                                ArtifactManager.S.GetArtifact(artifacts[0].artifactName);
                                break;

                            default:
                                break;
                        }
                    }
                    if (artifacts.Count == 0)
                    {
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                GetItemTextOn("해당 등급의 모든 아티팩트를 가지고있습니다.");
                                break;
                            case Options.Language.Eng:
                                GetItemTextOn("You have all the artifacts of that Tier.");
                                break;
                            default:
                                break;
                        }

                        ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    }
                    else
                    {
                        switch (Options.S.language)
                        {
                            case Options.Language.Kor:
                                GetItemTextOn("아티팩트를 획득했습니다. " + artifacts[0].artifactName.ToString());
                                break;
                            case Options.Language.Eng:
                                GetItemTextOn("You Found Artifact "+ artifacts[0].Ename.ToString());
                                break;
                            default:
                                break;
                        }

                    }

                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getarti");
                    return false;

                case 141://돌열쇠
                    AddItem.S.SearchItem("돌 열쇠", 1);
                    GetItemTextOn(mapCell.towerObjectData);
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getkey");
                    return false;
                case 142://쇠열쇠
                    AddItem.S.SearchItem("쇠 열쇠", 1);
                    GetItemTextOn(mapCell.towerObjectData);
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getkey");
                    return false;
                case 143://금열쇠
                    AddItem.S.SearchItem("금 열쇠", 1);
                    GetItemTextOn(mapCell.towerObjectData);
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getkey");
                    return false;
                case 144://보석열쇠
                    AddItem.S.SearchItem("보석 열쇠", 1);
                    GetItemTextOn(mapCell.towerObjectData);
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    SoundManager.S.PlaySE("getkey");
                    return false;
                case 145://해골 열쇠
                    AddItem.S.SearchItem("돌 열쇠", 1);
                    AddItem.S.SearchItem("쇠 열쇠", 1);
                    AddItem.S.SearchItem("금 열쇠", 1);
                    AddItem.S.SearchItem("보석 열쇠", 1);
                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            GetItemTextOn(mapCell.towerObjectData, " 모든 열쇠+1");
                            break;
                        case Options.Language.Eng:
                            GetItemTextOn(mapCell.towerObjectData," All Keys+1");
                            break;
                        default:
                            break;
                    }
                    SoundManager.S.PlaySE("getkey");
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    return false;
                case 181:
                    AddItem.S.SearchItem("비밀방 두루마리", 5);
                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            GetItemTextOn("비밀방 두루마리를 5개 획득했습니다.");
                            break;
                        case Options.Language.Eng:
                            GetItemTextOn("Get Reveal scroll X5");
                            break;
                        default:
                            break;
                    }
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    return false;
                case 182:
                    AddItem.S.SearchItem("연막탄", 10);
                    switch (Options.S.language)
                    {
                        case Options.Language.Kor:
                            GetItemTextOn("연막탄을 10개 획득했습니다.");
                            break;
                        case Options.Language.Eng:
                            GetItemTextOn("Get Smoke bomb X10");
                            break;
                        default:
                            break;
                    }

                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    return false;
                case 183:
                    AddItem.S.SearchItem("신석", 1);
                    GetItemTextOn(mapCell.towerObjectData);
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    return false;
                case 184:
                    AddItem.S.SearchItem("신비한 가루", 1);
                    GetItemTextOn(mapCell.towerObjectData);
                    ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    return false;

                case 191:
                    switch (Player.S.job)
                    {
                        case Player.PlayerJob.Inquisitor:
                            TextBox.S.TextBoxOn(TextBox.S.GetTexts(223, 223), true, 15);
                            break;
                        case Player.PlayerJob.Paladin:
                            TextBox.S.TextBoxOn(TextBox.S.GetTexts(167, 167),true,15);
                            break;
                        case Player.PlayerJob.WitchHunter:
                            TextBox.S.TextBoxOn(TextBox.S.GetTexts(164, 164), true, 15);
                            break;
                        case Player.PlayerJob.NightShadow:
                            break;
                        case Player.PlayerJob.Alchemist:
                            break;
                        case Player.PlayerJob.none:
                            break;
                        default:
                            break;
                    }
                    return false;
                case 192:
                    switch (Player.S.job)
                    {
                        case Player.PlayerJob.Inquisitor:
                            TextBox.S.TextBoxOn(TextBox.S.GetTexts(224, 224), true, 16);
                            break;
                        case Player.PlayerJob.Paladin:
                            TextBox.S.TextBoxOn(TextBox.S.GetTexts(168, 168), true, 16);
                            break;
                        case Player.PlayerJob.WitchHunter:
                            TextBox.S.TextBoxOn(TextBox.S.GetTexts(165, 165), true, 16);
                            break;
                        case Player.PlayerJob.NightShadow:
                            break;
                        case Player.PlayerJob.Alchemist:
                            break;
                        case Player.PlayerJob.none:
                            break;
                        default:
                            break;
                    }
                    return false;
                case 193:
                    switch (Player.S.job)
                    {
                        case Player.PlayerJob.Inquisitor:
                            TextBox.S.TextBoxOn(TextBox.S.GetTexts(225, 225), true, 17);
                            break;
                        case Player.PlayerJob.Paladin:
                            TextBox.S.TextBoxOn(TextBox.S.GetTexts(169, 169), true, 17);
                            break;
                        case Player.PlayerJob.WitchHunter:
                            TextBox.S.TextBoxOn(TextBox.S.GetTexts(166, 166), true, 17);
                            break;
                        case Player.PlayerJob.NightShadow:
                            break;
                        case Player.PlayerJob.Alchemist:
                            break;
                        case Player.PlayerJob.none:
                            break;
                        default:
                            break;
                    }
                    //Player.S.jobClass = 3;
                    //GetItemTextOn(mapCell.towerObjectData);
                    //ChangeMapCell(mapCell, fieldDummies[curTowerNum]);
                    return false;



                default:
                    Debug.Log("존재하지않는 오브젝트입니다.");
                    return false;
            }
        }
    }
    public List<MonsterData> ReturnNowFloorMonsters()
    {
        List<MonsterData> monsterDatas = new List<MonsterData>();
        bool sameName = false;
        for (int i = 0; i < mapCells.Count; i++)
        {
            if (mapCells[i].GetComponent<MapCell>().towerObjectData.objectType == TowerObjectData.ObjectType.Monster)
            {
                for (int j = 0; j < monsterDatas.Count; j++)
                {
                    if (monsterDatas[j].MonsterID == mapCells[i].GetComponent<MapCell>().towerObjectData.monsterData.MonsterID)
                    {
                        sameName = true;
                    }
                }
                if (sameName)
                {
                    sameName = false;
                }
                else
                {
                    monsterDatas.Add(mapCells[i].GetComponent<MapCell>().towerObjectData.monsterData);
                }
            }
        }

        return monsterDatas;
    }
    public void NPCEventOn(int _towerNum, int _floorNum, int _x, int _y)
    {
        int NPCNum = Dictionaries.S.GetNPCData(_towerNum, _floorNum, _x, _y);
        switch (NPCNum)
        {
            case 1:
                ChangeMapCell(towerCellArray[4, 7].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(3, 5));

                Tower0NPC1 = true;
                break;
            case 2:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(6, 9));
                Tower0NPC2 = true;
                break;
            case 3:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(10, 11));
                ChangeMapCell(towerCellArray[4, 6].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);

                break;
            case 4:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(12, 15));
                ChangeMapCell(towerCellArray[4, 1].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                break;

            case 5:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(16, 18));
                break;

            case 6:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(19, 22));
                break;

            case 7:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(23, 25));
                ChangeMapCell(towerCellArray[8, 1].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                break;

            case 8:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(26, 27));
                Player.S.MonsterBookOn = true;
                break;

            case 9:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(28, 29));
                ChangeMapCell(towerCellArray[4, 4].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                break;

            case 10:
                Player.S.MonsterBookOn = true;
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(32, 34));
                break;
            case 11:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(35, 36));
                break;

            case 12:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(37, 39));
                break;
            case 13:
                TowerMarket.S.TowerMarketOn(2);
                TowerVariable.S.npc13 = true;
                break;
            case 14:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(40, 42));
                break;
            case 15:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(43, 46));
                break;
            case 16:
                OneTimeShop.S.ShopUIOn(0);
                break;
            case 17:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(47, 48));
                break;
            case 18:
                Player.S.MoveFloorOn = true;
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(49, 51));
                ChangeMapCell(towerCellArray[1, 8].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                break;
            case 19:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(52, 54));
                ChangeMapCell(towerCellArray[2, 4].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                break;
            case 20:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(60, 62));
                break;
            case 21:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(63, 64));
                break;
            case 22:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(65, 65));
                break;
            case 23:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(66, 70));
                Player.S.SecretWallOn = true;
                AddItem.S.SearchItem("비밀방 두루마리", 10);
                ChangeMapCell(towerCellArray[_x, _y].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);

                break;
            case 24:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(71, 72));
                TowerVariable.S.npc24 = true;
                break;
            case 25:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(78, 81));
                ChangeMapCell(towerCellArray[4, 1].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                break;
            case 26:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(82, 84));
                break;
            case 27:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(85, 87));
                TowerVariable.S.npc27 = true;
                break;
            case 28:
                OneTimeShop.S.ShopUIOn(2);
                break;
            case 29:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(88, 90), true, 9);
                ChangeMapCell(towerCellArray[6, 6].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                break;
            case 30:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(91, 93), true, 10);
                ChangeMapCell(towerCellArray[6, 2].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                break;

            //2탑
            case 31://페어리
                if (Player.S.mainProgress==3)
                {
                    if (!TowerVariable.S.isMeetFairy)
                    {
                        TextBox.S.TextBoxOn(TextBox.S.GetTexts(107, 112));
                        ChangeMapCell(towerCellArray[4, 3].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                        TowerVariable.S.isMeetFairy = true;
                    }
                    else
                    {
                        TextBox.S.TextBoxOn(TextBox.S.GetTexts(113, 114));
                    }

                }
                if (Player.S.mainProgress == 4&&Player.S.jobClass!=0)
                {
                    if (!TowerVariable.S.isClassUp)
                    {
                        TextBox.S.TextBoxOn(TextBox.S.GetTexts(115, 118),true,13);
                        TowerVariable.S.isClassUp = true;
                        ChangeMapCell(towerCellArray[3, 2].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);

                    }
                    else
                    {
                        TextBox.S.TextBoxOn(TextBox.S.GetTexts(119, 119));
                    }

                }

                break;
            case 32:
                TowerMarket.S.TowerMarketOn(4);
                break;
            case 33:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(120, 121));
                break;
            case 34:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(122, 122));
                break;
            case 35:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(123, 124));
                break;
            case 36:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(125, 126));
                break;
            case 37:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(127, 127));
                break;
            case 38:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(128, 130));
                break;
            case 39:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(151, 152));
                break;
            case 40:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(153, 154));
                break;
            case 41:
                OneTimeShop.S.ShopUIOn(6);
                break;
            case 42:
               TextBox.S.TextBoxOn(TextBox.S.GetTexts(155, 155));
                break;
            case 43:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(158, 158));
                break;
            case 44:
                OneTimeShop.S.ShopUIOn(7);
                break;
            case 45:
                OneTimeShop.S.ShopUIOn(8);
                break;
            case 46:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(159, 160));
                break;
            case 47:
                OneTimeShop.S.ShopUIOn(9);
                break;
            case 48:
                OneTimeShop.S.ShopUIOn(10);
                break;

            //3탑
            case 49:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(186, 187));
                ChangeMapCell(towerCellArray[4, 1].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                break;
            case 50:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(188, 189));
                break;
            case 51:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(190, 191));
                break;
            case 52:
                TowerMarket.S.TowerMarketOn(9);
                break;
            case 53:
                OneTimeShop.S.ShopUIOn(14);
                break;
            case 54:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(193, 194));
                break;
            case 55:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(195, 196));
                break;
            case 56:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(197, 198));
                break;

            case 57:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(203, 204));
                break;
            case 58:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(205, 207));
                break;
            case 59:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(209, 211));
                break;
            case 60:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(212, 212));
                break;
            case 61:
                OneTimeShop.S.ShopUIOn(15);
                break;
            case 62:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(226, 229));
                break;
            case 63:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(230, 233));
                break;
            case 64:
                OneTimeShop.S.ShopUIOn(17);
                break;
            case 65:
                OneTimeShop.S.ShopUIOn(18);
                break;
            case 66:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(234, 236));
                break;
            case 67:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(237, 239));
                break;
            case 68:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(247, 250));
                break;
            case 69:
                 OneTimeShop.S.ShopUIOn(19);
                break;
            case 70:
                OneTimeShop.S.ShopUIOn(20);
                break;
            case 71:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(251, 253));
                break;
            case 72:
                if (!TowerVariable.S.opengoldGate)
                {
                    TextBox.S.TextBoxOn(TextBox.S.GetTexts(254, 255));
                }
                else
                {
                    TextBox.S.TextBoxOn(TextBox.S.GetTexts(256, 257));
                }
                TowerVariable.S.npc72 = true;

                break;
            case 73:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(258, 260),true,27);
                ChangeMapCell(towerCellArray[7, 0].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                break;

            //Tower4 Npc
            case 74:
                OneTimeShop.S.ShopUIOn(26);
                break;
            case 75:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(273, 275));
                break;
            case 76:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(276, 277));
                break;
            case 77:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(278, 280));
                break;
            case 78:
                TowerMarket.S.TowerMarketOn(16);
                break;
            case 79:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(281, 281));
                break;
            case 80:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(282, 283));
                break;
            case 81:
                OneTimeShop.S.ShopUIOn(23);
                break;
            case 82:
                OneTimeShop.S.ShopUIOn(24);
                break;
            case 83:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(284, 287));
                break;
            case 84:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(288, 290));
                break;
            case 85:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(291, 291));
                ChangeMapCell(towerCellArray[7, 4].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                break;
            case 86:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(292, 293));
                break;
            case 87:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(294, 295));
                TowerVariable.S.npc87 = true;
                break;
            case 88:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(296, 298),true,30);
                ChangeMapCell(towerCellArray[7, 0].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                break;
            case 89:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(299, 301));
                break;
            case 90:
                OneTimeShop.S.ShopUIOn(27);
                break;
            case 91:
                OneTimeShop.S.ShopUIOn(28);
                break;
            case 92:
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(302, 304));
                break;
            case 93:

                if (TowerVariable.S.GetStats)
                {
                    TextBox.S.TextBoxOn(TextBox.S.GetTexts(307, 307));
                }
                else
                {
                    TextBox.S.TextBoxOn(TextBox.S.GetTexts(305, 308));
                    Player.S.CRC += 2;
                    Player.S.AVD += 2;
                    Player.S.HIT += 1;
                    TowerVariable.S.GetStats = true;
                }
                break;

            case 200:
                OneTimeShop.S.ShopUIOn(29);
                break;







            case 250:
               TextBox.S.TextBoxOn(TextBox.S.GetTexts(161, 163));
                ChangeMapCell(towerCellArray[4, 3].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                break;
            case 251:

                if (Player.S.classUp)
                {
                    TextBox.S.TextBoxOn(TextBox.S.GetTexts(246, 246));
                    ChangeMapCell(towerCellArray[1, 4].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);

                }
                else
                {
                    TextBox.S.TextBoxOn(TextBox.S.GetTexts(156, 157));
                }
               
                break;



            default:
                break;
        }
    }
    public void Tower0Event()
    {
        //0층

        if (curFloorNum == 0)
        {
            if (GetTowerCellObData(4, 5).ObjectNum == 11)
            {
                if (Tower0NPC1 && Tower0NPC2)
                {
                    ChangeMapCell(towerCellArray[4, 5].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                }
            }

            if (currentCell.x==4&&currentCell.y==7&&!TowerVariable.S.isFrontStair)
            {
                TowerVariable.S.isFrontStair = true;
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(135, 137));

            }

        }
        if (curFloorNum == 1)
        {
            if (currentCell.x == 4 && currentCell.y == 5 && !TowerVariable.S.isFrontMonster)
            {
                TowerVariable.S.isFrontMonster = true;
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(148, 150));
                AddItem.S.SearchItem("연막탄", 10);
            }
        }
        if (curFloorNum == 2)
        {
            if (currentCell.x == 7 && currentCell.y == 5 && !TowerVariable.S.isIronWallTile)
            {
                TowerVariable.S.isIronWallTile = true;
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(142, 144));
            }

        }

    }
    public void Tower1Event()
    {
        if (MoveLock)
        {
            return;
        }
        if (curFloorNum == 0)
        {
            if (GetTowerCellObData(4, 3).ObjectNum == 12)
            {
                if (GetTowerCellObData(3, 2).ObjectNum == 2 && GetTowerCellObData(5, 2).ObjectNum == 2)
                {
                    ChangeMapCell(towerCellArray[4, 3].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                    TextBox.S.TextBoxOn(TextBox.S.GetTexts(30, 31), false);
                }
            }
        }
        if (curFloorNum == 8)
        {
            if (GetTowerCellObData(4, 4).ObjectNum == 2 && GetTowerCellObData(7, 4).ObjectNum != 2)
            {
                ChangeMapCell(towerCellArray[7, 4].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);

            }
        }
        if (curFloorNum == 14)
        {
            if (GetTowerCellObData(4, 3).ObjectNum == 2 && GetTowerCellObData(1, 3).ObjectNum == 12)
            {
                for (int i = 1; i < 8; i++)
                {
                    ChangeMapCell(towerCellArray[i, 3].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                }
                for (int i = 3; i < 6; i++)
                {
                    for (int j = 4; j < 7; j++)
                    {
                        ChangeMapCell(towerCellArray[i, j].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                    }

                }
            }
        }
        if (curFloorNum == 21)
        {
            if (!TowerVariable.S.on21Floor)
            {
                TowerVariable.S.on21Floor = true;
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(94, 96), true, 8);
            }
            if (GetTowerCellObData(3, 4).ObjectNum == 12)
            {
                if (TowerVariable.S.npc24)
                {
                    ChangeMapCell(towerCellArray[3, 4].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                }
            }
            if (GetTowerCellObData(4, 4).ObjectNum == 12)
            {
                if (TowerVariable.S.npc13)
                {
                    ChangeMapCell(towerCellArray[4, 4].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                }
            }
            if (GetTowerCellObData(5, 4).ObjectNum == 12)
            {
                if (TowerVariable.S.npc27)
                {
                    ChangeMapCell(towerCellArray[5, 4].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                }
            }
            if (GetTowerCellObData(3, 1).ObjectNum == 2 && GetTowerCellObData(3, 7).ObjectNum == 2 && GetTowerCellObData(6, 4).ObjectNum == 12)
            {
                ChangeMapCell(towerCellArray[6, 4].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(97, 98), false);
            }
        }



    }

    public void Tower2Event()
    {
        //0층

        if (curFloorNum == -1 &&!TowerVariable.S.isFloorB1)
        {
            TowerVariable.S.isFloorB1 = true;
            for (int i = 0; i < QuestManager.S.curQuestData.Count; i++)
            {
                if (QuestManager.S.curQuestData[i].questID == 21)
                {
                    QuestManager.S.curQuestData[i].questObjects[0].CountUp(1);
                }
            }
        }



        if (curFloorNum == 0&&Player.S.mainProgress==4)
        {
            if (GetTowerCellObData(7, 8).ObjectNum == 13)
            {
                ChangeMapCell(towerCellArray[7, 8].Cell.GetComponent<MapCell>(),Dictionaries.S.GetTowerObjectDic(33));
                ChangeMapCell(towerCellArray[7, 7].Cell.GetComponent<MapCell>(),fieldDummies[curTowerNum]);
            }
        }
        if (curFloorNum == 4 && Player.S.mainProgress == 4)
        {
            if (GetTowerCellObData(4, 2).ObjectNum == 13)
            {
                ChangeMapCell(towerCellArray[4, 2].Cell.GetComponent<MapCell>(), Dictionaries.S.GetTowerObjectDic(42));
            }
        }
        if (curFloorNum == 4 && Player.S.mainProgress == 4)
        {
            if (GetTowerCellObData(4, 2).ObjectNum == 3)
            {
                ChangeMapCell(towerCellArray[4, 1].Cell.GetComponent<MapCell>(), Dictionaries.S.GetTowerObjectDic(3));
                ChangeMapCell(towerCellArray[4, 0].Cell.GetComponent<MapCell>(), Dictionaries.S.GetTowerObjectDic(35));
            }
        }
        if (curFloorNum == -10 && Player.S.mainProgress == 4&& GetTowerCellObData(4, 2).ObjectNum == 3)
        {
            if (GetTowerCellObData(4, 7).ObjectNum == 13)
            {
                ChangeMapCell(towerCellArray[4, 3].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                ChangeMapCell(towerCellArray[4, 7].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
            }
        }
        if (curFloorNum == -10 && Player.S.mainProgress == 5 && GetTowerCellObData(4, 7).ObjectNum == 13)
        {
                ChangeMapCell(towerCellArray[4, 7].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
        }
        if (curFloorNum==10&&Player.S.mainProgress>=3)
        {
            TowerVariable.S.on10Floor = true;
            QuestManager.S.CountUpQuestByID(16);
        }
        if (curFloorNum == -20 && Player.S.mainProgress == 5 && GetTowerCellObData(4, 6).ObjectNum == 13)
        {
            if (TowerVariable.S.npc47&&TowerVariable.S.npc48&&TowerVariable.S.killVamLord>=4)
            {
                ChangeMapCell(towerCellArray[4, 6].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
            }

        }
        if (TowerVariable.S.killVamLord>=4&&!TowerVariable.S.killVamEvent)
        {
            TextBox.S.TextBoxOn(TextBox.S.GetTexts(174, 175), false);
            TowerVariable.S.killVamEvent = true;
        }
        if (curFloorNum==-7&&Player.S.mainProgress==5&& GetTowerCellObData(1, 4).ObjectNum == 251&&TowerVariable.S.isClassUp)
        {
            ChangeMapCell(towerCellArray[1, 4].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
        }
    }
    public void Tower7Event()
    {
        //0층

        if (curFloorNum == 2)
        {
            if (GetTowerCellObData(1, 7).ObjectNum == 3|| GetTowerCellObData(4, 7).ObjectNum == 3 || GetTowerCellObData(7, 7).ObjectNum == 3)
            {
                ChangeMapCell(towerCellArray[1, 7].Cell.GetComponent<MapCell>(), Dictionaries.S.GetTowerObjectDic(24));
                ChangeMapCell(towerCellArray[4, 7].Cell.GetComponent<MapCell>(), Dictionaries.S.GetTowerObjectDic(24));
                ChangeMapCell(towerCellArray[7, 7].Cell.GetComponent<MapCell>(), Dictionaries.S.GetTowerObjectDic(24));

            }
        }

    }
    public void Tower3Event()
    {
        if (curFloorNum == -14)
        {
            if (GetTowerCellObData(3, 3).ObjectNum == 4 && GetTowerCellObData(3, 5).ObjectNum == 4
                && GetTowerCellObData(2, 3).ObjectNum == 14)
            {
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(208, 208), false);
                ChangeMapCell(towerCellArray[2, 3].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                ChangeMapCell(towerCellArray[2, 4].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                ChangeMapCell(towerCellArray[2, 5].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
            }
        }
        if (curFloorNum==-20&&!TowerVariable.S.isfloorB20&&Player.S.mainProgress==7)
        {
            TowerVariable.S.isfloorB20 = true;
            TextBox.S.TextBoxOn(TextBox.S.GetTexts(220, 221), false);
        }
        if (curFloorNum == -21&&!TowerVariable.S.isgetallitemB21)
        {
            if (GetTowerCellObData(0, 0).ObjectNum == 4&& GetTowerCellObData(2, 0).ObjectNum == 4
                &&GetTowerCellObData(0, 6).ObjectNum == 4&& GetTowerCellObData(5, 8).ObjectNum == 4
                && GetTowerCellObData(8, 2).ObjectNum == 4)
            {
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(222, 222), false);
                TowerVariable.S.isgetallitemB21 = true;
            }
        }
        if (curFloorNum == -20&&TowerVariable.S.isgetallitemB21&& GetTowerCellObData(4, 3).ObjectNum == 14)
        {
            ChangeMapCell(towerCellArray[4, 3].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
        }
        if (curFloorNum == -21 && TowerVariable.S.changeStair && GetTowerCellObData(0, 1).ObjectNum == 4)
        {
            ChangeMapCell(towerCellArray[0, 1].Cell.GetComponent<MapCell>(),Dictionaries.S.GetTowerObjectDic(32));
        }
        if (curFloorNum == 0)
        {
            if (GetTowerCellObData(4, 6).ObjectNum == 14&& Player.S.mainProgress>=8)
            {
                ChangeMapCell(towerCellArray[4, 6].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
            }
        }
        if (curFloorNum == 9)
        {
            QuestManager.S.CountUpQuestByID(39);
        }
        if (curFloorNum == 13)
        {
            if (GetTowerCellObData(2, 3).ObjectNum == 4 && GetTowerCellObData(2, 5).ObjectNum == 4)
            {
                if (GetTowerCellObData(3, 4).ObjectNum == 14)
                {
                    ChangeMapCell(towerCellArray[3, 4].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                }
            }
        }
        if (curFloorNum == 18)
        {
            if (GetTowerCellObData(1, 5).ObjectNum == 4 &&
                GetTowerCellObData(2, 5).ObjectNum == 4 &&
                GetTowerCellObData(4, 5).ObjectNum == 4 &&
                GetTowerCellObData(3, 4).ObjectNum == 4 &&
                GetTowerCellObData(3, 1).ObjectNum == 4 &&
                GetTowerCellObData(3, 2).ObjectNum == 4 &&
                GetTowerCellObData(4, 3).ObjectNum == 4 &&
                GetTowerCellObData(4, 4).ObjectNum == 4 &&
                GetTowerCellObData(5, 4).ObjectNum == 4 &&
                GetTowerCellObData(5, 6).ObjectNum == 4 &&
                GetTowerCellObData(5, 7).ObjectNum == 4 &&
                GetTowerCellObData(6, 3).ObjectNum == 4 &&
                GetTowerCellObData(7, 3).ObjectNum == 4 )
            {
                if (GetTowerCellObData(1, 7).ObjectNum == 14)
                {
                    ChangeMapCell(towerCellArray[1, 7].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                    TowerVariable.S.opengoldGate = true;
                    TextBox.S.TextBoxOn(TextBox.S.GetTexts(256, 257),false);

                }
            }
        }
        if (curFloorNum == 19)
        {
            if (GetTowerCellObData(4, 1).ObjectNum == 14)
            {
                if (TowerVariable.S.golemKillCount>=11)
                {
                    ChangeMapCell(towerCellArray[4, 1].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                }
            }
            if (GetTowerCellObData(4, 3).ObjectNum == 14)
            {
                if (TowerVariable.S.npc72)
                {
                    ChangeMapCell(towerCellArray[4, 3].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                }
            }
            if (GetTowerCellObData(4, 2).ObjectNum == 14)
            {
                if (TowerVariable.S.T7F4)
                {
                    ChangeMapCell(towerCellArray[4, 2].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                }
            }
        }
    }
    public void Tower4Event()
    {
        if (curFloorNum == -2 && Player.S.mainProgress == 13)
        {
            if (GetTowerCellObData(4, 7).ObjectNum == 15)
            {
                ChangeMapCell(towerCellArray[4, 7].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
            }
        }
        if (curFloorNum == 0)
        {
            if (GetTowerCellObData(1, 0).ObjectNum == 15 && GetTowerCellObData(7, 0).ObjectNum == 15
                && TowerVariable.S.KillLady)
            {
                ChangeMapCell(towerCellArray[1, 0].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                ChangeMapCell(towerCellArray[7, 0].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
            }
        }
        if (curFloorNum == 6)
        {
            if (TowerVariable.S.KillMonsterInFloor6>=18&& GetTowerCellObData(4, 1).ObjectNum == 15)
            {
                ChangeMapCell(towerCellArray[4, 1].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
            }
        }
        if (curFloorNum == 11&&Player.S.mainProgress==12)
        {
            if (GetTowerCellObData(4, 8).ObjectNum == 15&& GetTowerCellObData(4, 0).ObjectNum == 15)
            {
                ChangeMapCell(towerCellArray[4, 8].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                ChangeMapCell(towerCellArray[4, 0].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
            }
        }
        if (curFloorNum == -4 && Player.S.mainProgress == 14)
        {
            if (GetTowerCellObData(3, 8).ObjectNum == 15&& GetTowerCellObData(5, 8).ObjectNum == 15)
            {
                ChangeMapCell(towerCellArray[3, 8].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                ChangeMapCell(towerCellArray[5, 8].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
            }
        }
        if (curFloorNum == 15 && TowerVariable.S.npc87)
        {
            if (GetTowerCellObData(3, 4).ObjectNum == 15)
            {
                ChangeMapCell(towerCellArray[3, 4].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
            }
        }

        if (curFloorNum == 24)
        {
            if (TowerVariable.S.floor24==false)
            {
                TowerVariable.S.floor24 = true;
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(347, 349), false);
            }
            if (TowerVariable.S.KillMonsterInFloor24 >= 11 && GetTowerCellObData(4, 5).ObjectNum == 15)
            {
                ChangeMapCell(towerCellArray[4, 5].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                ChangeMapCell(towerCellArray[3, 6].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                ChangeMapCell(towerCellArray[5, 6].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(350, 352), false);
            }
        }
    }


    public void PlayerImageChange(int num)//0=우,1=좌,2=위,3=아래
    {
        switch (num)
        {
            case 0:
                if (playerDirection != PlayerDirection.Right)
                {
                    // playerImageNum = 0;
                }
                playerImageNum += 1;
                if (playerImageNum >= RightImage.Length)
                {
                    playerImageNum = 1;
                }
                TowerPlayer.GetComponent<Image>().sprite = RightImage[playerImageNum];
                playerDirection = PlayerDirection.Right;
                break;
            case 1:
                if (playerDirection != PlayerDirection.Left)
                {
                    // playerImageNum = 0;
                }
                playerImageNum += 1;
                if (playerImageNum >= LeftImage.Length)
                {
                    playerImageNum = 1;
                }
                TowerPlayer.GetComponent<Image>().sprite = LeftImage[playerImageNum];
                playerDirection = PlayerDirection.Left;
                break;
            case 2:
                if (playerDirection != PlayerDirection.Up)
                {
                    // playerImageNum = 0;
                }
                playerImageNum += 1;
                if (playerImageNum >= UpImage.Length)
                {
                    playerImageNum = 1;
                }
                TowerPlayer.GetComponent<Image>().sprite = UpImage[playerImageNum];
                playerDirection = PlayerDirection.Up;
                break;
            case 3:
                if (playerDirection != PlayerDirection.Down)
                {
                    //playerImageNum = 0;
                }
                playerImageNum += 1;
                if (playerImageNum >= DownImage.Length)
                {
                    playerImageNum = 1;
                }
                TowerPlayer.GetComponent<Image>().sprite = DownImage[playerImageNum];
                playerDirection = PlayerDirection.Down;
                break;
            default:
                break;
        }
        rollBackTime = 0f;
    }
    public void PlayerImageRollBack()
    {
        if (playerImageNum > 0)
        {
            rollBackTime += 1 * Time.deltaTime;
            if (rollBackTime >= 0.5f)
            {
                switch (playerDirection)
                {
                    case PlayerDirection.Up:
                        TowerPlayer.GetComponent<Image>().sprite = UpImage[0];
                        break;
                    case PlayerDirection.Down:
                        TowerPlayer.GetComponent<Image>().sprite = DownImage[0];
                        break;
                    case PlayerDirection.Right:
                        TowerPlayer.GetComponent<Image>().sprite = RightImage[0];
                        break;
                    case PlayerDirection.Left:
                        TowerPlayer.GetComponent<Image>().sprite = LeftImage[0];
                        break;
                    default:
                        break;
                }
                playerImageNum = 0;
                rollBackTime = 0f;
            }
        }


    }
    public TowerObjectData GetTowerCellObData(int _x, int _y)
    {
        return towerCellArray[_x, _y].Cell.GetComponent<MapCell>().towerObjectData;
    }
    public void SecretWall()
    {
        AddItem.S.SearchItem("비밀방 두루마리", -1);
        if (Player.S.mainProgress>=6)
        {
            if (QuestManager.S.curQuestData.Find(x => x.questID == 29) == null)
            {
                
            }else if(!QuestManager.S.curQuestData.Find(x => x.questID == 29).isClear)
            {
                QuestManager.S.CountUpQuestByID(29, 1);
            }

        }
        if (curTowerNum == 1)
        {
            if (curFloorNum == 5 && GetTowerCellObData(1, 8).ObjectNum == 12)
            {
                if ((currentCell.x == 0 && currentCell.y == 8))
                {
                    scretWallOpen(1, 8);
                    return;
                }

            }
            if (curFloorNum == 12 && GetTowerCellObData(8, 5).ObjectNum == 12)
            {
                if ((currentCell.x == 7 && currentCell.y == 5))
                {
                    scretWallOpen(8, 5);
                    return;
                }

            }
            if (curFloorNum == 18 && GetTowerCellObData(4, 6).ObjectNum == 12)
            {
                if ((currentCell.x == 3 && currentCell.y == 6))
                {
                    scretWallOpen(4, 6);
                    return;
                }

            }
            if (curFloorNum == 18 && GetTowerCellObData(2, 4).ObjectNum == 12)
            {
                if ((currentCell.x == 2 && currentCell.y == 5))
                {
                    scretWallOpen(2, 4);
                    return;
                }

            }
        }
        if (curTowerNum == 2)
        {
            if (curFloorNum == 4 && GetTowerCellObData(3, 6).ObjectNum == 13)
            {
                if ((currentCell.x == 3 && currentCell.y == 5) || (currentCell.x == 2 && currentCell.y == 6))
                {
                    scretWallOpen(3, 6);
                    return;
                }

            }
            if (curFloorNum == 4 && GetTowerCellObData(5, 4).ObjectNum == 13)
            {
                if ((currentCell.x == 4 && currentCell.y == 4) || (currentCell.x == 5 && currentCell.y == 3))
                {
                    scretWallOpen(5, 4);
                    return;
                }

            }
            if (curFloorNum == 4 && GetTowerCellObData(7, 2).ObjectNum == 13)
            {
                if ((currentCell.x == 3 && currentCell.y == 5) || (currentCell.x == 7 && currentCell.y == 1))
                {
                    scretWallOpen(7, 2);
                    return;
                }

            }
            if (curFloorNum == 4 && GetTowerCellObData(4, 1).ObjectNum == 13)
            {
                if ((currentCell.x == 4 && currentCell.y == 2))
                {
                    scretWallOpen(4, 1);
                    return;
                }

            }
            if (curFloorNum == 4 && GetTowerCellObData(4, 0).ObjectNum == 13)
            {
                if ((currentCell.x == 4 && currentCell.y == 1))
                {
                    scretWallOpen(4, 0);
                    return;
                }

            }
            if (curFloorNum == -11 && GetTowerCellObData(1, 0).ObjectNum == 13)
            {
                if ((currentCell.x == 2 && currentCell.y == 0))
                {
                    scretWallOpen(1, 0);
                    return;
                }

            }
        }
        if (curTowerNum == 3)
        {
            if (curFloorNum == -3 && GetTowerCellObData(7, 3).ObjectNum == 14)
            {
                if ((currentCell.x == 7 && currentCell.y == 4))
                {
                    ChangeMapCell(7, 1, fieldDummies[curTowerNum]);
                    ChangeMapCell(7, 2, fieldDummies[curTowerNum]);
                    ChangeMapCell(7, 3, fieldDummies[curTowerNum]);
                    scretWallOpen(7, 0);
                    return;
                }

            }
            if (curFloorNum == -6 && GetTowerCellObData(1, 7).ObjectNum == 14)
            {
                if ((currentCell.x == 1 && currentCell.y == 6))
                {
                    scretWallOpen(1, 7);
                    return;
                }

            }
            if (curFloorNum == -20 && GetTowerCellObData(1, 0).ObjectNum == 14)
            {
                if ((currentCell.x == 0 && currentCell.y == 0))
                {
                    scretWallOpen(1, 0);
                    return;
                }

            }
            if (curFloorNum == 5 && GetTowerCellObData(2, 4).ObjectNum == 14)
            {
                if ((currentCell.x == 3 && currentCell.y == 4))
                {
                    scretWallOpen(2, 4);
                    return;
                }

            }
            if (curFloorNum == 5 && GetTowerCellObData(6, 4).ObjectNum == 14)
            {
                if ((currentCell.x == 5 && currentCell.y == 4))
                {
                    scretWallOpen(6, 4);
                    scretWallOpen(7, 4);
                    return;
                }

            }
            if (curFloorNum == 16 && GetTowerCellObData(7, 8).ObjectNum == 14)
            {
                if ((currentCell.x == 7 && currentCell.y == 7))
                {
                    scretWallOpen(7, 8);
                    return;
                }

            }
            if (curFloorNum == 15 )
            {
                if ((currentCell.x == 2 && currentCell.y == 4) && GetTowerCellObData(3, 4).ObjectNum == 14)
                {
                    scretWallOpen(3, 4);
                    return;
                }
                if ((currentCell.x == 6 && currentCell.y == 4) && GetTowerCellObData(5, 4).ObjectNum == 14)
                {
                    scretWallOpen(5, 4);
                    return;
                }
                if ((currentCell.x == 4 && currentCell.y == 2) && GetTowerCellObData(4, 3).ObjectNum == 14)
                {
                    scretWallOpen(4, 3);
                    return;
                }
                if ((currentCell.x == 4 && currentCell.y == 6) && GetTowerCellObData(4, 5).ObjectNum == 14)
                {
                    scretWallOpen(4, 5);
                    return;
                }


            }

        }
        if (curTowerNum == 4)
        {
            if (curFloorNum == 3 && GetTowerCellObData(2, 7).ObjectNum == 15)
            {
                if ((currentCell.x == 1 && currentCell.y == 7))
                {
                    ChangeMapCell(3, 7, fieldDummies[curTowerNum]);
                    scretWallOpen(2, 7);
                    return;
                }

            }
            if (curFloorNum == 3 && GetTowerCellObData(6, 7).ObjectNum == 15)
            {
                if ((currentCell.x == 7 && currentCell.y == 7))
                {
                    ChangeMapCell(5, 7, fieldDummies[curTowerNum]);
                    scretWallOpen(6, 7);
                    return;
                }

            }
            if (curFloorNum == 8 && GetTowerCellObData(7, 4).ObjectNum == 15)
            {
                if ((currentCell.x == 6 && currentCell.y == 4))
                {
                    ChangeMapCell(7, 4, fieldDummies[curTowerNum]);
                    scretWallOpen(8, 4);
                    return;
                }

            }
            if (curFloorNum == 19 && (GetTowerCellObData(4, 4).ObjectNum == 15||
                GetTowerCellObData(4, 6).ObjectNum == 15 ||
                GetTowerCellObData(4, 7).ObjectNum == 15 ||
                GetTowerCellObData(4, 8).ObjectNum == 15 ||
                GetTowerCellObData(4, 1).ObjectNum == 15 ||
                GetTowerCellObData(4, 2).ObjectNum == 15 ||
                GetTowerCellObData(4, 3).ObjectNum == 15
                ))
            {
                if ((currentCell.x == 4 && currentCell.y == 5))
                {
                    ChangeMapCell(4, 6, fieldDummies[curTowerNum]);
                    scretWallOpen(4, 4);
                    return;
                }
                if ((currentCell.x == 4 && currentCell.y == 6))
                {
                    scretWallOpen(4, 7);
                    return;
                }
                if ((currentCell.x == 4 && currentCell.y == 7))
                {
                    scretWallOpen(4, 8);
                    return;
                }
                if ((currentCell.x == 4 && currentCell.y == 4))
                {
                    scretWallOpen(4, 3);
                    return;
                }
                if ((currentCell.x == 4 && currentCell.y == 3))
                {
                    scretWallOpen(4, 2);
                    return;
                }
                if ((currentCell.x == 4 && currentCell.y == 2))
                {
                    scretWallOpen(4, 1);
                    return;
                }

            }
            if (curFloorNum == 23 && GetTowerCellObData(4, 7).ObjectNum == 15)
            {
                if ((currentCell.x == 4 && currentCell.y == 6))
                {
                    scretWallOpen(4, 7);
                    return;
                }

            }



        }
        switch (Options.S.language)
        {
            case Options.Language.Kor:
                GetItemTextOn("비밀방을 찾지 못했습니다.두루마리 -1");
                break;
            case Options.Language.Eng:
                GetItemTextOn("Cannot found Chamber. Scroll -1");
                break;
            default:
                break;
        }
        SecretWallUIClose();

    }
    public void SecretWallUIOpen()
    {
        secretWallUI.SetActive(true);
        MoveLock = true;
    }
    public void SecretWallUIClose()
    {
        secretWallUI.SetActive(false);
        MoveLock = false;
    }

    public void scretWallOpen(int _x, int _y)
    {


        secretWallUI.SetActive(false);

        if (curTowerNum == 2 & curFloorNum == 4&&Player.S.mainProgress>=4&&_x==4&&_y==0)
        {
            ChangeMapCell(towerCellArray[4, 0].Cell.GetComponent<MapCell>(), Dictionaries.S.GetTowerObjectDic(35));
        }
        else if (curTowerNum==3&&curFloorNum == -3 && Player.S.mainProgress >= 6 && _x == 7 && _y == 0)
        {
            ChangeMapCell(towerCellArray[7, 0].Cell.GetComponent<MapCell>(), Dictionaries.S.GetTowerObjectDic(35));
        }
        else if (curFloorNum == -20 && curTowerNum==3 && _x == 1 && _y == 0)
        {
            ChangeMapCell(towerCellArray[1, 0].Cell.GetComponent<MapCell>(), Dictionaries.S.GetTowerObjectDic(34));
            TowerVariable.S.changeStair = true;
        }
        else if (curFloorNum == 5 && curTowerNum == 3 && _x == 7 && _y == 4)
        {
            ChangeMapCell(towerCellArray[7, 4].Cell.GetComponent<MapCell>(), Dictionaries.S.GetTowerObjectDic(35));
        }
        else if (curFloorNum == 16 && curTowerNum == 3 && _x == 7 && _y == 8)
        {
            ChangeMapCell(towerCellArray[7, 8].Cell.GetComponent<MapCell>(), Dictionaries.S.GetTowerObjectDic(35));
        }
        else if (curFloorNum == 8 && curTowerNum == 4 && _x == 8 && _y == 4)
        {
            ChangeMapCell(towerCellArray[8, 4].Cell.GetComponent<MapCell>(), Dictionaries.S.GetTowerObjectDic(35));
        }
        else if (curFloorNum == 19 && curTowerNum == 4 && _x == 4 && _y == 8)
        {
            ChangeMapCell(towerCellArray[4, 8].Cell.GetComponent<MapCell>(), Dictionaries.S.GetTowerObjectDic(35));
        }
        else
        {
            ChangeMapCell(towerCellArray[_x, _y].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
        }


        switch (Options.S.language)
        {
            case Options.Language.Kor:
                GetItemTextOn("비밀문을 열었습니다.");
                break;
            case Options.Language.Eng:
                GetItemTextOn("Success to Find Chamber.");
                break;
            default:
                break;
        }
        MoveLock = false;
        SecretWallUIClose();
    }

    public void LoadMapByPogress()
    {
        switch (Player.S.mainProgress)
        {
            case 0:
                LoadMap(0, 0, 4, 0);
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(0, 2), true);
                break;

            case 1:
                LoadMap(1, 0, 4, 0);
                break;
            case 2:
                LoadMap(1, 15, 3, 1);
                break;
            case 3:
                LoadMap(2, 0, 4, 0);
                break;
            case 4:
                LoadMap(2, 10, 8, 8);
                break;
            case 5:
                LoadMap(2, -10, 4, 6);
                break;
            case 6:
                LoadMap(3, 0, 4, 4);
                TextBox.S.TextBoxOn(TextBox.S.GetTexts(184, 185), true);
                break;
            case 7:
                LoadMap(3, -11, 8, 4);
                break;
            case 8:
                LoadMap(3, -19, 6, 6);
                break;
            case 9:
                LoadMap(3, 10, 8, 1);
                break;
            case 10:
                LoadMap(4, 0, 4, 0);
                break;
            case 11:
                LoadMap(4, 7, 7, 8);
                break;
            case 12:
                LoadMap(4, -2, 4, 4);
                break;
            case 13:
                LoadMap(4, 15, 4, 4);
                break;
            case 14:
                LoadMap(4, 23, 4, 6);
                break;
            default:
                break;
        }
    }

    public void SetTowerMoveImage(Player.PlayerJob _job)
    {
        switch (_job)
        {
            case Player.PlayerJob.Inquisitor:
                UpImage = MoveImage_Inquisitor.up;
                DownImage = MoveImage_Inquisitor.down;
                LeftImage = MoveImage_Inquisitor.left;
                RightImage = MoveImage_Inquisitor.right;
                break;
            case Player.PlayerJob.Paladin:
                UpImage = MoveImage_crusader.up;
                DownImage = MoveImage_crusader.down;
                LeftImage = MoveImage_crusader.left;
                RightImage = MoveImage_crusader.right;
                break;
            case Player.PlayerJob.WitchHunter:
                UpImage = MoveImage_witch.up;
                DownImage = MoveImage_witch.down;
                LeftImage = MoveImage_witch.left;
                RightImage = MoveImage_witch.right;
                break;
            case Player.PlayerJob.NightShadow:
                break;
            case Player.PlayerJob.Alchemist:
                break;
            case Player.PlayerJob.none:
                break;
            default:
                break;
        }
    }
    public void PlayTowerBGM()
    {
        string bgmName="tower"+curTowerNum.ToString();
        SoundManager.S.PlayBG(bgmName);

    }
    public void SecretStair()
    {
        if (curTowerNum==2&&curFloorNum==4)
        {
            if (TowerVariable.S.isSecretStair)
            {
                CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
                LoadMap(7, 0, 4, 0);
                return;
            }
            else
            {
                TowerStory.S.StartStory(4);
                TowerVariable.S.isSecretStair = true;
                return;
            }

        }
        if (curTowerNum == 7 && curFloorNum == 0)
        {
            CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
            LoadMap(2, 4, 4, 0);
            return;
        }


        if (curTowerNum == 3 && curFloorNum == -3)
        {
            CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
            LoadMap(7, 3, 4,1);
            return;


        }
        if (curTowerNum == 7 && curFloorNum == 3)
        {
            CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
            LoadMap(3, -3, 7, 0);
            return;
        }

        if (curTowerNum == 3 && curFloorNum == 5)
        {
            CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
            LoadMap(7, 4, 4, 1);
            TowerVariable.S.T7F4 = true;
            return;


        }
        if (curTowerNum == 7 && curFloorNum == 4)
        {
            CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
            LoadMap(3, 5, 7, 4);
            return;
        }

        if (curTowerNum == 3 && curFloorNum == 16)
        {
            CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
            LoadMap(7, 5, 4, 1);
            return;


        }
        if (curTowerNum == 7 && curFloorNum == 5)
        {
            CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
            LoadMap(3, 16, 7, 7);
            return;
        }

        if (curTowerNum == 4 && curFloorNum == 8)
        {
            CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
            LoadMap(7, 6, 4, 1);
            return;


        }
        if (curTowerNum == 7 && curFloorNum == 6)
        {
            CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
            LoadMap(4, 8, 8, 4);
            return;
        }
        if (curTowerNum == 4 && curFloorNum == 19)
        {
            CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
            LoadMap(7, 7, 4, 4);
            return;


        }
        if (curTowerNum == 7 && curFloorNum == 7)
        {
            CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
            LoadMap(4, 19, 4, 8);
            return;
        }
        if (curTowerNum == 4 && curFloorNum == 23)
        {
            CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
            LoadMap(7, 8, 4, 7);
            return;


        }
        if (curTowerNum == 7 && curFloorNum == 8)
        {
            if (currentCell.y==2)
            {
                CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
                LoadMap(4, -5, 4, 0);
            }
            else
            {
                CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
                LoadMap(4, 23, 4, 8);
            }


            return;
        }



    }
    public void GetClassUpItem(int _num)
    {
        switch (_num)
        {
            case 0:
                Player.S.jobClass = 1;
                GetItemTextOn(towerCellArray[currentCell.x,currentCell.y+1].Cell.GetComponent<MapCell>().towerObjectData);
                

                break;
            case 1:
                Player.S.jobClass = 2;
                GetItemTextOn(towerCellArray[currentCell.x, currentCell.y + 1].Cell.GetComponent<MapCell>().towerObjectData);
                
                break;
            case 2:
                Player.S.jobClass = 3;
                GetItemTextOn(towerCellArray[currentCell.x, currentCell.y + 1].Cell.GetComponent<MapCell>().towerObjectData);
                
                break;
            default:
                break;
        }
        ChangeMapCell(1, 7, voidTile);
        ChangeMapCell(4, 7, voidTile);
        ChangeMapCell(7, 7, voidTile);
    }
    public void TownWarp(int _mainProgress,int x,int y)
    {
        switch (_mainProgress)
        {
            case 1:
                ChangeMapCell(towerCellArray[3, 6].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                ChangeMapCell(towerCellArray[5, 6].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
                TowerStory.S.StartStory(1);
                Player.S.PlusMainProgress();
                break;
            case 3:
                ChangeMapCell(towerCellArray[x, y].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
                TowerStory.S.StartStory(_mainProgress);
                Player.S.PlusMainProgress();
                break;
            case 4:
                ChangeMapCell(towerCellArray[x, y].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
                TowerStory.S.StartStory(5);
                Player.S.PlusMainProgress();
                break;
            case 6:
                ChangeMapCell(towerCellArray[x, y].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);

                ChangeMapCell(towerCellArray[3, 2].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                ChangeMapCell(towerCellArray[3, 3].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                ChangeMapCell(towerCellArray[3, 5].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                ChangeMapCell(towerCellArray[3, 6].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
                TowerStory.S.StartStory(7);
                Player.S.PlusMainProgress();
                break;
            case 7:
                ChangeMapCell(towerCellArray[x, y].Cell.GetComponent<MapCell>(), fieldDummies[curTowerNum]);
                CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
                TowerStory.S.StartStory(8);
                Player.S.PlusMainProgress();
                break;
            case 8:
                CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
                TowerStory.S.StartStory(9);
                Player.S.PlusMainProgress();
                break;
            case 10:
                CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
                TowerStory.S.StartStory(11);
                Player.S.PlusMainProgress();
                break;
            case 11:
                CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
                TowerStory.S.StartStory(12);
                Player.S.PlusMainProgress();
                break;
            case 12:
                CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
                TowerStory.S.StartStory(13);
                Player.S.PlusMainProgress();
                break;
            case 13:
                CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
                TowerStory.S.StartStory(14);
                Player.S.PlusMainProgress();
                break;
            case 14:
                CsvTest.S.SaveMap(curTowerNum, curFloorNum, mapCells);
                TowerStory.S.StartStory(15);
                Player.S.PlusMainProgress();
                break;
            default:
                break;
        }
    }
}
