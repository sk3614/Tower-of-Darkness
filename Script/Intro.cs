using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class IntroUI
{
    public Text dialogue;
    public Image image;
}

public class Intro : MonoBehaviour
{
    public GameObject introUI;
    public GameObject NextUI;
    public GameObject DemoUI;
    public GameObject TutoSkipUI;
    public IntroUI UI;

    private char[] pieceArr;
    private bool isPrint;
    [SerializeField]
    private List<string> scriptId = new List<string>();
    private List<string> scripts = new List<string>();
    public int numOfDialogue = 0;
    Coroutine tmp;

    public bool selectJob;
    public Sprite[] images;

    public GameObject jobSelectUI;
    public int playerjobNum;


    public Skill Snipe;//마녀사냥꾼 기본스킬
    public Skill Swing;//십자군기사 기본스킬
    public Skill CrossSlash;//이단심문관 기본스킬

    public List<Sprite> witchHunterImages;
    public List<Sprite> crusaderImages;
    public List<Sprite> InquisitorImages;
    public void Start()
    {
    }
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.Return))&& introUI.activeInHierarchy&& !jobSelectUI.activeInHierarchy)
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
        numOfDialogue = 0;
        tmp = StartCoroutine(PrintText(UI));
    }

    public void NextDialogue()
    {
        IntroUI _introUI;
        _introUI = UI;

        if (scripts.Count - 1 > numOfDialogue)
        {
            if (isPrint)
            {
                StopCoroutine(tmp);
                _introUI.dialogue.text = scripts[numOfDialogue];
                NextUI.SetActive(true);
                isPrint = false;
            }
            else
            {
                numOfDialogue += 1;
                tmp = StartCoroutine(PrintText(_introUI));
                NextUI.SetActive(false);
            }
        }
        else
        {
            if (!isPrint)
            {

                if (!selectJob)
                {
                    jobSelectUI.SetActive(true);
                    numOfDialogue = 0;
                }
                else
                {
                    TutoSkipUI.SetActive(true);
                }

            }
            StopCoroutine(tmp);
            _introUI.dialogue.text = scripts[numOfDialogue];
            isPrint = false;
        }

    }
    
    public void GoTuto()
    {
        SceneChanger.S.GameStart();
    }
    public void SkipTuto()
    {
        Player.S.endTuto = true;
        SceneChanger.S.GameStart();
    }

    IEnumerator PrintText(IntroUI _dialogueUI)
    {
        ChangeImage(System.Convert.ToInt32(scriptId[numOfDialogue]));
        _dialogueUI.dialogue.text = "";
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
        List<Dictionary<string, object>> data = CSVReader.Read("Intro", "CSV_Files/");

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
    public void Gointro()
    {
        if (Options.S.tutorialOff)
        {
            SceneChanger.S.GoTown();
        }
        introUI.SetActive(true);
        TextSet(0, 13);
    }
    public void SelectJob(int _num)
    {
        selectJob = true;
        switch (_num)
        {
            case 0:
                Player.S.SetClass(2);

                Player.S.PlayerImage = witchHunterImages[0];
                Player.S.PlayerTowerImage = witchHunterImages[0];
                Player.S.PlayerBattleAni[0] = witchHunterImages[1];
                Player.S.PlayerBattleAni[1] = witchHunterImages[2];
                jobSelectUI.SetActive(false);
                TextSet(14, 17);
                break;
            case 1:
                if (Player.S.isDemo)
                {
                    DemoUI.SetActive(true);
                    return;
                }
                Player.S.SetClass(1);
                jobSelectUI.SetActive(false);
                Player.S.PlayerImage = crusaderImages[0];
                Player.S.PlayerTowerImage = crusaderImages[0];
                Player.S.PlayerBattleAni[0] = crusaderImages[1];
                Player.S.PlayerBattleAni[1] = crusaderImages[2];
                TextSet(18, 21);
                break;
            case 2:
                Player.S.SetClass(0);
                Player.S.PlayerImage = InquisitorImages[0];
                Player.S.PlayerTowerImage = InquisitorImages[0];
                Player.S.PlayerBattleAni[0] = InquisitorImages[1];
                Player.S.PlayerBattleAni[1] = InquisitorImages[2];
                jobSelectUI.SetActive(false);
                TextSet(22, 25);
                break;
            default:
                break;
        }

        
    }

    public void JobInfoUI()
    {
        
    }


    public void ChangeImage(int _num)
    {
        if (_num<=3)
        {
            UI.image.sprite = images[0];
        }
        else if (_num <=7)
        {
            UI.image.sprite = images[1];
        }
        else if (_num <= 10)
        {
            UI.image.sprite = images[2];
        }
        else if (_num <= 13)
        {
            UI.image.sprite = images[3];
        }
        else if (_num <= 15)
        {
            UI.image.sprite = images[4];
        }
        else if (_num <= 17)
        {
            UI.image.sprite = images[6];
        }
        else if (_num > 17 && _num <= 19)
        {
            UI.image.sprite = images[5];
        }
        else if (_num <= 21)
        {
            UI.image.sprite = images[6];
        }
        else if (_num > 21 && _num <= 23)
        {
            UI.image.sprite = images[7];
        }
        else if (_num <= 25)
        {
            UI.image.sprite = images[8];
        }
    }
}
