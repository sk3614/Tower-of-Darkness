using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TownUI : MonoBehaviour
{

    public static TownUI S;
    public Text[] basicTexts;
    public List<Text> NPCTexts;
    public Image townImage;

    public Animator ani;

    public GameObject[] townUI;
    public GameObject[] CenterArea;
    public GameObject[] residenceArea;
    public GameObject[] PortArea;
    public GameObject[] MillitaryArea;
    public GameObject clouds;
    public Sprite[] TownSprites;
    public Sprite[] CenterAreaImage;
    public Sprite[] residenceAreaImage;
    public Sprite[] portAreaImage;
    public Sprite[] millitaryAreaImage;
    public GameObject zundo;
    public List<MarketPlace> markets;


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
    public void Start()
    {
        ResetTexts();
        if (Player.S.mainProgress==3)
        {
            Player.S.GetBasicClassSkill(2);
        }
        SoundManager.S.PlayBG("district");
        if (Player.S.mainProgress == 10)
        {
            if (QuestManager.S.AllQuestData[41].questState == QuestData.QuestState.Accept)
            {
                QuestManager.S.CountUpQuestByID(42);
            }

        }

    }
    public void CloseAllUI()
    {
        for (int i = 0; i < townUI.Length; i++)
        {
            townUI[i].SetActive(false);
        }
        for (int i = 0; i < CenterArea.Length; i++)
        {
            CenterArea[i].SetActive(false);
        }
        for (int i = 0; i < residenceArea.Length; i++)
        {
            residenceArea[i].SetActive(false);
        }
        for (int i = 0; i < PortArea.Length; i++)
        {
            PortArea[i].SetActive(false);
        }
        for (int i = 0; i < MillitaryArea.Length; i++)
        {
            MillitaryArea[i].SetActive(false);
        }
    }

    public void GotownUI(int _num)
    {
        if (_num==0)
        {
            clouds.SetActive(true);
        }
        else
        {
            clouds.SetActive(false);
        }

        SoundManager.S.PlaySE("node");
        if (_num == 2 && Player.S.mainProgress == 1)
        {
            DialogueManager.S.TextSet(44, 45);
            return;
        }
        if (_num == 4 && Player.S.mainProgress == 1)
        {
            DialogueManager.S.TextSet(46, 47);
            return;
        }

            SoundManager.S.PlayBG("district");



       CloseAllUI();
       townUI[_num].SetActive(true);
        switch (_num)
        {
            case 0:
                PlayAni("Town");
                break;
            case 1:
                PlayAni("town2");
                break;
            case 2:
                PlayAni("town3");
                break;
            case 3:
                PlayAni("town4");
                break;
            case 4:
                PlayAni("town5");
                break;
            default:
                break;
        }
        ChangeTownImage(TownSprites[_num]);

    }
    public void GoCenterUI(int _num)
    {
        SoundManager.S.PlaySE("node");
        if (_num==1&&Player.S.mainProgress<=2)
        {
            DialogueManager.S.TextSet(42, 42);
            return;
        }
        if (_num == 1 && Player.S.mainProgress ==4&&!Player.S.isMeetKing)
        {
            DialogueManager.S.TextSet(42, 42);
            return;
        }
        if (_num == 2 && Player.S.mainProgress == 2)
        {
            DialogueManager.S.TextSet(43, 43);
            return;
        }
        switch (_num)
        {
            case 0:
                SoundManager.S.PlayBG("castle");
                PlayAni("castle");
                break;
            case 1:
                SoundManager.S.PlayBG("scaredsector");
                PlayAni("holy");
                break;
            case 2:
                SoundManager.S.PlayBG("order");
                PlayAni("order");
                break;
            case 3:
                if (Player.S.mainProgress == 4 && Player.S.isMeetSaint)
                {
                    DialogueManager.S.TextSet(133, 134);
                }
                SoundManager.S.PlayBG("portal");
                PlayAni("portal");
                break;
        }
        CloseAllUI();
        townImage.sprite = CenterAreaImage[_num];
       CenterArea[_num].SetActive(true);

    }
    public void GoResidenceAreaUI(int _num)
    {
        SoundManager.S.PlaySE("node");
        if (_num != 1 && Player.S.mainProgress <= 1)
        {
            DialogueManager.S.TextSet(48, 49);
            return;
        }
        switch (_num)
        {
            case 0:
                SoundManager.S.PlayBG("downtown");
                PlayAni("downtown");
                break;
            case 1:
                SoundManager.S.PlayBG("square");
                PlayAni("plaza");
                break;
            case 2:
                SoundManager.S.PlayBG("slum");
                PlayAni("slum");
                break;
            case 3:
                SoundManager.S.PlayBG("pub");
                PlayAni("pub");
                break;
            default:
                break;
        }
        CloseAllUI();
        townImage.sprite = residenceAreaImage[_num];
        residenceArea[_num].SetActive(true);
    }
    public void GoCPortAreaUI(int _num)
    {
        SoundManager.S.PlaySE("node");
        if (_num != 0 && Player.S.mainProgress <= 1)
        {
            DialogueManager.S.TextSet(48, 49);
            return;
        }

        switch (_num)
        {
            case 0:
                SoundManager.S.PlayBG("guildtrader");
                PlayAni("marketplace");
                break;
            case 1:
                SoundManager.S.PlayBG("blackmarket");
                PlayAni("blackmarket");
                break;
            case 2:
                SoundManager.S.PlayBG("pawnbank");
                PlayAni("pawnshop");
                break;
            default:
                break;
        }
        CloseAllUI();
        townImage.sprite = portAreaImage[_num];
         PortArea[_num].SetActive(true);
    }
    public void GoMillitaryUI(int _num)
    {
        SoundManager.S.PlaySE("node");
        if (_num <= 1&&Player.S.mainProgress <= 1)
        {
            DialogueManager.S.TextSet(48, 49);
            return;
        }
        else if(_num == 1 && Player.S.mainProgress <= 5)
        {
            DialogueManager.S.TextSet(48, 49);
            return;
        }

        switch (_num)
        {
            case 0:
                SoundManager.S.PlayBG("training");
                PlayAni("trainroom");
                break;
            case 1:
                SoundManager.S.PlayBG("blacksmith");
                PlayAni("forge");
                break;
            case 2:
                SoundManager.S.PlayBG("seminary");
                PlayAni("seminary");

                break;
            default:
                break;
        }
        CloseAllUI();
        townImage.sprite = millitaryAreaImage[_num];
        MillitaryArea[_num].SetActive(true);
    }
    public void ResetTexts()
    {
        for (int i = 0; i < basicTexts.Length; i++)
        {
            basicTexts[i].text = TextManager.S.GetTextsByName("Basic", basicTexts[i].name);
        }
    }
    public void GoTower()
    {
        SceneManager.LoadScene(2);

    }

    public void ChangeTownImage(Sprite _sprite)
    {
        if (_sprite == null)
        {
            return;
        }
        townImage.sprite = _sprite;
    }
    public void PlayAni(string EffectName)
    {
        if (EffectName != "")
        {
            ani.Play(EffectName);
        }


    }

}
