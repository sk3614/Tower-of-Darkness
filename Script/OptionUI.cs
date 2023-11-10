using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionUI : MonoBehaviour
{

    public Options options;

    public GameObject UIBase;
    public Dropdown resolutionDropdown;
    public Dropdown LanguageDropdown;
    public Button FullScreenButton;

    public Slider slider_BGM;
    public Slider slider_SE;

    public GameObject GotitleUI;
    public GameObject korText;
    public GameObject EngText;

    public Title title;
    private void Awake()
    {
        options = GameObject.Find("Options").GetComponent<Options>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (options.language == Options.Language.Eng)
        {
            LanguageDropdown.value = 0;
            LanguageChange();
        }
        else
        {
            LanguageDropdown.value = 1;
            LanguageChange();
        }

        //if (PlayerPrefs.HasKey("Language"))
        //{
        //    LanguageDropdown.value = PlayerPrefs.GetInt("Language");
        //    LanguageDropdown.RefreshShownValue();
            
        //}
        if (PlayerPrefs.HasKey("Resolution"))
        {
            resolutionDropdown.value = PlayerPrefs.GetInt("Resolution");
            options.resolutionNum= (Options.ResolutionNum)PlayerPrefs.GetInt("Resolution");
            resolutionDropdown.RefreshShownValue();
            ResolutionChange();
        }
        if (PlayerPrefs.HasKey("FullScreen"))
        {
            if (PlayerPrefs.GetInt("FullScreen") == 0)
            {
               
                options.fullScreen = false;
                ResolutionChange();
            }
            if (PlayerPrefs.GetInt("FullScreen") == 1)
            {
                options.fullScreen = true;
                ResolutionChange();
            }
           

        }
        if (options.fullScreen)
        {
            switch (options.language)
            {
                case Options.Language.Kor:
                    FullScreenButton.GetComponentInChildren<Text>().text = "전체화면";
                    break;
                case Options.Language.Eng:
                    FullScreenButton.GetComponentInChildren<Text>().text = "Full Screen";
                    break;
                default:
                    break;
            }

        }
        else
        {
            switch (options.language)
            {
                case Options.Language.Kor:
                    FullScreenButton.GetComponentInChildren<Text>().text = "창모드";
                    break;
                case Options.Language.Eng:
                    FullScreenButton.GetComponentInChildren<Text>().text = "Window";
                    break;
                default:
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (options==null)
        {
            options = GameObject.Find("Options").GetComponent<Options>();
        }
    }

    public void OpenUI()
    {
        UIBase.SetActive(true);
        if (options.fullScreen)
        {

            switch (options.language)
            {
                case Options.Language.Kor:
                    FullScreenButton.GetComponentInChildren<Text>().text = "전체화면";
                    break;
                case Options.Language.Eng:
                    FullScreenButton.GetComponentInChildren<Text>().text = "Full Screen";
                    break;
                default:
                    break;
            }

        }
        else
        {
            switch (options.language)
            {
                case Options.Language.Kor:
                    FullScreenButton.GetComponentInChildren<Text>().text = "창모드";
                    break;
                case Options.Language.Eng:
                    FullScreenButton.GetComponentInChildren<Text>().text = "Window";
                    break;
                default:
                    break;
            }
        }
        slider_BGM.value = options.BgmSound;
        slider_SE.value = options.SESound;
        switch (options.language)
        {
            case Options.Language.Kor:
                LanguageDropdown.value = 1;

                break;
            case Options.Language.Eng:
                LanguageDropdown.value = 0;
                break;
            default:
                break;
        }
        LanguageDropdown.RefreshShownValue();
        resolutionDropdown.RefreshShownValue();
    }
    public void ResolutionChange()
    {
        options.ChangeResolution(resolutionDropdown.value);
        resolutionDropdown.RefreshShownValue();
    }
    public void LanguageChange()
    {
        options.ChangeLanguage(LanguageDropdown.value);
        if (title!=null)
        {
            title.GetTexts();
        }
        if (options.fullScreen)
        {

            switch (options.language)
            {
                case Options.Language.Kor:
                    FullScreenButton.GetComponentInChildren<Text>().text = "전체화면";
                    break;
                case Options.Language.Eng:
                    FullScreenButton.GetComponentInChildren<Text>().text = "Full Screen";
                    break;
                default:
                    break;
            }

        }
        else
        {
            switch (options.language)
            {
                case Options.Language.Kor:
                    FullScreenButton.GetComponentInChildren<Text>().text = "창모드";
                    break;
                case Options.Language.Eng:
                    FullScreenButton.GetComponentInChildren<Text>().text = "Window";
                    break;
                default:
                    break;
            }
        }
        if (LanguageDropdown.value == 0)
        {
            PlayerPrefs.SetInt("Language", 0);
            //EngText.SetActive(true);
            //korText.SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("Language", 1);
            //EngText.SetActive(false);
            //korText.SetActive(true);
        }
    }

    public void ChangeFullScreen()
    {
        if (options.fullScreen)
        {
            switch (options.language)
            {
                case Options.Language.Kor:
                    FullScreenButton.GetComponentInChildren<Text>().text = "창모드";
                    break;
                case Options.Language.Eng:
                    FullScreenButton.GetComponentInChildren<Text>().text = "Window";
                    break;
                default:
                    break;
            }
            options.fullScreen = false;
            PlayerPrefs.SetInt("FullScreen", 0);//window
        }
        else
        {
            switch (options.language)
            {
                case Options.Language.Kor:
                    FullScreenButton.GetComponentInChildren<Text>().text = "전체화면";
                    break;
                case Options.Language.Eng:
                    FullScreenButton.GetComponentInChildren<Text>().text = "Full Screen";
                    break;
                default:
                    break;
            }
            options.fullScreen = true;
            PlayerPrefs.SetInt("FullScreen", 1);//full
        }
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, options.fullScreen);
        ResolutionChange();
    }
    public void SetSEVolume()
    {
       options.SESound = slider_SE.value;
        SoundManager.S.SetSEVolume();
    }

    public void SetBGMVolume()
    {
        options.BgmSound = slider_BGM.value;
        SoundManager.S.SetBGMVolume();
    }

    public void GoTitleCaution()
    {
        GotitleUI.SetActive(true);
    }

    public void GoTitle()
    {
        SceneChanger.S.ResetGame();
    }
}
