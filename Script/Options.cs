using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;
public class Options : MonoBehaviour
{
    public static Options S;

    public enum Language
    {
        Kor,
        Eng
    }
    //해상도관련
    public enum ResolutionNum
    {
        R_1280_720,
        R_1600_900,
        R_1920_1080,

    }
    public ResolutionNum resolutionNum=(ResolutionNum)1;
    public bool fullScreen;


    public bool tutorialOff;
    public Language language = Language.Kor;
    public Text skipText;
    public GameObject tutoskipUI;
    public GameObject loadgame;

    public float BgmSound=1f;
    public float SESound=1f;
    public void Awake()
    {
        if (S == null)
        {
            S = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }


    }
    public void Start()
    {
        
        if (SteamApps.GetCurrentGameLanguage() == "english")
        {
            Debug.Log(SteamApps.GetCurrentGameLanguage());
            language = Language.Eng;
        }
        else
        {
            language = Language.Kor;
        }
    }
    public void TutorialOff()
    {
        if (tutorialOff)
        {
            skipText.text = "튜토리얼 끄기";
            tutorialOff = false;
            Player.S.MonsterBookOn = false;
        }
        else
        {
            skipText.text = "튜토리얼 켜기";
            tutoskipUI.SetActive(true);
            tutorialOff = true;
            Player.S.MonsterBookOn = true;
        }

    }
    public void loadgameClick()
    {
        loadgame.SetActive(true);

    }

    public void ChangeResolution(int _num)
    {
        switch (_num)
        {
            case 0:
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
                Screen.SetResolution(1280, 720, fullScreen);
                resolutionNum = (ResolutionNum)0;
                PlayerPrefs.SetInt("Resolution", 0);
                break;
            case 1:
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
                Screen.SetResolution(1600, 900, fullScreen);
                resolutionNum = (ResolutionNum)1;
                PlayerPrefs.SetInt("Resolution", 1);
                break;
            case 2:
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
                Screen.SetResolution(1920, 1080, fullScreen);
                resolutionNum = (ResolutionNum)2;
                PlayerPrefs.SetInt("Resolution", 2);
                break;
            default:
                break;
        }
    }
    public void ChangeLanguage(int _num)
    {
        switch (_num)
        {
            case 0:
                language = Language.Eng;
                break;
            case 1:
                language = Language.Kor;
                break;
            default:
                break;
        }
    }

}
