using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plaza : MonoBehaviour
{
    public static Plaza S;
    public List<GameObject> Npcs=new List<GameObject>();
    public List<int> npcNums = new List<int>();
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
    void Start()
    {
        if (Player.S.isLoad)
        {

        }
        else
        {
            SetTmi(3);
        }

    }

    public void SetTmi(int _num)
    {
        for (int i = 0; i < Npcs.Count; i++)
        {
            Npcs[i].SetActive(false);
        }

        List<GameObject> FirstList = new List<GameObject>();//메인프로그레스 맞는 npc
        List<GameObject> SecondList = new List<GameObject>();//npc수 고르기
        for (int i = 0; i <Npcs.Count ; i++)//메인프로그레스 맞는 npc고르기
        {
            for (int j = 0; j < Npcs[i].GetComponent<TownNPC>().npcTownProgress.Length; j++)
            {
                if (Npcs[i].GetComponent<TownNPC>().npcTownProgress[j].TownProgressNum ==Player.S.mainProgress)
                {
                    FirstList.Add(Npcs[i]);
                    break;
                }
            }
        }
        for (int i = 0; i < _num; i++)
        {
            int value = Random.Range(0, FirstList.Count);
            SecondList.Add(FirstList[value]);
            FirstList.RemoveAt(value);
        }


        for (int i = 0; i < SecondList.Count; i++)
        {
            SecondList[i].SetActive(true);
            npcNums.Add(Npcs.IndexOf(SecondList[i]));
        }
    }
    public void PlazaTextOn()
    {
        SoundManager.S.PlaySE("node");
        int value = Random.Range(0, 7);
        switch (Player.S.mainProgress)
        {
            case 0:
                
                break;
            case 1:
                switch (value)
                {
                    case 0:
                        DialogueManager.S.TextSetPlaza(1, 3);
                        break;
                    case 1:
                        DialogueManager.S.TextSetPlaza(1, 3);
                        break;
                    case 2:
                        DialogueManager.S.TextSetPlaza(1, 3);
                        break;
                    case 3:
                        DialogueManager.S.TextSetPlaza(1, 3);
                        break;
                    case 4:
                        DialogueManager.S.TextSetPlaza(1, 3);
                        break;
                    case 5:
                        DialogueManager.S.TextSetPlaza(1, 3);
                        break;
                    default:
                        DialogueManager.S.TextSetPlaza(1, 3);
                        break;
                }
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            default:
                break;
        }
    }
}
