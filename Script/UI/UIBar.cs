using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIBar : MonoBehaviour
{
    public static UIBar S;

    public Text[] texts;

    public GameObject UIBase;
    public GameObject UIOpenButton;

    //CharacterUI
    public GameObject CharacterUIBase;
    public GameObject[] CharacterUI;
    //End CharacterUI

    public GameObject DicUIbase;

    public Image playerImage;

    //MainStat
    public Text JobName;
    public Text LV;
    public Text HP;
    public Text ATK;
    public Text DEF;
    public Text HIT;
    public Text AVD;
    public Text SPD;
    public Text POW;
    public Text Gold;
    public Text EXP;

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

    public void StatusRefresh()
    {
        playerImage.sprite = Player.S.PlayerImage;
        JobName.text = Player.S.characterName;
        LV.text = "LV : "+Player.S.level.ToString();
        HP.text = "HP : " + Player.S.hp.ToString();
        ATK.text = "ATK : " + Player.S.ATK.ToString();
        DEF.text = "DEF : " + Player.S.DEF.ToString();
        HIT.text = "HIT : " + Player.S.HIT.ToString();
        AVD.text = "AVD : " + Player.S.AVD.ToString();
        SPD.text = "SPD : " + Player.S.SPD.ToString();
        POW.text = "POW : " + Player.S.POW.ToString();
        Gold.text = Player.S.gold.ToString();
        EXP.text = Player.S.exp.ToString();

    }
    public void Start()
    {
        ResetTexts();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (CharacterUIBase.activeInHierarchy)
            {
                CharacterUIBase.SetActive(false);
                UIClose();
            }
            else
            {
                if (Player.S.playerLocation==Player.PlayerLocation.Tower&&TowerMap.S.MoveLock)
                {

                }else OpenCharacterUI();



            }

        }
    }
    public void UIOpen()
    {
        UIBase.SetActive(true);
        UIOpenButton.SetActive(false);
    }
    public void UIClose()
    {
        if (Player.S.playerLocation == Player.PlayerLocation.Tower)
        {
            TowerMap.S.MoveLock = false;
        }
    }
    public void OpenCharacterUI()
    {
        for (int i = 0; i < CharacterUI.Length; i++)
        {
            CharacterUI[i].SetActive(false);
        }
        CharacterUIBase.SetActive(true);
        CharacterUI[0].SetActive(true);
        if (Player.S.playerLocation == Player.PlayerLocation.Tower)
        {
            TowerMap.S.MoveLock = true;
        }
        StatusRefresh();
    }
    public void OpenDic()
    {
        DicUIbase.SetActive(true);
        if (Player.S.playerLocation == Player.PlayerLocation.Tower)
        {
            TowerMap.S.MoveLock = true;
        }
    }
    public void GoCharacterUI(int _num)
    {
        for (int i = 0; i < CharacterUI.Length; i++)
        {
            CharacterUI[i].SetActive(false);
        }
        if (_num == 1)
        {
            Inventory.S.InventoryOn();
        }
        if (_num == 2)
        {
        }
        if (_num == 3)
        {
            SkillUI.S.isSet = false;
            SkillUI.S.OpenSkillUI();
        }
        if (_num == 4)
        {
            StatusUI.S.StatusUIOn();
        }
        CharacterUI[_num].SetActive(true);
    }
    public void GoSkillSetUI()
    {
        for (int i = 0; i < CharacterUI.Length; i++)
        {
            CharacterUI[i].SetActive(false);
        }
        SkillUI.S.isSet = true;
        SkillUI.S.OpenSkillUI();
        CharacterUI[3].SetActive(true);
    }
    public void ResetTexts()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = TextManager.S.GetTextsByName("Basic", texts[i].name);
        }

    }
}
