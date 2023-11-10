using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Glossary : MonoBehaviour
{
    public List<GameObject> C_list;

    public List<Dictionary<string, object>> DicData;
    public List<Sprite> images;

    public GameObject infoGO;
    public Image image;
    public Text flavorName;
    public Text flavorText;
    public Text Infotext;


    public void UIOFF()
    {
        if (Player.S.playerLocation==Player.PlayerLocation.Tower)
        {
            TowerMap.S.MoveLock = false;
        }
        SetBigCategory(0);
    }

    public void Start()
    {
        SetBigCategory(0);
        DicData = CSVReader.Read("Glossary", "CSV_Files/");

    }

    public void SetBigCategory(int _num)
    {
        infoGO.SetActive(false);
        for (int i = 0; i < C_list.Count; i++)
        {
            C_list[i].SetActive(false);
        }
        int start = 0;
        int end = 0;
        switch (_num)
        {
            case 0:
                break;
            case 1:
                start = 0;
                end = 23;
                break;
            case 2:
                start = 23;
                end = 31;
                break;
            case 3:
                start = 31;
                end = 42;
                break;
            case 4:
                start =42;
                end = 56;
                break;
            case 5:
                start = 56;
                end = 72;
                break;
            case 6:
                start = 72;
                end = 78;
                break;
            case 7:
                start = 78;
                end = 91;
                break;
            case 8:
                start = 91;
                end = 101;
                break;
            default:
                break;
        }
        for (int i = start; i < end; i++)
        {
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    C_list[i].GetComponentInChildren<Text>().text = DicData[i]["Name"].ToString();
                    break;
                case Options.Language.Eng:
                    C_list[i].GetComponentInChildren<Text>().text = DicData[i]["EName"].ToString();
                    break;
                default:
                    break;
            }

            C_list[i].SetActive(true);
        }

    }
    public void SetInfo(int _num)
    {
        infoGO.SetActive(true);
        if (images.Count > _num)
        {
            image.sprite = images[_num];
        }
        switch (Options.S.language)
        {
            case Options.Language.Kor:
                Infotext.text = DicData[_num]["Info"].ToString();
                flavorText.text = DicData[_num]["Flavor"].ToString();
                flavorName.text = DicData[_num]["FlavorName"].ToString();
                break;
            case Options.Language.Eng:
                Infotext.text = DicData[_num]["EInfo"].ToString();
                flavorText.text = DicData[_num]["EFlavor"].ToString();
                flavorName.text = DicData[_num]["FlavorEName"].ToString();
                break;
            default:
                break;
        }



    }
}
