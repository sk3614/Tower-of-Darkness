using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gambler : MonoBehaviour
{
    public static Gambler S;

    public GameObject uibase;
    public GameObject SetUi;
    public Text SetUIText;
    public Text[] SetUIButtonTexts;
    public GameObject rewardUi;
    public Text RewardUIText;
    public Button[] SetButtons;
    public Button rewardButton;
    public Text Nowgold;
    public Text rewardText;
    public Text curGrade;

    public GameObject[] SetGambleUI;
    public void Awake()
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
        if (Player.S.mainProgress == 3 ||
            Player.S.mainProgress == 5||
            Player.S.mainProgress== 7||
            Player.S.mainProgress==9||
            Player.S.mainProgress == 10)
        {
            Player.S.GambleNum = 0;
        }
    }

    public void UIOn()
    {
        uibase.SetActive(true);
        SetText();

        if (Player.S.mainProgress>=3)
        {
            if (Player.S.mainProgress%2==0)
            {
                rewardUi.SetActive(true);
                SetUi.SetActive(false);
                SetRewardText();
            }
            else
            {
                SetUi.SetActive(true);
                if (Player.S.GambleNum == 0)
                {
                    for (int i = 0; i < SetButtons.Length; i++)
                    {
                        SetButtons[i].gameObject.SetActive(true);
                        SetUIButtonTexts[i].gameObject.SetActive(true);
                    }
                }

                rewardUi.SetActive(false);
            }
        }
        /*
         switch (Player.S.mainProgress)
        {
            case 3:

                SetUi.SetActive(true);
                if (Player.S.GambleNum==0)
                {
                    for (int i = 0; i < SetButtons.Length; i++)
                    {
                        SetButtons[i].gameObject.SetActive(true);
                        SetUIButtonTexts[i].gameObject.SetActive(true);
                    }
                }

                rewardUi.SetActive(false);
                break;
            case 4:
                rewardUi.SetActive(true);
                SetUi.SetActive(false);
                SetRewardText();
                break;
            case 5:
                SetUi.SetActive(true);
                if (Player.S.GambleNum == 0)
                {
                    for (int i = 0; i < SetButtons.Length; i++)
                    {
                        SetButtons[i].gameObject.SetActive(true);
                        SetUIButtonTexts[i].gameObject.SetActive(true);
                    }
                }

                rewardUi.SetActive(false);
                break;
            case 6:
                rewardUi.SetActive(true);
                SetUi.SetActive(false);
                SetRewardText();
                break;
            case 7:
                SetUi.SetActive(true);
                if (Player.S.GambleNum == 0)
                {
                    for (int i = 0; i < SetButtons.Length; i++)
                    {
                        SetButtons[i].gameObject.SetActive(true);
                        SetUIButtonTexts[i].gameObject.SetActive(true);
                    }
                }

                rewardUi.SetActive(false);
                break;
            case 8:
                rewardUi.SetActive(true);
                SetUi.SetActive(false);
                SetRewardText();
                break;
            case 9:
                SetUi.SetActive(true);
                if (Player.S.GambleNum == 0)
                {
                    for (int i = 0; i < SetButtons.Length; i++)
                    {
                        SetButtons[i].gameObject.SetActive(true);
                        SetUIButtonTexts[i].gameObject.SetActive(true);
                    }
                }

                rewardUi.SetActive(false);
                break;
            case 10:
                rewardUi.SetActive(true);
                SetUi.SetActive(false);
                SetRewardText();
                break;
            case 11:
                SetUi.SetActive(true);
                if (Player.S.GambleNum == 0)
                {
                    for (int i = 0; i < SetButtons.Length; i++)
                    {
                        SetButtons[i].gameObject.SetActive(true);
                        SetUIButtonTexts[i].gameObject.SetActive(true);
                    }
                }

                rewardUi.SetActive(false);
                break;
            case 12:
                rewardUi.SetActive(true);
                SetUi.SetActive(false);
                SetRewardText();
                break;
            default:
                break;
        }*/

    }

    public void SetGambleCaution(int _num)
    {
        switch (_num)
        {
            case 1:
                DialogueManager.S.TextSet(180, 180, null, 8);
                break;
            case 2:
                DialogueManager.S.TextSet(181, 181, null, 9);
                break;
            case 3:
                DialogueManager.S.TextSet(182, 182,null,10);
                break;
            default:
                break;
        }
    }

    public void SetGamble(int _num)
    {
        switch (_num)
        {
            case 1:
                if (Player.S.gold >= GetNeedGold(1))
                {
                    Player.S.SpendGold(GetNeedGold(1));
                    DialogueManager.S.TextSet(183, 183);
                }
                else
                {
                    DialogueManager.S.TextSet(187, 187);
                    for (int i = 0; i < SetGambleUI.Length; i++)
                    {
                        SetGambleUI[i].SetActive(false);
                    }
                    return;
                }
                SoundManager.S.PlaySE("income");
                Player.S.GambleNum = 1;
                break;
            case 2:
                if (Player.S.gold >= GetNeedGold(2))
                {
                    Player.S.SpendGold(GetNeedGold(2));
                    DialogueManager.S.TextSet(183, 183);
                }
                else
                {
                    DialogueManager.S.TextSet(187, 187);
                    for (int i = 0; i < SetGambleUI.Length; i++)
                    {
                        SetGambleUI[i].SetActive(false);
                    }
                    return;
                }
                SoundManager.S.PlaySE("income");
                Player.S.GambleNum = 2;
                break;
            case 3:
                if (Player.S.gold >= GetNeedGold(3))
                {
                    Player.S.SpendGold(GetNeedGold(3));
                    DialogueManager.S.TextSet(183, 183);
                }
                else
                {
                    DialogueManager.S.TextSet(187, 187);
                    for (int i = 0; i < SetGambleUI.Length; i++)
                    {
                        SetGambleUI[i].SetActive(false);
                    }
                    return;
                }
                SoundManager.S.PlaySE("income");
                Player.S.GambleNum = 3;
                break;
            default:
                break;
        }
        for (int i = 0; i < SetButtons.Length; i++)
        {
            SetButtons[i].gameObject.SetActive(false);
            SetUIButtonTexts[i].gameObject.SetActive(false);
        }
        SetGambleGradeText();
        Player.S.GambleValue = Random.Range(0, 100);
        for (int i = 0; i < SetGambleUI.Length; i++)
        {
            SetGambleUI[i].SetActive(false);
        }
    }
    public void RewardSet()
    {
        Player.S.GambleNum = 0;
        rewardText.text = "";
        rewardButton.gameObject.SetActive(false);
        SetGambleGradeText();
    }

    public void SetText()
    {
        Nowgold.text = "GOLD : " + Player.S.gold;
        SetGambleGradeText();
        switch (Options.S.language)
        {
            case Options.Language.Kor:
                SetUIText.text = "도박사에게 일정 GOLD를 지불하여\n다음 타운 방문 시 보상을 얻을 수 있습니다.\n보상을 얻을 수 없을 수도 있습니다.";
                if (Player.S.GambleNum==0)
                {
                    RewardUIText.text = "도박을 하지않아 보상을 받을 수 없습니다.";
                }
                else
                {
                    RewardUIText.text = "받기 버튼을 눌러 보상을 확인 할 수 있습니다";
                }

                break;
            case Options.Language.Eng:
                SetUIText.text = "You can pay a certain GOLD to the gambler\n for a reward on your next town visit.\n You may not be able to get a reward.";
                if (Player.S.GambleNum == 0)
                {
                    RewardUIText.text = "You can't get compensation \n because you don't gamble.";
                }
                else
                {
                    RewardUIText.text = "You can confirm the reward by pressing\n the Receive button.";
                }

                break;
            default:
                break;
        }
        switch (Player.S.mainProgress)
        {
            case 3:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        SetUIButtonTexts[0].text = "일반 : 10 GOLD";
                        SetUIButtonTexts[1].text = "고급 : 20 GOLD";
                        SetUIButtonTexts[2].text = "올인 : 40 GOLD";
                        for (int i = 0; i < SetButtons.Length; i++)
                        {
                            SetButtons[i].gameObject.GetComponentInChildren<Text>().text = "선택";

                        }
                        break;
                    case Options.Language.Eng:
                        SetUIButtonTexts[0].text = "Normal : 10 GOLD";
                        SetUIButtonTexts[1].text = "HighRisk : 20 GOLD";
                        SetUIButtonTexts[2].text = "AllIn : 40 GOLD";
                        for (int i = 0; i < SetButtons.Length; i++)
                        {
                            SetButtons[i].gameObject.GetComponentInChildren<Text>().text = "Select";
                        }
                        break;
                    default:
                        break;
                }
                break;
            case 4:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        for (int i = 0; i < SetButtons.Length; i++)
                        {
                            rewardButton.gameObject.GetComponentInChildren<Text>().text = "보상 받기";
                        }
                        break;
                    case Options.Language.Eng:
                        rewardButton.gameObject.GetComponentInChildren<Text>().text = "Get Reward";
                        break;
                    default:
                        break;
                }
                break;
            case 5:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        SetUIButtonTexts[0].text = "일반 : 20 GOLD";
                        SetUIButtonTexts[1].text = "고급 : 40 GOLD";
                        SetUIButtonTexts[2].text = "올인 : 70 GOLD";
                        for (int i = 0; i < SetButtons.Length; i++)
                        {
                            SetButtons[i].gameObject.GetComponentInChildren<Text>().text = "선택";

                        }
                        break;
                    case Options.Language.Eng:
                        SetUIButtonTexts[0].text = "Normal : 20 GOLD";
                        SetUIButtonTexts[1].text = "HighRisk : 40 GOLD";
                        SetUIButtonTexts[2].text = "AllIn : 70 GOLD";
                        for (int i = 0; i < SetButtons.Length; i++)
                        {
                            SetButtons[i].gameObject.GetComponentInChildren<Text>().text = "Select";
                        }
                        break;
                    default:
                        break;
                }
                break;
            case 7:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        SetUIButtonTexts[0].text = "일반 : 30 GOLD";
                        SetUIButtonTexts[1].text = "고급 : 60 GOLD";
                        SetUIButtonTexts[2].text = "올인 : 100 GOLD";
                        for (int i = 0; i < SetButtons.Length; i++)
                        {
                            SetButtons[i].gameObject.GetComponentInChildren<Text>().text = "선택";

                        }
                        break;
                    case Options.Language.Eng:
                        SetUIButtonTexts[0].text = "Normal : 30 GOLD";
                        SetUIButtonTexts[1].text = "HighRisk : 60 GOLD";
                        SetUIButtonTexts[2].text = "AllIn : 100 GOLD";
                        for (int i = 0; i < SetButtons.Length; i++)
                        {
                            SetButtons[i].gameObject.GetComponentInChildren<Text>().text = "Select";
                        }
                        break;
                    default:
                        break;
                }
                break;
            case 9:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        SetUIButtonTexts[0].text = "일반 : 50 GOLD";
                        SetUIButtonTexts[1].text = "고급 : 75 GOLD";
                        SetUIButtonTexts[2].text = "올인 : 120 GOLD";
                        for (int i = 0; i < SetButtons.Length; i++)
                        {
                            SetButtons[i].gameObject.GetComponentInChildren<Text>().text = "선택";

                        }
                        break;
                    case Options.Language.Eng:
                        SetUIButtonTexts[0].text = "Normal : 50 GOLD";
                        SetUIButtonTexts[1].text = "HighRisk : 75 GOLD";
                        SetUIButtonTexts[2].text = "AllIn : 120 GOLD";
                        for (int i = 0; i < SetButtons.Length; i++)
                        {
                            SetButtons[i].gameObject.GetComponentInChildren<Text>().text = "Select";
                        }
                        break;
                    default:
                        break;
                }
                break;
            case 11:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        SetUIButtonTexts[0].text = "일반 : 100 GOLD";
                        SetUIButtonTexts[1].text = "고급 : 150 GOLD";
                        SetUIButtonTexts[2].text = "올인 : 200 GOLD";
                        for (int i = 0; i < SetButtons.Length; i++)
                        {
                            SetButtons[i].gameObject.GetComponentInChildren<Text>().text = "선택";

                        }
                        break;
                    case Options.Language.Eng:
                        SetUIButtonTexts[0].text = "Normal : 100 GOLD";
                        SetUIButtonTexts[1].text = "HighRisk : 150 GOLD";
                        SetUIButtonTexts[2].text = "AllIn : 200 GOLD";
                        for (int i = 0; i < SetButtons.Length; i++)
                        {
                            SetButtons[i].gameObject.GetComponentInChildren<Text>().text = "Select";
                        }
                        break;
                    default:
                        break;
                }
                break;
            case 13:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        SetUIButtonTexts[0].text = "일반 : 100 GOLD";
                        SetUIButtonTexts[1].text = "고급 : 150 GOLD";
                        SetUIButtonTexts[2].text = "올인 : 200 GOLD";
                        for (int i = 0; i < SetButtons.Length; i++)
                        {
                            SetButtons[i].gameObject.GetComponentInChildren<Text>().text = "선택";

                        }
                        break;
                    case Options.Language.Eng:
                        SetUIButtonTexts[0].text = "Normal : 100 GOLD";
                        SetUIButtonTexts[1].text = "HighRisk : 150 GOLD";
                        SetUIButtonTexts[2].text = "AllIn : 200 GOLD";
                        for (int i = 0; i < SetButtons.Length; i++)
                        {
                            SetButtons[i].gameObject.GetComponentInChildren<Text>().text = "Select";
                        }
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }
    public void SetRewardText()
    {
        switch (Player.S.mainProgress)
        {
            case 4:
                switch (Player.S.GambleNum)
                {
                    case 1:
                        DialogueManager.S.TextSet(185, 185);
                        if (Player.S.GambleValue < 50)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 초록 물약";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Green Potion";
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 돌 열쇠";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Stone Key";
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (Player.S.GambleValue < 87)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 푸른 물약";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 1 Blue Potion";
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 쇠 열쇠 1";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 1 Metal Key";
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case 2:
                        if (Player.S.GambleValue < 12)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 없음";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : None";
                                    break;
                                default:
                                    break;
                            }
                            Player.S.GambleNum = 0;
                            DialogueManager.S.TextSet(186, 186);
                        }
                        else if (Player.S.GambleValue < 25)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 20 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 20 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 50)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 루비(ATK+1)";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Ruby(ATK+1)";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 사파이어(DEF+1)";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Sapphire(DEF+1)";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 81)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 쇠 열쇠";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Metal Key";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 87)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 붉은 물약";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Red Potion";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 93)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 신비한 가루 3개";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Mystic Powder X3";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : CP 강화석";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : CP stone";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        break;
                    case 3:
                        if (Player.S.GambleValue < 50)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 없음";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : None";
                                    break;
                                default:
                                    break;
                            }
                            Player.S.GambleNum = 0;
                            DialogueManager.S.TextSet(186, 186);
                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 40 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 40 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 90)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 80 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 80 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 97)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 100 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 100 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 99)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 140 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 140 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 200 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 200 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        break;
                    default:
                        break;
                }

                break;
            case 6:
                switch (Player.S.GambleNum)
                {
                    case 1:
                        DialogueManager.S.TextSet(185, 185);
                        if (Player.S.GambleValue < 25)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 푸른 물약";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Blue Potion";
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 돌 열쇠 2개";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Stone Key X2";
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (Player.S.GambleValue < 87)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 비밀방 두루마리 X3";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Reveal scroll X3";
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 쇠 열쇠";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Metal Key";
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case 2:
                        if (Player.S.GambleValue < 12)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 없음";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : None";
                                    break;
                                default:
                                    break;
                            }
                            Player.S.GambleNum = 0;
                            DialogueManager.S.TextSet(186, 186);
                        }
                        else if (Player.S.GambleValue < 25)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 40 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 40 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 50)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 루비(ATK+2)";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Ruby(ATK+2)";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 사파이어(DEF+2)";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Sapphire(DEF+2)";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 81)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 쇠 열쇠 X2";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Metal Key X2";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 87)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 흰 물약";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : White Potion";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 93)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : LV +1";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : LV +1";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : CP 강화석";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : CP stone";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        break;
                    case 3:
                        if (Player.S.GambleValue < 50)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 없음";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : None";
                                    break;
                                default:
                                    break;
                            }
                            Player.S.GambleNum = 0;
                            DialogueManager.S.TextSet(186, 186);
                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 70 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 70 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 90)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 140 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 140 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 97)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 180 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 180 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 99)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 225 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 225 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 300 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 300 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        break;
                    default:
                        break;
                }

                break;
            case 8:
                switch (Player.S.GambleNum)
                {
                    case 1:
                        DialogueManager.S.TextSet(185, 185);
                        if (Player.S.GambleValue < 25)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 돌 열쇠 X2";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Stone Key X2";
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 쇠 열쇠";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Metal Key";
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (Player.S.GambleValue < 87)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 금 열쇠 ";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Gold Key";
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 연막탄 X5";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Smoke Bomb X5";
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case 2:
                        if (Player.S.GambleValue < 12)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 없음";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : None";
                                    break;
                                default:
                                    break;
                            }
                            Player.S.GambleNum = 0;
                            DialogueManager.S.TextSet(186, 186);
                        }
                        else if (Player.S.GambleValue < 25)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 60 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 60 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 50)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 루비(ATK+3)";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Ruby(ATK+3)";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 사파이어(DEF+3)";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Sapphire(DEF+3)";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 81)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 신석";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward :God stone";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 87)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 신비한 가루 X4";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Mystic powder X4";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 93)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 120 Gold";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward :120 Gold";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : CP 강화석";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : CP stone";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        break;
                    case 3:
                        if (Player.S.GambleValue < 50)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 없음";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : None";
                                    break;
                                default:
                                    break;
                            }
                            Player.S.GambleNum = 0;
                            DialogueManager.S.TextSet(186, 186);
                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 100 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 100 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 90)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 200 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 200 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 97)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 250 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 250 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 99)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 325 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 325 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 450 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 450 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        break;
                    default:
                        break;
                }
                break;
            case 10:
                switch (Player.S.GambleNum)
                {
                    case 1:
                        DialogueManager.S.TextSet(185, 185);
                        if (Player.S.GambleValue < 50)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 비밀방 두루마리 X2";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Reveal scroll X2";
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 연막탄 X5";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Smoke Bomb X5";
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (Player.S.GambleValue < 87)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 신비한 가루 X3 ";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Mystic powder X3";
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 보석 열쇠";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Jewel key";
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case 2:
                        if (Player.S.GambleValue < 12)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 없음";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : None";
                                    break;
                                default:
                                    break;
                            }
                            Player.S.GambleNum = 0;
                            DialogueManager.S.TextSet(186, 186);
                        }
                        else if (Player.S.GambleValue < 25)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 75 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 75 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 50)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 루비(ATK+3)";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Ruby(ATK+3)";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 사파이어(DEF+3)";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Sapphire(DEF+3)";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 81)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 신석";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward :God stone";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 87)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 신비한 가루 X4";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Mystic powder X4";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 93)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 150 Gold";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward :150 Gold";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : EXP +100";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : EXP +100";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        break;
                    case 3:
                        if (Player.S.GambleValue < 50)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 없음";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : None";
                                    break;
                                default:
                                    break;
                            }
                            Player.S.GambleNum = 0;
                            DialogueManager.S.TextSet(186, 186);
                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 120 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 120 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 90)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 240 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 240 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 97)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 300 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 300 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 99)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 400 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 400 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 600 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 600 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        break;
                    default:
                        break;
                }
                break;
            case 12:
                switch (Player.S.GambleNum)
                {
                    case 1:
                        DialogueManager.S.TextSet(185, 185);
                        if (Player.S.GambleValue < 50)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 푸른 물약 X1";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Blue Potion X1";
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 붉은 물약 X1";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Red Potion X1";
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (Player.S.GambleValue < 87)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 초록 물약 X1";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Green Potion X1";
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 흰 물약 X1";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : White Potion X1";
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case 2:
                        if (Player.S.GambleValue < 25)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 루비 (ATK+4)";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Ruby (ATK+4)";
                                    break;
                                default:
                                    break;
                            }
                            Player.S.GambleNum = 0;
                            DialogueManager.S.TextSet(186, 186);
                        }
                        else if (Player.S.GambleValue < 50)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 :사파이어 (DEF+3)";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Sapphire (DEF+4)";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 62)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 꽝";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : None";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 150 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 150 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 81)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 신석 X1";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward :God stone X1";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 87)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 신비한 가루 X6";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Mystic powder X6";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 93)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 300 Gold";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward :300 Gold";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 보석 열쇠 X1";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : Jewel key X1";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        break;
                    case 3:
                        if (Player.S.GambleValue < 50)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 없음";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : None";
                                    break;
                                default:
                                    break;
                            }
                            Player.S.GambleNum = 0;
                            DialogueManager.S.TextSet(186, 186);
                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 200 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 200 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 90)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 400 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 400 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 97)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 500 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 500 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 99)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 666 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 666 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            switch (Options.S.language)
                            {
                                case Options.Language.Kor:
                                    rewardText.text = "보상 : 1000 GOLD";
                                    break;
                                case Options.Language.Eng:
                                    rewardText.text = "Reward : 1000 GOLD";
                                    break;
                                default:
                                    break;
                            }
                            DialogueManager.S.TextSet(185, 185);
                        }
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }

    public void GetReward()
    {
        switch (Player.S.mainProgress)
        {
            case 4:
                switch (Player.S.GambleNum)
                {
                    case 1:
                        if (Player.S.GambleValue < 50)
                        {
                            Player.S.hp += 150;
                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            AddItem.S.SearchItem("돌 열쇠");
                        }
                        else if (Player.S.GambleValue < 87)
                        {
                            Player.S.hp += 300;
                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            AddItem.S.SearchItem("쇠 열쇠");
                        }
                        break;
                    case 2:
                        if (Player.S.GambleValue < 12)
                        {

                        }
                        else if (Player.S.GambleValue < 25)
                        {
                            Player.S.GetGold(20);
                        }
                        else if (Player.S.GambleValue < 50)
                        {
                            Player.S.ATK += 1;

                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            Player.S.DEF += 1;

                        }
                        else if (Player.S.GambleValue < 81)
                        {
                            AddItem.S.SearchItem("쇠 열쇠");

                        }
                        else if (Player.S.GambleValue < 87)
                        {
                            Player.S.hp += 500;

                        }
                        else if (Player.S.GambleValue < 93)
                        {
                            AddItem.S.SearchItem("신비한 가루",3);

                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            Player.S.CP += 1;
                        }
                        break;
                    case 3:
                        if (Player.S.GambleValue < 50)
                        {

                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            Player.S.GetGold(40);
                        }
                        else if (Player.S.GambleValue < 90)
                        {
                            Player.S.GetGold(80);
                        }
                        else if (Player.S.GambleValue < 97)
                        {
                            Player.S.GetGold(100);
                        }
                        else if (Player.S.GambleValue < 99)
                        {
                            Player.S.GetGold(140);
                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            Player.S.GetGold(200);
                        }
                        break;
                    default:
                        break;
                }

                break;
            case 6:
                switch (Player.S.GambleNum)
                {
                    case 1:
                        if (Player.S.GambleValue < 50)
                        {
                            Player.S.hp += 300;
                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            AddItem.S.SearchItem("돌 열쇠",2);
                        }
                        else if (Player.S.GambleValue < 87)
                        {
                            AddItem.S.SearchItem("비밀방 두루마리", 3);
                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            AddItem.S.SearchItem("쇠 열쇠");
                        }
                        break;
                    case 2:
                        if (Player.S.GambleValue < 12)
                        {

                        }
                        else if (Player.S.GambleValue < 25)
                        {
                            Player.S.GetGold(40);
                        }
                        else if (Player.S.GambleValue < 50)
                        {
                            Player.S.ATK += 2;

                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            Player.S.DEF += 2;

                        }
                        else if (Player.S.GambleValue < 81)
                        {
                            AddItem.S.SearchItem("쇠 열쇠",2);

                        }
                        else if (Player.S.GambleValue < 87)
                        {
                            Player.S.hp += 750;

                        }
                        else if (Player.S.GambleValue < 93)
                        {
                            Player.S.LevelUP();

                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            Player.S.CP += 1;
                        }
                        break;
                    case 3:
                        if (Player.S.GambleValue < 50)
                        {

                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            Player.S.GetGold(70);
                        }
                        else if (Player.S.GambleValue < 90)
                        {
                            Player.S.GetGold(140);
                        }
                        else if (Player.S.GambleValue < 97)
                        {
                            Player.S.GetGold(180);
                        }
                        else if (Player.S.GambleValue < 99)
                        {
                            Player.S.GetGold(225);
                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            Player.S.GetGold(300);
                        }
                        break;
                    default:
                        break;
                }

                break;
            case 8:
                switch (Player.S.GambleNum)
                {
                    case 1:
                        if (Player.S.GambleValue < 50)
                        {
                            AddItem.S.SearchItem("돌 열쇠",2);
                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            AddItem.S.SearchItem("쇠 열쇠");
                        }
                        else if (Player.S.GambleValue < 87)
                        {
                            AddItem.S.SearchItem("금 열쇠");
                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            AddItem.S.SearchItem("연막탄",5);
                        }
                        break;
                    case 2:
                        if (Player.S.GambleValue < 12)
                        {

                        }
                        else if (Player.S.GambleValue < 25)
                        {
                            Player.S.GetGold(60);
                        }
                        else if (Player.S.GambleValue < 50)
                        {
                            Player.S.ATK += 3;

                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            Player.S.DEF += 3;

                        }
                        else if (Player.S.GambleValue < 81)
                        {
                            AddItem.S.SearchItem("신석");

                        }
                        else if (Player.S.GambleValue < 87)
                        {
                            AddItem.S.SearchItem("신비한 가루", 4);

                        }
                        else if (Player.S.GambleValue < 93)
                        {
                            Player.S.GetGold(120);

                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            Player.S.CP += 1;
                        }
                        break;
                    case 3:
                        if (Player.S.GambleValue < 50)
                        {

                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            Player.S.GetGold(100);
                        }
                        else if (Player.S.GambleValue < 90)
                        {
                            Player.S.GetGold(200);
                        }
                        else if (Player.S.GambleValue < 97)
                        {
                            Player.S.GetGold(250);
                        }
                        else if (Player.S.GambleValue < 99)
                        {
                            Player.S.GetGold(325);
                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            Player.S.GetGold(450);
                        }
                        break;
                    default:
                        break;
                }

                break;
            case 10:
                switch (Player.S.GambleNum)
                {
                    case 1:
                        if (Player.S.GambleValue < 50)
                        {
                            AddItem.S.SearchItem("비밀방 두루마리", 2);
                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            AddItem.S.SearchItem("신비한 가루",3);
                        }
                        else if (Player.S.GambleValue < 87)
                        {
                            AddItem.S.SearchItem("보석 열쇠");
                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            AddItem.S.SearchItem("연막탄", 5);
                        }
                        break;
                    case 2:
                        if (Player.S.GambleValue < 12)
                        {

                        }
                        else if (Player.S.GambleValue < 25)
                        {
                            Player.S.GetGold(75);
                        }
                        else if (Player.S.GambleValue < 50)
                        {
                            Player.S.ATK += 3;

                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            Player.S.DEF += 3;

                        }
                        else if (Player.S.GambleValue < 81)
                        {
                            AddItem.S.SearchItem("신석");

                        }
                        else if (Player.S.GambleValue < 87)
                        {
                            AddItem.S.SearchItem("신비한 가루", 4);

                        }
                        else if (Player.S.GambleValue < 93)
                        {
                            Player.S.GetGold(150);

                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            Player.S.GetExp(100);
                        }
                        break;
                    case 3:
                        if (Player.S.GambleValue < 50)
                        {

                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            Player.S.GetGold(120);
                        }
                        else if (Player.S.GambleValue < 90)
                        {
                            Player.S.GetGold(240);
                        }
                        else if (Player.S.GambleValue < 97)
                        {
                            Player.S.GetGold(300);
                        }
                        else if (Player.S.GambleValue < 99)
                        {
                            Player.S.GetGold(400);
                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            Player.S.GetGold(600);
                        }
                        break;
                    default:
                        break;
                }

                break;
            case 12:
                switch (Player.S.GambleNum)
                {
                    case 1:
                        if (Player.S.GambleValue < 50)
                        {
                            Player.S.hp += 300;
                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            Player.S.hp += 500;
                        }
                        else if (Player.S.GambleValue < 87)
                        {
                            Player.S.hp += 150;
                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            Player.S.hp += 1000;
                        }
                        break;
                    case 2:
                        if (Player.S.GambleValue < 25)
                        {
                            Player.S.ATK += Player.S.CurTowerNum;
                        }
                        else if (Player.S.GambleValue < 50)
                        {
                            Player.S.DEF += Player.S.CurTowerNum;
                        }
                        else if (Player.S.GambleValue < 62)
                        {
                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            Player.S.GetGold(150);

                        }
                        else if (Player.S.GambleValue < 81)
                        {
                            AddItem.S.SearchItem("신석");

                        }
                        else if (Player.S.GambleValue < 87)
                        {
                            AddItem.S.SearchItem("신비한 가루", 6);

                        }
                        else if (Player.S.GambleValue < 93)
                        {
                            Player.S.GetGold(300);

                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            AddItem.S.SearchItem("보석 열쇠");
                        }
                        break;
                    case 3:
                        if (Player.S.GambleValue < 50)
                        {

                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            Player.S.GetGold(200);
                        }
                        else if (Player.S.GambleValue < 90)
                        {
                            Player.S.GetGold(400);
                        }
                        else if (Player.S.GambleValue < 97)
                        {
                            Player.S.GetGold(500);
                        }
                        else if (Player.S.GambleValue < 99)
                        {
                            Player.S.GetGold(666);
                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            Player.S.GetGold(1000);
                        }
                        break;
                    default:
                        break;
                }

                break;
            case 14:
                switch (Player.S.GambleNum)
                {
                    case 1:
                        if (Player.S.GambleValue < 50)
                        {
                            Player.S.hp += 300;
                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            Player.S.hp += 500;
                        }
                        else if (Player.S.GambleValue < 87)
                        {
                            Player.S.hp += 150;
                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            Player.S.hp += 1000;
                        }
                        break;
                    case 2:
                        if (Player.S.GambleValue < 25)
                        {
                            Player.S.ATK += Player.S.CurTowerNum;
                        }
                        else if (Player.S.GambleValue < 50)
                        {
                            Player.S.DEF += Player.S.CurTowerNum;
                        }
                        else if (Player.S.GambleValue < 62)
                        {
                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            Player.S.GetGold(150);

                        }
                        else if (Player.S.GambleValue < 81)
                        {
                            AddItem.S.SearchItem("신석");

                        }
                        else if (Player.S.GambleValue < 87)
                        {
                            AddItem.S.SearchItem("신비한 가루", 6);

                        }
                        else if (Player.S.GambleValue < 93)
                        {
                            Player.S.GetGold(300);

                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            AddItem.S.SearchItem("보석 열쇠");
                        }
                        break;
                    case 3:
                        if (Player.S.GambleValue < 50)
                        {

                        }
                        else if (Player.S.GambleValue < 75)
                        {
                            Player.S.GetGold(200);
                        }
                        else if (Player.S.GambleValue < 90)
                        {
                            Player.S.GetGold(400);
                        }
                        else if (Player.S.GambleValue < 97)
                        {
                            Player.S.GetGold(500);
                        }
                        else if (Player.S.GambleValue < 99)
                        {
                            Player.S.GetGold(666);
                        }
                        else if (Player.S.GambleValue < 100)
                        {
                            Player.S.GetGold(1000);
                        }
                        break;
                    default:
                        break;
                }

                break;
            default:
                break;
        }
        SoundManager.S.PlaySE("income");
        RewardSet();
        SetText();
    }
    public void SetGambleGradeText()
    {
        switch (Player.S.GambleNum)
        {
            case 0:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        curGrade.text = " 현재 선택 등급 : 없음.";
                        break;
                    case Options.Language.Eng:
                        curGrade.text = " Now Grade : None";
                        break;
                    default:
                        break;
                }

                break;
            case 1:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        curGrade.text = " 현재 선택 등급 : 일반";
                        break;
                    case Options.Language.Eng:
                        curGrade.text = " Now Grade : Normal";
                        break;
                    default:
                        break;
                }

                break;
            case 2:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        curGrade.text = " 현재 선택 등급 : 고급";
                        break;
                    case Options.Language.Eng:
                        curGrade.text = " Now Grade : High Risk";
                        break;
                    default:
                        break;
                }
                break;
            case 3:
                switch (Options.S.language)
                {
                    case Options.Language.Kor:
                        curGrade.text = " 현재 선택 등급 : 올인";
                        break;
                    case Options.Language.Eng:
                        curGrade.text = " Now Grade : AllIn";
                        break;
                    default:
                        break;
                }

                break;
            default:
                break;
        }
        Nowgold.text = "GOLD : " + Player.S.gold;
    }
    public int GetNeedGold(int _grade)
    {
        int gold=0;

        switch (Player.S.mainProgress)
        {
            case 3:
                if (_grade == 1) gold = 10;
                if (_grade == 2) gold = 20;
                if (_grade == 3) gold = 40;
                break;
            case 5:
                if (_grade == 1) gold = 20;
                if (_grade == 2) gold = 40;
                if (_grade == 3) gold = 70;
                break;
            case 7:
                if (_grade == 1) gold = 30;
                if (_grade == 2) gold = 60;
                if (_grade == 3) gold = 100;
                break;
            case 9:
                if (_grade == 1) gold = 50;
                if (_grade == 2) gold = 75;
                if (_grade == 3) gold = 120;
                break;
            case 11:
                if (_grade == 1) gold = 100;
                if (_grade == 2) gold = 150;
                if (_grade == 3) gold = 200;
                break;
            case 13:
                if (_grade == 1) gold = 100;
                if (_grade == 2) gold = 150;
                if (_grade == 3) gold = 200;
                break;
            case 15:
                if (_grade == 1) gold = 100;
                if (_grade == 2) gold = 150;
                if (_grade == 3) gold = 200;
                break;

            default:
                break;
        }




        return gold;
    }



}
