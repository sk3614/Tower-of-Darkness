using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TownStoryUI
{
    public Text dialogue;
    public Image image;
}

public class TownStory : MonoBehaviour
{
    public static TownStory S;

    public GameObject storyUI;
    public TownStoryUI UI;
    public GameObject nextUI;

    private char[] pieceArr;
    private bool isPrint;
    [SerializeField]
    private List<string> scriptId = new List<string>();
    private List<string> scripts = new List<string>();
    public int numOfDialogue = 0;
    Coroutine tmp;

    public Sprite[] images;

    public int lastId;//마지막 스크립트id 스크립트끝나고 호출할 함수 구분할떄 사용
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
    private void Start()
    {
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
        TownStoryUI storyUI;
        storyUI = UI;

        if (scripts.Count - 1 > numOfDialogue)
        {
            if (isPrint)
            {
                StopCoroutine(tmp);
                storyUI.dialogue.text = scripts[numOfDialogue];
                nextUI.SetActive(true);
                isPrint = false;
            }
            else
            {
                numOfDialogue += 1;
                nextUI.SetActive(false);
                tmp = StartCoroutine(PrintText(storyUI));

            }
        }
        else
        {
            if (!isPrint)
            {

                switch (lastId)
                {
                    case 6:
                        Player.S.PlusMainProgress();
                        TownProgressManager.S.ChangeTownProgress();
                        EndStory();
                        DialogueManager.S.TextSet(9, 10);
                        numOfDialogue = 0;
                        break;
                    case 9:
                       SceneChanger.S.GoTower();
                        break;
                    case 10:
                        SceneChanger.S.GoTower();
                        break;
                    case 12:
                        SceneChanger.S.GoTower();
                        break;
                    case 13:
                        SceneChanger.S.GoTower();
                        break;
                    case 15:
                        SceneChanger.S.GoTower();
                        break;
                    case 17:
                        SceneChanger.S.GoTower();
                        break;
                    case 18:
                        SceneChanger.S.GoTower();
                        break;
                    case 20:
                        SceneChanger.S.GoTower();
                        break;
                    case 22:
                        SceneChanger.S.GoTower();
                        break;
                    case 24:
                        SceneChanger.S.GoTower();
                        break;
                    case 27:
                        SceneChanger.S.GoTower();
                        break;
                    case 29:
                        SceneChanger.S.GoTower();
                        break;
                    case 31:
                        SceneChanger.S.GoTower();
                        break;
                    case 34:
                        SceneChanger.S.GoTower();
                        break;
                    default:
                        break;
                }

            }
            StopCoroutine(tmp);
            storyUI.dialogue.text = scripts[numOfDialogue];
            isPrint = false;
        }

    }

    IEnumerator PrintText(TownStoryUI _dialogueUI)
    {
        ChangeImage(System.Convert.ToInt32(scriptId[numOfDialogue]));
        _dialogueUI.dialogue.text = "";

        scripts[numOfDialogue] = scripts[numOfDialogue].Replace("(출신지)", Player.S.HomeTown);

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
        List<Dictionary<string, object>> data = CSVReader.Read("TownStory", "CSV_Files/");

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
    public void StartStory(int num)
    {
        switch (num)
        {
            case 0:
                if (Player.S.mainProgress == 0)
                {
                    TextSet(0, 6);
                    lastId = 6;
                    storyUI.SetActive(true);
                }
                break;
            case 1:
                if (Player.S.mainProgress==1)
                {
                    TextSet(7, 9);
                    lastId = 9;
                    storyUI.SetActive(true);
                }
                break;
            case 2:
                if (Player.S.mainProgress == 2)
                {
                    TextSet(10, 10);
                    lastId = 10;
                    storyUI.SetActive(true);
                }
                break;
            case 3:
                if (Player.S.mainProgress == 3)
                {
                    TextSet(11, 12);
                    lastId = 12;
                    storyUI.SetActive(true);
                }
                break;
            case 4:
                if (Player.S.mainProgress == 4)
                {
                    TextSet(13, 13);
                    lastId = 13;
                    storyUI.SetActive(true);
                }
                break;
            case 5:
                if (Player.S.mainProgress == 5)
                {
                    TextSet(14, 15);
                    lastId = 15;
                    storyUI.SetActive(true);
                }
                break;
            case 6:
                if (Player.S.mainProgress == 6)
                {
                    TextSet(16, 17);
                    lastId = 17;
                    storyUI.SetActive(true);
                }
                break;
            case 7:
                if (Player.S.mainProgress == 7)
                {
                    TextSet(18, 18);
                    lastId = 18;
                    storyUI.SetActive(true);
                }
                break;
            case 8:
                if (Player.S.mainProgress == 8)
                {
                    TextSet(19, 20);
                    lastId = 20;
                    storyUI.SetActive(true);
                }
                break;
            case 9:
                if (Player.S.mainProgress == 9)
                {
                    TextSet(21, 22);
                    lastId = 22;
                    storyUI.SetActive(true);
                }
                break;
            case 10:
                if (Player.S.mainProgress == 10)
                {
                    TextSet(23, 24);
                    lastId = 24;
                    storyUI.SetActive(true);
                }
                break;
            case 11:
                if (Player.S.mainProgress == 11)
                {
                    TextSet(25, 27);
                    lastId = 27;
                    storyUI.SetActive(true);
                }
                break;
            case 12:
                if (Player.S.mainProgress == 12)
                {
                    TextSet(28, 29);
                    lastId = 29;
                    storyUI.SetActive(true);
                }
                break;
            case 13:
                if (Player.S.mainProgress == 13)
                {
                    TextSet(30, 31);
                    lastId = 31;
                    storyUI.SetActive(true);
                }
                break;
            case 14:
                if (Player.S.mainProgress == 14)
                {
                    TextSet(32, 34);
                    lastId = 34;
                    storyUI.SetActive(true);
                }
                break;
            default:
                break;
        }

    }
    public void EndStory()
    {
        storyUI.SetActive(false);
    }
    public void ChangeImage(int _num)
    {
        if (_num <= 1)
        {
            UI.image.sprite = images[0];
        }
        else if (_num <= 6)
        {
            UI.image.sprite = images[1];
        }
        else if (_num <= 7)
        {
            UI.image.sprite = images[2];
        }
        else if (_num <= 9)
        {
            UI.image.sprite = images[3];
        }
        else if (_num <= 10)
        {
            UI.image.sprite = images[4];
        }
        else if (_num <= 12)
        {
            UI.image.sprite = images[5];
        }
        else if (_num <= 13)
        {
            UI.image.sprite = images[5];
        }
        else if (_num <= 15)
        {
            UI.image.sprite = images[5];
        }
        else if (_num <= 17)
        {
            UI.image.sprite = images[5];
        }
        else if (_num <= 18)
        {
            UI.image.sprite = images[5];
        }
        else if (_num <= 20)
        {
            UI.image.sprite = images[5];
        }
        else if (_num <= 22)
        {
            UI.image.sprite = images[5];
        }
        else if (_num <= 24)
        {
            UI.image.sprite = images[5];
        }
        else if (_num <= 27)
        {
            UI.image.sprite = images[5];
        }
        else if (_num <= 29)
        {
            UI.image.sprite = images[5];
        }
        else if (_num <= 31)
        {
            UI.image.sprite = images[5];
        }
        else if (_num <= 34)
        {
            UI.image.sprite = images[5];
        }
    }
}
