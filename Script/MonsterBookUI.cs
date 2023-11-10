using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MonsterBookUI : MonoBehaviour
{
    public static MonsterBookUI S;
    public Transform T_slots;
    public GameObject slotPrefab;

    public GameObject uiBase;
    public List<GameObject> slots = new List<GameObject>();

    public List<Dictionary<string, object>> monsterDatas;

    public Transform T_Skillslots;
    public List<GameObject> skillslots = new List<GameObject>();

    public BookInfoUI infoui;

    private void Awake()
    {
        if (S==null)
        {
            S = this;
            monsterDatas = CSVReader.Read("MonsterData");
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void UIOn()
    {
        infoui.uibase.SetActive(false);
        if (Player.S.MonsterBookOn)
        {
            if (TowerMap.S.MoveLock&&uiBase.activeInHierarchy)
            {
                uiBase.SetActive(false);
                TowerMap.S.MoveLock = false;
            }
            else if(!MoveFloorUI.S.uiBase.activeInHierarchy)
            {

                uiBase.SetActive(true);
                TowerMap.S.MoveLock = true;
                for (int i = slots.Count - 1; i >= 0; i--)
                {
                    Destroy(slots[i]);
                }
                slots.Clear();

                for (int i = skillslots.Count - 1; i >= 0; i--)
                {
                    Destroy(skillslots[i]);
                }
                skillslots.Clear();



                List<MonsterData> monsterDatas = TowerMap.S.ReturnNowFloorMonsters();

                for (int i = 0; i < monsterDatas.Count; i++)
                {
                    GameObject go = Instantiate(slotPrefab, T_slots);
                    go.GetComponent<MonsterBookSlot>().SetSlot(monsterDatas[i]);
                    slots.Add(go);
                }
            }


        }
    }
       
}
