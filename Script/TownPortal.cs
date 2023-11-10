using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownPortal : MonoBehaviour
{
    public GameObject PortalUI;


    public void PortalUIOn()
    {
        if (Player.S.mainProgress==0)
        {
            return;
        }
        if (Player.S.mainProgress == 4&&!Player.S.isMeetSaint)
        {
            DialogueManager.S.TextSet(131,132);
            return;
        }
        if (Player.S.mainProgress == 8 && !Player.S.classUp1)
        {
            DialogueManager.S.TextSet(299, 300);
            return;
        }
        if (Player.S.mainProgress == 13 && !Player.S.classUp2)
        {
            DialogueManager.S.TextSet(299, 300);
            return;
        }
        //if (Player.S.mainProgress == 4 && Player.S.isMeetSaint)
        //{
        //    DialogueManager.S.TextSet(133, 134);
        //}
        PortalUI.SetActive(true);
    }

    public void GoTower()
    {
        switch (Player.S.mainProgress)
        {
            case 0:
                break;

            case 1:
                TownStory.S.StartStory(1);
                break;

            case 2:
                TownStory.S.StartStory(2);
                break;

            case 3:
                TownStory.S.StartStory(3);
                break;
            case 4:
                TownStory.S.StartStory(4);
                break;
            case 5:
                TownStory.S.StartStory(5);
                break;
            case 6:
                TownStory.S.StartStory(6);
                break;
            case 7:
                TownStory.S.StartStory(7);
                break;
            case 8:
                TownStory.S.StartStory(8);
                break;
            case 9:
                TownStory.S.StartStory(9);
                break;
            case 10:
                TownStory.S.StartStory(10);
                break;
            case 11:
                TownStory.S.StartStory(11);
                break;
            case 12:
                TownStory.S.StartStory(12);
                break;
            case 13:
                TownStory.S.StartStory(13);
                break;
            case 14:
                TownStory.S.StartStory(14);
                break;


            default:
                break;
        }
    }
}
