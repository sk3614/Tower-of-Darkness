using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogueUI
{
    public GameObject ui;
    public Text characterName;
    public Text dialogue;
    public Image image;
}

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager S;
    public DialogueUI UI;

    private char[] pieceArr;
    private bool isPrint;
    private List<string> characterName = new List<string>();
    private List<string> scripts = new List<string>();
    private int numOfDialogue=0;
    public int EndEventNum;
    Coroutine tmp;
    public TownNPC npc;
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




    public void TextSet(int start, int end,TownNPC _npc=null,int EndEvent=0)
    {
        characterName.Clear();
        scripts.Clear();
        List<string[]> csvData = GetDialogue(start, end);
        for (int i = 0; i < csvData.Count; i++)
        {
            characterName.Add(csvData[i][0]);
            scripts.Add(csvData[i][1]);
        }
        UI.ui.SetActive(true);
        EndEventNum = EndEvent;
        npc = _npc;
        tmp = StartCoroutine(PrintText(UI));

    }


    public void NextDialogue()
    {
        DialogueUI _dialogueUI;
        _dialogueUI = UI;
        SoundManager.S.PlaySE("node");
        if (scripts.Count-1>numOfDialogue)
        {
            if (isPrint)
            {
                StopCoroutine(tmp);
                _dialogueUI.dialogue.text = scripts[numOfDialogue];
                isPrint = false;
            }
            else
            {
                numOfDialogue += 1;
                tmp= StartCoroutine(PrintText(_dialogueUI));
            }
        }
        else 
        {
            if (!isPrint)
            {
                UI.ui.SetActive(false);
                numOfDialogue = 0;
                EndEvent(EndEventNum);
                if (npc!=null)
                {
                    npc.EndEvent(npc.EventNum);
                    npc = null;
                }
            }
            StopCoroutine(tmp);
            _dialogueUI.dialogue.text = scripts[numOfDialogue];
            isPrint = false;

        }
       
    }

    IEnumerator PrintText(DialogueUI _dialogueUI)
    {
        _dialogueUI.dialogue.text = "";
        _dialogueUI.characterName.text = characterName[numOfDialogue];
        pieceArr = scripts[numOfDialogue].ToCharArray();
        isPrint = true;
        for (int i = 0; i < pieceArr.Length; i++) //0부터 배열의 길이만큼 ++
        {
            _dialogueUI.dialogue.text += pieceArr[i]; // 출력할 메세지는 배열에 하나씩 더한다
            yield return new WaitForSeconds(0.025f); // 0.1초간격으로 출력
        }
        isPrint = false;
    }

    public static List<string[]> GetDialogue(int start,int end)
    {
        List<Dictionary<string, object>> data = CSVReader.Read("TownNpcScript", "CSV_Files/");

        List<string[]> Maindata = new List<string[]>();

        for (int i = start; i <= end; i++)
        {
            string[] da = new string[2];

            if (Options.S.language==Options.Language.Kor)
            {
                da[0] = data[i]["Name"].ToString();
                da[1] = data[i]["Kor"].ToString();
            }
            else if(Options.S.language == Options.Language.Eng)
            {
                da[0] = data[i]["EName"].ToString();
                da[1] = data[i]["Eng"].ToString();
            }
           

            Maindata.Add(da);
        }

        return Maindata;

    }

    public void TextSetPlaza(int start, int end)
    {
        characterName.Clear();
        scripts.Clear();
        List<string[]> csvData = GetDialoguePlaza(start, end);
        for (int i = 0; i < csvData.Count; i++)
        {
            characterName.Add(csvData[i][0]);
            scripts.Add(csvData[i][1]);
        }
        UI.ui.SetActive(true);
        tmp = StartCoroutine(PrintText(UI));
    }
    public static List<string[]> GetDialoguePlaza(int start, int end)
    {
        List<Dictionary<string, object>> data = CSVReader.Read("Plaza", "CSV_Files/");

        List<string[]> Maindata = new List<string[]>();

        for (int i = start; i <= end; i++)
        {
            string[] da = new string[2];

            if (Options.S.language == Options.Language.Kor)
            {
                da[0] = data[i]["Name"].ToString();
                da[1] = data[i]["Kor"].ToString();
            }
            else if (Options.S.language == Options.Language.Eng)
            {
                da[0] = data[i]["EName"].ToString();
                da[1] = data[i]["Eng"].ToString();
            }


            Maindata.Add(da);
        }

        return Maindata;

    }

    public void EndEvent(int _num)
    {
        switch (_num)
        {
            case 7:
                TownUI.S.zundo.SetActive(true);
                break;
            case 8:
                Gambler.S.SetGambleUI[0].SetActive(true);
                break;
            case 9:
                Gambler.S.SetGambleUI[1].SetActive(true);
                break;
            case 10:
                Gambler.S.SetGambleUI[2].SetActive(true);
                break;
            case 11:
                Player.S.ClassUp2();
                break;
            case 15:
                Player.S.ClassUp3();
                TownUI.S.zundo.SetActive(false);
                break;
            default:
                break;
        }
        EndEventNum = 0;
    }

}
