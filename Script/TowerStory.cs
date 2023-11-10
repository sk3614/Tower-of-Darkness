using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TowerStoryUI
{
    public Text dialogue;
    public Image image;
}

public class TowerStory : MonoBehaviour
{
    public static TowerStory S;

    public GameObject storyUI;
    public TowerStoryUI UI;
    public GameObject nextUI;

    private char[] pieceArr;
    private bool isPrint;
    [SerializeField]
    private List<string> scriptId = new List<string>();
    private List<string> scripts = new List<string>();
    private int numOfDialogue = 0;
    Coroutine tmp;

    public Sprite[] images;

    public int lastId;//마지막 스크립트id 스크립트끝나고 호출할 함수 구분할떄 사용
    private void Awake()
    {
        if (S==null)
        {
            S = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) && storyUI.activeInHierarchy)
        {
            NextDialogue();
        }
    }

    public void TextSet(int start, int end, bool Kor = true)
    {
        scriptId.Clear();
        scripts.Clear();
        List<string[]> csvData = GetCSV(start, end);
        for (int i = 0; i < csvData.Count; i++)
        {
            scriptId.Add(csvData[i][0]);
            scripts.Add(csvData[i][1]);
        }
        tmp = StartCoroutine(PrintText(UI));
    }

    public void NextDialogue()
    {
        TowerStoryUI _storyUI;
        _storyUI = UI;

        if (scripts.Count - 1 > numOfDialogue)
        {
            if (isPrint)
            {
                StopCoroutine(tmp);
                _storyUI.dialogue.text = scripts[numOfDialogue];
                nextUI.SetActive(true);
                isPrint = false;
            }
            else
            {
                numOfDialogue += 1;
                tmp = StartCoroutine(PrintText(_storyUI));
                nextUI.SetActive(false);
            }
        }
        else
        {
            if (!isPrint)
            {

                switch (lastId)
                {
                    case 8:
                        switch (Player.S.job)
                        {
                            case Player.PlayerJob.Inquisitor:
                                break;
                            case Player.PlayerJob.Paladin:
                                Player.S.hp = 1000;
                                break;
                            case Player.PlayerJob.WitchHunter:
                                Player.S.hp = 500;
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
                        Player.S.MaxFloorNum = 0;
                        Player.S.MinFloorNum = 0;
                        TowerMap.S.MoveLock = false;
                        SceneChanger.S.GoTown();
                        break;
                    case 10:
                        TowerMap.S.MoveLock = false;
                        SceneChanger.S.GoTown();
                        break;
                    case 16:
                        TowerMap.S.MoveLock = false;
                        Player.S.PlusMainProgress();
                        Player.S.MaxFloorNum = 0;
                        Player.S.MinFloorNum = 0;
                        SceneChanger.S.GoTown();
                        break;
                    case 18:
                        TowerMap.S.MoveLock = false;
                        SceneChanger.S.GoTown();
                        break;
                    case 20:
                        TowerMap.S.MoveLock = false;
                        CsvTest.S.SaveMap(TowerMap.S.curTowerNum, TowerMap.S.curFloorNum, TowerMap.S.mapCells);
                        TowerMap.S.LoadMap(7, 0, 4, 0);
                        storyUI.SetActive(false);
                        break;
                    case 22:
                        TowerMap.S.MoveLock = false;
                        SceneChanger.S.GoTown();
                        break;
                    case 26:
                        TowerMap.S.MoveLock = false;
                        Player.S.PlusMainProgress();
                        Player.S.MaxFloorNum = 0;
                        Player.S.MinFloorNum = 0;
                        SceneChanger.S.GoTown();
                        break;
                    case 27:
                        TowerMap.S.MoveLock = false;
                        SceneChanger.S.GoTown();
                        break;
                    case 28:
                        TowerMap.S.MoveLock = false;
                        SceneChanger.S.GoTown();
                        break;
                    case 30:
                        TowerMap.S.MoveLock = false;
                        SceneChanger.S.GoTown();
                        break;
                    case 34:
                        TowerMap.S.MoveLock = false;
                        Player.S.PlusMainProgress();
                        Player.S.MaxFloorNum = 0;
                        Player.S.MinFloorNum = 0;
                        SceneChanger.S.GoTown();
                        break;
                    case 37:
                        TowerMap.S.MoveLock = false;
                        SceneChanger.S.GoTown();
                        break;
                    case 39:
                        TowerMap.S.MoveLock = false;
                        SceneChanger.S.GoTown();
                        break;
                    case 41:
                        TowerMap.S.MoveLock = false;
                        SceneChanger.S.GoTown();
                        break;
                    case 42:
                        TowerMap.S.MoveLock = false;
                        SceneChanger.S.GoTown();
                        break;
                    default:
                        break;
                }

            }
            StopCoroutine(tmp);
            _storyUI.dialogue.text = scripts[numOfDialogue];
            isPrint = false;
        }

    }

    IEnumerator PrintText(TowerStoryUI _dialogueUI)
    {
        ChangeImage(System.Convert.ToInt32(scriptId[numOfDialogue]));
        _dialogueUI.dialogue.text = "";

        
        scripts[numOfDialogue]=scripts[numOfDialogue].Replace("(출신지)", Player.S.HomeTown);
        scripts[numOfDialogue] = scripts[numOfDialogue].Replace("(the place of origin)", Player.S.HomeTown);
        
        pieceArr = scripts[numOfDialogue].ToCharArray();
        isPrint = true;
        for (int i = 0; i < pieceArr.Length; i++) //0부터 배열의 길이만큼 ++
        {
            _dialogueUI.dialogue.text += pieceArr[i]; // 출력할 메세지는 배열에 하나씩 더한다
            yield return new WaitForSeconds(0.025f); // 0.1초간격으로 출력
        }
        isPrint = false;
    }

    public List<string[]> GetCSV(int start, int end)
    {
        List<Dictionary<string, object>> data = CSVReader.Read("TowerStory", "CSV_Files/");

        List<string[]> Maindata = new List<string[]>();

        for (int i = start; i <= end; i++)
        {
            string[] da = new string[2];
            da[0] = data[i]["id"].ToString();
            if (Options.S.language == Options.Language.Kor)
            {
                da[1] = data[i]["Kor"].ToString();
            }
            else if (Options.S.language == Options.Language.Eng)
            {
                da[1] = data[i]["Eng"].ToString();
            }


            Maindata.Add(da);
        }

        return Maindata;

    }
    public void StartStory(int _towerNum)
    {
        storyUI.SetActive(true);
        TowerMap.S.MoveLock = true;
        switch (_towerNum)
        {
            case 0:
                TextSet(0, 8);
                lastId = 8;
                break;
            case 1:
                TextSet(9, 10);
                lastId = 10;
                break;
            case 2:
                TextSet(11, 16);
                lastId = 16;
                break;
            case 3:
                TextSet(17, 18);
                lastId = 18;
                break;
            case 4://비밀탑 스토리
                TextSet(19, 20);
                lastId = 20;
                break;
            case 5://원령 처리
                TextSet(21, 22);
                lastId = 22;
                break;
            case 6://드라큘라 원 처리
                TextSet(23, 26);
                lastId = 26;
                break;
            case 7://오버로드 처리
                TextSet(27, 27);
                lastId = 27;
                break;
            case 8://스핑크스 처리
                TextSet(28, 28);
                lastId = 28;
                break;
            case 9://타이탄 처리
                TextSet(29, 30);
                lastId = 30;
                break;
            case 10://황금왕 처리
                TextSet(31, 34);
                lastId = 34;
                break;
            case 11://더 스콜피온
                TextSet(35, 37);
                lastId = 37;
                break;
            case 12://마이 레이디 처리
                TextSet(38, 39);
                lastId = 39;
                break;
            case 13://켄세이 처리
                TextSet(40, 41);
                lastId = 41;
                break;
            case 14://켄세이 처리
                TextSet(42, 42);
                lastId = 42;
                break;
            default:
                break;
        }

    }
    public void ChangeImage(int _num)
    {
        if (_num <= 2)
        {
            UI.image.sprite = images[0];
        }
        else if (_num <= 5)
        {
            UI.image.sprite = images[1];
        }
        else if (_num <= 7)
        {
            UI.image.sprite = images[2];
        }
        else if (_num <= 8)
        {
            UI.image.sprite = images[3];
        }
        else if (_num <= 10)
        {
            UI.image.sprite = images[4];
        }
        else if (_num <= 13)
        {
            UI.image.sprite = images[5];
        }
        else if (_num <= 16)
        {
            UI.image.sprite = images[6];
        }
        else if (_num <= 18)
        {
            UI.image.sprite = images[7];
        }
        else if (_num <= 20)
        {
            UI.image.sprite = images[7];
        }
        else if (_num <= 22)
        {
            UI.image.sprite = images[7];
        }
        else if (_num <= 23)
        {
            UI.image.sprite = images[7];
        }
        else if (_num <= 26)
        {
            UI.image.sprite = images[7];
        }
        else if (_num <= 27)
        {
            UI.image.sprite = images[7];
        }
        else if (_num <= 28)
        {
            UI.image.sprite = images[7];
        }
        else if (_num <= 30)
        {
            UI.image.sprite = images[7];
        }
        else if (_num <= 34)
        {
            UI.image.sprite = images[7];
        }
        else if (_num <= 37)
        {
            UI.image.sprite = images[7];
        }
        else if (_num <= 39)
        {
            UI.image.sprite = images[7];
        }
        else if (_num <= 41)
        {
            UI.image.sprite = images[7];
        }
        else if (_num <= 42)
        {
            UI.image.sprite = images[7];
        }
    }
}
