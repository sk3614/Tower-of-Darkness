using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TownTutorial : MonoBehaviour
{
    public GameObject[] tutorial;
    public Font font;
    public bool GetSkill;
    public void Start()
    {
        if (Player.S.mainProgress==0)
        {
            TutorialOn(0);
        }

        for (int i = 0; i < tutorial.Length; i++)
        {
            tutorial[i].GetComponentInChildren<Text>().font = font;
            tutorial[i].GetComponentInChildren<Text>().fontStyle = FontStyle.Normal;
        }



    }
    public void Update()
    {
        if (Player.S.mainProgress!=0)
        {
            Destroy(gameObject);
        }

        if (Player.S.endTuto && Player.S.mainProgress == 0)
        {
            Player.S.TutoSet();
            Destroy(gameObject);
        }
        else if(Player.S.mainProgress==0)
        {
            if (!GetSkill)
            {
                Player.S.GetBasicClassSkill(0);
                GetSkill = true;
            }

        }
    }

    public void TutorialOn(int _num)
    {
        for (int i = 0; i < tutorial.Length; i++)
        {
            tutorial[i].SetActive(false);
        }
        tutorial[_num].SetActive(true);
    }

    public void EndTutorial()
    {
        for (int i = 0; i < tutorial.Length; i++)
        {
            tutorial[i].SetActive(false);
        }
        Destroy(gameObject);
    }


}
