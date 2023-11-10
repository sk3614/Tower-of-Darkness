using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class SaveLoadUI : MonoBehaviour
{
    public GameObject uibase;
    public GameObject noSaveDataUI;
    public Text jobName;
    public Text level;
    public Text progress;
    public Text date;
    public Text nodataCaution;
    // Start is called before the first frame update
    void Start()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/SaveData/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/SaveData/");
        }

    }



    public void LoadGame()
    {
        //csv파일있는지 확인
        if (File.Exists(Application.persistentDataPath +"/SaveData/Save1"+"/" + "Save.json"))
        {
            Player.S.loadNum = 1;
            SceneChanger.S.LoadGame();
        }
        else
        {
            Debug.Log("파일 없음");
        }

    }

    public void SaveGame(int _num)
    {
        SaveNLoad.S.Save(_num);
        UIOn();
    }

    public void UIOn()
    {

        if (File.Exists(Application.persistentDataPath + "/SaveData/Save1" + "/" + "Save.json"))
        {
            string path = Application.persistentDataPath + "/SaveData/Save1" + "/" + "Save.json";
            string data = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(data);

            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    jobName.text = "직업 : " + Player.S.ReturnCharacterName(saveData.job,saveData.jobClass);
                    level.text = "LV : " + saveData.level.ToString();
                    progress.text = "진행도 : " + saveData.mainProgress.ToString();
                    date.text = "날짜 : " + saveData.SaveDate;

                    break;
                case Options.Language.Eng:
                    jobName.text = "PlayerJob : " + Player.S.ReturnCharacterName(saveData.job, saveData.jobClass);
                    level.text = "LV : " + saveData.level.ToString();
                    progress.text = "Progress : " + saveData.mainProgress.ToString();
                    date.text = "Date : " + saveData.SaveDate;
                    break;
                default:
                    break;
            }


            uibase.SetActive(true);
        }
        else
        {
            noSaveDataUI.SetActive(true);
            switch (Options.S.language)
            {
                case Options.Language.Kor:
                    nodataCaution.text = "저장 데이터가 없습니다.";

                    break;
                case Options.Language.Eng:
                    nodataCaution.text = "No Save Data";
                    break;
                default:
                    break;
            }
            return;
        }


    }
}
