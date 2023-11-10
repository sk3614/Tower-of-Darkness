using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextManager : MonoBehaviour
{
    public static TextManager S;

 
    public void Awake()
    {
        if (S==null)
        {
            S = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public string GetTexts(string _fileName,int _id)
    {
        List<Dictionary<string, object>> data = CSVReader.Read(_fileName, "CSV_Files/");
        string language="";
        switch (Options.S.language)
        {
            case Options.Language.Kor:
                language ="Kor";
                break;
            case Options.Language.Eng:
                language="Eng";
                break;
            default:
                break;
        }
        string text = data[_id][language].ToString();
        return text;

       
    }

    public string GetTextsByName(string _fileName, string _id)
    {
        List<Dictionary<string, object>> data = CSVReader.Read(_fileName, "CSV_Files/");

        string language = "";
        switch (Options.S.language)
        {
            case Options.Language.Kor:
                language = "Kor";
                break;
            case Options.Language.Eng:
                language = "Eng";
                break;
            default:
                break;
        }
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i]["name"].ToString() == _id)
            {
                return data[i][language].ToString();
            }

        }

        return "error";
    }


    public string GetBasicText(string _id)
    {
        List<Dictionary<string, object>> data = CSVReader.Read("Basic", "CSV_Files/");

        string language = "";
        switch (Options.S.language)
        {
            case Options.Language.Kor:
                language = "Kor";
                break;
            case Options.Language.Eng:
                language = "Eng";
                break;
            default:
                break;
        }
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i]["name"].ToString() == _id)
            {
                return data[i][language].ToString();
            }
            
        }

        return "error";
    }
}
