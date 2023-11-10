using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
public class SceneChanger : MonoBehaviour
{
    public static SceneChanger S;

    private void Awake()
    {
        if (S==null)
        {
            S = this;
        }
        else
        {
            Destroy(this);
            
        }
    }
    public void GameStart()
    {
        Player.S.playerLocation = Player.PlayerLocation.Tower;
        SceneManager.LoadScene(3);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void LoadGame()
    {
        Player.S.playerLocation = Player.PlayerLocation.Town;
        Player.S.isLoad = true;
        SceneManager.LoadScene(2);
    }
    public void RestartGame()
    {
        Player.S.playerLocation = Player.PlayerLocation.Town;
        Player.S.isLoad = true;
        Player.S.loadNum = 1;
        SceneManager.LoadScene(2);
    }
    public void GoTower()
    {
        InventoryData.S.SaveKeys();
        Player.S.playerLocation = Player.PlayerLocation.Tower;
        SceneManager.LoadScene(3);


    }
    public void GoTown()
    {
        if (Player.S.playerLocation==Player.PlayerLocation.Tower)
        {
            InventoryData.S.SaveKeys();
        }
        Player.S.playerLocation = Player.PlayerLocation.Town;
        SceneManager.LoadScene(2);
    }
    public void ResetGame()
    {
        Destroy(GameObject.Find("Player"));
        Destroy(GameObject.Find("ArtifactManager"));
        Destroy(GameObject.Find("TowerVariable"));
        Destroy(GameObject.Find("QuestManager"));
        Destroy(GameObject.Find("SoundManager"));
        Destroy(GameObject.Find("Dictionaries"));
        Destroy(GameObject.Find("InventoryData"));
        Destroy(GameObject.Find("CSV"));
        Destroy(GameObject.Find("Options"));
        string[] allfiles = Directory.GetFiles(Application.persistentDataPath + "/MapSave/");

        for (int i = 0; i < allfiles.Length; i++)
        {
            File.Delete(allfiles[i]);
        }

        SceneManager.LoadScene(0);
    }
}
