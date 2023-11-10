using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
public class CsvTest : MonoBehaviour
{
    public static CsvTest S;

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
    void Start ()
    {
        Debug.Log($"{Application.persistentDataPath}");
        if (!Directory.Exists(Application.persistentDataPath+ "/MapSave/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/MapSave/");
        }
	}
    void OnApplicationQuit()
    {

        string[] allfiles = Directory.GetFiles(Application.persistentDataPath + "/MapSave/");

        for (int i = 0; i < allfiles.Length; i++)
        {
            File.Delete(allfiles[i]);
        }
       // Directory.Delete(Application.persistentDataPath + "/MapSave/");
    }

    public void SaveMap(int _tower, int _Floor, List<GameObject> mapCells)
    {

        string fileName = "T" + _tower.ToString() + "F" + _Floor.ToString() + ".csv";

        using (var writer = new CsvFileWriter(Application.persistentDataPath + "/MapSave/" + fileName))
        {
            List<string> Ob = new List<string>() { "X", "Y", "ObjectNum" };// making Index Row
            writer.WriteRow(Ob);
            Ob.Clear();

            for (int i = 0; i < mapCells.Count; i++)
            {
                Ob.Add(mapCells[i].GetComponent<MapCell>().X.ToString());
                Ob.Add(mapCells[i].GetComponent<MapCell>().Y.ToString());
                Ob.Add(mapCells[i].GetComponent<MapCell>().towerObjectData.ObjectNum.ToString());
                writer.WriteRow(Ob);
                Ob.Clear();
            }
        }
    }


}
