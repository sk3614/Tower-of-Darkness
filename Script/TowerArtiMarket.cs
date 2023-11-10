using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TowerArtiMarket : MonoBehaviour
{
    public GameObject InfoUI;
    public Image ArtiImage;
    public Text ArtiName;
    public Text artiInfo;
    public List<Artifact> SetArtis;
    public Image[] images;
    public GameObject[] obs;

    public void Floor9MarketOn()
    {
        SetArtis = new List<Artifact>(ArtifactManager.S.RandomArtifact("레어", 3));
        TowerVariable.S.floor9Shop2 = true;
        for (int i = 0; i < images.Length; i++)
        {
            images[i].sprite = SetArtis[i].image;
        }
    }
    public void FirstMarketOn()
    {
        SetArtis.Add(ArtifactManager.S.RandomArtifact("노말", 1)[0]);
        SetArtis.Add(ArtifactManager.S.RandomArtifact("레어", 1)[0]);
        SetArtis.Add(ArtifactManager.S.RandomArtifact("유니크", 1)[0]);
        TowerVariable.S.Arti1 = SetArtis[0].artifactName;
        TowerVariable.S.Arti2 = SetArtis[1].artifactName;
        TowerVariable.S.Arti3 = SetArtis[2].artifactName;

        TowerVariable.S.market11 = true;
        for (int i = 0; i < images.Length; i++)
        {
            images[i].sprite = SetArtis[i].image;
        }
    }

    public void T4FirstMarketOn()
    {
        SetArtis.Add(ArtifactManager.S.RandomArtifact("레어", 1)[0]);
        SetArtis.Add(ArtifactManager.S.RandomArtifact("유니크", 1)[0]);
        SetArtis.Add(ArtifactManager.S.RandomArtifact("에픽", 1)[0]);
        TowerVariable.S.T4Arti1 = SetArtis[0].artifactName;
        TowerVariable.S.T4Arti2 = SetArtis[1].artifactName;
        TowerVariable.S.T4Arti3 = SetArtis[2].artifactName;

        TowerVariable.S.market20 = true;
        for (int i = 0; i < images.Length; i++)
        {
            images[i].sprite = SetArtis[i].image;
        }
    }
    public void T4AfterMarketOn()
    {
        SetArtis.Clear();
        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(true);
        }
        SetArtis.Add(ArtifactManager.S.SearchArti(TowerVariable.S.T4Arti1));
        SetArtis.Add(ArtifactManager.S.SearchArti(TowerVariable.S.T4Arti2));
        SetArtis.Add(ArtifactManager.S.SearchArti(TowerVariable.S.T4Arti3));

        if (TowerVariable.S.T4Arti1 == "구매완료")
        {
            obs[0].gameObject.SetActive(false);
        }

        if (TowerVariable.S.T4Arti2 == "구매완료")
        {
            obs[1].gameObject.SetActive(false);
        }

        if (TowerVariable.S.T4Arti3 == "구매완료")
        {
            obs[2].gameObject.SetActive(false);
        }


        for (int i = 0; i < images.Length; i++)
        {
            if (SetArtis[i] == null)
            {
                continue;
            }
            images[i].sprite = SetArtis[i].image;
        }
    }

    public void AfterMarketOn()
    {
        SetArtis.Clear();
        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(true);
        }
        SetArtis.Add(ArtifactManager.S.SearchArti(TowerVariable.S.Arti1));
        SetArtis.Add(ArtifactManager.S.SearchArti(TowerVariable.S.Arti2));
        SetArtis.Add(ArtifactManager.S.SearchArti(TowerVariable.S.Arti3));

        if (TowerVariable.S.Arti1=="구매완료")
        {
            obs[0].gameObject.SetActive(false);
        }

        if (TowerVariable.S.Arti2 == "구매완료")
        {
            obs[1].gameObject.SetActive(false);
        }

        if (TowerVariable.S.Arti3 == "구매완료")
        {
            obs[2].gameObject.SetActive(false);
        }


        for (int i = 0; i < images.Length; i++)
        {
            if (SetArtis[i]==null)
            {
                continue;
            }
            images[i].sprite = SetArtis[i].image;
        }
    }


    public void OpenInfoUI(int _num)
    {
        if (!InfoUI.activeInHierarchy)
        {
            InfoUI.SetActive(true);
        }
        switch (Options.S.language)
        {
            case Options.Language.Kor:
                ArtiImage.sprite = SetArtis[_num].image;
                ArtiName.text = SetArtis[_num].artifactName;
                artiInfo.text = SetArtis[_num].Info;

                for (int i = 1; i <= 10; i++)
                {
                    artiInfo.text = artiInfo.text.Replace(i + "TN", (Player.S.CurTowerNum * i).ToString());
                }
                artiInfo.text = artiInfo.text.Replace("TN", Player.S.CurTowerNum.ToString());
                if (SetArtis[_num].artifactName == "황금 달걀")
                {
                    artiInfo.text = artiInfo.text.Replace("횟수", (50 - Player.S.GoldenEggStack).ToString() + "회 남음");
                }
                if (SetArtis[_num].artifactName == "신비하지 않은 사전")
                {
                    artiInfo.text = artiInfo.text.Replace("횟수", (50 - Player.S.BookStack).ToString() + "회 남음");
                }
                if (SetArtis[_num].artifactName == ArtifactManager.S.DirtSpoon.artifactName)
                {
                    artiInfo.text = artiInfo.text.Replace("횟수", (50 - Player.S.SpoonStack).ToString() + "회 남음");
                }
                break;
            case Options.Language.Eng:
                ArtiImage.sprite = SetArtis[_num].image;
                ArtiName.text = SetArtis[_num].Ename;
                artiInfo.text = SetArtis[_num].E_Info;

                for (int i = 1; i <= 10; i++)
                {
                    artiInfo.text = artiInfo.text.Replace(i + "TN", (Player.S.CurTowerNum * i).ToString());
                }
                artiInfo.text = artiInfo.text.Replace("TN", "(" + Player.S.CurTowerNum.ToString() + ")");
                if (SetArtis[_num].artifactName == "황금 달걀")
                {
                    artiInfo.text = artiInfo.text.Replace("횟수", "Remain " + (50 - Player.S.GoldenEggStack).ToString() + "Battle");
                }
                if (SetArtis[_num].artifactName == "신비하지 않은 사전")
                {
                    artiInfo.text = artiInfo.text.Replace("횟수", "Remain " + (50 - Player.S.BookStack).ToString() + "Battle");
                }
                if (SetArtis[_num].artifactName == ArtifactManager.S.DirtSpoon.artifactName)
                {
                    artiInfo.text = artiInfo.text.Replace("횟수", "Remain " + (50 - Player.S.SpoonStack).ToString() + "Battle");
                }

                break;
            default:
                break;
        }
      

    }

    public void UIClose()
    {
        TowerMarket.S.TowerMarketClose();
    }

    public void SelectArti(int _num)
    {
        switch (_num)
        {

            case 0:
                ArtifactManager.S.GetArtifact(SetArtis[0].artifactName);
                break;
            case 1:
                 ArtifactManager.S.GetArtifact(SetArtis[1].artifactName);
                break;
            case 2:
                ArtifactManager.S.GetArtifact(SetArtis[2].artifactName);
                break;
            default:
                break;
        }
        UIClose();
    }

    public void Buy(int _num)
    {
        switch (_num)
        {

            case 0:
                if (Player.S.gold >= 200)
                {
                    ArtifactManager.S.GetArtifact(SetArtis[0].artifactName);
                    SoundManager.S.PlaySE("income");
                    TowerVariable.S.Arti1 = "구매완료";
                    obs[0].gameObject.SetActive(false);
                    Player.S.SpendGold(200);
                }
                break;
            case 1:
                if (Player.S.gold >= 300)
                {
                    ArtifactManager.S.GetArtifact(SetArtis[1].artifactName);
                    SoundManager.S.PlaySE("income");
                    TowerVariable.S.Arti2 = "구매완료";
                    obs[1].gameObject.SetActive(false);
                    Player.S.SpendGold(300);
                }
                break;
            case 2:
                if (Player.S.gold >= 500)
                {
                    ArtifactManager.S.GetArtifact(SetArtis[2].artifactName);
                    SoundManager.S.PlaySE("income");
                    TowerVariable.S.Arti3 = "구매완료";
                    obs[2].gameObject.SetActive(false);
                    Player.S.SpendGold(500);
                }
                break;
            default:
                break;
        }

    }
    public void Tower4Buy(int _num)
    {
        switch (_num)
        {

            case 0:
                if (Player.S.gold >= 400)
                {
                    ArtifactManager.S.GetArtifact(SetArtis[0].artifactName);
                    SoundManager.S.PlaySE("income");
                    TowerVariable.S.T4Arti1 = "구매완료";
                    obs[0].gameObject.SetActive(false);
                    Player.S.SpendGold(400);
                }
                break;
            case 1:
                if (Player.S.gold >= 600)
                {
                    ArtifactManager.S.GetArtifact(SetArtis[1].artifactName);
                    SoundManager.S.PlaySE("income");
                    TowerVariable.S.T4Arti2 = "구매완료";
                    obs[1].gameObject.SetActive(false);
                    Player.S.SpendGold(600);
                }
                break;
            case 2:
                if (Player.S.gold >= 800)
                {
                    ArtifactManager.S.GetArtifact(SetArtis[2].artifactName);
                    SoundManager.S.PlaySE("income");
                    TowerVariable.S.T4Arti3 = "구매완료";
                    obs[2].gameObject.SetActive(false);
                    Player.S.SpendGold(800);
                }
                break;
            default:
                break;
        }

    }
}
